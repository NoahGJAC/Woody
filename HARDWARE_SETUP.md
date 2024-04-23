The purpose of this file is to document the setup process for each subsystem's hardware.

## Plant

## Geolocation

## Security  
- Loudness Sensor: Analog 4 pin connection to base hat.
- PIR Motion Sensor: Digital 4 pin connection to base hat.
- Door Magnet: Digital **2 pin connection** to base hat. One pin to the numbered digital pin and another to GND, see references.
- Door Servo: PWM **3 pin connection** to **exposed** base hat pins.
  - Brown Cable: Must be connected to GND.
  - Red Cable: Must be connected to 5V.
  - Orange Cable: Must be connected to GPIO 12 PWM0.
    - Note: See reference image below for connection points.
- Buzzer: Internal component in reTerminal.

## References
![baseHat](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/assets/98351050/fb257415-d486-4e30-8372-3f1d227ce53b)
![pins](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/assets/98351050/fd39d7bc-01a9-4301-9d47-8a80675b9c1c)  
![DoorMagnetEx](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-woody/assets/98351050/c592ca45-58d1-44a4-9fe1-49a437b1d856)

