![Banner](https://i.imgur.com/9xQotbv.png)

## 🚀 Team Information
- **Team Woody**
  - **Noah Groleau: 2028706**
  - **Diana Karpeev: 2059788**
  - **Katchenin Cindy Coulibaly: 2126537**



## 🌱 Woody - Smart Farming App
Introducing Woody, your ultimate solution for upgrading basic container greenhouses into smart farms.

With Woody, fleet owners can easily keep tabs on all their containers, worry-free about fleet security. Meanwhile, farm technicians can conveniently manage plants from outside the container, thanks to various monitoring sensors and controls that can adjust environmental conditions.

Our goal is to make it easier than ever to grow your crops in a smarter way!

Woody uses a seeed reTerminal to take sensor readings and control the container. The plant system includes a water level sensor, soil moisture sensor, RGB Led Stick, cooling fan and temperature and humidity sensor. The container always has geolocation so that you can easily track your container's location. This system includes a GPS and accelerometer. Besides geolocation, we also want the container to be secure, with a motion sensor, buzzer, door sensor, door lock and sound sensor you don't have to worry about security.

We wanted Woody to be practical which is why besides all the hardware we developed a .NET MAUI mobile app. The app allows you to create an account as a technician or fleet owner and view sensor readings and have access to control panels for your container.

[Design Document](https://docs.google.com/document/d/1MMz9QzN8PgNpPY9unR-ARdnCKaAS4ccCn0LuVVTUnHU/edit?usp=sharing)

[Team Contract](https://docs.google.com/document/d/1IBiwbEVRstDoR5DC-ZAtVx-RRKVjXoynYvoooI--fi4/edit?usp=sharing)

## 🤝 Contributions
|Team Member|Contributions|
|-----------|-------------|
| Diana| Plant Subsystem, UI Design and Polish|
| Noah | Security Subsystem, Azure Cloud Integration Python, Azure Portal Setup|
| Cindy | Geolocation Subsystem, Azure Cloud Integration C#, Firebase & Azure Portal Setup |

Diana created the scripts for all plant subsystem hardware and the controller. She also prepared the UI mockups and implemented them in our app, along with the UI polish. Cindy and I worked on the Azure Cloud Integration (researching and creating Azure resources), with Cindy focusing on the C# side and I on the Python side. Cindy also worked on the geolocation subsystem, and I worked on the security subsystem. Cindy also set up the Firebase and authentication for our project.

## 📈 Epics
- [Fleet Owner and Farm Technician Authentication and Views](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/49)
- [Azure Cloud Integration](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/28)
- [Security Monitoring of Container Farms for Fleet Owners](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/7)
- [Tracking and Location of Container Farms for Fleet Owners](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/6)
- [Control of Environmental Conditions for Farm Technicians](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/5)
- [Real-Time Environmental Monitoring for Farm Technicians](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/3)

## 📱 Mobile App
The Woody app revolutionizes traditional container farming by integrating smart technology to enhance efficiency and security. Designed for both fleet owners and farm technicians, this application leverages sensors and cloud integration to monitor and control environmental conditions within container greenhouses remotely. Key features include real-time notifications for critical events, role-based access for personalized user experiences, comprehensive container management tools, data visualization for trend analysis, and geolocation tracking for enhanced logistics. Developed using .NET MAUI, the app ensures ease of use and robust functionality.

### 🔑 Key Features

- **Real-time Notifications:** Receive timely notifications for events such as breached thresholds and security alerts, ensuring proactive management.
- **Role-based Access:** Role-specific views and functionalities cater to both farm technicians and fleet owners, ensuring tailored experiences.
- **Container Management:** Easily display and control container security and environmental conditions, providing comprehensive oversight.
- **Data Visualization:** Visualize data changes over time through interactive graphs, facilitating informed decision-making and trend analysis.
- **Geolocation Tracking:** Display container geolocation for fleet owners, enhancing fleet management and logistics planning.

### 👀 Sneakpeek

# TODO: ADD NEW SCREENSHOTS WITH CAPTIONS

![Sign Up](https://i.imgur.com/Wrc5sz5.png)![Sign In](https://i.imgur.com/h3DFLd1.png)![Home](https://i.imgur.com/MwZhD1I.png)

![Overview](https://i.imgur.com/qhQORXR.png)![Container-specific](https://i.imgur.com/Z1SObrf.png)![Container-specific Controls](https://i.imgur.com/CuVgVC6.png)

![Map](https://i.imgur.com/XsjH901.png)![Analytics](https://i.imgur.com/ARcJx6W.png)![Tasks](https://i.imgur.com/m1L4WGQ.png) 

![User Settings](https://i.imgur.com/xenAG0R.png)![About Us](https://i.imgur.com/p2l9vZ5.png)![Placeholder](https://i.imgur.com/BTBeYI2.png)

*Color versions coming soon!*
### 📊 UML Diagram
```mermaid
classDiagram
    GeoLocationRepo --|> FarmRepo : Is
    SecurityRepo --|> FarmRepo : Is
    PlantRepo --|> FarmRepo : Is
    FleetOwner --|> User : Implements
    FarmTechnician --|> User : Implements
    User "1" -- "0..*" Container : manages
    FleetOwner "1" -- "0..*" Container : owns
    FarmTechnician "1" -- "0..*" Container : manages
    Container o-- "1" FarmRepo : has
    FarmRepo "1" -- "0..*" ISensor : contains
    FarmRepo "1" -- "0..*" IActuator : controls
    ISensor "1" -- "0..*" IReading: produces
    IActuator "1" -- "1" ICommand: sends
    ICommand "1" -- "1" CommandType: has
    IReading "1" -- "1" ReadingType: has
    IReading "1" -- "1" ReadingUnit: has
    ICommand "1" -- "1" SubSystemType: has
    UserType "1" -- "1" User: has

    
    class ChartsRepo{
        + GetTemperatureChart(temperatureReadings: List[IReading]) CartesianChart
        + GetSoilMoistureChart(soilMoistureReadings:  List[IReading]) CartesianChart
        + GetHumidityChart(humidityReadings:  List[IReading]) CartesianChart
        + GetNoiseChart(noiseReadings:  List[IReading]) CartesianChart
        + GetLuminosityChart(luminosityReadings:  List[IReading]) CartesianChart

    }
    class FarmRepo{
        -securityRepo: SecurityRepo
        -plantRepo: PlantRepo
        -geoLocationRepo: GeoLocationRepo
        +SecurityRepo(): SecurityRepo
        +PlantRepo(): PlantRepo
        +GeoLocationRepo(): GeoLocationRepo
        +DeserializeOldDataAsync()
        +ValidReadingType(ReadingType)
        +AssignDataToRepos(IReading)
        +AssignDataToGeoLocationRepo(IReading)
        +AssignDataToSecurityRepo(IReading)
        +AssignDataToPlantRepo(IReading)
        +GetNewDataAsync(PartitionContext, EventData)
        +DeserializeNewData(string)
    }
    class ISensor{
        <<interface>>
        +string SensorID
        +string SensorModel
        +readSensor() List[IReading]
    }
    class IActuator{
        <<interface>>
        +string ActuatorID
        +string State
        +controlActuator(command: ICommand)
    }
    class IReading{
        <<interface>>
        +ReadingUnit Unit
        +ReadingType ReadingType
        +T Value
        +Datetime TimeStamp
        
    }
    class ICommand{
        <<interface>>
        +CommandType CommandType
        +SubSystemType SubSystem
        +T Value
    }
    class CommandType{
        <<enumeration>>
        FAN_ON_OFF,
        LIGHT_ON_OFF,
        LIGHT_PULSE,
        BUZZER_ON_OFF,
        DOOR_LOCK
    }
    class SubSystemType{
    <<enumeartion>>
    Security,
    Geolocation,
    Plant
    }
    class ReadingUnit{
        <<enumeration>>
        DEGREES,
        METERS,
        CELCIUS_HUMIDITY,
        MILLIMETERS,
        FARENHEIT,
        HUMIDITY,
        LUX,
        LOUDNESS,
        CELCIUS,
        PERCENTAGE,
        UNITLESS
    }
    class UserType{
        <<enumeration>>
        OWNER,
        FARMER
    }
    class ReadingType{
        <<enumeration>>
        BUZZER,
        PITCH,
        ROLL,
        HUMIDITY,
        TEMPERATURE,
        LUMINOSITY,
        LOUDNESS,
        DOOR,
        DOOR_LOCK,
        MOTION,
        LATITUDE,
        LONGITUDE,
        ALTITUDE,
        GPS,
        VIBRATION
    }
    class GeoLocationRepo{
        +PropertyChanged: PropertyChangedEventHandler
        -geoDb: ContainerDatabaseService~GeoLocationController~
        +GeoLocationController: GeoLocationController
        +GeoDb(): ContainerDatabaseService~GeoLocationController~
        +Pitch: IReading~double~
        +Roll: IReading~double~
        +BuzzerState: IReading~bool~
        +Vibration: IReading~bool~
        +GPS: IReading~GPSCoordinates~
        -AddTestData(sample_points: int)
    }
    class SecurityRepo{
        +PropertyChanged: PropertyChangedEventHandler
        -securityDb: ContainerDatabaseService~SecurityController~
        +SecurityController: SecurityController
        +SecurityDb(): ContainerDatabaseService~SecurityController~
        +NoiseLevels: List~IReading~float~~
        +LuminosityLevels: List~IReading~int~~
        +LuminosityCurrent(): IReading~int~
        +NoiseCurrent(): IReading~float~
        +DoorState: IReading~bool~
        +MotionState: IReading~bool~
        +BuzzerState: IReading~bool~
        +LockState: IReading~bool~
        -AddTestData(sample_points: int)
    }
    class PlantRepo{
        +PropertyChanged: PropertyChangedEventHandler
        -plantDb: ContainerDatabaseService~PlantController~
        +PlantDb(): ContainerDatabaseService~PlantController~
        +TemperatureLevels: List~IReading~double~~
        +HumidityLevels: List~IReading~double~~
        +SoilMoistureLevels: List~IReading~double~~
        +WaterLevel: IReading~int~
        +FanState: IReading~bool~
        +LightState: IReading~bool~
        +CurrentTemperature(): IReading~double~
        +AverageTemperature(): double
        +CurrentHumidity(): IReading~double~
        +CurrentSoilMoisture(): IReading~double~
        -AddTestData(sample_points: int)
    }
    class GPSCoordinates{
        +IReading longitude
        +IReading latitude
        +IReading altitude
    }
    class Container{
        +string containerID
        +string containerName
        +List<IController> SubSystems
    }
    class User {
        +string Key
        +string Uid
        +string Username
        +UserType UserType

        +List<Container> managedContainers
    }
    class FleetOwner{
    }
    class FarmTechnician{
    }

    %%WIP
    class AzureIoTHub {
         +DeviceClient deviceClient
        +BlobContainerClient blobContainerClient
        +EventProcessorClient eventProcessorClient
        +List<string> blobFile
        +ServiceClient serviceClient
        +ConnectToDeviceAsync()
        +DownloadBlobAsync()
        +ProcessEventHandler(ProcessEventArgs)
        +ProcessErrorHandler(ProcessErrorEventArgs)
        +GetCurrentMessageAsync()
        +SendCommandAsync<T>(ICommand<T>)
    }  
```

## 🛠️ App Setup
### Setup
Create an Azure IoT Hub, Event Hub, and Storage Account. Create a Firebase project and set up Firebase Authentication and RealTime DB. Add the required configurations to the appsettings.json file.

### Configuration
Required configurations needed to run the app.
- Iot Hub
  - IOTHubDeviceConnectionString: Navigate to IoTHub -> Devices -> Select Device -> Primary Connection String
  - IOTHubDeviceId: Navigate to IoTHub -> Devices -> Select Device -> Device Name/ID
  - IOTHubConnectionString: Navigate to IoTHub -> Shared access policies -> iothubowner -> Primary Connection String
  - BlobContainerName: Navigate to Storage account -> Containers -> Desired container name
  - BlobConnectionString: Navigate to Storage account -> Access keys -> Connection string
  - EventHubConnectionString: Navigate to Event Hubs Namespace -> Shared access policies -> RootManageSharedAccessKey -> Primary Connection String
  - EventHubName: Navigate to Event Hubs Namespace -> Event Hubs -> Desired Event Hub name
  - EventHubConsumer: Navigate to Event Hubs Instance -> Consumer groups -> Desired Consumer group name
- Firebase
    - FirebaseAuthorizedDomain: Navigate to Firebase Console -> Project -> Settings -> Authorized Domains
    - FirebaseApikey: Navigate to Firebase Console -> Project -> Project Settings -> General -> Web API key
    - FirebaseDatabaseUrl: Navigate to Firebase Console -> Project -> Realtime Database -> Copy URL

### Test Login Credentials
- **Fleet Owner**
    - Email: ownerowner@owner.ca
    - Password: ownerowner
- **Farm Technician**
    - Email: farmer@test.ca
    - Password: farmer

## 🔮 Future Works
- Features
    - Save User Preferences
    - Creating Multiple Containers
    - Tasks
    - Geolocation 3D Visualization
- Bugs
    - [App Carousel Swiping](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/133)
    - [IndicatorView delay](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/137)
## 🌟 Bonus Features & Project Documentation
### IoT Hub Storage
- Storage Type: Blob Storage
- Reason: IoT Hub is a fully managed service that enables reliable and secure bidirectional communication between millions of IoT devices and a solution back end. It also provides a secure way to store data from IoT devices. Blob storage is a good choice for storing large amounts of unstructured data, such as text or binary data.
- Create an event in Storage Account ![Event](https://i.imgur.com/0hxwAnG.png)

## 💡 Controlling Actuators

### Buzzer Control

**Actuator:** Buzzer  
**Subsystem(s):** Geo/Security  
**Communication Strategy:** Direct Methods  
**Reason:** Uses Direct Methods since they are preferred when real-time interaction with the device is necessary, and an immediate response is required. This ensures that the action taken by the backend is confirmed by the device, enhancing reliability and control.

**Example command to turn buzzer on:**

```bash 
 az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "on", "command-type": "buzzer-on-off", "subsystem-type": "security" }'
 ```

 **Example command to turn buzzer off:**

```bash 
 az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "off", "command-type": "buzzer-on-off", "subsystem-type": "security" }'
 ```



### Fan Control

**Actuator:** Fan  
**Subsystem(s):** Plant  
**Communication Strategy:** Direct Methods  
**Reason:** Uses Direct Methods since they are preferred when real-time interaction with the device is necessary, and an immediate response is required. This ensures that the action taken by the backend is confirmed by the device, enhancing reliability and control.

**Example command to turn the fan on:**

```bash 
 az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "on", "command-type": "fan-on-off", "subsystem-type": "plant" }'
 ```

**Example command to turn the fan off:**

```bash 
 az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "off", "command-type": "fan-on-off", "subsystem-type": "plant" }'
 ```

### RGB LED Control

**Actuator:** RGB Led  
**Subsystem(s):** Plant  
**Communication Strategy:** Direct Methods  
**Reason:** Uses Direct Methods since they are preferred when real-time interaction with the device is necessary, and an immediate response is required. This ensures that the action taken by the backend is confirmed by the device, enhancing reliability and control.

**Example command to turn the RGB LED on:**
```bash 
az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "on", "command-type": "light-on-off", "subsystem-type": "plant" }'
```

**Example command to turn the RGB LED off:**
```bash 
az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "off", "command-type": "light-on-off", "subsystem-type": "plant" }'
```

### Door Lock Control

**Actuator:** Door Lock  
**Subsystem(s):** Security  
**Communication Strategy:** Direct Methods  
**Reason:** Uses Direct Methods since they are preferred when real-time interaction with the device is necessary, and an immediate response is required. This ensures that the action taken by the backend is confirmed by the device, enhancing reliability and control.

**Example command to control the door lock:**

```bash  
az iot hub invoke-device-method --hub-name {iothub_name} --device-id {device_id} --method-name control_actuators --method-payload '{ "value": "<string representation of integer between -1 and 1>", "command-type": "door-lock", "subsystem-type": "security" }'
```
 
## 🐱‍💻 Authors
Diana Karpeev <br>
Katchenin Cindy Coulibaly <br>
Noah Groleau
