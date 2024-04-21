#!/usr/bin/env python3
import seeed_python_reterminal.core as rt
import seeed_python_reterminal.acceleration as rt_accel
from python.sensors.sensors import ISensor,AReading
import math

# class vibration(ISensor):
#     def __init__(self, gpio: int | None, model: str | None, type: AReading.Type):
#         """Initializes the vibration sensor.

#         Args:
#             gpio (int | None): The gpio of the vibration, vibration is internal, no gpio needed.
#             type (AReading.Type): The type of reading the vibration accepts.
#         """
#         type = AReading.Type.VIBRATION
#         self.device = rt.get_acceleration_device()
    
#     def read_sensor(self) -> list[AReading]:
#         """read all of the vibration levels aka: Velocity, Acceleration and Displacement.

#         Return:
#             returns the list of all the different readings
#         """
#         pass
        
        
    
    

# if __name__ == "__main__":

#     vib = vibration(None,None,AReading.Type.VIBRATION)
#     try:
#         while True:
#             readings = vib.read_sensor()
#             for reading in readings:
#                     print(f'{reading.reading_type.value}: {reading.value}{reading.reading_unit.value}')

#     except KeyboardInterrupt:
#         vib.clean_up()
#         print("Exiting...")
        













# 


device = rt.get_acceleration_device()

first_x = first_y = first_z = None
# Initialize variables to store the last readings
last_x = last_y = last_z = None

# Continuously read and calculate pitch and roll angles
while True:
    for event in device.read_loop():
        accelEvent = rt_accel.AccelerationEvent(event)
        if accelEvent.name is not None:
            if accelEvent.name == rt_accel.AccelerationName.X:
                last_x = accelEvent.value
            elif accelEvent.name == rt_accel.AccelerationName.Y:
                last_y = accelEvent.value
            elif accelEvent.name == rt_accel.AccelerationName.Z:
                last_z = accelEvent.value
            
            # Calculate pitch and roll angles only if all readings are available
            if last_x is not None and last_y is not None and last_z is not None:
                # Calculate pitch angle (in radians)
                pitch = math.atan2(last_x, math.sqrt(last_y**2 + last_z**2))
                # Calculate roll angle (in radians)
                roll = math.atan2(last_y, math.sqrt(last_x**2 + last_z**2))
                
                # Convert angles to degrees
                pitch_deg = math.degrees(pitch)
                roll_deg = math.degrees(roll)
                
                print(f"Pitch: {pitch_deg}°, Roll: {roll_deg}°")