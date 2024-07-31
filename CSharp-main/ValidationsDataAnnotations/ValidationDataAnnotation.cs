using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.ValidationsDataAnnotations
{
    /// <summary>Apenas valida o [Required]</summary>
    public class ValidationDataAnnotation
    {
        public static bool getValidationDataAnnotationsModelErros(object obj)
        {
            var contexto = new ValidationContext(obj,null);
            var resultadoValidacao = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, contexto, resultadoValidacao);

            if (!isValid || resultadoValidacao.Count > 0)
            {
                for (int i = 0; i < resultadoValidacao.Count; i++)
                {
                    Console.WriteLine(resultadoValidacao[i].ErrorMessage);
                }

                return false;
            } 
            return true;
        }
    }
}