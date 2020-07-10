using System;
using System.Windows;
using Media = System.Windows.Media;
using Serilog;

namespace NotePad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Models.DocumentModel _documentModel;
        internal static Models.FormatModel _formatModel;

        public App()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\NotePad.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            _documentModel = new Models.DocumentModel();
            _formatModel = new Models.FormatModel();
            LoadState();
        }
        private void NotePadExit(object sender, ExitEventArgs e)
        {
            SaveState();
        }

        private void SaveState()
        {
            Log.Information("Saving State");
            var regAccessor = new RegistryAccessor();
            regAccessor.Write("FONTFAMILY", _formatModel.Family);
            regAccessor.Write("FONTWEIGHT", _formatModel.Weight.ToOpenTypeWeight().ToString());
            regAccessor.Write("FONTSIZE", _formatModel.Size);
            regAccessor.Write("FONTSYLE", _formatModel.Style.ToString());
        }

        private void LoadState()
        {
            Log.Information("Loading State");
            var regAccessor = new RegistryAccessor();
            var fontFamConverter = new Media.FontFamilyConverter();
            var fontFamily = regAccessor.Read("FONTFAMILY");
            var fontWeightToBeConverted = regAccessor.Read("FONTWEIGHT");
            var fontSize = regAccessor.Read("FONTSIZE");
            var fontStyle = regAccessor.Read("FONTSYLE");

            if (fontFamily != null)
                _formatModel.Family = fontFamConverter.ConvertFromString(fontFamily) as Media.FontFamily;

            if (fontWeightToBeConverted != null)
                _formatModel.Weight = FontWeight.FromOpenTypeWeight(Convert.ToInt32(regAccessor.Read("FONTWEIGHT")));

            if (fontSize != null && fontSize != "0")
                _formatModel.Size = Convert.ToDouble(fontSize);

            if (fontStyle != null)
                _formatModel.Style = ConvertFontSytleFromString(fontStyle);
        }

        private static System.Windows.FontStyle ConvertFontSytleFromString(string style)
        {
            var defaultFontSyle = System.Windows.FontStyles.Normal;

            switch (style.ToLowerInvariant())
            {
                case "normal":
                    return System.Windows.FontStyles.Normal;
                case "italic":
                    return System.Windows.FontStyles.Italic;
                case "oblique":
                    return System.Windows.FontStyles.Oblique;
                default:
                    return defaultFontSyle;
            } 
        }
    }
}
