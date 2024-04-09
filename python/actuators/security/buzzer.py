#!/usr/bin/env python3

import seeed_python_reterminal.core as rt

import time

# buzzer permisions require sudo
try:
    print("BUZZER ON")
    # change state
    rt.buzzer = True

    # read state
    print(rt.buzzer)
    time.sleep(1)

    print("BUZZER OFF")
    rt.buzzer = False
    print(rt.buzzer)
except PermissionError as e:
    print(f"{e}\nDid you forget permissions?\n$ sudo chmod a+rw /sys/class/leds/usr_buzzer/brightness")