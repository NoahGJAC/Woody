#!/usr/bin/env python3


from python.sensors.sensors import ISensor, AReading
from grove.grove_loudness_sensor import GroveLoudnessSensor
import grove.i2c
import time


LOUDNESS_GPIO_PIN: int = 0


class LoudnessSensor(ISensor):
    """A class that represents a loudness sensor. (ANALOG)
    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """Initialize the loudness sensor.

        Args:
            gpio (int): The gpio pin of the sensor.
            model (str): The model name of the sensor.
            type (AReading.Type): The type of reading the sensor takes.
        """
        self._sensor = GroveLoudnessSensor(gpio)

        # bug with adc, need to manually update address and bus
        self._sensor.adc.address = 0x04
        self._sensor.adc.bus = grove.i2c.Bus(1)

        self._sensor_model: str = model
        self.reading_type: AReading.Type = type

    def read_sensor(self) -> list[AReading]:
        """Returns an AReading list with the loudness sensor data.

        Returns:
            list[AReading]: The list of AReadings taken by the sensor.
        """
        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.LOUDNESS,
                value=float(
                    self._sensor.value))]


def main():
    loudness_sensor = LoudnessSensor(
        gpio=LOUDNESS_GPIO_PIN,
        model='Grove - Loudness Sensor',
        type=AReading.Type.LOUDNESS)
    print('Listening...')
    try:
        while True:
            readings = loudness_sensor.read_sensor()
            for reading in readings:
                print(reading)
            time.sleep(0.1)
    except KeyboardInterrupt:
        print('Exiting')


if __name__ == '__main__':
    main()
