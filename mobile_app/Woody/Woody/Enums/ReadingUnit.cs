using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Enums
{
    public class ReadingUnitAttribute : Attribute
    {
        public string Value { get; private set; }
        public ReadingUnitAttribute(string value) 
        {
            Value = value;        
        }
    }

    public static class ReadingUnitTypesExtensions
    {
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
    public enum ReadingUnit
    {
        // TODO: ADD MORE READING UNITS
        [ReadingUnit("°")]
        DEGREES,

        [ReadingUnit("m")]
        METERS,

        [ReadingUnit("°C-% HR")]
        CELCIUS_HUMIDITY,

        [ReadingUnit("mm")]
        MILLIMETERS,

        [ReadingUnit("°F")]
        FARENHEIT,

        [ReadingUnit("% HR")]
        HUMIDITY,

        [ReadingUnit("lx")]
        LUX,

        [ReadingUnit("% Loudness")]
        LOUDNESS,

        [ReadingUnit("°C")]
        CELCIUS,

        [ReadingUnit("%")]
        PERCENTAGE,

        [ReadingUnit("")]
        UNITLESS
    }
}