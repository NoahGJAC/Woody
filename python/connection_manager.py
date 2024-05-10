import asyncio
from typing import Callable

from python.actuators.actuators import ACommand
from python.sensors.sensors import AReading
from python.enums.SubSystemType import SubSystemType

from azure.iot.device.aio import IoTHubDeviceClient
from azure.iot.device import Message

import dotenv
import os


class ConnectionConfig:
    """Represents all information required to successfully connect client to cloud gateway.
    """
    # Key names for configuration values inside .env file. See .env.example
    # Constants included as static class property

    def __init__(self, device_str: str) -> None:
        self._device_connection_str = device_str


class ConnectionManager:
    """Component of HVAC system responsible for communicating with cloud gateway.
    Includes registering command and reading endpoints and sending and receiving data.

    """

    def __init__(self) -> None:
        """Constructor for ConnectionManager and initializes an internal cloud gateway client.
        """
        self._connected = False
        self._config: ConnectionConfig = self._load_connection_config()
        self._client = IoTHubDeviceClient.create_from_connection_string(
            self._config._device_connection_str)

    def _load_connection_config(self) -> ConnectionConfig:
        """Loads connection credentials from .env file in the project's top-level directory.

        :return ConnectionConfig: object with configuration information loaded from .env file.
        """
        dotenv.load_dotenv(override=True)
        if not dotenv.find_dotenv() or (device_str := os.getenv(
                'IOTHUB_DEVICE_CONNECTION_STRING')) is None:
            raise EnvironmentError(
                "Unable to retrieve device connection string")
        return ConnectionConfig(device_str)

    def _on_message_received(self, message: Message) -> None:
        """Callback for handling new messages received from cloud gateway. Once the message is
        received and processed, it dispatches an ACommand to DeviceManager using _command_callback()

        :param Message message: Incoming cloud gateway message. Messages with actuator commands
        must contain a custom property of "command-type" and a json encoded string as the body.
        """
        command_type = message.custom_properties.get('command-type')
        subsytem_type = message.custom_properties.get('subsystem-type')
        print(subsystem_type + " " + command_type)
        if command_type and subsystem_type:
            command = ACommand(target=ACommand.Type(command_type), value=message.data.decode(
                message.content_encoding if message.content_encoding is not None else 'utf-8'),
                               subsystem_type=SubSystemType(subsytem_type))
            self._command_callback(command)

    async def connect(self) -> None:
        """Connects to cloud gateway using connection credentials and setups up a message handler
        """
        await self._client.connect()
        self._connected = True
        print("Connected")
        # Setup the callback handler for on_message_received of the
        # IoTHubDeviceClient instance.
        self._client.on_message_received = self._on_message_received

    def register_command_callback(
            self, command_callback: Callable[[ACommand], None]) -> None:
        """Registers an external callback function to handle newly received commands.

        :param Callable[[ACommand], None] command_callback: function to be called whenever a new command is received.
        """
        self._command_callback = command_callback

    async def send_readings(self, readings: list[AReading]) -> None:
        """Send a list of sensor readings as messages to the cloud gateway.

        :param list[AReading] readings: List of readings to be sent.
        """
        for reading in readings:
            message = Message(reading.export_json())
            message.custom_properties = {
                'reading-type-name': reading.reading_type.name,
                'reading-type': reading.reading_type.value}
            await self._client.send_message(message)


"""This script is intented to be used as a module, however, code below can be used for testing.
"""


async def main_demo():

    def dummy_callback(command: ACommand):
        print(command)

    connection_manager = ConnectionManager()
    connection_manager.register_command_callback(dummy_callback)
    await connection_manager.connect()

    TEST_SLEEP_TIME = 3

    while True:

        # ===== Create a list of fake readings =====
        fake_temperature_reading = AReading(
            AReading.Type.TEMPERATURE, AReading.Unit.CELCIUS, 12.34)
        fake_humidity_reading = AReading(
            AReading.Type.HUMIDITY, AReading.Unit.HUMIDITY, 56.78)

        # ===== Send fake readings =====
        await connection_manager.send_readings([
            fake_temperature_reading,
            fake_humidity_reading
        ])

        await asyncio.sleep(TEST_SLEEP_TIME)

if __name__ == "__main__":
    asyncio.run(main_demo())
