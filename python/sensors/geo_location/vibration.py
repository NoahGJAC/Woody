#!/usr/bin/env python3
import seeed_python_reterminal.core as rt
import seeed_python_reterminal.acceleration as rt_accel
from python.sensors.sensors import ISensor,AReading

class vibration(ISensor):
    def __init__(self, gpio: int | None, model: str | None, type: AReading.Type):
        """Initializes the vibration sensor.

        Args:
            gpio (int | None): The gpio of the vibration, vibration is internal, no gpio needed.
            type (AReading.Type): The type of reading the vibration accepts.
        """
        type = AReading.Type.VIBRATION
        self.device = rt.get_acceleration_device()
    
    def read_sensor(self) -> list[AReading]:
        """read all of the vibration levels aka: Velocity, Acceleration and Displacement.

        Return:
            returns the list of all the different readings
        """

        readings : list[AReading] = []
        for event in self.device.read_loop():
            # Create an AccelerationEvent object
            accelEvent = rt_accel.AccelerationEvent(event)
            # Check if the event name is not None
            if accelEvent.name != None:
                if accelEvent.name == rt_accel.AccelerationName.X:
                    readings.append(AReading(AReading.Type.DISPLACEMENT,AReading.Unit.METER, accelEvent.value))
                    return readings
                elif accelEvent.name == rt_accel.AccelerationName.Y:
                    readings.append(AReading(AReading.Type.VELOCITY,AReading.Unit.METER_PER_SECOND, accelEvent.value))
                    return readings
                elif accelEvent.name == rt_accel.AccelerationName.Z:
                    readings.append(AReading(AReading.Type.ACCELERATION,AReading.Unit.METER_PER_SECOND_SQUARE, accelEvent.value))
                    return readings
        return readings
        
        
    
    def clean_up(self) -> None:
        """Sets the buzzer state to False, meant for cleaning up.
        """
        self.device = False
    

if __name__ == "__main__":

    vib = vibration(None,None,AReading.Type.VIBRATION)
    try:
        while True:
            readings = vib.read_sensor()
            for reading in readings:
                    print(f'{reading.reading_type.value}: {reading.value}{reading.reading_unit.value}')

    except KeyboardInterrupt:
        vib.clean_up()
        print("Exiting...")
        













# 




# Initialize the accelerometer device
# device = rt.get_acceleration_device()

# # Continuously read accelerometer data

# # Read accelerometer data once
# for event in device.read_loop(3):
#     # Create an AccelerationEvent object
#     accelEvent = rt_accel.AccelerationEvent(event)
#     # Check if the event name is not None
#     if accelEvent.name != None:
#         # Print the name and value of the event
#         print(f"name={str(accelEvent.name)} value={accelEvent.value}")