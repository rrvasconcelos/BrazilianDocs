using System.Text.RegularExpressions;

namespace BrazilianDocs
{
    public sealed record Cnpj
    {
        public string Value { get; }

        private Cnpj(string value) => Value = Normalize(value);

        public static Cnpj Create(string input)
        {
            var normalized = Normalize(input);
            if (!IsValid(normalized))
                throw new ArgumentException("CNPJ inválido", nameof(input));
            return new Cnpj(normalized);
        }

        public static bool TryCreate(string input, out Cnpj? cnpj)
        {
            try
            {
                cnpj = Create(input);
                return true;
            }
            catch
            {
                cnpj = null;
                return false;
            }
        }

        public static bool IsValid(string input)
        {
            var value = Normalize(input);
            if (value.Length != 14 || Regex.IsMatch(value, @"^(.)\1{13}$"))
                return false;

            int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = value.Substring(0, 12);
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * mult1[i];

            int remainder = sum % 11;
            int digit1 = remainder < 2 ? 0 : 11 - remainder;
            tempCnpj += digit1;

            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * mult2[i];

            remainder = sum % 11;
            int digit2 = remainder < 2 ? 0 : 11 - remainder;

            return value.EndsWith($"{digit1}{digit2}");
        }

        public string Format() =>
             Regex.Replace(Value, @"^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})$", "$1.$2.$3/$4-$5");


        public static string Normalize(string input) =>
            Regex.Replace(input ?? string.Empty, "[^0-9]", "");

        public static Cnpj Generate()
        {
            var random = new Random();

            var raiz = new int[8];
            for (int i = 0; i < 8; i++)
                raiz[i] = random.Next(0, 10);

            var filial = new int[] { 0, 0, 0, 1 };

            var numeros = raiz.Concat(filial).ToArray();

            int dv1 = CalculateDigit(numeros, [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);
            int dv2 = CalculateDigit(numeros.Append(dv1).ToArray(), [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2]);

            string cnpjStr = string.Join("", numeros) + dv1 + dv2;
            return new Cnpj(cnpjStr);
        }

        private static int CalculateDigit(int[] numbers, int[] multipliers)
        {
            int sum = 0;
            for (int i = 0; i < multipliers.Length; i++)
                sum += numbers[i] * multipliers[i];
            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }
    }
}
