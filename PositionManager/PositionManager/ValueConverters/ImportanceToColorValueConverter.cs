using System.Globalization;


namespace PositionManager.ValueConverters
{
    public class ImportanceToColorValueConverter : IValueConverter
    {
        private readonly Color Important = Colors.LightPink;
        private readonly Color Default = Colors.WhiteSmoke;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 2)
            {
                return Important;
            }
            return Default;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
