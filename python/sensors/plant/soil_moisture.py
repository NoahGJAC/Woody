#!/usr/bin/env python3

# Imports
import time
import grove.i2c
from ..sensors import ISensor, AReading
from grove.adc import ADC


class customADC(ADC):
    def __init__(self, address=0x04, bus=1):
        self.address = address
        self.bus = grove.i2c.Bus(bus)


class SoilMoistureSensor(ISensor):
    # A class for soil moisture sensor readings.

    def __init__(
        self,
        gpio: int = None,
        model: str = "Soil Moisture Sensor",
        type: AReading.Type = AReading.Type.SOIL_MOISTURE,
    ):
        """Initializes the soil moisture sensor.

        Args:
            gpio (int): The gpio of the soil moisture sensor.
            model (str, optional): The model of the soil moisture sensor. Defaults to 'Soil Moisture Sensor'.
            type (AReading.Type, optional): The first reading type of the soil moisture sensor. Defaults to AReading.Type.TEMPERATURE_HUMIDITY.
        """
        self._address = 0x04
        self.sensor = customADC()
        self._sensor_model = model
        self.reading_type = type

    def read_sensor(self) -> list[AReading]:
        """Reads soil moisture sensor data and returns a list of readings.

        Returns:
            list[AReading]: A list of readings taken by the sensor.
        """
        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.UNITLESS,
                value=self.sensor.read_voltage(0),
            )
        ]


def main():
    soil_moisture_sensor = SoilMoistureSensor()
    try:
        while True:
            readings = soil_moisture_sensor.read_sensor()
            for reading in readings:
                print(repr(reading))
            time.sleep(0.2)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == "__main__":
    main()
