using Microsoft.Extensions.Options;

namespace OptionConfiguration;

/*

    IOptions
        Обновляет информацию о конфигурации при запуске приложения

    IOptionsSnapshot
        Обновляет информацию о конфигурации при каждом запросе
        Если в рамках одного запроса есть нескольок обращения к конфигурации и она может за втоемя запроса измениться,
        то в разных местах получим ОДИНАКОВЫЕ значения,
        полученные один раз в начале запроса

    IOptionsMonitor
        Если в рамках одного запроса есть нескольок обращения к конфигурации и она может за втоемя запроса измениться,
        то в разных местах получим РАЗНЫЕ значения,
        получаемые при каждом обращении к конфигурации
*/

public class ApplicationOptionsSetup
{
    private readonly MyConfigs_1 MyConfigs_1;
    private readonly MyConfigs_2 MyConfigs_2;
    private readonly MyConfigs_3 MyConfigs_3;

    public ApplicationOptionsSetup(
     IOptions<MyConfigs_1> _options,
     IOptionsSnapshot<MyConfigs_2> _optionsSnapshot,
     IOptionsMonitor<MyConfigs_3> _optionsMonitor)
    {
        MyConfigs_1 = _options.Value;
        MyConfigs_2 = _optionsSnapshot.Value;
        MyConfigs_3 = _optionsMonitor.CurrentValue;
    }

    public void Configure(ApplicationOptionsSetup options)
    {
        var config1 = MyConfigs_1.ExampleValue;
        var config2 = MyConfigs_2.ExampleValue;
        var config3 = MyConfigs_3.ExampleValue;
    }
}
