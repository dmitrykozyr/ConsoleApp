using IOptionsConfig.Interfaces;
using IOptionsConfig.Models.IOptions;
using Microsoft.Extensions.Options;

namespace IOptionsConfig.Services;

public class MyService : IMyService
{
    // Обновляет информацию о конфигурации один раз при старте приложения
    private readonly IOptions<MyConfigs> _testOptions;

    // Обновляет информацию о конфигурации при каждом запросе и не изменяет ее во время запроса
    private readonly IOptionsSnapshot<MyConfigs> _testOptionsSnapshot;

    // Обновляет информацию о конфигурации при каждом обращении к конфигурации
    // Если получаем конфигурацию в дрвух местах и за время запроса она изменилась,
    // то в друх местах будут разные значения
    private readonly IOptionsMonitor<MyConfigs> _testOptionsMonitor;

    public MyService(
        IOptions<MyConfigs> testOptions,
        IOptionsSnapshot<MyConfigs> testOptionsSnapshot,
        IOptionsMonitor<MyConfigs> testOptionsMonitor)
    {
        _testOptions            = testOptions;
        _testOptionsSnapshot    = testOptionsSnapshot;
        _testOptionsMonitor     = testOptionsMonitor;
    }

    public void GetData()
    {
        var optionsConfig           = _testOptions.Value;
        var optionsSnapshotConfig   = _testOptionsSnapshot.Value;
        var optionsMonitorConfig    = _testOptionsMonitor.CurrentValue;
    }
}
