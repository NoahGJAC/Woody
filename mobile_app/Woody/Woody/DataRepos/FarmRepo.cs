using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using Woody.Enums;
using Woody.Interfaces;
using Woody.Models;
/*
 * Team: Woody
 * Section 1
 * Winter 2024, 05/16/2024
 * 420-6A6 App Dev III
 */
namespace Woody.DataRepos
{
    public class FarmRepo 
    {
        /// <summary>
        /// Represents a repository for the farm data in general, give the data for the other repo
        /// </summary>
        private SecurityRepo securityRepo;
        /// <summary>
        /// Gets the security repository.
        /// </summary>
        public SecurityRepo SecurityRepo { get { return securityRepo; } }
        /// <summary>
        /// Gets the plant repository.
        /// </summary>
        private PlantRepo plantRepo;
        public PlantRepo PlantRepo { get { return plantRepo; } }

        private GeoLocationRepo geoLocationRepo;

        /// <summary>
        /// Gets the geolocation repository
        /// </summary>
        public GeoLocationRepo GeoLocationRepo { get { return geoLocationRepo; } }
        /// <summary>
        /// instantiate the other repos  <see cref="FarmRepo"/>
        /// </summary>
        public FarmRepo()
        {
            securityRepo = new SecurityRepo();
            plantRepo = new PlantRepo();
            geoLocationRepo = new GeoLocationRepo();
        }

        /// <summary>
        /// Deserialize the old data.
        /// </summary>
        public async Task DeserializeOldDataAsync()
        {
            var blobList = await App.IoTDevice.DownloadBlobAsync(); //this get the data from the blob

            //DeserializeData
            foreach (var blob in blobList)
            {
                var blobReadings = blob.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string reading in blobReadings)
                {
                    try
                    {
                        // get the raw readings and get the readings in the BlobReadings object
                        var blobReadingObject = JsonConvert.DeserializeObject<BlobReadings>(reading);
                        var rawReading = JsonConvert.DeserializeObject<Dictionary<string, object>>(reading);

                        //get all the properties of the reading
                        var properties = rawReading["Properties"];

                        // timestamp
                        var enqueuedTime = rawReading["EnqueuedTimeUtc"];

                        //put the readings in a dictionary of ease of use
                        var propertiesDictionary = new Dictionary<string, object>();
                        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(properties))
                        {
                            propertiesDictionary[property.Name] = property.GetValue(properties);
                        }

                        //get the reading type and parse it
                        var readingTypeName = propertiesDictionary["reading-type-name"];
                        var readingType = (ReadingType)Enum.Parse(typeof(ReadingType), readingTypeName.ToString(), true);

                        //check if the readingType is already assign if it's not a list
                        if (ValidReadingType(readingType))
                        {
                            continue;
                        }

                        // get the values inside of the body aka the reading unit and the value
                        var body = Convert.FromBase64String(blobReadingObject.Body);
                        string decodedStr = Encoding.UTF8.GetString(body);
                        var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedStr);
                        var value = values["value"];
                        var valueString = value.ToString();
                        var unitValue = values["unit"];

                        //parse the reading unit
                        ReadingUnit unitType;
                        if(readingType == ReadingType.LOUDNESS)
                        {
                            unitType = ReadingUnit.LOUDNESS;
                        }
                        else if(readingType == ReadingType.TEMPERATURE_HUMIDITY)
                        {
                            continue;
                        }
                        else if ((readingType == ReadingType.TEMPERATURE && string.IsNullOrEmpty(valueString)) || (readingType == ReadingType.HUMIDITY && string.IsNullOrEmpty(valueString)))
                        {
                            continue;
                        }
                        else
                        {
                           unitType = EnumExtensions.GetEnumFromString<ReadingUnit>(unitValue.ToString());
                        }

                        //get the type of the value to create a sensor reading
                        Type type = value.GetType();

                        //create the sensorReading
                        var sensorReadingType = typeof(SensorReading<>).MakeGenericType(type);
                        var constructorInfo = sensorReadingType.GetConstructor(new[] { type, typeof(DateTime), typeof(ReadingUnit), typeof(ReadingType)});

                        // Invoke the constructor
                        var sensorReadingInstance = constructorInfo.Invoke(new object[] { value, enqueuedTime, unitType, readingType});

                        AssignDataToRepos((IReading)sensorReadingInstance);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }


                }
            }
        }

        /// <summary>
        /// only used at the beginning to make sure that we aren't reasigning data that isn't necessary
        /// </summary>
        /// <param name="type">the reading type to make sure that we aren't reasigning data</param>
        /// <returns></returns>
        private bool ValidReadingType(ReadingType type)
        {
            switch (type)
            {
                case ReadingType.DOOR:
                    return securityRepo.DoorState != null;
                case ReadingType.MOTION:
                    return securityRepo.MotionState != null;
                case ReadingType.BUZZER:
                    return securityRepo.BuzzerState != null || geoLocationRepo.BuzzerState != null;
                case ReadingType.DOOR_LOCK:
                    return securityRepo.LockState != null;
                case ReadingType.WATER_LEVEL:
                    return plantRepo.WaterLevel != null;
                case ReadingType.FAN:
                    return plantRepo.FanState != null;
                case ReadingType.LED:
                    return plantRepo.LightState != null;
                case ReadingType.GPS:
                    return GeoLocationRepo.GPS != null;
                case ReadingType.LATITUDE:
                    return geoLocationRepo.GPS?.Value?.Latitude != null;
                case ReadingType.LONGITUDE:
                    return geoLocationRepo.GPS?.Value?.Longitude != null;
                case ReadingType.ALTITUDE:
                    return geoLocationRepo.GPS?.Value?.Altitude != null;
                case ReadingType.PITCH:
                    return geoLocationRepo.Pitch != null;
                case ReadingType.ROLL:
                    return geoLocationRepo.Roll != null;
                case ReadingType.VIBRATION:
                    return geoLocationRepo.Vibration != null;
                default:
                    return false;
            }
        }

        private void AssignDataToRepos(IReading jsonObject)
        {
            var securityReadingType = new List<ReadingType>()
            {
                ReadingType.DOOR,
                ReadingType.MOTION,
                ReadingType.BUZZER,
                ReadingType.DOOR_LOCK,
                ReadingType.LOUDNESS,
                ReadingType.LUMINOSITY
            };
            var plantReadingType = new List<ReadingType>()
            {
                 ReadingType.WATER_LEVEL,
                 ReadingType.FAN,
                 ReadingType.LED,
                 ReadingType.TEMPERATURE,
                 ReadingType.SOIL_MOISTURE,
                 ReadingType.HUMIDITY
            };
            var geoLoactionReadingType = new List<ReadingType>()
            {
                ReadingType.BUZZER,
                ReadingType.GPS,
                ReadingType.LATITUDE,
                ReadingType.LONGITUDE,
                ReadingType.ALTITUDE,
                ReadingType.PITCH,
                ReadingType.ROLL,
                ReadingType.VIBRATION
            };

            if(geoLoactionReadingType.Contains(jsonObject.ReadingType))
            {
                AssignDataToGeoLocationRepo(jsonObject);
            }
            else if (plantReadingType.Contains(jsonObject.ReadingType))
            {
                AssignDataToPlantRepo(jsonObject);
            }
            else if (securityReadingType.Contains(jsonObject.ReadingType))
            {
                AssignDataToSecurityRepo(jsonObject);
            }

        }

        /// <summary>
        /// Add the value into the repo that are related to the geoLoaction repo
        /// </summary>
        /// <param name="jsonObject"></param>
        private void AssignDataToGeoLocationRepo(IReading jsonObject)
        {
            if (jsonObject.ReadingType == ReadingType.BUZZER)
            {
                var tempObj = (IReading<string>)jsonObject;
                var value = bool.Parse(tempObj.Value);
                var buzzerCommand = new Models.Command<string>(tempObj.Value, CommandType.BUZZER_ON_OFF, SubSystemType.GEOLOCATION);
                geoLocationRepo.BuzzerState = new SensorReading<bool>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType,buzzerCommand);
                    
                securityRepo.BuzzerState = geoLocationRepo.BuzzerState;
            }
            else if(jsonObject.ReadingType == ReadingType.GPS)
            {
                
            }
            else if(jsonObject.ReadingType== ReadingType.LATITUDE)
            {
                geoLocationRepo.GPS.Value.Latitude = (IReading<double>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.LONGITUDE)
            {
                geoLocationRepo.GPS.Value.Longitude = (IReading<double>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.ALTITUDE)
            {
                geoLocationRepo.GPS.Value.Altitude = (IReading<double>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.PITCH)
            {
                geoLocationRepo.Pitch = (IReading<double>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.ROLL)
            {
                geoLocationRepo.Roll = (IReading<double>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.VIBRATION)
            {
                geoLocationRepo.Vibration = (IReading<bool>)jsonObject;
            }
        }
        /// <summary>
        /// Add the value into the repo that are related to the security repo
        /// </summary>
        /// <param name="jsonObject"></param>
        private void AssignDataToSecurityRepo(IReading jsonObject)
        {
            if(jsonObject.ReadingType== ReadingType.DOOR)
            {
                securityRepo.DoorState = (IReading<bool>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.MOTION)
            {
                securityRepo.MotionState = (IReading<bool>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.BUZZER)
            {
                var tempObj = (IReading<string>)jsonObject;
                var value = bool.Parse(tempObj.Value);
                var buzzerCommand = new Models.Command<string>(tempObj.Value, CommandType.BUZZER_ON_OFF, SubSystemType.SECURITY);
                securityRepo.BuzzerState = new SensorReading<bool>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType, buzzerCommand);
                geoLocationRepo.BuzzerState = securityRepo.BuzzerState;
            }
            else if(jsonObject.ReadingType == ReadingType.DOOR_LOCK)
            {
                var temp = (IReading<double>)jsonObject;
                var value = temp.Value > 0;
                var commandValue = value ? "on" : "off";

                var doorCommand = new Models.Command<string>(commandValue, CommandType.DOOR_LOCK, SubSystemType.SECURITY);
                securityRepo.LockState = new SensorReading<bool>(value,temp.TimeStamp,temp.Unit,temp.ReadingType, doorCommand);
            }
            else if(jsonObject.ReadingType == ReadingType.LOUDNESS)
            {
                var temp = (IReading<double>)jsonObject;

                var value = (float)temp.Value;
                var reading = new SensorReading<float>(value,temp.TimeStamp,temp.Unit,temp.ReadingType);
                securityRepo.NoiseLevels.Add(reading);
            }
            else if(jsonObject.ReadingType== ReadingType.LUMINOSITY)
            {
                var tempObj = (IReading<long>)jsonObject;
                var value = (int)tempObj.Value;
                var luminosityReading = new SensorReading<int>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType);
                securityRepo.LuminosityLevels.Add(luminosityReading);
                
            }
                
        }
        /// <summary>
        /// Add the value into the repo that are related to the plant repo
        /// </summary>
        /// <param name="jsonObject"></param>
        private void AssignDataToPlantRepo(IReading jsonObject)
        {
            if (jsonObject.ReadingType == ReadingType.WATER_LEVEL)
            {
                var temp = (IReading<long>)jsonObject;
                var value = (int)temp.Value;
                plantRepo.WaterLevel = new SensorReading<int>(value,temp.TimeStamp,temp.Unit,temp.ReadingType);
            }
            else if(jsonObject.ReadingType == ReadingType.FAN)
            {
                var tempObj = (IReading<long>)jsonObject;
                var value = tempObj.Value > 0;
                var fanReading = new SensorReading<bool>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType);
                plantRepo.FanState = fanReading;
                var commandValue = plantRepo.FanState.Value ? "on" : "off";
                var fanCommand = new Models.Command<string>(commandValue, CommandType.FAN_ON_OFF, SubSystemType.PLANT);
                plantRepo.FanState.Command = fanCommand;
            }
            else if(jsonObject.ReadingType == ReadingType.LED)
            {
                plantRepo.LightState = (IReading<bool>)jsonObject;
                var commandValue = plantRepo.LightState.Value ? "on" : "off";
                var lightCommand = new Models.Command<string>(commandValue, CommandType.LIGHT_ON_OFF, SubSystemType.PLANT);
                plantRepo.LightState.Command = lightCommand;
            }
            else if(jsonObject.ReadingType == ReadingType.TEMPERATURE)
            {
                try
                {
                    plantRepo.TemperatureLevels.Add((IReading<double>)jsonObject);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if(jsonObject.ReadingType == ReadingType.SOIL_MOISTURE)
            {

                try
                {
                    var tempObj = (IReading<long>)jsonObject;

                    var value = (double)tempObj.Value;

                    var soilMoistureReading = new SensorReading<double>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType);
                    plantRepo.SoilMoistureLevels.Add(soilMoistureReading);

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if(jsonObject.ReadingType == ReadingType.HUMIDITY)
            {
                plantRepo.HumidityLevels.Add((IReading<double>)jsonObject);
            }
        }

        /// <summary>
        /// Get the new data from the blob
        /// </summary>
        /// <param name="partition"> the context</param>
        /// <param name="data">the blob file data</param>
        /// <returns></returns>
        internal async Task GetNewDataAsync(PartitionContext partition, EventData data)
        {
            // get all the information from the data.body
            var jsonString = Encoding.UTF8.GetString(data.Body.ToArray());
            var dictionary = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);

            // get the file name that is added
            var subjectString = dictionary[0]["subject"].ToString();
            int subjectIndex = subjectString.IndexOf("Woody");
            string filePath = subjectString.Substring(subjectIndex);

            //if that file was already downloaded don't do anything
            if (App.IoTDevice.blobFile.Contains(filePath))
                return;

            //get the data
            var blobClient = App.IoTDevice.blobContainerClient.GetBlobClient(filePath);
            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0;

            var blobFile = new StreamReader(memoryStream).ReadToEnd();
            memoryStream.Close();

            DeserializeNewData(blobFile);
            App.IoTDevice.blobFile.Add(filePath);
                
        }
        /// <summary>
        /// Desirialise that data from the blob file. this is only for the new data because there's 1 step that is different
        /// </summary>
        /// <param name="blobfile">the string of content of the blob file</param>
        private void DeserializeNewData(string blobfile)
        {
            var blobReadings = blobfile.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string reading in blobReadings)
            {
                try
                {
                    // get the raw readings and get the readings in the BlobReadings object
                    var blobReadingObject = JsonConvert.DeserializeObject<BlobReadings>(reading);
                    var rawReading = JsonConvert.DeserializeObject<Dictionary<string, object>>(reading);

                    //get all the properties of the reading
                    var properties = rawReading["Properties"];

                    // timestamp
                    var enqueuedTime = rawReading["EnqueuedTimeUtc"];

                    //put the readings in a dictionary of ease of use
                    var propertiesDictionary = new Dictionary<string, object>();
                    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(properties))
                    {
                        propertiesDictionary[property.Name] = property.GetValue(properties);
                    }

                    //get the reading type and parse it
                    var readingTypeName = propertiesDictionary["reading-type-name"];
                    var readingType = (ReadingType)Enum.Parse(typeof(ReadingType), readingTypeName.ToString(), true);

                    // get the values inside of the body aka the reading unit and the value
                    var body = Convert.FromBase64String(blobReadingObject.Body);
                    string decodedStr = Encoding.UTF8.GetString(body);
                    var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedStr);
                    var value = values["value"];
                    var unitValue = values["unit"];

                    //parse the reading unit
                    ReadingUnit unitType;
                    if (readingType == ReadingType.LOUDNESS)
                    {
                        unitType = ReadingUnit.LOUDNESS;
                    }
                    else if (readingType == ReadingType.TEMPERATURE_HUMIDITY)
                    {
                        unitType = ReadingUnit.CELCIUS_HUMIDITY;
                    }
                    else
                    {
                        unitType = EnumExtensions.GetEnumFromString<ReadingUnit>(unitValue.ToString());
                    }
                    //get the type of the value to create a sensor reading
                    Type type = value.GetType();

                    //create the sensorReading
                    var sensorReadingType = typeof(SensorReading<>).MakeGenericType(type);
                    var constructorInfo = sensorReadingType.GetConstructor(new[] { type, typeof(DateTime), typeof(ReadingUnit), typeof(ReadingType) });
                    // Invoke the constructor
                    var sensorReadingInstance = constructorInfo.Invoke(new object[] { value, enqueuedTime, unitType, readingType });

                    AssignDataToRepos((IReading)sensorReadingInstance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

            }
        }

    }


}

