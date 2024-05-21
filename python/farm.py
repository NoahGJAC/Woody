from python.controllers import IDeviceController, GeoLocationController, SecurityController, PlantController

from python.connection_manager import ConnectionManager
from python.actuators.actuators import ACommand
from python.sensors.sensors import AReading
from python.enums.SubSystemType import SubSystemType


from python.sensors import GPSSensor, PitchSensor, RollSensor, VibrationSensor, DoorSensor, LoudnessSensor, LuminositySensor, MotionSensor, SoilMoistureSensor, WaterLevelSensor, TemperatureHumiditySensor
from python.actuators import GeoBuzzerController, SecurityBuzzerController, DoorLockController, FanController, LightController


import asyncio


class Farm:
    """A class that acts as the main point of contact for all subsystems in the farm.
    """
    DEBUG = True
    LOOP_INTERVAL = 5  # in seconds
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
        self._connection_manager.register_command_callback(
            self.command_callback
        )

        # get interval
        self.LOOP_INTERVAL = await self._connection_manager.get_desired_interval()

        # set callback for twin property updates
        self._connection_manager.register_twin_callback(self.twin_callback)

        while True:
            readings = []

            for subsystem in self._subsystems:
                for reading in subsystem.read_sensors():
                    readings.append(reading)

            if self.DEBUG:
                print("Farm Readings:\n", readings, "\n")

            await self._connection_manager.send_readings(readings)
            await asyncio.sleep(self.LOOP_INTERVAL)

    def command_callback(self, command: ACommand) -> None:
        """Callback for when a command is received from the connection manager.

        Args:
            command (ACommand): The command to be executed.
        """
        subsystem = self._subsystem_dict.get(command.target_subsystem)
        if subsystem is None:
            print(f"No subsystem found for command: {command}")
            return
        subsystem.control_actuators([command])

    async def twin_callback(self, patch: dict) -> None:
        """Callback for when desired twin properties are changed.

        Args:
            patch (dict): The desired twin properties patch.
        """
        self.LOOP_INTERVAL = await self._connection_manager.get_desired_interval()
        if self.DEBUG:
            print(f'Twin property changed: {patch}')

    def _get_subsystem_dict(self) -> dict[SubSystemType, IDeviceController]:
        return {subsystem.system_type: subsystem for subsystem in self._subsystems}


async def farm_main():
    fan = FanController(gpio=16, command_type=ACommand.Type.FAN_ON_OFF, reading_type=AReading.Type.FAN)
    led = LightController(
        gpio=12, command_type=ACommand.Type.LIGHT_ON_OFF, reading_type=AReading.Type.LED)
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
        GeoBuzzerController(
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
        GeoBuzzerController(
            gpio=None,
            command_type=ACommand.Type.BUZZER_ON_OFF,
            model='ReTerminal Buzzer',
            reading_type=AReading.Type.BUZZER,
            initial_state='off')
    ]),
        SecurityController(actuators=[
            SecurityBuzzerController(
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
            SecurityBuzzerController(
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
        ])
        ,
        PlantController(sensors=[
             SoilMoistureSensor(),
             WaterLevelSensor(),
             TemperatureHumiditySensor(type=AReading.Type.TEMPERATURE),
             TemperatureHumiditySensor(type=AReading.Type.HUMIDITY),
             fan,
             led
        ],
        actuators=[
             fan,
             led
        ])
        ]

    farm = Farm(subsystems)
    await farm.loop()

if __name__ == '__main__':
    asyncio.run(farm_main())
