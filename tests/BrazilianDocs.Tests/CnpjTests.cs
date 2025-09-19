using Xunit;

namespace BrazilianDocs.Tests
{
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

        // Testa criação de CNPJ inválido
        [Theory]
        [InlineData("11.111.111/1111-11")]
        [InlineData("12345678000100")]
        public void Create_InvalidCnpj_ShouldThrow(string input)
        {
            Assert.Throws<ArgumentException>(() => Cnpj.Create(input));
        }

        // Testa TryCreate com CNPJ válido
        [Fact]
        public void TryCreate_ValidCnpj_ShouldReturnTrue()
        {
            bool result = Cnpj.TryCreate("04.252.011/0001-10", out var cnpj);
            Assert.True(result);
            Assert.NotNull(cnpj);
            Assert.True(Cnpj.IsValid(cnpj!.Value));
        }

        // Testa TryCreate com CNPJ inválido
        [Fact]
        public void TryCreate_InvalidCnpj_ShouldReturnFalse()
        {
            bool result = Cnpj.TryCreate("12345678000100", out var cnpj);
            Assert.False(result);
            Assert.Null(cnpj);
        }

        // Testa Generate: o CNPJ gerado deve ser válido e ter filial 0001
        [Fact]
        public void Generate_ShouldCreateValidCnpj()
        {
            var cnpj = Cnpj.Generate();
            Assert.NotNull(cnpj);
            Assert.True(Cnpj.IsValid(cnpj.Value));

            // Verifica se a filial é 0001
            string filial = cnpj.Value.Substring(8, 4);
            Assert.Equal("0001", filial);
        }

        // Testa a formatação do CNPJ
        [Fact]
        public void Format_ShouldReturnFormattedCnpj()
        {
            var cnpj = Cnpj.Create("04252011000110");
            string formatted = cnpj.Format();
            Assert.Equal("04.252.011/0001-10", formatted);
        }
    }
}
