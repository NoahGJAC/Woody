using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Enums
{
    /// <summary>
    /// Represents the types of readings that can be recorded.
    /// </summary>
    public enum ReadingType
    {
        /// <summary>
        /// Represents a reading type for a buzzer.
        /// </summary>
        BUZZER,

        /// <summary>
        /// Represents a reading type for pitch.
        /// </summary>
        PITCH,

        /// <summary>
        /// Represents a reading type for roll.
        /// </summary>
        ROLL,

        /// <summary>
        /// Represents a reading type for humidity.
        /// </summary>
        HUMIDITY,

        /// <summary>
        /// Represents a reading type for temperature.
        /// </summary>
        TEMPERATURE,

        /// <summary>
        /// Represents a reading type for luminosity.
        /// </summary>
        LUMINOSITY,

        /// <summary>
        /// Represents a reading type for loudness.
        /// </summary>
        LOUDNESS,

        /// <summary>
        /// Represents a reading type for door state.
        /// </summary>
        DOOR,

        /// <summary>
        /// Represents a reading type for door lock state.
        /// </summary>
        DOOR_LOCK,

        /// <summary>
        /// Represents a reading type for motion detection.
        /// </summary>
        MOTION,

        /// <summary>
        /// Represents a reading type for latitude.
        /// </summary>
        LATITUDE,

        /// <summary>
        /// Represents a reading type for longitude.
        /// </summary>
        LONGITUDE,

        /// <summary>
        /// Represents a reading type for altitude.
        /// </summary>
        ALTITUDE,

        /// <summary>
        /// Represents a reading type for GPS coordinates.
        /// </summary>
        GPS,

        /// <summary>
        /// Represents a reading type for vibration.
        /// </summary>
        VIBRATION,
        WATER_LEVEL,
        FAN,
        LIGHT,
        SOIL_MOISTURE
    }
}
