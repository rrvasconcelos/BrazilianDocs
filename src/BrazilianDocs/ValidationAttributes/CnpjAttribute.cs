using System.ComponentModel.DataAnnotations;

namespace BrazilianDocs.ValidationAttributes;

public class CnpjAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string s)
            return BrazilianDocs.Cnpj.IsValid(s);
        return false;
    }
}