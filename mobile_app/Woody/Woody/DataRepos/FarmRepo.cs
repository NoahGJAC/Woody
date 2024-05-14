using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;
using Woody.Models;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using Woody.Interfaces;
using System.ComponentModel;

namespace Woody.DataRepos
{
    public class FarmRepo 
    {
        private SecurityRepo securityRepo;
        /// <summary>
        /// Gets the Security repository.
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

        public FarmRepo()
        {
            securityRepo = new SecurityRepo();
            plantRepo = new PlantRepo();
            geoLocationRepo = new GeoLocationRepo();
        }

        public async void DeserializeDataAsync()
        {
            var blobList = await App.IoTDevice.DownloadBlobAsync(); //this get the data from the blob
            var parseJsonObjects = new List<IReading>();

            //DeserializeData
            foreach (var blob in blobList)
            {
                var jsonObjects = blob.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string jsonObject in jsonObjects)
                {
                    try
                    {
                        // Parse each JSON string and add it to the list
                        var tempObject = JsonConvert.DeserializeObject<BlobReadings>(jsonObject);
                        var lol = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonObject);
                        var properties = lol["Properties"];

                        var propertiesDictionary = new Dictionary<string, object>();
                        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(properties))
                        {
                            propertiesDictionary[property.Name] = property.GetValue(properties);
                        }

                        var readingTypeName = propertiesDictionary["reading-type-name"];
                        var readingType = (ReadingType)Enum.Parse(typeof(ReadingType), readingTypeName.ToString(), true);
                        var body = Convert.FromBase64String(tempObject.Body);
                        string decodedStr = Encoding.UTF8.GetString(body);
                        var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(decodedStr);
                        var value = values["value"];
                        var unitValue = values["unit"];
                        var uniType = EnumExtensions.GetEnumFromString<ReadingUnit>(unitValue.ToString());
                        Type type = value.GetType();
                        var sensorReadingType = typeof(SensorReading<>).MakeGenericType(type);
                        var constructorInfo = sensorReadingType.GetConstructor(new[] { type, typeof(DateTime), typeof(ReadingUnit), typeof(ReadingType) });
                        // Invoke the constructor
                        var sensorReadingInstance = constructorInfo.Invoke(new object[] { value, tempObject.TimeStamp, uniType, readingType });

                        parseJsonObjects.Add((IReading)sensorReadingInstance);
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }


                }
            }

            AssignDataToRepos(parseJsonObjects);
        }

        private void AssignDataToRepos(List<IReading> parsedJsonObjects)
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
                 ReadingType.LIGHT,
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
                ReadingType.ROLL
            };
            foreach (var jsonObject in parsedJsonObjects)
            {
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
        }

        /// <summary>
        /// Add the value into the repo that are related to the geoLoaction repo
        /// </summary>
        /// <param name="jsonObject"></param>
        private void AssignDataToGeoLocationRepo(IReading jsonObject)
        {
            if (jsonObject.ReadingType == ReadingType.BUZZER)
            {
                try
                {
                    geoLocationRepo.BuzzerState = (IReading<bool>)jsonObject;
                }
                catch (Exception ex)
                {
                    var tempObj = (IReading<string>)jsonObject;
                    var value = bool.Parse(tempObj.Value);
                    geoLocationRepo.BuzzerState = new SensorReading<bool>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType);
                    
                }
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
                try
                {
                    securityRepo.BuzzerState = (IReading<bool>)jsonObject;
                }
                catch (Exception ex)
                {
                    var tempObj = (IReading<string>)jsonObject;
                    var value = bool.Parse(tempObj.Value);
                    securityRepo.BuzzerState = new SensorReading<bool>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType);

                }
            }
            else if(jsonObject.ReadingType == ReadingType.DOOR_LOCK)
            {
                var temp = (IReading<double>)jsonObject;

                var value = false;
                if (temp.Value > 0)
                    value = true;

                securityRepo.LockState = new SensorReading<bool>(value,temp.TimeStamp,temp.Unit,temp.ReadingType);
            }
            else if(jsonObject.ReadingType == ReadingType.LOUDNESS)
            {
                securityRepo.NoiseLevels.Add((IReading<float>)jsonObject);
            }
            else if(jsonObject.ReadingType== ReadingType.LUMINOSITY)
            {
                try
                {
                    securityRepo.LuminosityLevels.Add((IReading<int>)jsonObject);
                }
                catch(Exception ex)
                {
                    var tempObj = (IReading<long>)jsonObject;
                    var value = (int)tempObj.Value;
                    var luminosityReading = new SensorReading<int>(value, tempObj.TimeStamp, tempObj.Unit, tempObj.ReadingType);
                    securityRepo.LuminosityLevels.Add(luminosityReading);
                }
                
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
                plantRepo.FanState = (IReading<bool>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.LIGHT)
            {
                plantRepo.LightState = (IReading<bool>)jsonObject;
            }
            else if(jsonObject.ReadingType == ReadingType.TEMPERATURE)
            {
                plantRepo.TemperatureLevels.Add((IReading<double>)jsonObject);
            }
            else if(jsonObject.ReadingType == ReadingType.SOIL_MOISTURE)
            {
                plantRepo.SoilMoistureLevels.Add((IReading<double>)jsonObject);
            }
            else if(jsonObject.ReadingType == ReadingType.HUMIDITY)
            {
                plantRepo.HumidityLevels.Add((IReading<double>)jsonObject);
            }
        }
    }
}
