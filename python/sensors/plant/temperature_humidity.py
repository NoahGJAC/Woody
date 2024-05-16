#!/usr/bin/env python3

import time
from ..sensors import ISensor, AReading
from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20


class TemperatureHumiditySensor(ISensor):
    # A class for temperature and humidity sensor readings.

    def __init__(
        self,
        gpio: int = None,
        model: str = "AHT20 Temperature & Humidity Sensor",
        type: AReading.Type = AReading.TEMPERATURE_HUMIDITY
    ):
        """Initializes the temperature and humidity sensor.

        Args:
            gpio (int): The gpio of the temperature and humidity sensor.
            model (str, optional): The model of the temperature and humidity sensor. Defaults to 'Adjustable PIR Motion Sensor'.
            types (AReading.Type, optional): The reading type of the temperature and humidity sensor. Defaults to AReading.Type.TEMPERATURE_HUMIDITY.
        """
        self._address = 0x38
        self._bus = 4
        self.sensor = GroveTemperatureHumidityAHT20(
            address=self._address, bus=self._bus
        )
        self._sensor_model = model
        self.reading_types = types

    def read_sensor(self) -> list[AReading]:
        """Reads temperature and humidity sensor data and returns a list of readings.

        Returns:
            list[AReading]: A list of readings taken by the sensor.
        """
        readings: list[AReading] = []
        types = list[AReading.Type.TEMPERATURE, AReading.Type.HUMIDITY]

        for type in types:
            readings.append(
                AReading(
                    type=type,
                    unit=(
                        AReading.Unit.CELCIUS
                        if type == AReading.Type.TEMPERATURE
                        else AReading.Unit.HUMIDITY
                    ),
                    value=self.sensor.read()
                )
            )

        return readings


def main():
    temperature_humidity_sensor = TemperatureHumiditySensor()
    try:
        while True:
            readings = temperature_humidity_sensor.read_sensor()
            for reading in readings:
                print(repr(reading))

            time.sleep(0.2)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == "__main__":
    main()
