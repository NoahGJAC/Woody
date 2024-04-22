#!/usr/bin/env python

import seeed_python_reterminal.core as rt
import seeed_python_reterminal.acceleration as rt_accel
import math
from python.sensors.sensors import ISensor, AReading


class RollSensor(ISensor):
    def __init__(self, gpio: int | None, model: str, type: AReading.Type):
        """
        Initialise the Roll Sensor
        Args:
            gpio (int | None): The gpio of the roll, accelerometer is internal, no gpio needed.
            model (str): specific model of roll hardware. i.e: Built-in Accelerometer
            type (AReading.Type): The type of reading is roll
        """
        self.reading_type = type
        self._sensor_model = model
        self.device = rt.get_acceleration_device()

    def read_sensor(self) -> list[AReading]:
        """Takes a reading form the Roll sensor

        :return list[AReading]: List of readinds of type roll and unit of degrees
        """
        last_x = last_y = last_z = None
        readings: list[AReading] = []
        for event in self.device.read_loop():
            accelEvent = rt_accel.AccelerationEvent(event)
            if accelEvent.name is not None:
                if accelEvent.name == rt_accel.AccelerationName.X:
                    last_x = accelEvent.value
                elif accelEvent.name == rt_accel.AccelerationName.Y:
                    last_y = accelEvent.value
                elif accelEvent.name == rt_accel.AccelerationName.Z:
                    last_z = accelEvent.value

                # only calculate the Roll if there is an x,y and z value
                if last_x is not None and last_y is not None and last_z is not None:
                    Roll = self._calculate_roll(last_x, last_y, last_z)
                    readings.append(
                        AReading(
                            AReading.Type.ROLL,
                            AReading.Unit.DEGREE,
                            Roll))
                    break
        return readings

    def _calculate_roll(self, x, y, z) -> float:
        """
        Calculate the roll angle in degrees
        """
        return math.degrees(math.atan2(y, math.sqrt(x**2 + z**2)))


def main():
    roll = RollSensor(None, 'Built-in Accelerometer', AReading.Type.ROLL)

    while True:
        readings = roll.read_sensor()

        for reading in readings:
            print(f'Roll: {reading.value:.2f}{reading.reading_unit.value}')


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("Exiting...")
