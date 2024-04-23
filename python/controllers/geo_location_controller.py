#!/usr/bin/env python


from time import sleep
import pynmea2
from python.actuators.actuators import ACommand, IActuator
from python.sensors.sensors import AReading, ISensor
from python.controllers.device_controllers import IDeviceController
from python.sensors.geo_location.gps import GPSSensor
from python.sensors.geo_location.pitch import PitchSensor
from python.sensors.geo_location.roll import RollSensor
from python.actuators.geo_location.buzzer import BuzzerController
import colorama


class Geo_Location_Controller(IDeviceController):
    def __init__(self) -> None:
        super().__init__()
    
    def _initialize_actuators(self) -> list[IActuator]:
        return [
            BuzzerController(
        gpio=None,
        command_type=ACommand.Type.BUZZER_ON_OFF,
        model='ReTerminal Buzzer',
        reading_type=AReading.Type.BUZZER,
        initial_state='off')
        ]
    
    def _initialize_sensors(self) -> list[ISensor]:
        return [
            RollSensor(gpio=None,model='Built-in Accelerometer',type=AReading.Type.ROLL),
            PitchSensor(gpio=None, model='Built-in Accelerometer',type=AReading.Type.PITCH),
            GPSSensor(gpio=None, model='GPS (Air 530)',type=AReading.Type.GPS),
            BuzzerController(
            gpio=None,
            command_type=ACommand.Type.BUZZER_ON_OFF,
            model='ReTerminal Buzzer',
            reading_type=AReading.Type.BUZZER,
            initial_state='off')
        ]
    


    def control_actuators(self, commands: list[ACommand]) -> None:
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
                    colorama.Fore.RESET
                )
                continue

            actuator.control_actuator(value=command.value)
            print(
                f"Executed command: {command}"
            )

    def _get_actuator_dict(self) -> dict[ACommand.Type, IActuator]:
        return {actuator.type: actuator for actuator in self._actuators}
    
    def read_sensors(self) -> list[AReading]:
        readings: list[AReading] = [reading for sensor in self._sensors for reading in sensor.read_sensor()]
        return readings

    def loop(self):
        pre_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.BUZZER_ON_OFF, value='on')
        ]
        post_commands: list[ACommand] = [
            ACommand(target=ACommand.Type.BUZZER_ON_OFF, value='off')
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
    controller = Geo_Location_Controller()
    controller.loop()


if __name__ == "__main__":
    try: 
        main()
    except KeyboardInterrupt:
        print("Exiting...")
    except pynmea2.ParseError as e:
        f"{e} \nCould not parse the information? you need to plug the GPS on UART port and wait 5 seconds"
        pass

