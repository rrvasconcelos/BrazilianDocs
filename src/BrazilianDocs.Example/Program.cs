using BrazilianDocs;

var cpf = Cpf.Generate();

Console.WriteLine($"CPF: {cpf.Value}");

Console.WriteLine($"Valido? {Cpf.IsValid(cpf.Value)}");

var cnpj = Cnpj.Generate();

Console.WriteLine($"CNPJ: {cnpj.Value}");

Console.WriteLine($"Valido? {Cnpj.IsValid(cnpj.Value)}");