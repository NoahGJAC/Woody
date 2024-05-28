using Azure.Storage.Blobs;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Woody.Models;
using Woody.Interfaces;
using Woody.Enums;
using Microsoft.Azure.Devices.Shared;
using Avro.File;
using Avro.Generic;

namespace Woody.Services
{
    /*
     * Team: Woody
     * Section 1
     * Winter 2024, 05/16/2024
     * 420-6A6 App Dev III
    */
    public class AzureIoTHubService
    {
        public DeviceClient deviceClient { get; set; }
        public BlobContainerClient blobContainerClient { get; set; }
        public EventProcessorClient eventProcessorClient { get; set; }
        public List<string> blobFile = new List<string>();
        public ServiceClient serviceClient { get; set; }
        private RegistryManager registryManager;

        /// <summary>
        /// Connect to the IoTHub using the Device Connection String,
        /// the blob to get all the data
        /// the event hub to get all the current data being processed.
        /// </summary>
        /// <returns>True if everything went well and false otherwise</returns>
        public async Task<bool> ConnectToDeviceAsync()
        {
            try
            {
                var transportType = Microsoft.Azure.Devices.Client.TransportType.Mqtt;

                deviceClient = DeviceClient.CreateFromConnectionString(App.Settings.IOTHubDeviceConnectionString, transportType);
                serviceClient = ServiceClient.CreateFromConnectionString(App.Settings.IOTHubConnectionString);
                blobContainerClient = new BlobContainerClient(App.Settings.BlobConnectionString, App.Settings.BlobContainerName);
                eventProcessorClient = new EventProcessorClient(blobContainerClient,App.Settings.EventHubConsumer,App.Settings.EventHubConnectionString, App.Settings.EventHubName);
                registryManager = RegistryManager.CreateFromConnectionString(App.Settings.IOTHubConnectionString);

                // Register handlers for processing events and handling errors
                eventProcessorClient.ProcessEventAsync += ProcessEventHandler;
                eventProcessorClient.ProcessErrorAsync += ProcessErrorHandler;
                
                Console.WriteLine("Connected to Azure IoT Hub and to the Blob");
                GetCurrentMessageAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Download everything from the blob before the app start so that there is historical data
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> DownloadBlobAsync()
        {
            var blobList = new List<string>();

            await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
            {
                var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                string blobContent;

                blobFile.Add(blobItem.Name);
                var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0; // Reset the position to the start of the stream

                if (blobClient.Name.Contains("avro"))
                {
                    blobContent = ConvertAvroToJson(memoryStream);
                }
                else
                {
                    // Assuming the blob contains text, read the content and add it to the list
                    blobContent = new StreamReader(memoryStream).ReadToEnd();
                }
                
                blobList.Add(blobContent);
                memoryStream.Close();
            }

            return blobList;
        }

        public string ConvertAvroToJson(Stream memoryStream)
        {
            string value;
            using (var reader = DataFileReader<GenericRecord>.OpenReader(memoryStream))
            {
                var records = new List<Dictionary<string, object>>();

                while (reader.HasNext())
                {
                    var record = reader.Next();
                    var recordDict = new Dictionary<string, object>();

                    foreach (var field in record.Schema.Fields)
                    {
                        recordDict[field.Name] = record[field.Name];
                    }

                    records.Add(recordDict);
                }

                // Convert records to JSON string
                string jsonString = JsonConvert.SerializeObject(records, Formatting.Indented);
                value = jsonString;
                Console.WriteLine(jsonString);
            }

            return value;


        }
        /// <summary>
        /// if there is a something happening in the blob while the app is running
        /// it will get the new data into the application
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            try
            {
                Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
                await App.FarmRepo.GetNewDataAsync(eventArgs.Partition, eventArgs.Data);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }
        /// <summary>
        /// it there is a error happening when getting the data from the blob
        /// while running the the application this will be called
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        async Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window

            try
            {
                Console.WriteLine($"\tPartition '{eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
                Console.WriteLine(eventArgs.Exception.Message);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        /// <summary>
        /// background task to make sure that the application gets the new data
        /// </summary>
        /// <returns></returns>
        private async Task GetCurrentMessageAsync()
        {
            await eventProcessorClient.StartProcessingAsync();
            try
            {
                // The processor performs its work in the background; block until cancellation
                // to allow processing to take place.

                await Task.Delay(Timeout.Infinite);
            }
            catch (TaskCanceledException)
            {
                // This is expected when the delay is canceled.
            }

            try
            {
                await eventProcessorClient.StopProcessingAsync();
            }
            finally
            {
                // To prevent leaks, the handlers should be removed when processing is complete.

                eventProcessorClient.ProcessEventAsync -= ProcessEventHandler;
                eventProcessorClient.ProcessErrorAsync -= ProcessErrorHandler;
            }

        }
        /// <summary>
        /// Sends a command to the IoThub
        /// </summary>
        /// <typeparam name="T">the type of the command</typeparam>
        /// <param name="command">the command itself i.e: on or off for what subsystem</param>
        /// <returns></returns>
        public async Task SendCommandAsync<T>(Models.Command<T> command)
        {
            var methodInvocation = new CloudToDeviceMethod("command")
            {
                ResponseTimeout = TimeSpan.FromSeconds(30)
            };
            var commandObject = new Dictionary<string, string>
            {
                ["command-type"] = CommandTypeExtensions.ToDescription(command.CommandType),
                ["subsystem-type"] = SubSystemTypeExtensions.ToDescription(command.SubSystem),
                ["value"] = command.Value.ToString(),
            };

            var payload = JsonConvert.SerializeObject(commandObject);
            methodInvocation.SetPayloadJson(payload);

            try
            {
                var response = await serviceClient.InvokeDeviceMethodAsync(App.Settings.IOTHubDeviceId, methodInvocation);
                Console.WriteLine($"Response status: {response.Status}, payload: {response.GetPayloadAsJson()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to invoke device method: {ex.Message}");
            }
        }
        /// <summary>
        /// Get the twin device
        /// </summary>
        /// <returns></returns>
        public async Task<Twin> GetTwinAsync()
        {
            try
            {
                Twin twin = await deviceClient.GetTwinAsync();
                return twin;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get reported properties: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// change the twin device value
        /// </summary>
        /// <param name="propertiesToUpdate"></param>
        /// <returns></returns>
        public async Task UpdateDeviceTwinPropertiesAsync(Dictionary<string, object> propertiesToUpdate)
        {
            try
            {
                Twin twin = await registryManager.GetTwinAsync(App.Settings.IOTHubDeviceId);
                twin.Properties.Desired[propertiesToUpdate.Keys.First()] = propertiesToUpdate.Values.First();
                await registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
                Console.WriteLine("Updated device twin properties successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update device twin properties: {ex.Message}");
            }
        }
    }
}
