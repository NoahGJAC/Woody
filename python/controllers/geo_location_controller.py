#!/usr/bin/env python
from time import sleep
import pynmea2
from python.actuators.actuators import ACommand
from python.controllers.device_controllers import IDevice_Controller
from python.sensors.geo_location.gps import GPSSensor
from python.sensors.geo_location.pitch import PitchSensor
from python.sensors.geo_location.roll import RollSensor
from python.sensors.sensors import AReading
from python.actuators.geo_location.buzzer import BuzzerController


class Geo_Location_Controller(IDevice_Controller):
    def __init__(self) -> None:
        self.roll = RollSensor(None, 'Built-in Accelerometer', AReading.Type.ROLL)
        self.pitch = PitchSensor(None, 'Built-in Accelerometer', AReading.Type.PITCH)
        self.gps = GPSSensor(None, 'GPS (Air 530)', AReading.Type.GPS)
        self.buzzer = BuzzerController(
        gpio=None,
        command_type=ACommand.Type.BUZZER_ON_OFF,
        model='ReTerminal Buzzer',
        reading_type=AReading.Type.BUZZER,
        initial_state='off')


    def control_actuators(self) -> None:
        self._print_readings(self.buzzer.read_sensor())
        sleep(1)
        # change state
        self.buzzer.control_actuator('on')

        self._print_readings(self.buzzer.read_sensor())
        sleep(1)

        self.buzzer.control_actuator('off')
        self._print_readings(self.buzzer.read_sensor())
        sleep(1)

    def read_sensors(self) -> None:
        self._print_readings(self.roll.read_sensor())
        self._print_readings(self.pitch.read_sensor())
        self._print_readings(self.gps.read_sensor())

    def loop(self):
        while True:
            self.control_actuators()
            self.read_sensors()
            sleep(2)

    def _print_readings(self,readings: list[AReading]) -> None:
        for reading in readings:
            print(reading)


def main():
    controller = Geo_Location_Controller()
    controller.loop()



if __name__ == "__main__":
    try: 
        main()
    except KeyboardInterrupt:
        print("Exiting...")
    except pynmea2.ParseError as e:
        f"{e} \nCould not parse the information? you need to plug the GPS on UART port and wait 5 seconds")
        pass

