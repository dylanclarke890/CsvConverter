using Aspose.Cells;
using Aspose.Cells.Utility;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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

            File.WriteAllText(outputFilePath + "output.json", data);
            Console.WriteLine($"output.json created in {outputFilePath}.");
        }

        public static void ToXml(string inputFilePath, string outputFilePath)
        {
            Console.WriteLine("Converting to XML...");

            var lines = File.ReadAllLines(inputFilePath);

            var xmlTree = new XElement("CSV");

            AddHeader(lines[0], ref xmlTree);

            foreach (var line in lines.Skip(1))
            {
                AddContentForEachLine(line, ref xmlTree);
            }
            File.WriteAllText(outputFilePath + "output.xml", xmlTree.ToString());

            Console.WriteLine($"output.xml created in {outputFilePath}.");
        }

        private static void AddHeader(string line, ref XElement xmlTree)
        {
            var currentTree = new XElement("Row");

            string[] slices = line.Split(",");
            for (int i = 0; i < slices.Length; i++)
            {
                currentTree.Add(new XElement($"Header{i}", slices[i].ToString().Trim()));
            }

            xmlTree.Add(currentTree);
        }

        private static void AddContentForEachLine(string line, ref XElement xmlTree)
        {
            var currentTree = new XElement("Row");

            string[] slices = line.Split(",");
            for (int i = 0; i < slices.Length; i++)
            {
                currentTree.Add(new XElement($"Column{i}", slices[i].ToString().Trim()));
            }

            xmlTree.Add(currentTree);
        }
    }
}
