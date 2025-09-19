using System.ComponentModel.DataAnnotations;

namespace BrazilianDocs.ValidationAttributes;

public class CpfAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string s)
            return BrazilianDocs.Cpf.IsValid(s);
        return false;
    }
}