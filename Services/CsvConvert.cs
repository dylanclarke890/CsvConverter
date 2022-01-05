using Aspose.Cells;
using Aspose.Cells.Utility;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CsvConverter.Services
{
    public class CsvConvert
    {
        /// <summary>
        /// Uses Aspose.Cells to convert a CSV file to JSON.
        /// </summary>
        /// <param name="inputFilePath">A valid path to a CSV file.</param>
        /// <param name="outputFilePath">A valid directory to output the JSON file to.</param>
        public static void ToJson(string inputFilePath, string outputFilePath)
        {
            if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
            {
                Console.WriteLine("Invalid path provided.");
                return;
            }

            Console.WriteLine("Converting to JSON...");

            Workbook workbook = new(inputFilePath, new(LoadFormat.Csv));
            Cell lastCell = workbook.Worksheets[0].Cells.LastCell;

            Aspose.Cells.Range range = workbook.Worksheets[0].Cells.CreateRange(0, 0, lastCell.Row + 1, lastCell.Column + 1);
            string data = JsonUtility.ExportRangeToJson(range, new());

            File.WriteAllText(outputFilePath + "output.json", data);
            Console.WriteLine($"output.json created in {outputFilePath}.");
        }

        /// <summary>
        /// Uses System.Xml.Linq to build an XML tree from a CSV file, and saves it to an XML file.
        /// </summary>
        /// <param name="inputFilePath">A valid path to a CSV file.</param>
        /// <param name="outputFilePath">A valid directory to output the XML file to.</param>
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

        /// <summary>
        /// Adds a header element to the tree.
        /// </summary>
        /// <param name="line">A valid row of CSV headers.</param>
        /// <param name="xmlTree">A reference to the XML tree being built.</param>
        private static void AddHeader(string line, ref XElement xmlTree)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var currentTree = new XElement("Line");

            string[] slices = line.Split(",");
            for (int i = 0; i < slices.Length; i++)
            {
                currentTree.Add(new XElement($"Header{i}", slices[i].ToString().Trim()));
            }

            xmlTree.Add(currentTree);
        }

        /// <summary>
        /// Adds a column element to the tree.
        /// </summary>
        /// <param name="line">A valid row of CSV values.</param>
        /// <param name="xmlTree">A reference to the XML tree being built.</param>
        private static void AddContentForEachLine(string line, ref XElement xmlTree)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var currentTree = new XElement("Line");

            string[] slices = line.Split(",");
            for (int i = 0; i < slices.Length; i++)
            {
                currentTree.Add(new XElement($"Column{i}", slices[i].ToString().Trim()));
            }

            xmlTree.Add(currentTree);
        }
    }
}
