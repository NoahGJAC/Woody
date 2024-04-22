#!/usr/bin/env python3

# Imports
from ..actuators import IActuator, ACommand
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
        type: ACommand.Type,
        initial_state: FanState = FanState.OFF,
    ) -> None:
        """
        Initializes the fan.

        Args:
            gpio int: The gpio of the fan
            type (ACommand.Type): The type of command the fan accepts.
            initial_state (str, optional): The initial state of the fan ('on' or 'off'). Defaults to 'off'.
        """
        self._validate_integer(gpio, " GPIO")
        self.gpio = gpio
        self.type = type
        self.fan = OutputDevice(pin=gpio)
        self._current_state = initial_state.value

        self.control_actuator(self._current_state)

    def _validate_integer(
        self, value: int, name: str, min_value: int = 0, max_value: int = None
    ) -> None:
        if max_value is not None:
            if not min_value <= value <= max_value:
                raise ValueError(
                    f"{name} value must be between {min_value} and {max_value}"
                )
        elif value < min_value:
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

        self.fan.value = 1 if value is FanState.ON.value else 0

        self._current_state = value

        return previous_state != self._current_state

    def clean_up(self) -> None:
        # Sets the fan's state to False, meant for cleaning up.
        self.fan.value = False

    def read_state(self) -> bool:
        """
        Returns true if the fan's state is truthy, false otherwise.

        Returns:
            bool: The state of the fan.
        """
        return self.fan.value

if __name__ == "__main__":
    fan_controller = FanController(gpio=16, type=ACommand.Type.FAN_ON_OFF)

    while True:
        print(f"Fan is {'on' if fan_controller.read_state() else 'off'}")
        sleep(2)

        fan_controller.control_actuator("on")

        print(f"Fan is {'on' if fan_controller.read_state() else 'off'}")
        sleep(2)

        fan_controller.control_actuator("off")

        print(f"Fan is {'on' if fan_controller.read_state() else 'off'}")
        sleep(2)

    fan_controller.clean_up()
