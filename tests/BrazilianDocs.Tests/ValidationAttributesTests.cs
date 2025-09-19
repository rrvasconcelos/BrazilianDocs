using System.ComponentModel.DataAnnotations;
using BrazilianDocs.ValidationAttributes;
using Xunit;

namespace BrazilianDocs.Tests
{
    public class ValidationAttributesTests
    {
        // Classe de teste para CPF
        private class Cliente
        {
            [Cpf]
            public string Cpf { get; set; } = "";
        }

        // Classe de teste para CNPJ
        private class Empresa
        {
            [Cnpj]
            public string Cnpj { get; set; } = "";
        }

        // ----------------------------
        // Testes CPF
        // ----------------------------

        [Fact]
        public void CpfAttribute_ShouldValidate_ValidCpf()
        {
            var cliente = new Cliente { Cpf = "529.982.247-25" };
            var context = new ValidationContext(cliente);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(cliente, context, results, true);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void CpfAttribute_ShouldInvalidate_InvalidCpf()
        {
            var cliente = new Cliente { Cpf = "123.456.789-00" };
            var context = new ValidationContext(cliente);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(cliente, context, results, true);

            Assert.False(isValid);
            Assert.NotEmpty(results);
        }

        // ----------------------------
        // Testes CNPJ
        // ----------------------------

        [Fact]
        public void CnpjAttribute_ShouldValidate_ValidCnpj()
        {
            var empresa = new Empresa { Cnpj = "04.252.011/0001-10" };
            var context = new ValidationContext(empresa);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(empresa, context, results, true);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void CnpjAttribute_ShouldInvalidate_InvalidCnpj()
        {
            var empresa = new Empresa { Cnpj = "11.111.111/1111-11" };
            var context = new ValidationContext(empresa);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(empresa, context, results, true);

            Assert.False(isValid);
            Assert.NotEmpty(results);
        }
    }
}
