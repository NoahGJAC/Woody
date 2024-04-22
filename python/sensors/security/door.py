#!/usr/bin/env python3


from time import sleep
import RPi.GPIO as GPIO
from gpiozero import Button
from python.sensors.sensors import ISensor, AReading


class DoorSensor(ISensor):
    
    def __init__(self, gpio: int | None, model: str, type: AReading.Type):
        """Initializes the door sensor.

        Args:
            gpio (int | None): The pin of the door sensor.
            model (str): The model of the door sensor.
            type (AReading.Type): Type of reading the door sensor produces.
        """
        self._button = Button(gpio)
        self._sensor_model = model
        self.reading_type = type
    
    def read_sensor(self) -> list[AReading]:
        """Takes a reading from the door lock sensor.

        Returns:
            list[AReading]: List of readings measured by the sensor.
        """
        return [AReading(type=self.reading_type, unit=AReading.Unit.UNITLESS, value=self._button.is_pressed)]


def main():
    door_sensor = DoorSensor(gpio=5, model='Magnetic door sensor reed switch', type=AReading.Type.DOOR)
    try:
        while True:
            readings = door_sensor.read_sensor()
            for reading in readings:
                print(reading)
    except KeyboardInterrupt:
        print('Exiting')


if __name__ == '__main__':
    main()