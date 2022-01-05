using Aspose.Cells;
using Aspose.Cells.Utility;
using System;
using System.IO;

namespace DylanClarkeCsvToJson.Services
{
    public class CsvConvert
    {
        public static void ToJson(string inputFilePath, string outputFilePath)
        {
            Console.WriteLine("Converting to JSON...");

            Workbook workbook = new(inputFilePath, new(LoadFormat.Csv));
            Cell lastCell = workbook.Worksheets[0].Cells.LastCell;

            Aspose.Cells.Range range = workbook.Worksheets[0].Cells.CreateRange(0, 0, lastCell.Row + 1, lastCell.Column + 1);
            string data = JsonUtility.ExportRangeToJson(range, new());

            Console.WriteLine(data);
            File.WriteAllText(outputFilePath + "output.json", data);
            Console.WriteLine($"output.json created in {outputFilePath}.");
        }

        public static void ToXml(string inputFilePath, string outputFilePath)
        {
            Console.WriteLine("Converting to XML...");

            var fileContents = File.ReadAllLines(inputFilePath);
            var headers = fileContents[0].Split(", ");
            foreach (var header in headers)
            {
                Console.WriteLine(header);
            }
        }
    }
}
