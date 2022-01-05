using DylanClarkeCsvToJson.Services;

namespace DylanClarkeCsvToJson
{
    class Program
    {
        /// <summary>
        /// Given a filepath, a conversion type and an output path, will output a JSON or XML file.
        /// </summary>
        /// <param name="args">
        /// <c>args[0]</c> should be a path to the CSV file (defaults to sample file provided).
        /// <c>args[1]</c> should be either json or xml.
        /// <c>args[2]</c> should be path to output to (defaults to current working directory).
        /// </param>
        static void Main(string[] args)
        {
            InputParameters inputParameters = new InputService(args).GetInputParameters();

            switch (inputParameters?.Conversion)
            {
                case ConversionType.Json:
                    CsvConvert.ToJson(inputParameters.CsvFilePath, inputParameters.OutputDirectory);
                    break;
                case ConversionType.Xml:
                    CsvConvert.ToXml(inputParameters.CsvFilePath, inputParameters.OutputDirectory);
                    break;
                default:
                    return;
            }
        }
    }
}
