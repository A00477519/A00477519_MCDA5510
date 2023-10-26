using System.IO;
using System;
using System.Collections.Generic;
using CsvHelper;

public class DirWalker
{
    static Boolean flg = true;
    public static void TraverseDirectory(string directoryPath)
    {
        if (Directory.Exists(directoryPath))
        {
            string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv", SearchOption.AllDirectories);
            String outputPath = (new FileInfo(@"../../../Output.csv")).FullName.ToString();



            Console.WriteLine("Output File = " + outputPath);
            Console.WriteLine(File.Exists(outputPath));

            foreach (var csvFile in csvFiles)

            {
                List<CustomerInfo> list =  CsvProcessor.ProcessCsvFile(csvFile, CsvProcessor.GetDateTime());
                Console.WriteLine("Processed file = "+csvFile);



                //FileStream fs;
                StreamWriter sw;

                if (flg)
                {
                    //flg = false;
                    //fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
                    sw = new StreamWriter(outputPath, false);
                }
                else
                {
                    //fs = new FileStream(outputPath, FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(outputPath, true);
                }


                using (CsvWriter writer = new CsvWriter(sw, System.Globalization.CultureInfo.InvariantCulture))
                {
                    if (flg)
                    {
                        flg = false;
                        writer.WriteHeader<CustomerInfo>();
                    }

                    //writer.NextRecord();
                    writer.WriteRecords(list);
                    writer.Flush();


                    //sw.Close();
                    //fs.Close();
                }  
            }
        }
        else
        {
            // Handle directory not found error
            Console.WriteLine($"Directory '{directoryPath}' not found.");
        }
    }
}
