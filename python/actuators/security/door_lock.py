#!/usr/bin/env python3
import serial
from gpiozero import Servo
from time import sleep
# for jitter
from gpiozero.pins.pigpio import PiGPIOFactory

try:

    factory = PiGPIOFactory()
    servo = Servo(12, min_pulse_width= 0.5/1000, max_pulse_width=2.5/1000, pin_factory=factory)
    while True:
        print('MIN')
        servo.min()
        sleep(2)
        print('MID')
        servo.mid()
        sleep(2)
        print('MAX')
        servo.max()
        sleep(2)
except  IOError as e:
    print(f'{e}\nDid you forget $ sudo pigpiod')