using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Enums
{
    /// <summary>
    /// Represents the types of commands that can be issued.
    /// </summary>
    public enum CommandType
    {
        FAN_ON_OFF,
        LIGHT_ON_OFF,
        LIGHT_PULSE,
        BUZZER_ON_OFF,
        DOOR_LOCK
    }
    public static class CommandTypeExtensions
    {
        public static string ToDescription(this CommandType type)
        {
            switch (type)
            {
                case CommandType.FAN_ON_OFF:
                    return "fan-on-off";
                case CommandType.LIGHT_ON_OFF:
                    return "light-on-off";
                case CommandType.LIGHT_PULSE:
                    return "light-pulse";
                case CommandType.BUZZER_ON_OFF:
                    return "buzzer-on-off";
                case CommandType.DOOR_LOCK:
                    return "door-lock";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static CommandType FromDescription(string description)
        {
            switch (description.ToLower())
            {
                case "fan-on-off":
                    return CommandType.FAN_ON_OFF;
                case "light-on-off":
                    return CommandType.LIGHT_ON_OFF;
                case "light-pulse":
                    return CommandType.LIGHT_PULSE;
                case "buzzer-on-off":
                    return CommandType.BUZZER_ON_OFF;
                case "door-lock":
                    return CommandType.DOOR_LOCK;
                default:
                    throw new ArgumentException("Invalid actuator type", nameof(description));
            }
        }
    }
}
