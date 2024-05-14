#!/usr/bin/env python3


import seeed_python_reterminal.core as rt
from python.sensors.sensors import ISensor, AReading


class LuminositySensor(ISensor):
    """A sensor class for reading luminosity levels."""

    def __init__(
        self,
        gpio: int | None = None,
        model: str = "Built-in Luminosity Sensor",
        type: AReading.Type = AReading.Type.LUMINOSITY,
    ):
        """Initialize a Luminosity sensor object.

        Args:
            gpio (int | None, optional): The GPIO pin number. Defaults to None.
            model (str, optional): The model of the luminosity sensor. Defaults to 'Built-in Luminosity Sensor'.
            type (AReading.Type, optional): The type of reading taken by the sensor. Defaults to AReading.Type.LUMINOSITY.
        """
        self._sensor_model = model
        self.reading_type = type

    def read_sensor(self) -> list[AReading]:
        """Takes a reading from the luminosity sensor.

        Returns:
            list[AReading]: A list of luminosity readings.
        """
        return [
            AReading(
                type=self.reading_type, unit=AReading.Unit.LUX, value=rt.illuminance
            )
        ]


def main():
    luminosity_sensor = LuminositySensor(
        gpio=None, model="Built-in Luminosity Sensor", type=AReading.Type.LUMINOSITY
    )
    try:
        while True:
            readings = luminosity_sensor.read_sensor()
            for reading in readings:
                print(reading)
    except KeyboardInterrupt:
        print("Exiting...")


if __name__ == "__main__":
    main()
