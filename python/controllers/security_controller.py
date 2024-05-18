#!/usr/bin/env python


from time import sleep
from python.controllers.device_controllers import IDeviceController
from python.sensors.sensors import AReading, ISensor
from python.actuators.actuators import ACommand, IActuator
from python.actuators.security.buzzer import BuzzerController
from python.actuators.security.door_lock import DoorLockController
from python.sensors.security.door import DoorSensor
from python.sensors.security.loudness import LoudnessSensor
from python.sensors.security.luminosity import LuminositySensor
from python.sensors.security.motion import MotionSensor
import colorama


class SecurityController(IDeviceController):
    """A class that represents a security subsystem device controller."""

    def __init__(
            self,
            sensors: list[ISensor],
            actuators: list[IActuator]) -> None:
        """Initializes a SecurityController

        Args:
            sensors (list[ISensor]): The list of sensors to initialize.
            actuators (list[IActuator]): The list of actuators to initialize.
        """

        super().__init__(sensors=sensors, actuators=actuators)

    def control_actuators(self, commands: list[ACommand]) -> None:
        """Runs the commands on their corresponding actuators.

        Args:
            commands (list[ACommand]): The list of commands to run.
        """
        actuator_dict = self._get_actuator_dict()
        for command in commands:
            actuator = actuator_dict.get(command.target_type)
            if actuator is None:
                print(
                    colorama.Fore.RED +
                    f"No actuator found for command: {command}" +
                    colorama.Fore.RESET
                )
                continue

            if not actuator.validate_command(command=command):
                print(
                    colorama.Fore.RED +
                    f"Invalid command for actuator: {actuator.type}\n\tCommand: {command}" +
                    colorama.Fore.RESET)
                continue

            actuator.control_actuator(value=command.value)
            print(
                f"Executed command: {command}"
            )

    def _get_actuator_dict(self) -> dict[ACommand.Type, IActuator]:
        """Returns a dictionary with the actuators as values to their command type key.

        Returns:
            dict[ACommand.Type, IActuator]: The dictionary with the actuators as values.
        """
        return {actuator.type: actuator for actuator in self._actuators}

    def read_sensors(self) -> list[AReading]:
        """Returns a list of readings from all sensors.

        Returns:
            list[AReading]: The readings from the sensors.
        """
        readings: list[AReading] = [
            reading for sensor in self._sensors for reading in sensor.read_sensor()]
        return readings

    def loop(self):
        """Loops through controlling actuators and reading sensors. Intended for testing.
        """
        pre_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.BUZZER_ON_OFF, value='on'),
            ACommand(target=ACommand.Type.DOOR_LOCK, value='1')
        ]
        post_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.BUZZER_ON_OFF, value='off'),
            ACommand(target=ACommand.Type.DOOR_LOCK, value='-1')
        ]
        while True:

            self.control_actuators(commands=pre_commands)
            readings = self.read_sensors()
            for reading in readings:
                print(reading)
            sleep(2)

            self.control_actuators(commands=post_commands)
            readings = self.read_sensors()
            for reading in readings:
                print(reading)
            sleep(2)


def main():
    controller = SecurityController(actuators=[
        BuzzerController(
            gpio=None,
            command_type=ACommand.Type.BUZZER_ON_OFF,
            model='ReTerminal Buzzer',
            reading_type=AReading.Type.BUZZER,
            initial_state='off'),
        DoorLockController(
            model='180 degree servo',
            gpio=12,
            command_type=ACommand.Type.DOOR_LOCK,
            reading_type=AReading.Type.DOOR_LOCK,
            initial_state='-1')
    ],
        sensors=[
            DoorSensor(
                gpio=5,
                model='Magnetic door sensor reed switch',
                type=AReading.Type.DOOR),
            LoudnessSensor(
                gpio=0,
                model='Grove - Loudness Sensor',
                type=AReading.Type.LOUDNESS),
            LuminositySensor(
                gpio=None,
                model='Built-in Luminosity Sensor',
                type=AReading.Type.LUMINOSITY),
            MotionSensor(
                gpio=22,
                model='Adjustable PIR Motion Sensor',
                type=AReading.Type.MOTION),
            BuzzerController(
                gpio=None,
                command_type=ACommand.Type.BUZZER_ON_OFF,
                model='ReTerminal Buzzer',
                reading_type=AReading.Type.BUZZER,
                initial_state='off'),
            DoorLockController(
                model='180 degree servo',
                gpio=12,
                command_type=ACommand.Type.DOOR_LOCK,
                reading_type=AReading.Type.DOOR_LOCK,
                initial_state='-1')
    ])
    controller.loop()


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("Exiting...")