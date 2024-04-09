#!/usr/bin/env python3

# Imports
import seeed_python_reterminal.core as rt
import time


class BuzzerController():

    def __init__(self) -> None:
        """Initializes the BuzzerController
        """
        pass

    def close(self) -> None:
        """Sets the buzzer state to False, meant for cleaning up.
        """
        rt.buzzer = False

    def read_state(self) -> bool:
        """Returns true if the buzzer state is truthy, false otherwise.

        Returns:
            bool: The state of the buzzer.
        """
        return rt.buzzer
    

if __name__ == "__main__":
    buzzer_controller = BuzzerController()
    try:
        while True:
            print(f"Buzzer is {'on' if buzzer_controller.read_state() else 'off'}")
            time.sleep(1)
            # change state
            rt.buzzer = True
            
            print(f"Buzzer is {'on' if buzzer_controller.read_state() else 'off'}")
            time.sleep(1)

            rt.buzzer = False
            print(f"Buzzer is {'on' if buzzer_controller.read_state() else 'off'}")
            time.sleep(1)
    except PermissionError as e:
        print(f"{e}\nDid you forget permissions?\n$ sudo chmod a+rw /sys/class/leds/usr_buzzer/brightness")
    finally:
        buzzer_controller.close()