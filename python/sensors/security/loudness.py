#!/usr/bin/env python3


from python.sensors.sensors import ISensor, AReading
from grove.grove_loudness_sensor import GroveLoudnessSensor


class LoudnessSensor(ISensor):
    """A class that represents a loudness sensor.
    """
    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """Initialize the loudness sensor.

        Args:
            gpio (int): The gpio pin of the sensor.
            model (str): The model name of the sensor.
            type (AReading.Type): The type of reading the sensor takes.
        """
        self._sensor = GroveLoudnessSensor(gpio)
        self._sensor_model: str = model
        self.reading_type: AReading.Type = type

    def read_sensor(self) -> list[AReading]:
        """Returns an AReading list with the loudness sensor data.

        Returns:
            list[AReading]: The list of AReadings taken by the sensor.
        """
        return [AReading(type = self.reading_type, unit = AReading.Unit.LOUDNESS, value=float(self._sensor.value))]