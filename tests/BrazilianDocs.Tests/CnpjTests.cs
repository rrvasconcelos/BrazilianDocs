namespace BrazilianDocs.Tests;

public class CnpjTests
{
    [Theory]
    [InlineData("04.252.011/0001-10")]
    [InlineData("04252011000110")]
    public void Create_ValidCnpj_ShouldNotThrow(string input)
    {
        var cnpj = Cnpj.Create(input);
        Assert.NotNull(cnpj);
        Assert.True(Cnpj.IsValid(cnpj.Value));
    }

    [Theory]
    [InlineData("11.111.111/1111-11")]
    [InlineData("12345678000100")]
    public void Create_InvalidCnpj_ShouldThrow(string input)
    {
        Assert.Throws<ArgumentException>(() => Cnpj.Create(input));
    }

    [Fact]
    public void TryCreate_ValidCnpj_ShouldReturnTrue()
    {
        bool result = Cnpj.TryCreate("04.252.011/0001-10", out var cnpj);
        Assert.True(result);
        Assert.NotNull(cnpj);
        Assert.True(Cnpj.IsValid(cnpj!.Value));
    }

    [Fact]
    public void TryCreate_InvalidCnpj_ShouldReturnFalse()
    {
        bool result = Cnpj.TryCreate("12345678000100", out var cnpj);
        Assert.False(result);
        Assert.Null(cnpj);
    }

    [Fact]
    public void Generate_ShouldCreateValidCnpj()
    {
        var cnpj = Cnpj.Generate();
        Assert.NotNull(cnpj);
        Assert.True(Cnpj.IsValid(cnpj.Value));
    }

    [Fact]
    public void Format_ShouldReturnFormattedCnpj()
    {
        var cnpj = Cnpj.Create("04252011000110");
        string formatted = cnpj.Format();
        Assert.Equal("04.252.011/0001-10", formatted);
    }
}
