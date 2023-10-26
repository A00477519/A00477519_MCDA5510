using System.IO;
using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Entry of the program ... Hit enter to continue ....");
        Console.ReadLine();

        //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "/Users/papantiamoah/Documents/customer-data-processor/CSV-traverse/Sample Data");

        string filePath = @"/Users/papantiamoah/Documents/ProgAssign1/Sample Data/2017/11/8";


        DirWalker.TraverseDirectory(filePath);

        Console.ReadLine();

    }
}



