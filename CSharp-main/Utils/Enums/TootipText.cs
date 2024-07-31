using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Portal.Enums
{

    public enum ToolTipText
    {
        [Description("Aqui você pode baixar o comprovante")]
        ToolTipTextComprovante,
        [Description("Aqui você pode ir para página de detalhes")]
        ToolTipTextDetalhes,
        [Description("Aqui você pode solicitar uma disputa(Contestação da transação)")]
        ToolTipTextDispute
    }

    public static class Tootip
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}


