#!/usr/bin/env python3
import seeed_python_reterminal.core as rt
import seeed_python_reterminal.acceleration as rt_accel
from python.sensors.sensors import ISensor,AReading
import math

class vibration(ISensor):
    def __init__(self, gpio: int | None, model: str , type: AReading.Type):
        """Initializes the vibration sensor.

        Args:
            gpio (int | None): The gpio of the vibration, accelerometer is internal, no gpio needed.
            type (AReading.Type): The type of reading the vibration accepts.
        """
        self.reading_type = AReading.Type.VIBRATION
        self._sensor_model = model
        self.device = rt.get_acceleration_device()
        self.first_x = self.first_y = self.first_z = None
        # Initialize variables to store the last readings
        self.last_x = self.last_y = self.last_z = None
        self.variant = 19
    
    def read_sensor(self) -> list[AReading]:
        """read all of the vibration level: aka if a vibration accured

        Return:
            returns the list of all the different readings
        """
        readings : list[AReading] = []
        for event in self.device.read_loop():
            accelEvent = rt_accel.AccelerationEvent(event)
            if accelEvent.name is not None:
                if accelEvent.name == rt_accel.AccelerationName.X:
                    self.last_x = accelEvent.value
                elif accelEvent.name == rt_accel.AccelerationName.Y:
                    self.last_y = accelEvent.value
                elif accelEvent.name == rt_accel.AccelerationName.Z:
                    self.last_z = accelEvent.value
                
                # Calculate pitch and roll angles only if all readings are available
                if self.last_x is not None and self.last_y is not None and self.last_z is not None and self.first_x is None and self.first_y is None and self.first_z is None:
                    self.first_x = self.last_x
                    self.first_y = self.last_y
                    self.first_z = self.last_z
                
                if self.first_x is not None and self.first_y is not None and self.first_z is not None:
                    if self._range(self.last_x,self.first_x):
                        self.first_x = self.last_x
                        readings.append(AReading(AReading.Type.VIBRATION,AReading.Unit.UNITLESS,True))
                    elif self._range(self.last_y, self.first_y):
                        self.first_y = self.last_y
                        readings.append(AReading(AReading.Type.VIBRATION,AReading.Unit.UNITLESS,True))
                    elif self._range(self.last_z,self.first_z):
                        self.first_z = self.last_z
                        readings.append(AReading(AReading.Type.VIBRATION,AReading.Unit.UNITLESS,True))
                    else:
                        readings.append(AReading(AReading.Type.VIBRATION,AReading.Unit.UNITLESS,False))
                    break

        return readings
    
    def _range(self,last_value:float,current_value:float) -> bool:
        """
        Return:
            return a boolean if the current is not between the buffers then there is vibration

        """
        return last_value-self.variant>= current_value or current_value >= last_value+self.variant


        
        
def main():
    vib = vibration(None,'Built-in Accelerometer',AReading.Type.VIBRATION)
    try:
        while True:
            readings = vib.read_sensor()
            for reading in readings:
                    print(f'{reading.reading_type.value}: {reading.value} {reading.reading_unit.value}')

    except KeyboardInterrupt:
        print("Exiting...")

    

if __name__ == "__main__":
    main()
        
            
