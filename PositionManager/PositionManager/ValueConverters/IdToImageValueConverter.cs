using System.Globalization;


namespace PositionManager.ValueConverters
{
    public class IdToImageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = ((int)value).ToString() + ".jpg";

            string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            if (!File.Exists(filePath))
            {
                return "placeholder.jpg";
            }

            return filePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

