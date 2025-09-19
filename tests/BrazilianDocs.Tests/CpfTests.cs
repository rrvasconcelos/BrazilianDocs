namespace BrazilianDocs.Tests;

public class CpfTests
{
    [Theory]
    [InlineData("529.982.247-25")]
    [InlineData("52998224725")]
    public void Create_ValidCpf_ShouldNotThrow(string input)
    {
        var cpf = Cpf.Create(input);
        Assert.NotNull(cpf);
        Assert.True(Cpf.IsValid(cpf.Value));
    }

    [Theory]
    [InlineData("123.456.789-00")]
    [InlineData("11111111111")]
    public void Create_InvalidCpf_ShouldThrow(string input)
    {
        Assert.Throws<ArgumentException>(() => Cpf.Create(input));
    }

    [Fact]
    public void TryCreate_ValidCpf_ShouldReturnTrue()
    {
        bool result = Cpf.TryCreate("529.982.247-25", out var cpf);
        Assert.True(result);
        Assert.NotNull(cpf);
        Assert.True(Cpf.IsValid(cpf!.Value));
    }

    [Fact]
    public void TryCreate_InvalidCpf_ShouldReturnFalse()
    {
        bool result = Cpf.TryCreate("123.456.789-00", out var cpf);
        Assert.False(result);
        Assert.Null(cpf);
    }

    [Fact]
    public void Generate_ShouldCreateValidCpf()
    {
        var cpf = Cpf.Generate();
        Assert.NotNull(cpf);
        Assert.True(Cpf.IsValid(cpf.Value));
    }

    [Fact]
    public void Format_ShouldReturnFormattedCpf()
    {
        var cpf = Cpf.Create("52998224725");
        string formatted = cpf.Format();
        Assert.Equal("529.982.247-25", formatted);
    }
}
