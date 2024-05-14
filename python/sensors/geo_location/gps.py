#!/usr/bin/env python
import serial
import pynmea2
from python.sensors.sensors import ISensor, AReading


class GPSSensor(ISensor):

    def __init__(self, gpio: str | None, model: str, type: AReading.Type):
        """Initializes the GPS sensor.
        Args:
            gpio (int | None): The gpio of the GPS, the GPS uses the UART port, no gpio needed.
            model (str): specific model of GPS hardware. i.e: GPS (Air 530)
            type (AReading.Type): The type of reading the GPS accepts.
        """
        self.port = "/dev/ttyS0"
        self.reading_type = type
        self._sensor_model = model
        self.gps = serial.Serial(port=self.port, baudrate=9600, timeout=1)
        self.gps.reset_input_buffer()
        self.gps.flush()

    def read_sensor(self) -> list[AReading]:

        readings: list[AReading] = []
        try:
            line = self.gps.readline().decode("utf-8").strip()
            sentence = pynmea2.parse(line)

            if isinstance(sentence, pynmea2.types.talker.GGA):
                # GGA sentence (Fix information)
                self.current_latitude = sentence.latitude
                self.current_longitude = sentence.longitude
                readings.append(
                    AReading(
                        AReading.Type.LATITUDE, AReading.Unit.DEGREE, sentence.latitude
                    )
                )
                readings.append(
                    AReading(
                        AReading.Type.LONGITUDE,
                        AReading.Unit.DEGREE,
                        sentence.longitude,
                    )
                )
                readings.append(
                    AReading(
                        AReading.Type.ALTITUDE, AReading.Unit.METERS, sentence.altitude
                    )
                )

            return readings
        except pynmea2.ParseError as e:
            print(
                f"{e} \nCould not parse the information? you need to plug the GPS on UART port. Also make sure that your GPS is near an open window and replug your GPS after restarting your Raspberri Pi."
            )
            return readings

    def close(self) -> None:
        self.gps.close()

    def change_location(self, lat: float, long: float) -> None:
        if self.current_latitude != lat | self.current_longitude != long:
            print(
                f"location changed for {self.current_latitude},{self.current_longitude} Lat/Long"
            )


if __name__ == "__main__":
    GPS_sensor = GPSSensor(None, "GPS (Air 530)", AReading.Type.GPS)
    try:
        print("start reading")
        try:
            while True:
                readings = GPS_sensor.read_sensor()
                if readings:
                    for reading in readings:
                        print(
                            f"{reading.reading_type.value}: {reading.value} {reading.reading_unit.value}"
                        )

        except pynmea2.ParseError as e:
            print(
                f"{e} \nCould not parse the information? you need to plug the GPS on UART port and wait 5 seconds"
            )
            pass

    except KeyboardInterrupt:

        GPS_sensor.close()
        print("Exiting...")
