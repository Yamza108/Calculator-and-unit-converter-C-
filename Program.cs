using System;
using System.Globalization;

namespace CalcConverter
{
    public class Program
    {
    
        public static void Main()
        {
            Console.WriteLine("=== Calculator & Unit Converter ===");

            bool running = true;

            // Keep showing the main menu until the user chooses Exit.
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("1) Calculator");
                Console.WriteLine("2) Unit converter");
                Console.WriteLine("0) Exit");

                string choice = ReadInput("Choose an option: ");

                if (choice == "1")
                {
                    RunCalculator();
                }
                else if (choice == "2")
                {
                    RunConverter();
                }
                else if (choice == "0")
                {
                    Console.WriteLine("Goodbye!");
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1, 2 or 0.");
                }
            }
        }

        // ---------- Calculator menu ----------

        private static void RunCalculator()
        {
            Console.WriteLine();
            Console.WriteLine("1) Add   2) Subtract   3) Multiply   4) Divide");
            string operation = ReadInput("Choose an operation: ");

            // Check the choice BEFORE asking for numbers, so we don't waste the user's time.
            if (operation != "1" && operation != "2" && operation != "3" && operation != "4")
            {
                Console.WriteLine("Invalid operation.");
                return; // go back to the main menu
            }

            decimal first = ReadNumber("First number: ");
            decimal second = ReadNumber("Second number: ");

            // try/catch: if the code inside "try" throws an error,
            // the program jumps to "catch" instead of crashing.
            try
            {
                decimal result = 0;

                switch (operation)
                {
                    case "1":
                        result = Calculator.Add(first, second);
                        break;
                    case "2":
                        result = Calculator.Subtract(first, second);
                        break;
                    case "3":
                        result = Calculator.Multiply(first, second);
                        break;
                    case "4":
                        result = Calculator.Divide(first, second);
                        break;
                }

                Console.WriteLine("Result: " + FormatNumber(result));
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: the result is too large.");
            }
        }

        // ---------- Converter menu ----------

        private static void RunConverter()
        {
            Console.WriteLine();
            Console.WriteLine("1) Kilometres -> Miles");
            Console.WriteLine("2) Miles -> Kilometres");
            Console.WriteLine("3) Celsius -> Fahrenheit");
            Console.WriteLine("4) Fahrenheit -> Celsius");
            Console.WriteLine("5) GHS -> USD");
            string choice = ReadInput("Choose a conversion: ");

            if (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
            {
                Console.WriteLine("Invalid conversion.");
                return;
            }

            // Only the GHS -> USD option needs an exchange rate.
            decimal rate = 0;
            if (choice == "5")
            {
                rate = ReadNumber("Exchange rate (how many GHS per 1 USD): ");
            }

            decimal value = ReadNumber("Value to convert: ");

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine(FormatNumber(value) + " km = " +
                            FormatNumber(UnitConverter.KilometresToMiles(value)) + " miles");
                        break;
                    case "2":
                        Console.WriteLine(FormatNumber(value) + " miles = " +
                            FormatNumber(UnitConverter.MilesToKilometres(value)) + " km");
                        break;
                    case "3":
                        Console.WriteLine(FormatNumber(value) + " C = " +
                            FormatNumber(UnitConverter.CelsiusToFahrenheit(value)) + " F");
                        break;
                    case "4":
                        Console.WriteLine(FormatNumber(value) + " F = " +
                            FormatNumber(UnitConverter.FahrenheitToCelsius(value)) + " C");
                        break;
                    case "5":
                        Console.WriteLine("GHS " + FormatNumber(value) + " = USD " +
                            FormatNumber(UnitConverter.GhsToUsd(value, rate)));
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Error: the result is too large.");
            }
        }

        // ---------- Helper methods ----------

        // Shows a message, reads one line the user types, and removes extra spaces.
        private static string ReadInput(string message)
        {
            Console.Write(message);
            string line = Console.ReadLine();

            // ReadLine returns null if there is no more input (e.g. the user pressed Ctrl+Z).
            // Without this check the program could loop forever, so we stop cleanly.
            if (line == null)
            {
                Environment.Exit(0);
            }

            return line.Trim();
        }

        // Keeps asking until the user types a valid number.
        private static decimal ReadNumber(string message)
        {
            while (true)
            {
                string text = ReadInput(message);

                decimal number;
                if (InputParser.TryParseNumber(text, out number))
                {
                    return number;
                }

                Console.WriteLine("Invalid number. Please try again (e.g. 12.5).");
            }
        }

        // Shows at most 4 decimal places and drops trailing zeros (2.5000 -> 2.5).
        private static string FormatNumber(decimal number)
        {
            return number.ToString("0.####", CultureInfo.InvariantCulture);
        }
    }
}
