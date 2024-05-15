using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LiteDB;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using System.Net;

namespace Woody.Services
{
    public class AzureIoTHubService
    {
        public DeviceClient deviceClient { get; set; }
        public BlobContainerClient blobContainerClient { get; set; }
        public EventProcessorClient eventProcessorClient { get; set; }
        /// <summary>
        /// Connect to the IoTHub using the Device Connection String
        /// </summary>
        /// <returns>True if everything went well and false otherwise</returns>
        public async Task<bool> ConnectToDeviceAsync()
        {
            try
            {
                var transportType = Microsoft.Azure.Devices.Client.TransportType.Mqtt;
                deviceClient = DeviceClient.CreateFromConnectionString(App.Settings.IOTHubDeviceConnectionString, transportType);
                blobContainerClient = new BlobContainerClient(App.Settings.BlobConnectionString, App.Settings.BlobContainerName);
                eventProcessorClient = new EventProcessorClient(blobContainerClient,App.Settings.EventHubConsumer,App.Settings.EventHubConnectionString, App.Settings.EventHubName);

                // Register handlers for processing events and handling errors
                eventProcessorClient.ProcessEventAsync += ProcessEventHandler;
                eventProcessorClient.ProcessErrorAsync += ProcessErrorHandler;
                
                Console.WriteLine("Connected to Azure IoT Hub and to the Blob");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<string>> DownloadBlobAsync()
        {
            var blobList = new List<string>();

            await foreach (var blobItem in blobContainerClient.GetBlobsAsync())
            {
                var blobClient = blobContainerClient.GetBlobClient(blobItem.Name);
                var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0; // Reset the position to the start of the stream

                // Assuming the blob contains text, read the content and add it to the list
                var blobContent = new StreamReader(memoryStream).ReadToEnd();
                blobList.Add(blobContent);
                memoryStream.Close();
            }

            return blobList;
        }

        async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            try
            {
                Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));
                await App.FarmRepo.DeserializeNewDataAsync(eventArgs.Data.Body);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
        }

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
    }
}
