from python.controllers import IDeviceController, GeoLocationController, SecurityController, PlantController

from python.connection_manager import ConnectionManager
from python.actuators.actuators import ACommand
from python.sensors.sensors import AReading
from python.enums.SubSystemType import SubSystemType


from python.sensors import GPSSensor, PitchSensor, RollSensor, VibrationSensor, DoorSensor, LoudnessSensor, LuminositySensor, MotionSensor, WaterLevelSensor, TemperatureHumiditySensor
from python.actuators import BuzzerController, DoorLockController, FanController


import asyncio


class Farm:
    """A class that acts as the main point of contact for all subsystems in the farm.
    """
    DEBUG = True
    LOOP_INTERVAL = 4  # in seconds
    _subsystems: list[IDeviceController]
    _subsystem_dict: dict[SubSystemType, IDeviceController]

    def __init__(self, subsystems: list[IDeviceController]) -> None:
        """Initializes a Farm

        Args:
            subsystems (list[IDeviceController]): The list of subsystems.
        """
        self._subsystems = subsystems
        self._connection_manager = ConnectionManager()
        self._subsystem_dict = self._get_subsystem_dict()

    async def loop(self) -> None:
        """Main loop of the Farm system. Collects new readings, send them to connection manager, collect new commands and dispatch them to subsystems."""
        await self._connection_manager.connect()
        """ Actuator commands, WIP. Switch to direct methods
        self._connection_manager.register_command_callback(
            self.command_callback
        )
        """

        while True:
            readings = []

            # try except for now, since i don't have all hardware to read from
            # and pi may throw exceptions
            try:
                for subsystem in self._subsystems:
                    for reading in subsystem.read_sensors():
                        readings.append(reading)
            except BaseException:
                pass

            if self.DEBUG:
                print(readings)

            await self._connection_manager.send_readings(readings)
            await asyncio.sleep(self.LOOP_INTERVAL)

    def command_callback(self, command: ACommand) -> None:
        """Callback for when a command is received from the connection manager.

        Args:
            command (ACommand): The command to be executed.
        """
        subsystem = self._subsystem_dict.get(command.subsystem_type)
        if subsystem is None:
            print(f"No subsystem found for command: {command}")
            return

        subsystem.control_actuators([command])

    def _get_subsystem_dict(self) -> dict[SubSystemType, IDeviceController]:
        return {subsystem.system_type: subsystem for subsystem in self._subsystems}


async def farm_main():
    subsystems = [GeoLocationController(sensors=[
        RollSensor(
            gpio=None,
            model='Built-in Accelerometer',
            type=AReading.Type.ROLL),
        PitchSensor(
            gpio=None,
            model='Built-in Accelerometer',
            type=AReading.Type.PITCH),
        GPSSensor(
            gpio=None,
            model='GPS (Air 530)',
            type=AReading.Type.GPS),
        BuzzerController(
            gpio=None,
            command_type=ACommand.Type.BUZZER_ON_OFF,
            model='ReTerminal Buzzer',
            reading_type=AReading.Type.BUZZER,
            initial_state='off'),
        VibrationSensor(
            gpio=None,
            model='Built-in Accelerometer',
            type=AReading.Type.VIBRATION
        )
    ],
        actuators=[
        BuzzerController(
            gpio=None,
            command_type=ACommand.Type.BUZZER_ON_OFF,
            model='ReTerminal Buzzer',
            reading_type=AReading.Type.BUZZER,
            initial_state='off')
    ]),
        SecurityController(actuators=[
            BuzzerController(
                gpio=None,
                command_type=ACommand.Type.BUZZER_ON_OFF,
                model='ReTerminal Buzzer',
                reading_type=AReading.Type.BUZZER,
                initial_state='off'),
            DoorLockController(
                model='180 degree servo',
                gpio=12,
                command_type=ACommand.Type.DOOR_LOCK,
                reading_type=AReading.Type.DOOR_LOCK,
                initial_state='-1')
        ],
        sensors=[
            DoorSensor(
                gpio=5,
                model='Magnetic door sensor reed switch',
                type=AReading.Type.DOOR),
            LoudnessSensor(
                gpio=0,
                model='Grove - Loudness Sensor',
                type=AReading.Type.LOUDNESS),
            LuminositySensor(
                gpio=None,
                model='Built-in Luminosity Sensor',
                type=AReading.Type.LUMINOSITY),
            MotionSensor(
                gpio=22,
                model='Adjustable PIR Motion Sensor',
                type=AReading.Type.MOTION),
            BuzzerController(
                gpio=None,
                command_type=ACommand.Type.BUZZER_ON_OFF,
                model='ReTerminal Buzzer',
                reading_type=AReading.Type.BUZZER,
                initial_state='off'),
            DoorLockController(
                model='180 degree servo',
                gpio=12,
                command_type=ACommand.Type.DOOR_LOCK,
                reading_type=AReading.Type.DOOR_LOCK,
                initial_state='-1')
        ]),
        PlantController(sensors=[
            # SoilMoistureSensor(),
            WaterLevelSensor(),
            TemperatureHumiditySensor()
        ],
        actuators=[
            FanController(gpio=16, type=ACommand.Type.FAN_ON_OFF)
        ])]

    farm = Farm(subsystems)
    await farm.loop()

if __name__ == '__main__':
    asyncio.run(farm_main())
