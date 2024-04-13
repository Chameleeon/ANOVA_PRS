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

            // Print each member of the result[i-1]
            foreach (double num in result[i])
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();

        }
        return result;
    }
}
