namespace CsvConverter
{
    public class InputParameters
    {
        public string CsvFilePath { get; set; }

        public ConversionType ConvertTo { get; set; }

        public string OutputDirectory { get; set; }
    }
}
