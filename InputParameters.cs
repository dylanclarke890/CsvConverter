namespace DylanClarkeCsvConverter
{
    public class InputParameters
    {
        public string CsvFilePath { get; set; }

        public ConversionType Conversion { get; set; }

        public string OutputDirectory { get; set; }
    }
}
