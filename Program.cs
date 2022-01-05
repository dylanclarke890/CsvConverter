using DylanClarkeCsvConverter.Services;

namespace DylanClarkeCsvConverter
{
    class Program
    {
        /// <summary>
        /// Given a filepath, a conversion type and an output path, will output a JSON or XML file.
        /// </summary>
        /// <param name="args">
        /// <c>args[0]</c> should be a valid path to the CSV file.
        /// <c>args[1]</c> should be either json or xml.
        /// <c>args[2]</c> should be path to output the file to (cam default to current directory if not valid/provided).
        /// </param>
        static void Main(string[] args)
        {
            InputParameters inputParameters = new InputService(args).GetInputParameters();

            switch (inputParameters?.Conversion)
            {
                case ConversionType.Json:
                    CsvConvert.ToJson(inputParameters.CsvFilePath, inputParameters.OutputDirectory);
                    return;
                case ConversionType.Xml:
                    CsvConvert.ToXml(inputParameters.CsvFilePath, inputParameters.OutputDirectory);
                    return;
                default:
                    System.Console.WriteLine("Couldn't determine conversion type");
                    return;
            }
        }
    }
}
