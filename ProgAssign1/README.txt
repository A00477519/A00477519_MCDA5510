# Assignment #1

## Overview

The objective of this assignment is to create a consolidated program that recursively reads a series of data files in CSV format and combines them into a single output file. 
The program is required to log the time taken to read the files in each directory and the time it takes to write the files to the output using a logging mechanism.

## Program Description

The program is designed to traverse a directory structure containing CSV files. 
It reads the data from each CSV file, combines the records, and writes the consolidated data into a single output file. 
The program utilizes logging to capture the execution time for reading files in each directory and the time taken to write the combined data.

## Components

### 1. CsvProcessor

The `CsvProcessor` class is responsible for processing CSV files. 
It reads data from individual CSV files, performs any necessary validations, and maintains a consolidated set of records.

### 2. DirWalker

The `DirWalker` class is responsible for recursively traversing a directory structure. 
It identifies CSV files in each directory and delegates the processing to the `CsvProcessor`.

### 3. Program

The `Program` class serves as the entry point of the application. 
It initiates the directory traversal using `DirWalker` and captures the overall execution time.

## Logging

The program employs a logging mechanism, powered by the Serilog library. 
It records essential information such as the time taken to read files in each directory and the time taken to write the consolidated data. Log entries are available both in the console and in a log file named `log-.txt` with daily log rolling.

## Execution

To run the program, specify the root directory containing the CSV files in the `directoryPath` variable inside the `Main` method of the `Program` class. 
After execution, the program will provide log entries in the console and create a log file for reference.

## Dependencies

The program relies on the following libraries:

- CsvHelper: Used for reading and writing CSV files.
- Serilog: Utilized for logging purposes.

## Instructions for Running the Program

1. Ensure you have the .NET runtime installed on your machine.
2. Open a terminal or command prompt.
3. Navigate to the directory containing the compiled program.
4. Run the program using the following command:
    ```bash
    dotnet run
    ```
5. Adjust the `directoryPath` variable in the `Main` method of `Program.cs` to point to your desired root directory.