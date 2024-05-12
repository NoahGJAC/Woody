using LiteDB;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Services
{
    public  class AzureIoTHubService
    {
        public  DeviceClient deviceClient { get; set; }

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
                await deviceClient.OpenAsync();
                Console.WriteLine("Connected to Azure IoT Hub.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Get's all of the readings from the IoT.
        /// </summary>
        /// <returns></returns>
        public async Task<string> ReceiveReadingsAsync()
        {
            try
            {
                var receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null)
                    return "No new Message";

                var messageData = Encoding.UTF8.GetString(receivedMessage.GetBytes());
                var deserializedObject = JsonConvert.DeserializeObject<dynamic>(messageData);
                Console.WriteLine($"Received message from Azure IoT Hub: {messageData}");
                await deviceClient.CompleteAsync(receivedMessage); //this delete the message that was sent but idk if we want that
                return deserializedObject;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to receive message from Azure IoT Hub: {ex.Message}");
                return null;
            }
        }
    }
}
