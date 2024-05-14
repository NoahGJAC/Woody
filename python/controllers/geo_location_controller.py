#!/usr/bin/env python


from time import sleep
import pynmea2
from python.actuators.actuators import ACommand, IActuator
from python.sensors.sensors import AReading, ISensor
from python.controllers.device_controllers import IDeviceController
from python.sensors.geo_location.gps import GPSSensor
from python.sensors.geo_location.pitch import PitchSensor
from python.sensors.geo_location.roll import RollSensor
from python.actuators.geo_location.buzzer import BuzzerController
from python.sensors.geo_location.vibration import VibrationSensor
import colorama


class GeoLocationController(IDeviceController):
    """A class that represents a geolocation subsystem device controller."""

    def __init__(self, sensors: list[ISensor], actuators: list[IActuator]) -> None:
        """Initializes a GeoLocationController

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
                    colorama.Fore.RED
                    + f"No actuator found for command: {command}"
                    + colorama.Fore.RESET
                )
                continue

            if not actuator.validate_command(command=command):
                print(
                    colorama.Fore.RED
                    + f"Invalid command for actuator: {actuator.type}\n\tCommand: {command}"
                    + colorama.Fore.RESET
                )
                continue

            actuator.control_actuator(value=command.value)
            print(f"Executed command: {command}")

    def _get_actuator_dict(self) -> dict[ACommand.Type, IActuator]:
        """Returns a dictionary with the actuators as values to their command type key.

        Returns:
            dict[ACommand.Type, IActuator]: The dictionary with the actuators as values.
        """
        return {actuator.type: actuator for actuator in self._actuators}

    def read_sensors(self) -> list[AReading]:
        """Reads data from all initialized sensors.

        :return list[AReading]: a list containing all readings collected from the sensors.
        """
        readings: list[AReading] = [
            reading for sensor in self._sensors for reading in sensor.read_sensor()
        ]
        return readings

    def loop(self):
        """Loops through controlling actuators and reading sensors. Intended for testing."""
        pre_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.BUZZER_ON_OFF, value="on")
        ]
        post_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.BUZZER_ON_OFF, value="off")
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
    controller = GeoLocationController(
        sensors=[
            RollSensor(
                gpio=None, model="Built-in Accelerometer", type=AReading.Type.ROLL
            ),
            PitchSensor(
                gpio=None, model="Built-in Accelerometer", type=AReading.Type.PITCH
            ),
            GPSSensor(gpio=None, model="GPS (Air 530)", type=AReading.Type.GPS),
            BuzzerController(
                gpio=None,
                command_type=ACommand.Type.BUZZER_ON_OFF,
                model="ReTerminal Buzzer",
                reading_type=AReading.Type.BUZZER,
                initial_state="off",
            ),
            VibrationSensor(
                gpio=None, model="Built-in Accelerometer", type=AReading.Type.VIBRATION
            ),
        ],
        actuators=[
            BuzzerController(
                gpio=None,
                command_type=ACommand.Type.BUZZER_ON_OFF,
                model="ReTerminal Buzzer",
                reading_type=AReading.Type.BUZZER,
                initial_state="off",
            )
        ],
    )
    controller.loop()


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("Exiting...")
    except pynmea2.ParseError as e:
        f"{e} \nCould not parse the information? you need to plug the GPS on UART port and wait 5 seconds"
        pass
