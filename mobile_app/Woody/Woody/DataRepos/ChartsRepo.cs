using System.Collections.ObjectModel;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using Woody.Enums;
using Woody.Interfaces;

namespace Woody.DataRepos
{
    public static class ChartsRepo
    {
        #region Plant Charts
        public static CartesianChart GetTemperatureChart(List<IReading<double>> temperatureReadings)
        {
            var dateTimePoints = temperatureReadings
                .Select(x => new DateTimePoint(x.TimeStamp, x.Value))
                .ToList();

            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Temperature",
                    Values = new ObservableCollection<DateTimePoint>(dateTimePoints),
                    Stroke = new SolidColorPaint(SKColors.Orange),
                    GeometrySize = 0,
                    GeometryStroke = null,
                    LineSmoothness = 0.95
                }
            };

            var yAxis = new Axis[]
            {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 40,
                    Name = temperatureReadings[0].Unit.GetReadingUnitValue(),
                    NameTextSize = 14
                }
            };

            var dateTimeAxis = new DateTimeAxis(
                unit: TimeSpan.FromDays(1),
                formatter: date => date.ToString("MM-dd")
            );

            var xAxis = new Axis[] { dateTimeAxis };

            var labelVisual = new LabelVisual()
            {
                Text = "Temperature",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black),
            };

            var cartesianChart = new CartesianChart()
            {
                Series = series,
                YAxes = yAxis,
                XAxes = xAxis,
                Title = labelVisual
            };

            return cartesianChart;
        }

        public static CartesianChart GetSoilMoistureChart(
            List<IReading<double>> soilMoistureReadings
        )
        {
            var dateTimePoints = soilMoistureReadings
                .Select(x => new DateTimePoint(x.TimeStamp, x.Value))
                .ToList();

            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Soil Moisture",
                    Values = new ObservableCollection<DateTimePoint>(dateTimePoints),
                    Stroke = new SolidColorPaint(SKColors.Violet),
                    GeometrySize = 0,
                    GeometryStroke = null,
                    LineSmoothness = 0.95
                }
            };

            var yAxis = new Axis[]
            {
                new Axis
                {
                    MinLimit = 400,
                    MaxLimit = 2400,
                    Name = soilMoistureReadings[0].Unit.GetReadingUnitValue(),
                    NameTextSize = 14
                }
            };

            var dateTimeAxis = new DateTimeAxis(
                unit: TimeSpan.FromDays(1),
                formatter: date => date.ToString("MM-dd")
            );

            var xAxis = new Axis[] { dateTimeAxis };

            var labelVisual = new LabelVisual()
            {
                Text = "Soil Moisture",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black),
            };

            var cartesianChart = new CartesianChart()
            {
                Series = series,
                YAxes = yAxis,
                XAxes = xAxis,
                Title = labelVisual
            };

            return cartesianChart;
        }

        public static CartesianChart GetHumidityChart(List<IReading<double>> humidityReadings)
        {
            var dateTimePoints = humidityReadings
                .Select(x => new DateTimePoint(x.TimeStamp, x.Value))
                .ToList();

            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Humidity",
                    Values = new ObservableCollection<DateTimePoint>(dateTimePoints),
                    Stroke = new SolidColorPaint(SKColors.Orange),
                    GeometrySize = 0,
                    GeometryStroke = null,
                    LineSmoothness = 0.95
                }
            };

            var yAxis = new Axis[]
            {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 100,
                    Name = humidityReadings[0].Unit.GetReadingUnitValue(),
                    NameTextSize = 14
                }
            };

            var dateTimeAxis = new DateTimeAxis(
                unit: TimeSpan.FromDays(1),
                formatter: date => date.ToString("MM-dd")
            );

            var xAxis = new Axis[] { dateTimeAxis };

            var labelVisual = new LabelVisual()
            {
                Text = "Humidity",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black),
            };

            var cartesianChart = new CartesianChart()
            {
                Series = series,
                YAxes = yAxis,
                XAxes = xAxis,
                Title = labelVisual
            };

            return cartesianChart;
        }
        #endregion

        #region Security Charts
        public static CartesianChart GetNoiseChart(List<IReading<float>> noiseReadings)
        {
            var dateTimePoints = noiseReadings
                .Select(x => new DateTimePoint(x.TimeStamp, x.Value))
                .ToList();
            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Noise",
                    Values = new ObservableCollection<DateTimePoint>(dateTimePoints),
                    Stroke = new SolidColorPaint(SKColors.Blue),
                    GeometrySize = 0,
                    GeometryStroke = null,
                    LineSmoothness = 0.95
                }
            };

            var yAxis = new Axis[]
            {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 100,
                    Name = noiseReadings[0].Unit.GetReadingUnitValue(),
                    NameTextSize = 14
                }
            };

            var dateTimeAxis = new DateTimeAxis(
                unit: TimeSpan.FromDays(1),
                formatter: date => date.ToString("MM-dd")
            );

            var xAxis = new Axis[] { dateTimeAxis };

            var labelVisual = new LabelVisual()
            {
                Text = "Noise",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black),
            };

            var cartesianChart = new CartesianChart()
            {
                Series = series,
                YAxes = yAxis,
                XAxes = xAxis,
                Title = labelVisual
            };

            return cartesianChart;
        }

        public static CartesianChart GetLuminosityChart(List<IReading<int>> luminosityReadings)
        {
            var dateTimePoints = luminosityReadings
                .Select(x => new DateTimePoint(x.TimeStamp, x.Value))
                .ToList();
            LineSeries<DateTimePoint>[] series =
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Luminosity",
                    Values = new ObservableCollection<DateTimePoint>(dateTimePoints),
                    Stroke = new SolidColorPaint(SKColors.Yellow),
                    GeometrySize = 0,
                    GeometryStroke = null,
                    LineSmoothness = 0.95
                }
            };

            var yAxis = new Axis[]
            {
                new Axis
                {
                    MinLimit = 0,
                    // may need to tweak the max value
                    MaxLimit = 100000,
                    Name = luminosityReadings[0].Unit.GetReadingUnitValue(),
                    TextSize = 12,
                    NameTextSize = 14
                }
            };

            var dateTimeAxis = new DateTimeAxis(
                unit: TimeSpan.FromDays(1),
                formatter: date => date.ToString("MM-dd")
            );

            var xAxis = new Axis[] { dateTimeAxis };

            var labelVisual = new LabelVisual()
            {
                Text = "Luminosity",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black)
            };

            var cartesianChart = new CartesianChart()
            {
                Series = series,
                YAxes = yAxis,
                XAxes = xAxis,
                Title = labelVisual
            };

            return cartesianChart;
        }
        #endregion
    }
}
