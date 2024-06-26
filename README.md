![Banner](https://i.imgur.com/9xQotbv.png)

## 🚀 Team Information
- **Team Woody**
  - **[Noah Groleau](https://github.com/NoahGJAC)**
  - **[Diana Karpeev](https://github.com/dianakarpeev)**
  - **[Katchenin Cindy Coulibaly](https://github.com/Cindy-Coulibaly)**



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
<table border="0" cellspacing="10" cellpadding="10">
  <tr>
    <td><img src="https://i.imgur.com/w9I7uRv.png" alt="Log in" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/gGqKEQO.png" alt="Create Account" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/cf6k8Vu.png" alt="Forgotten Password" style="width: 150px; height: 325px;"></td>
  </tr>
  <tr>
    <td><figcaption>Sign In</figcaption></td>
    <td><figcaption>Sign Up</figcaption></td>
    <td><figcaption>Password Reset</figcaption></td>
  </tr>
</table>

<table border="0" cellspacing="10" cellpadding="10">
  <tr>
    <td><img src="https://i.imgur.com/UpR4u1Q.png" alt="Home" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/DPmVgUv.png" alt="Settings" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/LjZ0vO5.png" alt="Dashboard" style="width: 150px; height: 325px;"></td>
  </tr>
  <tr>
    <td><figcaption>Home</figcaption></td>
    <td><figcaption>Settings</figcaption></td>
    <td><figcaption>Dashboard</figcaption></td>
  </tr>
</table>

<table border="0" cellspacing="10" cellpadding="10">
  <tr>
    <td><img src="https://i.imgur.com/lgOgghS.png" alt="Plant" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/PQ0fxj7.png" alt="Security" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/DDs3hti.png" alt="Geolocation" style="width: 150px; height: 325px;"></td>
  </tr>
  <tr>
    <td><figcaption>Plant Subsystem</figcaption></td>
    <td><figcaption>Security Subsystem</figcaption></td>
    <td><figcaption>Geolocation Subsystem</figcaption></td>
  </tr>
</table>

<table border="0" cellspacing="10" cellpadding="10">
  <tr>
    <td><img src="https://i.imgur.com/Kr4Nfc1.png" alt="Analytics" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/zz9cvHL.png" alt="Security Breaches" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/KsDcuGT.png" alt="About Us" style="width: 150px; height: 325px;"></td>
  </tr>
  <tr>
    <td><figcaption>Analytics</figcaption></td>
    <td><figcaption>Analytics</figcaption></td>
    <td><figcaption>About Us</figcaption></td>
  </tr>
</table>

<table border="0" cellspacing="10" cellpadding="10">
  <tr>
    <td><img src="https://i.imgur.com/ywYVgF0.png" alt="Tasks" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/k0uWkyC.png" alt="Create a Task" style="width: 150px; height: 325px;"></td>
    <td><img src="https://i.imgur.com/t63iWIv.png" alt="Splash" style="width: 150px; height: 325px;"></td>
  </tr>
  <tr>
    <td><figcaption>Tasks</figcaption></td>
    <td><figcaption>Create a Task</figcaption></td>
    <td><figcaption>Splash Screen</figcaption></td>
  </tr>
</table>

### 📊 UML Diagram
```mermaid
classDiagram
    %% Interfaces
    class IReading~T~ {
        +T Value
        +DateTime TimeStamp
        +ReadingUnit Unit
        +ReadingType ReadingType
        +Models.Command~string~ Command
    }
    class ICommand~T~ {
        +CommandType CommandType
        +SubSystemType SubSystem
        +T Value
    }
    class IDataStore~T~ {
        +AddItemsAsync(T item): bool
        +UpdateItemsAsync(T item): bool
        +DeleteItemsAsync(T item): bool
        +GetItemsAsync(forceRefresh: bool): IEnumerable~T~
        +ObservableCollection~T~ Items
    }
    class IHasUKey {
        +string Key
    }

    class INotifyPropertyChanged {
        +event PropertyChangedEventHandler PropertyChanged
    }
    
    %% Classes implementing interfaces
    class BlobReadings {
        +string Value
        +string Body
        +DateTime TimeStamp
        +ReadingUnit Unit
        +ReadingType ReadingType
    }
    BlobReadings --|> IReading~T~
    
    class Command~T~ {
        +T Value
        +CommandType CommandType
        +SubSystemType SubSystem
    }
    Command~T~ --|> ICommand~T~
    
    class GPSCoordinates {
        +IReading~double~ Longitude
        +IReading~double~ Latitude
        +IReading~double~ Altitude
    }
    GPSCoordinates --|> IReading~T~
    
    class SensorReading~T~ {
        +T Value
        +DateTime TimeStamp
        +ReadingUnit Unit
        +ReadingType ReadingType
        +Models.Command~string~ Command
    }
    SensorReading~T~ --|> IReading~T~
    
    class User {
        +string Key
        +string Uid
        +string Username
        +UserType UserType
    }
    User --|> IHasUKey
    
    %% Existing classes
    class PlantRepo {
        +event PropertyChangedEventHandler PropertyChanged
        -List~IReading~ TemperatureLevels
        -List~IReading~ HumidityLevels
        -List~IReading~ SoilMoistureLevels
        -IReading~int~ WaterLevel
        -IReading~bool~ FanState
        -IReading~bool~ LightState
        +IReading~double~ CurrentTemperature
        +double AverageTemperature
        +IReading~double~ CurrentHumidity
        +IReading~double~ CurrentSoilMoisture
    }
    
    class FarmRepo {
        -SecurityRepo securityRepo
        -PlantRepo plantRepo
        -GeoLocationRepo geoLocationRepo
        +Task DeserializeOldDataAsync()
        +bool ValidReadingType(ReadingType type)
        +void AssignDataToRepos(IReading jsonObject)
        +void AssignDataToGeoLocationRepo(IReading jsonObject)
        +void AssignDataToSecurityRepo(IReading jsonObject)
        +void AssignDataToPlantRepo(IReading jsonObject)
        +Task GetNewDataAsync(PartitionContext partition, EventData data)
        +void DeserializeNewData(string blobfile)
    }
    
    class SecurityRepo {
        +event PropertyChangedEventHandler PropertyChanged
        -List~IReading~float~ NoiseLevels
        -List~IReading~int~ LuminosityLevels
        +IReading~int~ LuminosityCurrent
        +IReading~float~ NoiseCurrent
        -IReading~bool~ DoorState
        -IReading~bool~ MotionState
        -IReading~bool~ BuzzerState
        -IReading~bool~ LockState
    }
    class GeoLocationRepo {
        +event PropertyChangedEventHandler PropertyChanged
        +IReading~double~ Pitch
        +IReading~double~ Roll
        +IReading~bool~ BuzzerState
        +IReading~bool~ Vibration
        +IReading~GPSCoordinates~ GPS
    }
    GeoLocationRepo --|> INotifyPropertyChanged

    class UserRepo {
        -User User
        -UserDatabaseService~User~ userDb
        +UserDatabaseService~User~ UserDb
    }
    
    %% Relationships
    PlantRepo --|> INotifyPropertyChanged
    SecurityRepo --|> INotifyPropertyChanged
    FarmRepo -- PlantRepo
    FarmRepo -- SecurityRepo
    FarmRepo -- GeoLocationRepo

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
    - Filter Graphs by dates
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

## 📬 D2C Messages
> [!NOTE]  
> The Reading type and type name are set as custom propeties in the message.  

### Door Lock Reading
**Sensor:** Door Lock  
**Subsystem(s):** Security  

```bash
# Sending door lock reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=door lock state;reading-type-name=DOOR_LOCK' --data '{"value":-1.0,"unit":""}'
```

### Roll Reading
**Sensor:** Internal Acceleration Device  
**Subsystem(s):** Geolocation

```bash
# Sending roll reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=roll;reading-type-name=ROLL' --data '{ "value":-1.3140687970083822,"unit":"\u00b0"}'
```

### Pitch Reading
**Sensor:** Internal Acceleration Device  
**Subsystem(s):** Geolocation

```bash
# Sending pitch reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=pitch;reading-type-name=PITCH' --data '{ "value":0.3140687970083822,"unit":"\u00b0"}'
```

### Buzzer Reading
**Sensor:** Internal Buzzer  
**Subsystem(s):** Security/Geolocation

```bash
# Sending buzzer reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=buzzer state;reading-type-name=BUZZER' --data '{ "value":"False","unit":""}'
```

### Vibration Reading
**Sensor:** Internal Vibration Sensor  
**Subsystem(s):** Geolocation

```bash
# Sending vibration reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=vibration;reading-type-name=VIBRATION' --data '{ "value":false,"unit":""}'
```

### Door Reading
**Sensor:** Magnetic Door Sensor Reed Switch  
**Subsystem(s):** Security

```bash
# Sending door reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=door state;reading-type-name=DOOR' --data '{ "value":false,"unit":""}'
```

### Loudness Reading
**Sensor:** Loudness Sensor  
**Subsystem(s):** Security

```bash
# Sending loudness reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=loudness;reading-type-name=LOUDNESS' --data '{"value": 503.0,"unit":"%loudnessStrength"}'
```

### Luminosity Reading
**Sensor:** Internal Luminosity Sensor  
**Subsystem(s):** Security

```bash
# Sending luminosity reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=luminosity;reading-type-name=LUMINOSITY' --data '{"value":6,"unit":"lx"}'
```

### Motion Reading
**Sensor:** Motion Sensor  
**Subsystem(s):** Security

```bash
# Sending motion reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=motion;reading-type-name=MOTION' --data '{"value":true,"unit":""}'
```

### Soil Moisture Reading
**Sensor:** Soil Moisture Sensor  
**Subsystem(s):** Plant

```bash
# Sending soil moisture reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=soil moisture;reading-type-name=SOIL_MOISTURE' --data '{"value":1686,"unit":""}'
```

### Water Level Reading
**Sensor:** Water Level Sensor  
**Subsystem(s):** Plant

```bash
# Sending water level reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=water level;reading-type-name=WATER_LEVEL' --data '{"value":504,"unit":""}'
```

### Temperature Reading
**Sensor:** Temperature and Humidity Sensor  
**Subsystem(s):** Plant

```bash
# Sending temperature reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=temperature;reading-type-name=TEMPERATURE' --data '{"value":24.0,"unit":"\u00b0C"}'
```

### Humidity Reading
**Sensor:** Temperature and Humidity Sensor  
**Subsystem(s):** Plant

```bash
# Sending humidity reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=humidity;reading-type-name=HUMIDITY' --data '{"value":50.0,"unit":"%HR"}'
```

### Fan Reading
**Sensor:** Fan with Relay  
**Subsystem(s):** Plant

```bash
# Sending fan reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=fan state;reading-type-name=FAN' --data '{"value":0,"unit":""}'
```

### LED Reading
**Sensor:** RGB LED Stick  
**Subsystem(s):** Plant

```bash
# Sending LED reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=LED state;reading-type-name=LED' --data '{"value":false,"unit":""}'
```

### Longitude Reading
**Sensor:** GPS  
**Subsystem(s):** Geolocation

```bash
# Sending longitude reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=longitude;reading-type-name=LONGITUDE' --data '{"value":45.0,"unit":"\u00b0"}'
```

### Latitude Reading
**Sensor:** GPS  
**Subsystem(s):** Geolocation

```bash
# Sending latitude reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=latitude;reading-type-name=LATITUDE' --data '{"value":45.0,"unit":"\u00b0"}'
```

### Altitude Reading
**Sensor:** GPS  
**Subsystem(s):** Geolocation

```bash
# Sending altitude reading
az iot device send-d2c-message -n {iothub_name} -d {device_id} --props 'reading-type=altitude;reading-type-name=ALTITUDE' --data '{"value":45.0,"unit":"m"}'
```


## 🐱‍💻 Authors
Diana Karpeev <br>
Katchenin Cindy Coulibaly <br>
Noah Groleau
