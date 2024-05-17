from time import sleep
from abc import ABC, abstractmethod
from python.actuators.actuators import IActuator, ACommand
from python.sensors.sensors import ISensor, AReading



class IDeviceController(ABC):
    """Interface for all device controllers.
    """

    # Class properties that must be defined in implementation classes.
    _sensors: list[ISensor]
    _actuators: list[IActuator]

    @abstractmethod
    def __init__(self,
                 sensors: list[ISensor],
                 actuators: list[IActuator]) -> None:
        """
            Initialize the actuators and sensors
        """
        self._sensors: list[ISensor] = sensors
        self._actuators: list[IActuator] = actuators

    @abstractmethod
    def control_actuators(self, commands: list[ACommand]) -> None:
        """Controls actuators according to a list of commands. Each command is applied to it's respective actuator.

        :param list[ACommand] commands: List of commands to be dispatched to corresponding actuators.
        """

    @abstractmethod
    def read_sensors(self) -> list[AReading]:
        """Reads data from all initialized sensors.

        :return list[AReading]: a list containing all readings collected from sensors.
        """
