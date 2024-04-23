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
    def __init__(self) -> None:
        """
            Initialize the actuators and sensors
        """
        self._sensors: list[ISensor] = self._initialize_sensors()
        self._actuators: list[IActuator] = self._initialize_actuators()

    @abstractmethod
    def _initialize_sensors(self) -> list[ISensor]:
        """Initializes all sensors and returns them as a list. Intended to be used in class constructor.

        Returns:
            list[ISensor]: List of initialized sensors.
        """

    @abstractmethod
    def _initialize_actuators(self) -> list[IActuator]:
        """Initializes all actuators and returns them as a list. Intended to be used in class constructor.

        Returns:
            list[IActuator]: List of initialized actuators.
        """

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
