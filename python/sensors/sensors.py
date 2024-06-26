from abc import ABC, abstractmethod
from enum import Enum
import json


class AReading(ABC):
    """Abstract class for sensor readings. Can be instantiated directly or inherited.
    Also defines all possible types of readings and reading units using enums.
    """

    class Type(str, Enum):
        """Enum defining all possible types of readings that sensors might make.
        """
        # Add new reading types here.
        WATER_LEVEL = "water level"
        SOIL_MOISTURE = "soil moisture"
        TEMPERATURE = "temperature"
        HUMIDITY = "humidity"
        LUMINOSITY = "luminosity"
        BUZZER = "buzzer state"
        DOOR = "door state"
        DOOR_LOCK = "door lock state"
        FAN = "fan state"
        LED = "LED state"
        MOTION = "motion"
        VIBRATION = "vibration"
        LOUDNESS = "loudness"
        LATITUDE = "latitude"
        LONGITUDE = "longitude"
        ALTITUDE = "altitude"
        GPS = "GPS"
        PITCH = "pitch"
        ROLL = "roll"
        
        
    class Unit(str, Enum):
        """Enum defining all possible units for sensor measuremens."""

        # Add new reading units here.
        # TODO: ° does not work for json exporting
        MILLIMITERS = "mm"
        CELCIUS = "°C"
        FAHRENHEIT = "°F"
        HUMIDITY = "% HR"
        UNITLESS = ""
        LUX = "lx"
        LOUDNESS = "% loudness strength"
        DEGREE = "°"
        METERS = "m"
        PERCENTAGE = "%"
        FAILURE = "failure"

        
    # Class properties that must be defined in implementation classes
    reading_type: Type
    reading_unit: Unit
    value: float | str | bool

    def __init__(
            self,
            type: Type,
            unit: Unit,
            value: float | str | bool) -> None:
        self.reading_type = type
        self.reading_unit = unit
        self.value = value

    def __repr__(self) -> str:
        """String representation of a reading object"""
        return f"AReading(reading_type={self.reading_type}, value={self.value}, unit={self.reading_unit})"

    def __str__(self) -> str:
        return f"{self.reading_type.value}: {self.value} {self.reading_unit.value}"

    def export_json(self) -> str:
        """Exports a reading as a json encoded string

        :return str: json string representation of the reading
        """

        return json.dumps(
            {"value": self.value, "unit": self.reading_unit.value})


class ISensor(ABC):
    """Interface for all sensors."""

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

        :return list[AReading]: List of readings measured by the sensor. Most sensors return a list with a single item.
        """
        pass


class MockSensor(ISensor):
    """A class to represent a mock sensor that implements ISensor."""

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
        print("Mock sensor reading...")
        return [
            AReading(AReading.Type.TEMPERATURE, AReading.Unit.CELCIUS, -200.0),
            AReading(AReading.Type.HUMIDITY, AReading.Unit.HUMIDITY, 101.0),
            AReading(AReading.Type.LUMINOSITY, AReading.Unit.LUX, 3.846e26),
            AReading(AReading.Type.BUZZER, AReading.Unit.UNITLESS, "OFF"),
        ]
