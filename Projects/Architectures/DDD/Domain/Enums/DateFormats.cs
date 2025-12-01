using System.ComponentModel;

namespace Domain.Enums;

public enum DateFormats
{
    [Description("yyyyMMdd")]
    ISO8601,

    [Description("MM/dd/yyyy")]
    MMddyyyy,

    [Description("dd.MM.yyyy H:mm:ss")]
    ddMMyyyyHmmss,

    [Description("dd.MM.yyyy")]
    ddMMyyyy,

    [Description("ddMMyyyy")]
    ddMMyyyy_withoutSymbols
}
