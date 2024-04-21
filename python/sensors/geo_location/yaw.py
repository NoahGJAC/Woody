#!/usr/bin/env python

import seeed_python_reterminal.core as rt
import seeed_python_reterminal.acceleration as rt_accel
import math
from python.sensors.sensors import ISensor, AReading


class YawSensor(ISensor):
    def __init__(self, gpio: int | None, model: str, type: AReading.Type):
        """
        Initialise the yaw Sensor
        Args:
            gpio (int | None): The gpio of the yaw, accelerometer is internal, no gpio needed.
            model (str): specific model of yaw hardware. i.e: Built-in Accelerometer
            type (AReading.Type): The type of reading is yaw
        """
        self.reading_type = type
        self._sensor_model = model
        self.device = rt.get_acceleration_device()

    def read_sensor(self) -> list[AReading]:
        """Takes a reading form the yaw sensor

        :return list[AReading]: List of readinds of type yaw and unit of degrees
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

                # only calculate the yaw if there is an x,y and z value
                if last_x is not None and last_y is not None and last_z is not None:
                    yaw = self._calculate_yaw(last_x, last_y, last_z)
                    readings.append(
                        AReading(
                            AReading.Type.YAW,
                            AReading.Unit.DEGREE,
                            yaw))
                    break
        return readings

    def _calculate_yaw(self, x, y, z) -> float:
        """
        Calculate the yaw angle in degrees
        """
        return math.degrees(math.atan2(z, math.sqrt(x**2 + y**2)))


def main():
    yaw = YawSensor(None, 'Built-in Accelerometer', AReading.Type.YAW)

    while True:
        readings = yaw.read_sensor()

        for reading in readings:
            print(f'yaw: {reading.value:.2f}{reading.reading_unit.value}')


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("Exiting...")
