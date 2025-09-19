using System;
using System.Text.RegularExpressions;

namespace BrazilianDocs;

/// <summary>
/// Representa um CPF (Cadastro de Pessoas Físicas) válido como Value Object.
/// Fornece métodos para validação, formatação, normalização e geração de CPFs válidos.
/// </summary>
public sealed record Cpf
{
    /// <summary>
    /// Retorna o valor do CPF sem formatação (apenas números).
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Construtor privado que normaliza o CPF.
    /// </summary>
    /// <param name="value">CPF já validado e normalizado.</param>
    private Cpf(string value) => Value = Normalize(value);

    /// <summary>
    /// Cria um novo CPF validado. Lança <see cref="ArgumentException"/> se o CPF for inválido.
    /// </summary>
    /// <param name="input">O CPF como string, com ou sem máscara.</param>
    /// <returns>Um objeto <see cref="Cpf"/> válido.</returns>
    public static Cpf Create(string input)
    {
        var normalized = Normalize(input);
        if (!IsValid(normalized))
            throw new ArgumentException("CPF inválido", nameof(input));
        return new Cpf(normalized);
    }

    /// <summary>
    /// Tenta criar um CPF. Retorna falso se inválido.
    /// </summary>
    /// <param name="input">CPF em string.</param>
    /// <param name="cpf">CPF resultante, ou nulo se inválido.</param>
    /// <returns>True se o CPF for válido; caso contrário, false.</returns>
    public static bool TryCreate(string input, out Cpf? cpf)
    {
        try
        {
            cpf = Create(input);
            return true;
        }
        catch
        {
            cpf = null;
            return false;
        }
    }

    /// <summary>
    /// Verifica se o CPF informado é válido.
    /// </summary>
    /// <param name="input">CPF em string.</param>
    /// <returns>True se válido; caso contrário, false.</returns>
    public static bool IsValid(string input)
    {
        var value = Normalize(input);

        // CPF deve ter 11 dígitos e não pode ter todos iguais
        if (value.Length != 11 || Regex.IsMatch(value, @"^(.)\1{10}$"))
            return false;

        int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = value.Substring(0, 9);
        int sum = 0;

        // Calcula o primeiro dígito verificador
        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * mult1[i];

        int remainder = sum % 11;
        int digit = remainder < 2 ? 0 : 11 - remainder;
        string cpfWithDigit = tempCpf + digit;

        // Calcula o segundo dígito verificador
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(cpfWithDigit[i].ToString()) * mult2[i];

        remainder = sum % 11;
        digit = remainder < 2 ? 0 : 11 - remainder;
        cpfWithDigit += digit;

        return value.EndsWith(cpfWithDigit.Substring(cpfWithDigit.Length - 2, 2));
    }

    /// <summary>
    /// Retorna o CPF formatado como XXX.XXX.XXX-XX.
    /// </summary>
    /// <returns>CPF formatado.</returns>
    public string Format() =>
        Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");

    /// <summary>
    /// Normaliza o CPF, removendo caracteres não numéricos.
    /// </summary>
    /// <param name="input">CPF em string.</param>
    /// <returns>CPF contendo apenas números.</returns>
    public static string Normalize(string input) =>
        Regex.Replace(input ?? string.Empty, "[^0-9]", "");

    /// <summary>
    /// Gera um CPF válido aleatório. Útil para testes.
    /// </summary>
    /// <returns>Um objeto <see cref="Cpf"/> válido.</returns>
    public static Cpf Generate()
    {
        var random = new Random();
        var numbers = new int[9];

        for (int i = 0; i < 9; i++)
            numbers[i] = random.Next(0, 10);

        int firstDigit = CalculateDigit(numbers, [10, 9, 8, 7, 6, 5, 4, 3, 2]);
        int[] numbersPlusFirst = new int[10];
        Array.Copy(numbers, numbersPlusFirst, 9);
        numbersPlusFirst[9] = firstDigit;
        int secondDigit = CalculateDigit(numbersPlusFirst, [11, 10, 9, 8, 7, 6, 5, 4, 3, 2]);

        string cpfStr = string.Join("", numbers) + firstDigit + secondDigit;
        return new Cpf(cpfStr);
    }

    /// <summary>
    /// Calcula o dígito verificador com base nos números e multiplicadores fornecidos.
    /// </summary>
    /// <param name="numbers">Array de números do CPF.</param>
    /// <param name="multipliers">Array de multiplicadores correspondentes.</param>
    /// <returns>Dígito verificador calculado.</returns>
    private static int CalculateDigit(int[] numbers, int[] multipliers)
    {
        int sum = 0;
        for (int i = 0; i < multipliers.Length; i++)
            sum += numbers[i] * multipliers[i];
        int remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }
}
