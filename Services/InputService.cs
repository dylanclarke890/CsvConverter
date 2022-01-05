using System;
using System.IO;

namespace DylanClarkeCsvToJson.Services
{
    public class InputService
    {
        private readonly string[] Args;

        public InputService(string[] args)
        {
            Args = args;
        }

        public InputParameters GetInputParameters()
        {
            InputParameters inputParameters = new();

            // input file path
            if (Args == null || Args.Length == 0)
            {
                Console.WriteLine("Please specify a filepath as a parameter:");
                inputParameters.CsvFilePath = Console.ReadLine();
            }
            else
            {
                inputParameters.CsvFilePath = Args[0];
            }
            if (string.IsNullOrWhiteSpace(inputParameters.CsvFilePath) || !File.Exists(Path.GetFullPath(inputParameters.CsvFilePath)))
            {
                Console.WriteLine($"File was not found at {inputParameters.CsvFilePath}.");
                return default!;
            }
            inputParameters.CsvFilePath = Path.GetFullPath(inputParameters.CsvFilePath);

            // conversion type
            if (Args == null || Args.Length <= 1)
            {
                Console.WriteLine("JSON or XML?");
                inputParameters.Conversion = ParseConversionType(Console.ReadLine());
            }
            else
            {
                inputParameters.Conversion = ParseConversionType(Args[1]);
            }
            if (inputParameters.Conversion is ConversionType.NotSpecified)
            {
                return default!;
            }

            // output directory
            if (Args == null || Args.Length <= 2)
            {
                Console.WriteLine("Please provide the directory to output to:");
                inputParameters.OutputDirectory = Console.ReadLine();
            }
            else
            {
                inputParameters.OutputDirectory = Args[2];
            }

            if (string.IsNullOrWhiteSpace(inputParameters.OutputDirectory) || !Directory.Exists(Path.GetFullPath(inputParameters.OutputDirectory)))
            {
                Console.WriteLine($"Unable to find existing directory for {inputParameters.OutputDirectory}, would you like to use the current directory? Enter 'Y' to confirm or anything else to exit.");
                if (Console.ReadLine().ToLower() != "y")
                {
                    return default!;
                }

                Console.WriteLine("Using current directory...");
                inputParameters.OutputDirectory = Directory.GetCurrentDirectory();
            }
            else
            {
                inputParameters.OutputDirectory = Path.GetFullPath(inputParameters.OutputDirectory);
            }


            return inputParameters;
        }

        private static ConversionType ParseConversionType(string textToParse)
        {
            if (string.IsNullOrWhiteSpace(textToParse) || !Enum.TryParse<ConversionType>(textToParse, true, out var result))
            {
                Console.WriteLine($"Unable to determine conversion type for {textToParse}.");
                return ConversionType.NotSpecified;
            }
            return result;
        }
    }
}
