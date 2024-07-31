using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.FluentVallidation
{
       public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected string MensagemCampoObrigatorio(string campo) => $"O campo {campo} é obrigatório.";
        protected string MensagemCampoInvalido(string campo) => $"O valor para o campo {campo} é inválido.";
        protected string MensagemTamanhoMaximoCampo(string campo, int tamanho) => $"O campo {campo} deve conter no máximo {tamanho} caracteres";
        protected string MensagemTamanhoMinimoCampo(string campo, int tamanho) => $"O campo {campo} deve conter no mínimo {tamanho} caracteres";
        protected string MensagemTamanhoCampo(string campo, int tamanho) => $"O campo {campo} deve conter {tamanho} caracteres";
        protected string MensagemPrecisaoDecimal(string campo, int casasDecimais) => $"O campo {campo} deve conter precisão de {casasDecimais} casas decimais";
        protected string MensagemValorMinMax<N>(string campo, N min, N max) => $"O campo {campo} deve conter um valor entre {min} e {max}";
    }

}

 public class EventoValidator : BaseValidator<Evento>
    {
        public EventoValidator()
        {
            RuleFor(x=>x.Nome).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(MensagemCampoObrigatorio("Nome")).
                MaximumLength(50).WithMessage(MensagemTamanhoMaximoCampo("Nome", 50));
            RuleFor(x => x.Reponsavel).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(MensagemCampoObrigatorio("Responsavel"));
            RuleFor(x => x.Vagas).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(MensagemCampoObrigatorio("Vagas"));
            RuleFor(x => x.Observacao).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(MensagemCampoObrigatorio("Observação"));
            RuleFor(x => x.Data).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(MensagemCampoObrigatorio("Data")).
                Must(BeAValidDate).WithMessage(MensagemCampoInvalido("Data"));
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }

     public static class ValidateData
    {
        public static bool getValidationModelErros(object obj, IValidator validator)
        {
            var contexto = new ValidationContext<object>(obj);

            var validation =  validator.Validate(contexto);

            for (int i = 0; i < validation.Errors.Count; i++)
            {
                Console.WriteLine(validation.Errors[i].ErrorMessage);
            }

            if (!validation.IsValid)
            {
                return false;
            }
            return true;
        }

    }
