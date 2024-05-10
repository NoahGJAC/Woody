from controllers.device_controllers import IDeviceController

class Farm:
    DEBUG = True
    LOOP_INTERVAL = 4 # in seconds
    _subsystems: list[IDeviceController]
    
    def __init__(self, subsystems: list[IDeviceController]) -> None:
        self._subsystems = subsystems
        self._connection_manager = ConnectionManager()
        
    