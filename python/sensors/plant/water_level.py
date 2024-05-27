#!/usr/bin/env python3

# Imports
import time
import grove.i2c
from python.sensors.sensors import ISensor, AReading
from grove.adc import ADC


class customADC(ADC):
    def __init__(self, address=0x04, bus=1):
        self.address = address
        self.bus = grove.i2c.Bus(bus)


class WaterLevelSensor(ISensor):
    # A class for water level sensor readings.

    def __init__(
        self,
        gpio: int = None,
        model: str = "Water Level Sensor",
        type: AReading.Type = AReading.Type.WATER_LEVEL,
    ):
        """Initializes the water level sensor.

        Args:
            gpio (int): The gpio of the water level sensor. Defaults to none.
            model (str, optional): The model of the water level sensor. Defaults to 'Water Level Sensor'.
            type (AReading.Type, optional): The first reading type of the water level sensor. Defaults to AReading.Type.WATER_LEVEL.
        """
        self.sensor = customADC()
        self._sensor_model = model
        self.reading_type = type

    def read_sensor(self) -> list[AReading]:
        """Reads water level sensor data and returns a list of readings.

        Returns:
            list[AReading]: A list of readings taken by the sensor.
        """
        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.UNITLESS,
                value=self.sensor.read(0),
            )
        ]


def main():
    water_level_sensor = WaterLevelSensor()
    try:
        while True:
            readings = water_level_sensor.read_sensor()
            for reading in readings:
                print(repr(reading))
            time.sleep(0.2)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == "__main__":
    main()
