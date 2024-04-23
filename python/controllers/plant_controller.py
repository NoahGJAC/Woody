#!/usr/bin/env python
from time import sleep
from actuators.plant.fan import FanController
from actuators.plant.light import LightController
from python.actuators.actuators import ACommand
from python.controllers.device_controllers import IDevice_Controller
from sensors.plant.soil_moisture import SoilMoistureSensor
from sensors.plant.water_level import WaterLevelSensor
from sensors.plant.temperature_humidity import TemperatureHumiditySensor


class PlantController(IDevice_Controller):
    def __init__(self) -> None:
        self.light = LightController(gpio=5, type=ACommand.Type.LIGHT_ON_OFF)
        self.fan = FanController(gpio=16, type=ACommand.Type.FAN_ON_OFF)
        self.soil_moisture = SoilMoistureSensor(gpio=18)
        self.water_level = WaterLevelSensor(gpio=22)
        self.temperature_humidity = TemperatureHumiditySensor(gpio=24)

    def control_actuators(self) -> None:
        print(f"Fan is {'on' if self.fan.read_state() else 'off'}")
        sleep(1)
        self.fan.control_actuator('on')
        print(f"Fan is {'on' if self.fan.read_state() else 'off'}")
        sleep(1)
        self.fan.control_actuator('off')
        print(f"Fan is {'on' if self.fan.read_state() else 'off'}")
        
        print(f"Light is {'on' if self.light.read_state() else 'off'}")
        sleep(1)
        self.fan.control_actuator('on')
        print(f"Light is {'on' if self.light.read_state() else 'off'}")
        sleep(1)
        self.fan.control_actuator('off')
        print(f"Light is {'on' if self.light.read_state() else 'off'}")

    def read_sensors(self) -> None:
        self._print_readings(self.soil_moisture.read_sensor())
        self._print_readings(self.temperature_humidity.read_sensor())
        self._print_readings(self.water_level.read_sensor())

    def loop(self):
        while True:
            self.control_actuators()
            self.read_sensors()
            sleep(2)

    def _print_readings(self,readings: list[AReading]) -> None:
        for reading in readings:
            print(reading)


def main():
    controller = PlantController()
    controller.loop()

if __name__ == "__main__":
    try: 
        main()
    except KeyboardInterrupt:
        print("Exiting...")