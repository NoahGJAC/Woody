#!/usr/bin/env python3

# Imports
import seeed_python_reterminal.core as rt
import time
from python.actuators.actuators import IActuator, ACommand
from python.sensors.sensors import ISensor, AReading


class BuzzerController(IActuator, ISensor):
    """A class to control reterminal's built-in buzzer."""

    def __init__(
        self,
        gpio: int | None,
        model: str,
        command_type: ACommand.Type,
        reading_type: AReading.Type,
        initial_state: str = "off",
    ) -> None:
        """Initializes the buzzer controller.

        Args:
            gpio (int | None): The gpio of the buzzer, buzzer is internal, no gpio needed.
            command_type (ACommand.Type): The type of command the buzzer accepts.
            model (str): The model of the buzzer.
            reading_type (AReading.Type): The type of reading the buzzer produces.
            initial_state (str, optional): The initial state of the buzzer ('on' or 'off'). Defaults to 'off'.

        Raises:
            PermissionError: Raises when permission to write to or read '/sys/class/leds/usr_buzzer/brightness' is denied.
        """
        self._current_state = True if initial_state.lower() == "on" else False
        self._sensor_model = model
        self.type = command_type  # TODO: need to update interface IActuator
        self.reading_type = reading_type

        # set the buzzer state
        try:
            rt.buzzer = self._current_state
        except PermissionError as e:
            print(
                f"{e}\nDid you forget permissions?\n$ sudo chmod a+rw /sys/class/leds/usr_buzzer/brightness"
            )
        return

    def validate_command(self, command: ACommand) -> bool:
        """Validates a command that can be used with the buzzer.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid, False otherwise.
        """
        return (
            command.target_type == self.type
            and type(command.value) is str
            and (command.value.lower() == "on" or command.value.lower() == "off")
        )

    def control_actuator(self, value: str) -> bool:
        """Controls the buzzer's state.

        Args:
            value (str): The new state of the buzzer, 'on' or 'off'.

        Returns:
            bool: True if buzzer state changes, False otherwise.
        """
        if value.lower() == "off":
            # is buzzer on
            if rt.buzzer:
                rt.buzzer = False
                self._current_state = rt.buzzer
                return True
        elif value.lower() == "on":
            # is buzzer off
            if not rt.buzzer:
                rt.buzzer = True
                self._current_state = rt.buzzer
                return True

        return False

    def __del__(self) -> None:
        """Sets the buzzer state to False, meant for cleaning up."""
        rt.buzzer = False

    def read_sensor(self) -> list[AReading]:
        """Returns an AReading list from the sensor.

        Returns:
            list[AReading]: The list of readings measured by the buzzer.
        """
        return [
            AReading(
                type=self.reading_type,
                unit=AReading.Unit.UNITLESS,
                value=str(rt.buzzer),
            )
        ]


def print_readings(readings: list[AReading]) -> None:
    for reading in readings:
        print(reading)


if __name__ == "__main__":
    buzzer_controller = BuzzerController(
        gpio=None,
        command_type=ACommand.Type.BUZZER_ON_OFF,
        model="ReTerminal Buzzer",
        reading_type=AReading.Type.BUZZER,
        initial_state="off",
    )
    try:
        while True:
            print_readings(buzzer_controller.read_sensor())
            time.sleep(1)
            # change state
            buzzer_controller.control_actuator("on")

            print_readings(buzzer_controller.read_sensor())
            time.sleep(1)

            buzzer_controller.control_actuator("off")
            print_readings(buzzer_controller.read_sensor())
            time.sleep(1)
    except PermissionError as e:
        print(
            f"{e}\nDid you forget permissions?\n$ sudo chmod a+rw /sys/class/leds/usr_buzzer/brightness"
        )
    finally:
        buzzer_controller.clean_up()
