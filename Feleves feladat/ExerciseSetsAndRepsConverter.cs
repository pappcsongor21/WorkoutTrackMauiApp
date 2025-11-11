using Feleves_feladat.Models;
using System.Globalization;

namespace Feleves_feladat
{
    public class ExerciseSetsAndRepsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Exercise)
            {
                return $"{((Exercise)value).TargetSets} X {((Exercise)value).TargetReps}";
            }
            return string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
