# BrazilianDocs

Uma biblioteca .NET para validação e manipulação de documentos brasileiros, incluindo CPF e CNPJ, com suporte a Value Objects, extensões de string e DataAnnotations.

---

## Funcionalidades

- Validação de CPF e CNPJ
- Criação segura de Value Objects (Cpf, Cnpj)
- Formatação de documentos
- Geração aleatória de CPF e CNPJ válidos
- Extensions: IsValidCpf(), IsValidCnpj()
- DataAnnotations: [Cpf] e [Cnpj] para validação automática em objetos de domínio ou DTOs

---

## Instalação

Via NuGet:

dotnet add package BrazilianDocs

Ou via Package Manager Console:

Install-Package BrazilianDocs

---

## Uso Básico

### CPF

using BrazilianDocs;

// Criar CPF a partir de string
var cpf = Cpf.Create("529.982.247-25");

// Verificar se é válido
bool isValid = Cpf.IsValid("529.982.247-25");

// Formatar
string formatted = cpf.Format(); // "529.982.247-25"

// Gerar CPF aleatório válido
var randomCpf = Cpf.Generate();

### CNPJ

using BrazilianDocs;

// Criar CNPJ a partir de string
var cnpj = Cnpj.Create("04.252.011/0001-10");

// Verificar se é válido
bool isValid = Cnpj.IsValid("04.252.011/0001-10");

// Formatar
string formatted = cnpj.Format(); // "04.252.011/0001-10"

// Gerar CNPJ aleatório válido
var randomCnpj = Cnpj.Generate();

### Extensions

string cpfStr = "529.982.247-25";
bool validCpf = cpfStr.IsValidCpf();

string cnpjStr = "04.252.011/0001-10";
bool validCnpj = cnpjStr.IsValidCnpj();

### DataAnnotations

using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Cpf]
    public string Cpf { get; set; } = "";
}

public class Empresa
{
    [Cnpj]
    public string Cnpj { get; set; } = "";
}

// Funciona com Validator.TryValidateObject ou frameworks que suportam DataAnnotations.

---

## Testes

O projeto inclui testes completos usando xUnit:

- CpfTests.cs e CnpjTests.cs → testam criação, validação, formatação e geração aleatória
- ExtensionsTests.cs → testam as extensões de string
- ValidationAttributesTests.cs → testam [Cpf] e [Cnpj]

Executar:

dotnet test BrazilianDocs.Tests

---

## Compatibilidade

- .NET Standard 2.0
- Compatível com .NET Core 2.0+, .NET 5+, .NET Framework 4.6.1+

---

## Contribuição

Pull requests e issues são bem-vindos!

- Siga o padrão Value Object + extensão + atributo opcional.
- Mantenha tests cobrindo todas as funcionalidades.

---

## Licença

MIT License © Rodrigo # BrazilianDocs

Uma biblioteca .NET para validação e manipulação de documentos brasileiros, incluindo CPF e CNPJ, com suporte a Value Objects, extensões de string e DataAnnotations.

---

## Funcionalidades

- Validação de CPF e CNPJ
- Criação segura de Value Objects (Cpf, Cnpj)
- Formatação de documentos
- Geração aleatória de CPF e CNPJ válidos
- Extensions: IsValidCpf(), IsValidCnpj()
- DataAnnotations: [Cpf] e [Cnpj] para validação automática em objetos de domínio ou DTOs

---

## Instalação

Via NuGet:

dotnet add package BrazilianDocs

Ou via Package Manager Console:

Install-Package BrazilianDocs

---

## Uso Básico

### CPF

using BrazilianDocs;

// Criar CPF a partir de string
var cpf = Cpf.Create("529.982.247-25");

// Verificar se é válido
bool isValid = Cpf.IsValid("529.982.247-25");

// Formatar
string formatted = cpf.Format(); // "529.982.247-25"

// Gerar CPF aleatório válido
var randomCpf = Cpf.Generate();

### CNPJ

using BrazilianDocs;

// Criar CNPJ a partir de string
var cnpj = Cnpj.Create("04.252.011/0001-10");

// Verificar se é válido
bool isValid = Cnpj.IsValid("04.252.011/0001-10");

// Formatar
string formatted = cnpj.Format(); // "04.252.011/0001-10"

// Gerar CNPJ aleatório válido
var randomCnpj = Cnpj.Generate();

### Extensions

string cpfStr = "529.982.247-25";
bool validCpf = cpfStr.IsValidCpf();

string cnpjStr = "04.252.011/0001-10";
bool validCnpj = cnpjStr.IsValidCnpj();

### DataAnnotations

using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Cpf]
    public string Cpf { get; set; } = "";
}

public class Empresa
{
    [Cnpj]
    public string Cnpj { get; set; } = "";
}

// Funciona com Validator.TryValidateObject ou frameworks que suportam DataAnnotations.

---

## Testes

O projeto inclui testes completos usando xUnit:

- CpfTests.cs e CnpjTests.cs → testam criação, validação, formatação e geração aleatória
- ExtensionsTests.cs → testam as extensões de string
- ValidationAttributesTests.cs → testam [Cpf] e [Cnpj]

Executar:

dotnet test BrazilianDocs.Tests

---

## Compatibilidade

- .NET Standard 2.0
- Compatível com .NET Core 2.0+, .NET 5+, .NET Framework 4.6.1+

---

## Contribuição

Pull requests e issues são bem-vindos!

- Siga o padrão Value Object + extensão + atributo opcional.
- Mantenha tests cobrindo todas as funcionalidades.

---

## Licença

MIT License © Rodrigo Vasconcelos
