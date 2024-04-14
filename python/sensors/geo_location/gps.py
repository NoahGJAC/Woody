#!/usr/bin/env python
import serial
import pynmea2
from ..sensors import ISensor, AReading


class GPSSensor(ISensor):

    def __init__(self, gpio: str | None, model: str, type: AReading.Type):
        """Initializes the buzzer controller.
        Args:
            gpio (int | None): The gpio of the GPS, the GPS uses the UART port, no gpio needed.
            model (str): specific model of GPS hardware. i.e: GPS (Air 530)
            type (AReading.Type): The type of reading the GPS accepts.
        """
        self.port = '/dev/ttyS0'
        self.reading_type = type
        self._sensor_model = model
        self.gps = serial.Serial(
            port=self.port,
            baudrate=9600,
            timeout=1
        )

    def read_sensor(self) -> list[AReading]:

        readings: list[AReading] = []
        try:
            line = self.gps.readline().decode('utf-8').strip()
            sentence = pynmea2.parse(line)

            if isinstance(sentence, pynmea2.types.talker.GGA):
                # GGA sentence (Fix information)
                readings.append(
                    AReading(
                        AReading.Type.LATITUDE,
                        AReading.Unit.DEGREE,
                        sentence.latitude))
                readings.append(
                    AReading(
                        AReading.Type.LATITUDE,
                        AReading.Unit.DEGREE,
                        sentence.longitude))
                readings.append(
                    AReading(
                        AReading.Type.ALTITUDE,
                        AReading.Unit.METERS,
                        sentence.altitude))

            return readings
        except pynmea2.ParseError:
            print(f'{e}: could not parse the information from UART port')

    def close(self) -> None:
        self.gps.close()


if __name__ == "__main__":
    GPS_sensor = GPSSensor(None, 'GPS (Air 530)', AReading.Type.GPS)
    try:
        print('start reading')
        try:
            while True:
                readings = GPS_sensor.read_sensor()

                for reading in readings:
                    print(
                        f'{reading.reading_type.value}: {reading.value}{reading.reading_unit.value}')

        except pynmea2.ParseError as e:
            print(f'{e}: could not parse the information from UART port')
            pass
    except KeyboardInterrupt:

        GPS_sensor.close()
        print("Exiting...")
