using System.Text.RegularExpressions;

namespace BrazilianDocs;

public sealed record Cnpj
{
    public string Value { get; }

    private Cnpj(string value) => Value = Normalize(value);

    public static Cnpj Create(string input)
    {
        var normalized = Normalize(input);
        if (!IsValid(normalized))
            throw new ArgumentException("CNPJ inválido", nameof(input));
        return new Cnpj(normalized);
    }

    public static bool TryCreate(string input, out Cnpj? cnpj)
    {
        try
        {
            cnpj = Create(input);
            return true;
        }
        catch
        {
            cnpj = null;
            return false;
        }
    }

    public static bool IsValid(string input)
    {
        var value = Normalize(input);
        if (value.Length != 14 || Regex.IsMatch(value, @"^(.)\1{13}$"))
            return false;

        int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = value.Substring(0, 12);
        int sum = 0;
        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * mult1[i];

        int remainder = sum % 11;
        int digit = remainder < 2 ? 0 : 11 - remainder;
        string cnpjWithDigit = tempCnpj + digit;

        sum = 0;
        for (int i = 0; i < 13; i++)
            sum += int.Parse(cnpjWithDigit[i].ToString()) * mult2[i];

        remainder = sum % 11;
        digit = remainder < 2 ? 0 : 11 - remainder;
        cnpjWithDigit += digit;

        return value.EndsWith(cnpjWithDigit.Substring(cnpjWithDigit.Length - 2, 2));
    }

    public string Format() =>
        Convert
            .ToUInt64(Value.Substring(0, 12))
            .ToString(@"00\.000\.000\/0000\-") + Value.Substring(Value.Length - 2, 2);

    public static string Normalize(string input) =>
        Regex.Replace(input ?? string.Empty, "[^0-9]", "");

    public static Cnpj Generate()
    {
        var random = new Random();
        var numbers = new int[12];
        for (int i = 0; i < 12; i++)
            numbers[i] = random.Next(0, 10);

        int firstDigit = CalculateDigit(numbers, [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
        int[] numbersPlusFirst = new int[13];
        Array.Copy(numbers, numbersPlusFirst, 12);
        numbersPlusFirst[12] = firstDigit;
        int secondDigit = CalculateDigit(numbersPlusFirst, [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);

        string cnpjStr = string.Join("", numbers) + firstDigit + secondDigit;
        return new Cnpj(cnpjStr);
    }

    private static int CalculateDigit(int[] numbers, int[] multipliers)
    {
        int sum = 0;
        for (int i = 0; i < multipliers.Length; i++)
            sum += numbers[i] * multipliers[i];
        int remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }
}
