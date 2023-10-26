using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Serilog;

public class CsvProcessor
{
    static CsvProcessor()
    {
        ConfigureLogger();
    }

    public static DateTime GetDateTime()
    {
        
        return DateTime.Now;
    }

    public static List<CustomerInfo> ProcessCsvFile(string filePath, DateTime dateTime)
    {
            List<CustomerInfo> validRecords = new List<CustomerInfo>();
        try
        {
            Log.Information("Started processing CSV files.");

            int skippedRows = 0;

            //foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.csv", SearchOption.AllDirectories))
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    try
                    {
                        var records = csv.GetRecords<CustomerInfo>();
                        foreach (var record in records)
                        {
                            if (IsValidRecord(record))
                            {
                                record.Date = dateTime.ToString("yyyy/MM/dd");
                                validRecords.Add(record);
                            }
                            else
                            {
                                skippedRows++;
                                Log.Warning($"Skipped incomplete record in file '{filePath}'.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error processing file '{filePath}': {ex.Message}");
                    }
                }
            }
            Log.Information($"Processed {validRecords.Count} valid rows.");
            Log.Information($"Skipped {skippedRows} incomplete rows.");
            Log.Information($"Total execution time: {DateTime.Now}");

            // Log the summary to a separate file
            LogSummary(validRecords, skippedRows);
        }
        catch (Exception ex)
        {
            Log.Error($"Error: {ex.Message}");
        }
        finally
        {
            Log.CloseAndFlush();
        }
            return validRecords;
    }

    private static void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../../../log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    private static bool IsValidRecord(CustomerInfo record)
    {
        if (string.IsNullOrWhiteSpace(record.FirstName) ||
            string.IsNullOrWhiteSpace(record.LastName) ||
            string.IsNullOrWhiteSpace(record.StreetNumber) ||
            string.IsNullOrWhiteSpace(record.Street) ||
            string.IsNullOrWhiteSpace(record.City) ||
            string.IsNullOrWhiteSpace(record.Province) ||
            string.IsNullOrWhiteSpace(record.Country) ||
            string.IsNullOrWhiteSpace(record.PostalCode) ||
            string.IsNullOrWhiteSpace(record.PhoneNumber) ||
            string.IsNullOrWhiteSpace(record.EmailAddress) ||
            !IsValidEmail(record.EmailAddress))
        {
            Log.Warning($"Skipped invalid record: {record.FirstName} {record.LastName}");
            return false;
        }

        return true;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static void LogSummary(List<CustomerInfo> validRecords, int skippedRowCount)
    {
        // Log the summary to a separate file
        string summaryFilePath = "../../../logs.txt";
        using (var summaryWriter = new StreamWriter(summaryFilePath))
        {
            summaryWriter.WriteLine($"Total execution time: {DateTime.Now}");
            summaryWriter.WriteLine($"Total number of valid rows: {validRecords.Count}");
            summaryWriter.WriteLine($"Total number of skipped rows: {skippedRowCount}");
        }

        // Save valid records to CSV file
        string outputFilePath = "../../../Output.csv";
        using (var csvWriter = new CsvWriter(new StreamWriter(outputFilePath), CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(validRecords.ToArray());
        }
    }
}
