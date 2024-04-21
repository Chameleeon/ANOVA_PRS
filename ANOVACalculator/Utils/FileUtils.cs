using System;

public class FileUtils
{
    private FileUtils() { }

    public static double[][] ReadCSV(string path)
    {
        var lines = System.IO.File.ReadAllLines(path);
        var result = new double[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = Array.ConvertAll(lines[i].Split(','), double.Parse);
        }
        return result;
    }
}
