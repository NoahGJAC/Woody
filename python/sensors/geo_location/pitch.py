#!/usr/bin/env python

import seeed_python_reterminal.core as rt
import seeed_python_reterminal.acceleration as rt_accel
import math
from python.sensors.sensors import ISensor, AReading


class PitchSensor(ISensor):
    def __init__(self, gpio: int | None, model: str, type: AReading.Type):
        """
        Initialise the Pitch Sensor
        Args:
            gpio (int | None): The gpio of the pitch, accelerometer is internal, no gpio needed.
            model (str): specific model of Pitch hardware. i.e: Built-in Accelerometer
            type (AReading.Type): The type of reading is Pitch
        """
        self.reading_type = type
        self._sensor_model = model
        self.device = rt.get_acceleration_device()

    def read_sensor(self) -> list[AReading]:
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

                # only calculate the pitch if there is an x,y and z value
                if last_x is not None and last_y is not None and last_z is not None:
                    pitch = self._calculate_pitch(last_x, last_y, last_z)
                    readings.append(
                        AReading(
                            AReading.Type.PITCH,
                            AReading.Unit.DEGREE,
                            pitch))
                    break
        return readings

    def _calculate_pitch(self, x, y, z) -> float:
        """
        Calculate the pitch angle in degrees
        """
        return math.degrees(math.atan2(x, math.sqrt(y**2 + z**2)))


def main():
    pitch = PitchSensor(None, 'Built-in Accelerometer', AReading.Type.PITCH)

    while True:
        readings = pitch.read_sensor()

        for reading in readings:
            print(f'Pitch: {reading.value:.2f}{reading.reading_unit.value}')


if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("Exiting...")
