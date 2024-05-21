![Banner](https://i.imgur.com/9xQotbv.png)
## 🌱 Woody - Smart Farming App
Introducing Woody, your ultimate solution for upgrading basic container greenhouses into smart farms.

With Woody, fleet owners can easily keep tabs on all their containers, worry-free about fleet security. Meanwhile, farm technicians can conveniently manage plants from outside the container, thanks to various monitoring sensors and controls that can adjust environmental conditions. 

Our goal is to make it easier than ever to grow your crops in a smarter way!

[Design Document](https://docs.google.com/document/d/1MMz9QzN8PgNpPY9unR-ARdnCKaAS4ccCn0LuVVTUnHU/edit?usp=sharing)

[Team Contract](https://docs.google.com/document/d/1IBiwbEVRstDoR5DC-ZAtVx-RRKVjXoynYvoooI--fi4/edit?usp=sharing)

## 📈 Epics
- [Fleet Owner and Farm Technician Authentication and Views](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/49)
- [Azure Cloud Integration](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/28)
- [Security Monitoring of Container Farms for Fleet Owners](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/7)
- [Tracking and Location of Container Farms for Fleet Owners](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/6)
- [Control of Environmental Conditions for Farm Technicians](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/5)
- [Real-Time Environmental Monitoring for Farm Technicians](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/issues/3)

## 🔑 Key Features

- **Real-time Notifications:** Receive timely notifications for events such as breached thresholds and security alerts, ensuring proactive management.
- **Role-based Access:** Role-specific views and functionalities cater to both farm technicians and fleet owners, ensuring tailored experiences.
- **Container Management:** Easily display and control container security and environmental conditions, providing comprehensive oversight.
- **Data Visualization:** Visualize data changes over time through interactive graphs, facilitating informed decision-making and trend analysis.
- **Geolocation Tracking:** Display container geolocation for fleet owners, enhancing fleet management and logistics planning.

## 👀 Sneakpeek
![Sign Up](https://i.imgur.com/Wrc5sz5.png)![Sign In](https://i.imgur.com/h3DFLd1.png)![Home](https://i.imgur.com/MwZhD1I.png)

![Overview](https://i.imgur.com/qhQORXR.png)![Container-specific](https://i.imgur.com/Z1SObrf.png)![Container-specific Controls](https://i.imgur.com/CuVgVC6.png)

![Map](https://i.imgur.com/XsjH901.png)![Analytics](https://i.imgur.com/ARcJx6W.png)![Tasks](https://i.imgur.com/m1L4WGQ.png) 

![User Settings](https://i.imgur.com/xenAG0R.png)![About Us](https://i.imgur.com/p2l9vZ5.png)![Placeholder](https://i.imgur.com/BTBeYI2.png)

*Color versions coming soon!*

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
