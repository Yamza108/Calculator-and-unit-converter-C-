using System;

namespace CalcConverter
{
    // This class holds all the unit conversions.
    // If the input makes no sense (e.g. a rate of 0), we throw an ArgumentException.
    public static class UnitConverter
    {
        
        private const decimal KmPerMile = 1.609344m;       // 1 mile = 1.609344 km
        private const decimal AbsoluteZeroCelsius = -273.15m;
        private const decimal AbsoluteZeroFahrenheit = -459.67m;

        public static decimal KilometresToMiles(decimal km)
        {
            return km / KmPerMile;
        }

        public static decimal MilesToKilometres(decimal miles)
        {
            return miles * KmPerMile;
        }

        public static decimal CelsiusToFahrenheit(decimal celsius)
        {
            if (celsius < AbsoluteZeroCelsius)
            {
                throw new ArgumentException("Temperature cannot be below absolute zero (-273.15 C).");
            }

            // Formula: F = C x 9/5 + 32
            return celsius * 9 / 5 + 32;
        }

        public static decimal FahrenheitToCelsius(decimal fahrenheit)
        {
            if (fahrenheit < AbsoluteZeroFahrenheit)
            {
                throw new ArgumentException("Temperature cannot be below absolute zero (-459.67 F).");
            }

            // Formula: C = (F - 32) x 5/9
            return (fahrenheit - 32) * 5 / 9;
        }

        // ghsPerUsd means "how many cedis you need to buy 1 US dollar", e.g. 15.5
        public static decimal GhsToUsd(decimal ghs, decimal ghsPerUsd)
        {
            if (ghsPerUsd <= 0)
            {
                throw new ArgumentException("Exchange rate must be greater than zero.");
            }

            return ghs / ghsPerUsd;
        }
    }
}
