The purpose of this file is to document the setup process for each subsystem's hardware.

## Hardware Setup

| Subsystem | Component          | Connection Type | Port/Location | Notes |
|-----------|--------------------|-----------------|---------------|-------|
|Plant      | Fan                   | Digital                | GPIO 16 and exposed pins              | Use a relay to connect. View specific instructions below |
| Plant | RGB Led Stick      | PWM        | GPIO 12      | Add user to groups kmem and gpio     |
| Plant | Temperature and Humidity Sensor      | I2C        | GPIO 26      | Create I2C bus on GPIOs 26 and 27     |
| Plant | Soil Moisture Sensor      | Analog        | Analog 4 pin      | Temporary pin     |
| Plant | Water Level Sensor      | Analog        | Analog 4 pin      | Temporary pin    |
| Geolocation | GPS Sensor        | UART            | UART pin      | Configure Raspberry Pi for Serial Communication. |
| Geolocation | Pitch Sensor      | Built-in        | Internal      | -     |
| Geolocation | Roll Sensor       | Built-in        | Internal      | -     |
| Geolocation | Vibration Sensor  | Built-in        | Internal      | -     |
| Geolocation | Buzzer            | Built-in        | Internal      | -     |
| Security   | Loudness Sensor   | Analog          | Analog 4 pin  | -     |
| Security   | PIR Motion Sensor | Digital         | Digital 4 pin | -     |
| Security   | Door Magnet        | Digital         | Digital 2 pin | One pin to the numbered digital pin and another to GND. |
| Security   | Door Servo         | PWM             | Exposed pins  | Brown Cable to GND, Red Cable to 5V, Orange Cable to GPIO 12 PWM0. |
| Security   | Buzzer            | Built-in        | Internal      | -     |

## Fan Connection
1. Attach two jump wires (dupont wires) to the screw terminals of the relay.
2. Connect one of the wires to the black cable (negative) of the fan.
3. Connect the red cable of the fan (positive) to the 5V pin of the Base Hat
4. Attach the remaining relay cable to the underside GND pin of the Base Hat (pin #6)
5. Connect the relay's GPIO socket to GPIO 16

## References
![baseHat](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/assets/98351050/fb257415-d486-4e30-8372-3f1d227ce53b)
![pins](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/assets/98351050/fd39d7bc-01a9-4301-9d47-8a80675b9c1c)  
![DoorMagnetEx](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/assets/98351050/c592ca45-58d1-44a4-9fe1-49a437b1d856)

