from time import sleep


class IDevice_Controller:
    def __init__(self) -> None:
        """
            Initialize the actuators and sensors
        """

    def control_actuators(self) -> None:
        """
            Control all the actuators
        """

    def read_sensors(self) -> None:
        """
            reads all of the sensors
        """

    def loop(self):
        while True:
            self.control_actuators()
            self.read_sensors()
            sleep(2)