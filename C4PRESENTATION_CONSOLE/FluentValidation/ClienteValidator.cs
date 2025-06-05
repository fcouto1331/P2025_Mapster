using C4PRESENTATION_CONSOLE.DTOs;
using FluentValidation;

namespace C4PRESENTATION_CONSOLE.FluentValidation
{
    public class ClienteValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteValidator()
        {

            RuleFor(o => o.Nome)
                .NotEmpty().WithMessage("Nome obrigatório.")
                .Length(1, 100).WithMessage("Nome máximo 100 caracteres.")
                .Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ0-9\s]+$").WithMessage("Nome não pode conter caracteres especiais.");
            //.Matches(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$").WithMessage("Nome não pode conter caracteres especiais ou números.");


            RuleFor(o => o.Telefone)
                .NotEmpty().WithMessage("Telefone obrigatório.")
                .Length(14, 15).WithMessage("Telefone deve ter 15 caracteres.")
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").WithMessage("Telefone inválido. Formato: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX");

            RuleFor(o => o.Cidade)
                .NotEmpty().WithMessage("Cidade obrigatória.")
                .Length(1, 100).WithMessage("Cidade máximo 100 caracteres.")
                .Must(RegraCidade).WithMessage("Cidade cadastrada não autorizada.");
        }

        private static bool RegraCidade(string cidade)
        {
            if (cidade.ToUpper().Equals("SÃO PAULO")) return true;
            else if (cidade.ToUpper().Equals("RIO DE JANEIRO")) return true;
            else return false;
        }
    }
}
