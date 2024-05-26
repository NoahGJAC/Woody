using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Enums
{
    public enum SubSystemType
    {
        Security,
        Geolocation,
        Plant
    }
    public static class SubSystemTypeExtensions
    {
        public static string ToDescription(this SubSystemType type)
        {
            switch (type)
            {
                case SubSystemType.Security:
                    return "security";
                case SubSystemType.Geolocation:
                    return "geolocation";
                case SubSystemType.Plant:
                    return "plant";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static SubSystemType FromDescription(string description)
        {
            switch (description.ToLower())
            {
                case "security":
                    return SubSystemType.Security;
                case "geolocation":
                    return SubSystemType.Geolocation;
                case "plant":
                    return SubSystemType.Plant;
                default:
                    throw new ArgumentException("Invalid subsystem type", nameof(description));
            }
        }
    }
}
