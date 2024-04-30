using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Represents an attribute that specifies the unit of measurement for a reading.
    /// </summary>
    public class ReadingUnitAttribute : Attribute
    {
        /// <summary>
        /// Gets the value of the unit of measurement.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingUnitAttribute"/> class with the specified unit value.
        /// </summary>
        /// <param name="value">The unit of measurement value.</param>
        public ReadingUnitAttribute(string value) 
        {
            Value = value;        
        }
    }

    /// <summary>
    /// Provides extension methods for the <see cref="ReadingUnit"/> enum to retrieve the unit value.
    /// </summary>
    public static class ReadingUnitTypesExtensions
    {
        /// <summary>
        /// Gets the unit value for the specified <see cref="ReadingUnit"/>.
        /// </summary>
        /// <param name="unit">The <see cref="ReadingUnit"/> to get the value for.</param>
        /// <returns>The unit value as a string.</returns>
        public static string GetReadingUnitValue(this ReadingUnit unit)
        {
            var field = unit.GetType().GetField(unit.ToString());
            var attribute = field.GetCustomAttribute<ReadingUnitAttribute>();
            return attribute == null ? unit.ToString() : attribute.Value;
        }
    }


    // Unit value can be used like so:
    // var metersValue = ReadingUnit.METERS.GetReadingUnitValue();
    // var percentValue = ReadingUnit.PERCENTAGE.GetReadingUnitValue();
    /// <summary>
    /// Represents the units of measurement for readings.
    /// </summary>
    public enum ReadingUnit
    {
        /// <summary>
        /// Represents degrees (°).
        /// </summary>
        [ReadingUnit("°")]
        DEGREES,

        /// <summary>
        /// Represents meters (m).
        /// </summary>
        [ReadingUnit("m")]
        METERS,

        /// <summary>
        /// Represents Celsius with humidity (°C-% HR).
        /// </summary>
        [ReadingUnit("°C-% HR")]
        CELCIUS_HUMIDITY,

        /// <summary>
        /// Represents millimeters (mm).
        /// </summary>
        [ReadingUnit("mm")]
        MILLIMETERS,

        /// <summary>
        /// Represents Fahrenheit (°F).
        /// </summary>
        [ReadingUnit("°F")]
        FARENHEIT,

        /// <summary>
        /// Represents humidity percentage (% HR).
        /// </summary>
        [ReadingUnit("% HR")]
        HUMIDITY,

        /// <summary>
        /// Represents lux (lx).
        /// </summary>
        [ReadingUnit("lx")]
        LUX,

        /// <summary>
        /// Represents loudness percentage (% Loudness).
        /// </summary>
        [ReadingUnit("% Loudness")]
        LOUDNESS,

        /// <summary>
        /// Represents Celsius (°C).
        /// </summary>
        [ReadingUnit("°C")]
        CELCIUS,

        /// <summary>
        /// Represents percentage (%).
        /// </summary>
        [ReadingUnit("%")]
        PERCENTAGE,

        /// <summary>
        /// Represents a unitless measurement.
        /// </summary>
        [ReadingUnit("")]
        UNITLESS
    }
}