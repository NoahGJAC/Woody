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

namespace Woody.Services
{
    public class AzureIoTHubService
    {
        public DeviceClient deviceClient { get; set; }
        public BlobContainerClient blobContainerClient { get; set; }

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
                await deviceClient.OpenAsync();
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
            }

            return blobList;
        }
    }
}
