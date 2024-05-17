from enum import Enum


class SubSystemType(str, Enum):
    """Enum defining types of device controllers that can be targets for a command"""

    # Add types as needed
    SECURITY = "security"
    GEOLOCATION = "geolocation"
    PLANT = "plant"
