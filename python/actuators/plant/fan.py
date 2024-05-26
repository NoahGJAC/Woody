#!/usr/bin/env python3

# Imports
from ..actuators import IActuator, ACommand
from python.sensors.sensors import ISensor, AReading
from enum import Enum
from gpiozero import OutputDevice
from time import sleep


class FanState(Enum):
    ON = "on"
    OFF = "off"


class FanController(IActuator):
    # A class to control a fan through the reterminal.

    def __init__(
        self,
        gpio: int,
        command_type: ACommand.Type,
        reading_type: AReading.Type,
        initial_state: FanState = FanState.OFF,
    ) -> None:
        """
        Initializes the fan.

        Args:
            gpio int: The gpio of the fan
            command_type (ACommand.Type): The type of commands the fan responds to.
            reading_type (AReading.Type): The type of reading the fan produces.
            initial_state (str, optional): The initial state of the fan ('on' or 'off'). Defaults to 'off'.
        """
        self._validate_integer(gpio, " GPIO")
        self.gpio = gpio
        self.type = command_type
        self.reading_type = reading_type
        self.fan = OutputDevice(pin=gpio)
        self._current_state = initial_state.value

        self.control_actuator(self._current_state)

    def _validate_integer(self, value: int, name: str, min_value: int = 0) -> None:
        if value < min_value:
            raise ValueError(f"{name} value must be positive")

    def validate_command(self, command: ACommand) -> bool:
        """Validates a command that can be used with the fan.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid, False otherwise.
        """
        return (
            command.target_type == self.type
            and isinstance(command.value, str)
            and (command.value.lower() in (FanState.ON.value, FanState.OFF.value))
        )

    def control_actuator(self, value: str) -> bool:
        """Controls the fan's state.

        Args:
            value (str): The new state of the fan, 'on' or 'off'.

        Returns:
            bool: True if the fan's state changes, False otherwise.
        """
        previous_state = self._current_state

        if value.lower() not in (FanState.ON.value, FanState.OFF.value):
            raise ValueError(f"Invalid argument {value}, must be 'on' or 'off'")

        self.fan.value = 1 if value.lower() == FanState.ON.value else 0

        self._current_state = value

        return previous_state != self._current_state

    def __del__(self) -> None:
        # Sets the fan's state to False, meant for cleaning up.
        self.fan.value = False

    def read_sensor(self) -> list[AReading]:
        """Returns an AReading list from the sensor.

        Returns:
            list[AReading]: The list of readings measured by the fan.
        """
        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.UNITLESS,
                value=self.fan.value,
            )
        ]


def print_readings(readings: list[AReading]) -> None:
    for reading in readings:
        print(reading)


if __name__ == "__main__":
    fan_controller = FanController(
        gpio=16, command_type=ACommand.Type.FAN_ON_OFF, reading_type=AReading.Type.FAN
    )

    while True:
        print_readings(fan_controller.read_sensor())
        sleep(1)

        fan_controller.control_actuator("on")

        print_readings(fan_controller.read_sensor())
        sleep(1)

        fan_controller.control_actuator("off")

        print_readings(fan_controller.read_sensor())
        sleep(1)
