using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Enums
{
    /// <summary>
    /// Represent each subsytem in the application
    /// </summary>
    public enum SubSystemType
    {
        /// <summary>
        /// The security subsytem
        /// </summary>
        SECURITY,

        /// <summary>
        /// The geolocation subsytem
        /// </summary>
        GEOLOCATION,

        /// <summary>
        /// The plant subsytem
        /// </summary>
        PLANT
    }

    /// <summary>
    /// get the description, aka the string, of each subsystem
    /// </summary>
    public static class SubSystemTypeExtensions
    {
        public static string ToDescription(this SubSystemType type)
        {
            switch (type)
            {
                case SubSystemType.SECURITY:
                    return "security";
                case SubSystemType.GEOLOCATION:
                    return "geolocation";
                case SubSystemType.PLANT:
                    return "plant";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        /// <summary>
        /// from the description of the sybstem get the enum.
        /// </summary>
        public static SubSystemType FromDescription(string description)
        {
            switch (description.ToLower())
            {
                case "security":
                    return SubSystemType.SECURITY;
                case "geolocation":
                    return SubSystemType.GEOLOCATION;
                case "plant":
                    return SubSystemType.PLANT;
                default:
                    throw new ArgumentException("Invalid subsystem type", nameof(description));
            }
        }
    }
}
