#!/usr/bin/env python


from time import sleep
from actuators.plant.fan import FanController
from actuators.plant.light import LightController
from python.actuators.actuators import ACommand, IActuator
from python.controllers.device_controllers import IDeviceController
from python.sensors.sensors import AReading, ISensor
from sensors.plant.soil_moisture import SoilMoistureSensor
from sensors.plant.water_level import WaterLevelSensor
from sensors.plant.temperature_humidity import TemperatureHumiditySensor
import colorama


class PlantController(IDeviceController):
    """A class that represents a plant subsystem device controller."""'

    def __init__(self) -> None:
        """Initialize a PlantController
        """
        super().__init__()

    def _initialize_actuators(self) -> list[IActuator]:
        return [LightController(gpio=5, type=ACommand.Type.LIGHT_ON_OFF),
                FanController(gpio=16, type=ACommand.Type.FAN_ON_OFF)]

    def _initialize_sensors(self) -> list[ISensor]:
        return [SoilMoistureSensor(gpio=18),
                WaterLevelSensor(gpio=22),
                TemperatureHumiditySensor(gpio=24)]

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
        """Reads data from all initialized sensors.

        :return list[AReading]: a list containing all readings collected from the sensors.
        """
        readings: list[AReading] = [
            reading for sensor in self._sensors for reading in sensor.read_sensor()]
        return readings

    def loop(self):
        """Loops through controlling actuators and reading sensors. Intended for testing.
        """
        pre_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.LIGHT_ON_OFF, value='on'),
            ACommand(target=ACommand.Type.FAN_ON_OFF, value='on')
        ]
        post_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.LIGHT_ON_OFF, value='off'),
            ACommand(target=ACommand.Type.FAN_ON_OFF, value='off')
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
    controller = PlantController()
    controller.loop()


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("Exiting...")
