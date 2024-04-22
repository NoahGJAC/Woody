#!/usr/bin/env python3

# Imports
from python.actuators.actuators import IActuator, ACommand
from enum import Enum
from rpi_ws281x import GroveWS2813RgbStrip, PixelStrip, Color


class LightState(Enum):
    ON = "on"
    OFF = "off"


class LightController(IActuator):
    # A class to control a RGB led stick through the reterminal.

    def __init__(
        self,
        gpio: int,
        type: ACommand.Type,
        count: int = 1,
        brightness: int = 255,
        initial_state: LightState = LightState.OFF,
    ) -> None:
        """
        Initializes the RGB led stick.

        Args:
            gpio int: The gpio of the RGB led stick
            type (ACommand.Type): The type of command the RGB led stick accepts.
            count (int, optional): Number of strip LEDS. Defaults to one.
            brightness (int, optional): Brightness level (0 to 255) of the "ON" state. Defaults to 255.
            initial_state (str, optional): The initial state of the RGB led stick ('on' or 'off'). Defaults to 'off'.
        """
        self._validate_integer(gpio, "Light GPIO")
        self._validate_integer(count, "Light strip LEDS count")
        self._validate_integer(brightness, 0, 255, "Light brightness")

        self.gpio = gpio
        self.count = count
        self.brightness = brightness
        self.type = type
        self._current_state = initial_state == LightState.ON
        self.rgb_stick = GroveWS2813RgbStrip(self.gpio, self.count, self.brightness)

        # TODO: set RGB led stick to initial state

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
        """Validates a command that can be used with the RGB led stick.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid, False otherwise.
        """
        return (
            command.target_type == self.type
            and isinstance(command.value, str)
            and (command.value.lower() in (LightState.ON.value, LightState.OFF.value))
        )

    def control_actuator(self, value: str) -> bool:
        """Controls the RGB led stick's state.

        Args:
            value (str): The new state of the RGB led stick, 'on' or 'off'.

        Returns:
            bool: True if RGB led stick state changes, False otherwise.
        """
        if value.lower() == LightState.OFF.value:
            if self._current_state:
                self.rgb_stick.brightness = 0
                self._current_state = False
                return True
        elif value.lower() == LightState.ON.value:
            if not self._current_state:
                self.rgb_stick.brightness = self.brightness
                self._current_state = True
                return True

        return False
