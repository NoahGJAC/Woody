#!/usr/bin/env python
from time import sleep
import pynmea2
from python.actuators.actuators import ACommand
from python.controllers.device_controllers import IDevice_Controller
from python.sensors.sensors import AReading
from python.actuators.security.buzzer import BuzzerController
from python.actuators.security.door_lock import DoorLockController
from python.sensors.security.door import DoorSensor
from python.sensors.security.loudness import LoudnessSensor
from python.sensors.security.luminosity import LuminositySensor
from python.sensors.security.motion import MotionSensor

# TODO: Refactor
class SecurityController(IDevice_Controller):
    def __init__(self) -> None:
        self.door_sensor = DoorSensor(gpio=5, model='Magnetic door sensor reed switch', type=AReading.Type.DOOR)
        
        self.loudness = LoudnessSensor(
        gpio=0,
        model='Grove - Loudness Sensor',
        type=AReading.Type.LOUDNESS)
        
        self.luminosity = LuminositySensor(
        gpio=None,
        model='Built-in Luminosity Sensor',
        type=AReading.Type.LUMINOSITY)

        self.motion = MotionSensor(
        gpio=22,
        model='Adjustable PIR Motion Sensor',
        type=AReading.Type.MOTION)

        self.buzzer = BuzzerController(
        gpio=None,
        command_type=ACommand.Type.BUZZER_ON_OFF,
        model='ReTerminal Buzzer',
        reading_type=AReading.Type.BUZZER,
        initial_state='off')

        self.door = DoorLockController(
        model='180 degree servo',
        gpio=12,
        command_type=ACommand.Type.DOOR_LOCK,
        reading_type=AReading.Type.DOOR_LOCK,
        initial_state='-1')


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

        self.door.control_actuator('1')
        self._print_readings(self.door.read_sensor())
        sleep(1)
        self.door.control_actuator('-1')
        self._print_readings(self.door.read_sensor())
        sleep(1)
        self.door.control_actuator('-0')
        self._print_readings(self.door.read_sensor())
        sleep(1)
        self.door.control_actuator('0.5')
        self._print_readings(self.door.read_sensor())
        sleep(1)
        self.door.control_actuator('-0.5')
        self._print_readings(self.door.read_sensor())
        sleep(1)

    def read_sensors(self) -> None:
        self._print_readings(self.motion.read_sensor())
        self._print_readings(self.luminosity.read_sensor())
        self._print_readings(self.door_sensor.read_sensor())
        self._print_readings(self.loudness.read_sensor())

    def loop(self):
        while True:
            self.control_actuators()
            self.read_sensors()
            sleep(2)

    def _print_readings(self,readings: list[AReading]) -> None:
        for reading in readings:
            print(reading)


def main():
    controller = SecurityController()
    controller.loop()


if __name__ == "__main__":
    try: 
        main()
    except KeyboardInterrupt:
        print("Exiting...")
    except pynmea2.ParseError as e:
        f"{e} \nCould not parse the information? you need to plug the GPS on UART port and wait 5 seconds"
        pass

