# Console Calculator & Unit Converter (C#)

A menu-driven console app written in C# (.NET 8) that:

- adds, subtracts, multiplies and divides
- converts kilometres <-> miles and Celsius <-> Fahrenheit
- converts GHS to USD using an exchange rate that you type in
- handles wrong input without crashing

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download) (check with `dotnet --version`)

## How to run

```bash
git clone <your-repo-url>
cd CalcConverter
dotnet run --project CalcConverter
```

Type the number of the menu option you want and press Enter. Option `0` exits.

### Example session

```
=== Calculator & Unit Converter ===

1) Calculator
2) Unit converter
0) Exit
Choose an option: 2

1) Kilometres -> Miles
...
5) GHS -> USD
Choose a conversion: 5
Exchange rate (how many GHS per 1 USD): 15.5
Value to convert: 155
GHS 155 = USD 10
```

## How to run the tests

```bash
dotnet test
```

## Project layout

| File | Purpose |
|------|---------|
| `CalcConverter/Program.cs` | Entry point (`Main`), the menus, and reading input |
| `CalcConverter/Calculator.cs` | The four arithmetic operations |
| `CalcConverter/UnitConverter.cs` | All unit conversions and their validation |
| `CalcConverter/InputParser.cs` | Turns typed text into a number (or says it is invalid) |
| `CalcConverter.Tests/` | xUnit tests for the maths and the input parser |

## Design

The maths lives in `Calculator` and `UnitConverter`, which never print or read anything. They throw an error
when the input makes no sense (e.g. dividing by zero). `Program.cs` handles the menus, checks what the user typed,
and uses `try`/`catch` to turn those errors into friendly messages, so the program never crashes on bad input.
Keeping the maths separate from the console code is what makes it easy to unit test.

## Assumptions

- The exchange rate is entered as **how many GHS equal 1 USD** (e.g. `15.5`), and USD = GHS / rate.
- Numbers use a dot as the decimal separator (`12.5`), regardless of the computer's regional settings.
- `decimal` is used for all maths, so results like `0.1 + 0.2` are exact.
- Results are displayed rounded to 4 decimal places.
- Miles/km uses the exact factor 1 mile = 1.609344 km.

## Known limitations

- There is no live exchange rate, so the user must type one in.
- Only GHS -> USD is supported, not USD -> GHS.
- Calculator handles two numbers at a time (no expressions like `2 + 3 * 4`).
- Temperatures below absolute zero are rejected.
- Very large numbers beyond the range of `decimal` (about 7.9 x 10^28) produce an error message.
