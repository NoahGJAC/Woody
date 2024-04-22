#!/usr/bin/env python3


from gpiozero import Servo
from time import sleep
import math
from gpiozero.pins.pigpio import PiGPIOFactory
from python.actuators.actuators import IActuator, ACommand


DOOR_LOCK_PIN: int = 12


class DoorLockController(IActuator):

    def __init__(
            self,
            gpio: int,
            type: ACommand.Type,
            initial_state: str) -> None:
        """Initializes the door lock controller.

        Args:
            gpio (int): The pin of the door lock controller.
            type (ACommand.Type): The type of commands the door lock responds to.
            initial_state (str): The starting state of the door lock. Should be a string representation of a float between -1.0 and 1.0.
        Except:
            IOError: When pigiod doesnt have a daemon running ($ sudo pigpiod). Needed to remove servo jitters.
        """
        try:
            self._servo: Servo = Servo(
                gpio,
                min_pulse_width=0.5 / 1000,
                max_pulse_width=2.5 / 1000,
                pin_factory=PiGPIOFactory())
        except IOError as e:
            print(f'{e}\nDid you forget $ sudo pigpiod')
            exit(1)

        self.type: ACommand.Type = type
        self._current_state: str = initial_state

        # Set initial state
        self._servo.value = float(initial_state) if self.validate_command(
            ACommand(target=self.type, value=initial_state)) else -1

    def validate_command(self, command: ACommand) -> bool:
        """Validates that a command can be used with the door lock actuator.

        Args:
            command (ACommand): The command to be validated

        Returns:
            bool: True if command is valid, false otherwise.

        Except:
            ValueError: When unable to convert command.value string into a float. Returns False.
        """
        try:
            command_value_float = float(command.value)
        except ValueError:
            return False

        return command.target_type == self.type and type(
            command.value) is str and -1 <= command_value_float <= 1

    def control_actuator(self, value: str) -> bool:
        """Controls the door lock's state.

        Args:
            value (str): The new state of the door lock, string representation of a float between '-1.0' and '1.0'.

        Returns:
            bool: True if door lock state changes, False otherwise.

        Except:
            ValueError: When unable to convert value to float. Returns False
        """
        try:
            if (float(value) == float(self._current_state)):
                self._servo.value = float(value)
                return False
            else:
                self._servo.value = float(value)
                self._current_state = value
                return True
        except ValueError:
            return False

    def __del__(self):
        """Destructor:
                Cleans up servo.
        """
        self._servo.close()


def main():
    door_lock_controller = DoorLockController(
        gpio=DOOR_LOCK_PIN,
        type=ACommand.Type.DOOR_LOCK,
        initial_state='-1')
    try:
        while True:
            door_lock_controller.control_actuator('1')
            print('1')
            sleep(1)
            door_lock_controller.control_actuator('-1')
            print('-1')
            sleep(1)
            door_lock_controller.control_actuator('0')
            print('0')
            sleep(1)
            door_lock_controller.control_actuator('0.5')
            print('0.5')
            sleep(1)
            door_lock_controller.control_actuator('-0.5')
            print('-0.5')
            sleep(1)
    except KeyboardInterrupt:
        print('exiting..')


if __name__ == '__main__':
    main()
