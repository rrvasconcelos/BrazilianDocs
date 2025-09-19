namespace BrazilianDocs.Extensions;

public static class StringExtensions
{
    public static bool IsValidCpf(this string input) => Cpf.IsValid(input);
    public static bool IsValidCnpj(this string input) => Cnpj.IsValid(input);
}