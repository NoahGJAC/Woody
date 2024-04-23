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
        type: AReading.Type = AReading.Type.TEMPERATURE_HUMIDITY,
    ):
        """Initializes the temperature and humidity sensor.

        Args:
            gpio (int): The gpio of the temperature and humidity sensor.
            model (str, optional): The model of the temperature and humidity sensor. Defaults to 'Adjustable PIR Motion Sensor'.
            type (AReading.Type, optional): The first reading type of the temperature and humidity sensor. Defaults to AReading.Type.TEMPERATURE_HUMIDITY.
        """
        self._address = 0x38
        self._bus = 4
        self.sensor = GroveTemperatureHumidityAHT20(
            address=self._address, bus=self._bus
        )
        self._sensor_model = model
        self.reading_type = type

    def read_sensor(self) -> list[AReading]:
        """Reads temperature and humidity sensor data and returns a list of readings.

        Returns:
            list[AReading]: A list of readings taken by the sensor.
        """
        readings = self.sensor.read()
        combined_readings = []

        for temperature, humidity in readings:
            combined_readings.append(temperature)
            combined_readings.append(humidity)

        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.CELSIUS_HUMIDITY,
                value=combined_readings,
            )
        ]


def main():
    temperature_humidity_sensor = TemperatureHumiditySensor()
    try:
        while True:
            readings: list[AReading.Type.TEMPERATURE, AReading.Type.HUMIDITY] = (
                temperature_humidity_sensor.read_sensor()
            )
            num_readings = len(readings)
            for i in range(num_readings):
                if i % 2 == 0:
                    print("temperature: {:.2f} {0}".format(readings[i], AReading.Unit.CELCIUS))
                else:
                    print("humidity: {:.2f} {0}".format(readings[i], AReading.Unit.HUMIDITY))
            sleep(0.2)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == "__main__":
    main()
