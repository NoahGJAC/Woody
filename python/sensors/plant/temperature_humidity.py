#!/usr/bin/env python3

import time
from ..sensors import ISensor, AReading
from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20


class TemperatureHumiditySensor(ISensor):
    # A class for temperature and humidity sensor readings.

    def __init__(
        self,
        type: AReading.Type,
        gpio: int = None,
        model: str = "AHT20 Temperature & Humidity Sensor",
    ):
        """Initializes the temperature and humidity sensor.

        Args:
            type (AReading.Type): The first reading type of the temperature and humidity sensor.
            gpio (int, optional): The gpio of the temperature and humidity sensor. Defaults to none.
            model (str, optional): The model of the temperature and humidity sensor. Defaults to 'Adjustable PIR Motion Sensor'.
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
        try:
            temperature, humidity = self.sensor.read()

            if self.reading_type == AReading.Type.TEMPERATURE:
                return [
                    AReading(
                        type=AReading.Type.TEMPERATURE,
                        unit=AReading.Unit.CELCIUS,
                        value=temperature,
                    )
                ]
            elif self.reading_type == AReading.Type.HUMIDITY:
                return [
                    AReading(
                        type=AReading.Type.HUMIDITY,
                        unit=AReading.Unit.HUMIDITY,
                        value=humidity,
                    )
                ]
            else:
                raise BaseException(
                    f"Invalid reading type. Cannot read {self.reading_type.value} from a temperature and humidity sensor."
                )
        except BaseException:
            return [
                AReading(type=self.reading_type, unit=AReading.Unit.FAILURE, value="")
            ]


def main():
    temperature_humidity_sensor = TemperatureHumiditySensor()
    try:
        while True:
            reading = temperature_humidity_sensor.read_sensor()
            print(repr(reading))
            time.sleep(0.2)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == "__main__":
    main()
