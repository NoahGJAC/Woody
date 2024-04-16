#!/usr/bin/env python3


import time
from grove.gpio import GPIO
from python.sensors.sensors import ISensor, AReading


MOTION_SENSOR_GPIO: int = 22


class MotionSensor(ISensor):
    """A class for motion sensor readings.
    """

    def __init__(
            self,
            gpio: int,
            model: str = 'Adjustable PIR Motion Sensor',
            type: AReading.Type = AReading.Type.MOTION):
        """Initializes the Motion Sensor.

        Args:
            gpio (int): The gpio of the motion sensor.
            model (str, optional): The model of the motion sensor. Defaults to 'Adjustable PIR Motion Sensor'.
            type (AReading.Type, optional): The reading type of the motion sensor. Defaults to AReading.Type.MOTION.
        """
        self.pir = GPIO(gpio, GPIO.IN)
        self._sensor_model = model
        self.reading_type = type

    def read_sensor(self) -> list[AReading]:
        """ Reads motion sensor data and returns a list of readings.

        Returns:
            list[AReading]: A list of readings taken by the sensor.
        """
        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.UNITLESS,
                value=self.pir.read())]


def main():
    motion_sensor = MotionSensor(
        gpio=MOTION_SENSOR_GPIO,
        model='Adjustable PIR Motion Sensor',
        type=AReading.Type.MOTION)
    try:
        while True:
            readings: list[AReading] = motion_sensor.read_sensor()
            for reading in readings:
                print(reading)
            time.sleep(0.2)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == '__main__':
    main()
