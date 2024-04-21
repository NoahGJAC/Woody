from abc import ABC, abstractmethod
from enum import Enum


class AReading(ABC):
    """Abstract class for sensor readings. Can be instantiated directly or inherited.
    Also defines all possible types of readings and reading units using enums.
    """

    class Type(str, Enum):
        """Enum defining all possible types of readings that sensors might make.
        """
        # Add new reading types here.
        TEMPERATURE = 'temperature'
        HUMIDITY = 'humidity'
        LUMINOSITY = 'luminosity'
        BUZZER = 'buzzer state'
        DOOR = 'door state'
        DOOR_LOCK = 'door lock state'
        LATITUDE = 'latitude'
        LONGITUDE ='longitude'
        ALTITUDE = 'altitude'
        GPS = 'GPS'
        DISPLACEMENT = 'displacement'
        VELOCITY = 'velocity'
        ACCELERATION = 'acceleration'
        VIBRATION = 'vibration'

    class Unit(str, Enum):
        """Enum defining all possible units for sensor measuremens.
        """
        # Add new reading units here.
        MILLIMITERS = 'mm'
        CELCIUS = '°C'
        FAHRENHEIT = '°F'
        HUMIDITY = '% HR'
        UNITLESS = 'unitless'
        LUX = 'lx'
        DEGREE = '°'
        METERS = 'm'
        METER = 'm'
        METER_PER_SECOND = 'm/s'
        METER_PER_SECOND_SQUARE = 'm/s^2'


    # Class properties that must be defined in implementation classes
    reading_type: Type
    reading_unit: Unit
    value: float | str

    def __init__(self, type: Type, unit: Unit, value: float | str | bool) -> None:
        self.reading_type = type
        self.reading_unit = unit
        self.value = value

    def __repr__(self) -> str:
        """String representation of a reading object
        """
        return f"{self.reading_type}: {self.value} {self.reading_unit}"


class ISensor(ABC):
    """Interface for all sensors.
    """

    # Class properties that must be defined in implementation classes
    _sensor_model: str
    reading_type: AReading.Type

    @abstractmethod
    def __init__(self, gpio: int | None, model: str, type: AReading.Type):
        """Constructor for Sensor  class. May be called from childclass.

        :param str model: specific model of sensor hardware. Ex. AHT20 or LTR-303ALS-01
        :param ReadingType type: Type of reading this sensor produces. Ex. 'TEMPERATURE'
        """

    @abstractmethod
    def read_sensor(self) -> list[AReading]:
        """Takes a reading form the sensor

        :return list[AReading]: List of readinds measured by the sensor. Most sensors return a list with a single item.
        """
        pass


class MockSensor(ISensor):
    """A class to represent a mock sensor that implements ISensor.
    """

    def __init__(self, gpio: int, model: str, type: AReading.Type) -> None:
        """Initialize the mock sensor and sets the properties required by the interface

        Args:
            gpio (int): The mock address of the sensor.
            model (str): The model of the mock sensor.
            type (AReading.Type): Type of reading 'produced' by the mock sensor.
        """
        self._sensor_model = model
        self.reading_type = type

    def read_sensor(self) -> list[AReading]:
        """Returns an AReading list with mock sensor data.

        Returns:
            list[AReading]: A list of the fake readings.
        """
        print('Mock sensor reading...')
        return [
            AReading(
                AReading.Type.TEMPERATURE, AReading.Unit.CELCIUS, -200.0),
            AReading(
                AReading.Type.HUMIDITY, AReading.Unit.HUMIDITY, 101.0),
            AReading(
                AReading.Type.LUMINOSITY, AReading.Unit.LUX, 3.846e26),
            AReading(
                AReading.Type.BUZZER, AReading.Unit.UNITLESS, 'OFF')
        ]
