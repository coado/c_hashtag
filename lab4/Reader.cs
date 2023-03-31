using Microsoft.VisualBasic.FileIO;
using System.IO;


public class Reader<T> {
    public List<T> Read(String path, Func<String[], T> generate) {
        if (!File.Exists(path)) throw new Exception("The file does not exist");

        List<T> parsedData = new List<T>();

        bool isFirstLine = true;
        using (TextFieldParser parser = new TextFieldParser(path)) {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            while (!parser.EndOfData) {
                string[]? fields = parser.ReadFields();
                
                // omitting first line of csv file
                if (isFirstLine) {
                    isFirstLine = false;
                    continue;
                }

                if (fields == null) continue;

                parsedData.Add(generate(fields));
            }
        }

        return parsedData;
    }   
}