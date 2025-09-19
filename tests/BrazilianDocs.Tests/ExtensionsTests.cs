using BrazilianDocs.Extensions;

namespace BrazilianDocs.Tests;

public class ExtensionsTests
{
    [Fact]
    public void StringExtensions_IsValidCpf_ShouldWork()
    {
        Assert.True("529.982.247-25".IsValidCpf());
        Assert.False("123.456.789-00".IsValidCpf());
    }

    [Fact]
    public void StringExtensions_IsValidCnpj_ShouldWork()
    {
        Assert.True("04.252.011/0001-10".IsValidCnpj());
        Assert.False("11.111.111/1111-11".IsValidCnpj());
    }
}
