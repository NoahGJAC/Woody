#!/usr/bin/env python3

# Imports
from python.actuators.actuators import IActuator, ACommand
from enum import Enum
from grove.grove_ws2813_rgb_led_strip import GroveWS2813RgbStrip
from rpi_ws281x import Color
from time import sleep


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
        color: Color = Color(255, 255, 255)
    ) -> None:
        """
        Initializes the RGB led stick.

        Args:
            gpio int: The gpio of the RGB led stick
            type (ACommand.Type): The type of command the RGB led stick accepts.
            count (int, optional): Number of strip LEDS. Defaults to one.
            brightness (int, optional): Brightness level (0 to 255) of the "ON" state. Defaults to 255.
            initial_state (str, optional): The initial state of the RGB led stick ('on' or 'off'). Defaults to 'off'.
            color (Color, optional): Color of the light. Defaults to white.

        Raises:
           ValueError if gpio, count, brightness or initial state are invalid inputs.
        """
        self._validate_integer(gpio, "Light GPIO")
        self._validate_integer(count, "Light strip LEDS count")
        self._validate_integer(value=brightness, min_value=0, name="Light brightness", max_value=255)

        self.gpio = gpio
        self.count = count
        self.brightness = brightness
        self.color = color
        self.type = type
        self._current_state = initial_state == LightState.ON
        self.rgb_stick = GroveWS2813RgbStrip(self.gpio, self.count, self.brightness)

        self.control_actuator(initial_state.value)

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
        previous_state = self._current_state

        if value.lower() not in (LightState.ON.value, LightState.OFF.value):
            raise ValueError(f"Invalid argument {value}, must be 'on' or 'off'")

        if (value is LightState.ON.value):
            theaterChase(self.rgb_stick, self.color)
            self._current_state = value is LightState.ON
        elif (value is LightState.OFF.value):
            colorWipe(self.rgb_stick, Color(0,0,0), 10)
            self._current_state = value is LightState.OFF
        
        return previous_state != self._current_state

    def clean_up(self) -> None:
        # Sets the RGB led stick's state to False, meant for cleaning up.
        colorWipe(self.rgb_stick, Color(0,0,0), 10)
        
    def read_state(self) -> bool:
        """
        Returns true if the RGB led stick's state is truthy, false otherwise.

        Returns:
            bool: The state of the RGB led stick.
        """
        return self._current_state


if __name__ == "__main__":
    light_controller = LightController(gpio=12, type=ACommand(ACommand.Type.LIGHT_ON_OFF, LightState.OFF))

    while True:
        print(f"Light is {'on' if light_controller.read_state() else 'off'}")
        sleep(1)

        light_controller.control_actuator("on")

        print(f"Light is {'on' if light_controller.read_state() else 'off'}")
        sleep(1)

        light_controller.control_actuator("off")

        print(f"Light is {'on' if light_controller.read_state() else 'off'}")
        sleep(1)

    light_controller.clean_up()