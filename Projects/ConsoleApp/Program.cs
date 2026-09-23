public class Program
{
    //!
    /*
        gRPC
        SOAP
        Mapster
        Асинхронность
        Многопоточность
        Паттерн SAGA для распределенных транзакций

        Grafana
        Prometheus
        Elastic log
        Serilog logging

        Unit of Work
        Идемпотентность
        MSSQL (UI в DataGrip)
        PostgreSQL(JSONb, blob, EntityFramework)

        CI/CD (build, test, test, deploy)
        Балансировщик нагрузки

        Portainer
        Kubernetes
        Redis (UI в DataGrip)
        Оркестратор, библиотека MassTransit
        Docker (настроить prod-версию vault)
        Docker (загрузка проекта в Docker Hub)

        Заменить throw new Exception(ex.Message) на throw new Exception("Сообщение об ошибке ", ex)
        Как правильно логировать без потери стек трейса?
        Передача токена отмены во все асинхронные методы БД
        Операции с БД всегда должны быть асинхронными
        Просканировать проект и доделать все, что не доделано

        AI
        Codex - расширение для VS Code
        Midjourney
        Copilot
        Gemini CLI — бесплатный агент Google для терминала и IDE https://blog.google/technology/developers/introducing-gemini-cli-open-source-ai-agent/
        Google Firebase Studio - облачная среда агентской разработки приложений https://firebase.blog/posts/2025/04/introducing-firebase-studio/
        Runway выпустила Gen-4, модель AI для генерации видео https://the-decoder.com/runway-releases-gen-4-video-model-with-focus-on-consistency/
    */

    static void Main()
    {
    }
}


/*
 
public async Task UpdateProceedFlagOrderInBuffer(Guid guid, bool isSuccess = true, string? errorMessage = null)
  {
   var requestModel = new
   {
    guid,
    isSuccess,
    errorMessage
   };

   await _repositoryComplexOrder.CallProcedure(
    StoreProcedures.[имя процедуры],
    requestModel,
    databaseType);
  }
  
 public class Repository<TResult> : IRepository<TResult>, IHealthCheck
 {
  private readonly IEncryptionService _encryptionService;
  private readonly ILoggingService _logging;

  private readonly DatabaseOptions DatabaseOptions;

  private readonly string? _connectionStringGeneral;
  private readonly string? _connectionStringBuffer;

  public Repository(
   IEncryptionService encryptionService,
   ILoggingService logging,
   IOptions<DatabaseOptions> databaseOptions)
  {
   _encryptionService = encryptionService;
   _logging = logging;

   DatabaseOptions = databaseOptions.Value;

   Guard.IsNotNullOrWhiteSpace(DatabaseOptions.ConnStr);
   Guard.IsNotNullOrWhiteSpace(DatabaseOptions.DbPassword);
   Guard.IsNotNullOrWhiteSpace(DatabaseOptions.ConnStrBuffer);
   Guard.IsNotNullOrWhiteSpace(DatabaseOptions.DbPasswordBuffer);

   _connectionStringGeneral = GetConnectionString(
    DatabaseOptions.ConnStr,
    DatabaseOptions.DbPassword);

   _connectionStringBuffer = GetConnectionString(
    DatabaseOptions.ConnStrBuffer,
    DatabaseOptions.DbPasswordBuffer);
  }

  public async Task<IEnumerable<TResult>?> CallProcedure(string procedureName, object? parameters, DatabaseType databaseType)
  {
   try
   {
    string connectionString = GetConnectionStringForCurrentDb(databaseType);

    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    IEnumerable<TResult> result = await connection.QueryAsync<TResult>(
     procedureName,
     parameters,
     commandType: CommandType.StoredProcedure,
     commandTimeout: DatabaseOptions.SqlCommandTimeout);

    return result;
   }
   catch (Exception ex)
   {
    await _logging.LogToFile(LoggingTypes.Error, $"Ошибка вызова процедуры {procedureName}: {ex}");

 #if DEBUG
    throw;
 #else
    return default;
 #endif
   }
  }

  public string GetConnectionStringForCurrentDb(DatabaseType databaseType)
  {
   string? connectionString = databaseType switch
   {
    DatabaseType.General => _connectionStringGeneral,
    DatabaseType.Buffer => _connectionStringBuffer,
    _ => default
   };

   Guard.IsNotNull(connectionString);
   return connectionString;
  }
 }




public class HttpClientData<T>(
   ILoggingService logging,
   IHttpClientFactory httpClientFactory)
  : IHttpClientData<T> where T : class
 {
  private readonly ILoggingService _logging = logging;
  private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
  
  public async Task<T?> GetRequestGeneric(string url, Dictionary<string, string?>? queryParams = null)
  {
   try
   {
    using HttpClient httpClient = _httpClientFactory.CreateClient();

    if (queryParams is not null && queryParams.Count > 0)
    {
     url = QueryHelpers.AddQueryString(url, queryParams);
    }

    httpClient.BaseAddress = new Uri(url);

    HttpResponseMessage response = await httpClient.GetAsync(url);
    if (response.IsSuccessStatusCode)
    {
     // Используем для автоматической десериализации JSON в объект
     T? result = await response.Content.ReadFromJsonAsync<T>();
     return result;
    }
    else
    {
     await _logging.LogToFile(LoggingTypes.Error, $"Ошибка при отправке GET-запроса, статус: {response.StatusCode}");
    }
   }
   catch (Exception ex)
   {
    await _logging.LogToFile(LoggingTypes.Error, $"Исключение при отправке GET-запроса: {ex}");

 #if DEBUG
    throw;
 #endif
   }

   return null;
  }
  
  public async Task<string?> PostRequestReturnString(string url, T body, Dictionary<string, string?>? queryParams = null)
  {
   try
   {
    using HttpClient httpClient = _httpClientFactory.CreateClient();

    if (queryParams is not null && queryParams.Count > 0)
    {
     url = QueryHelpers.AddQueryString(url, queryParams);
    }

    httpClient.BaseAddress = new Uri(url);

    ServicePointManager.SecurityProtocol =
     SecurityProtocolType.Tls12 |
     SecurityProtocolType.Tls13;

    HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, body);
    if (response != null && response.IsSuccessStatusCode)
    {
     string result = await response.Content.ReadAsStringAsync();
     return result;
    }
    else
    {
     await _logging.LogToFile(LoggingTypes.Error, $"Ошибка при отправке POST-запроса, статус: {response?.StatusCode}");
    }
   }
   catch (Exception ex)
   {
    await _logging.LogToFile(LoggingTypes.Error, $"Исключение при отправке POST-запроса: {ex}");

 #if DEBUG
    throw;
 #endif
   }

   return null;
  }
  
  public async Task<string?> PostFileRequest(string url, string xmlFile, FileTypes fileType)
  {
   using HttpClient httpClient = _httpClientFactory.CreateClient();
   using var form = new MultipartFormDataContent();

   var fileContent = new StringContent(xmlFile, Encoding.UTF8, "application/xml");

   form.Add(fileContent, "File", "document.xml");
   form.Add(new StringContent(nameof(fileType)), "Type");

   using HttpResponseMessage response = await httpClient.PostAsync(url, form);

   string result = await response.Content.ReadAsStringAsync();

   response.EnsureSuccessStatusCode();
   return result;
  }
  
 }

 
 
*/


/*
 
	Exception
	  throw new Exception(ex.Message)
	  =>
	  #if DEBUG
		 throw;
	  #else
		 return false;
	  #endif


	 // CancellationToken
	  // Передача токена отмены во все асинхронные методы БД
	  private static CancellationTokenSource? cancellationTokenSource;

	  [HttpPost]
	  public ActionResult SomeMethod()
	  {
	   if (cancellationTokenSource is null)
	   {
		cancellationTokenSource = new CancellationTokenSource();
	   }
	   else
	   {
		cancellationTokenSource.Dispose();
		cancellationTokenSource = new CancellationTokenSource();
	   }   
	   
	   _service.F2(cancellationTokenSource);
	  }
	  
	  [HttpPost]
	  public ActionResult Stop()
	  {
	   if (cancellationTokenSource is not null)
	   {
		// Прерывание
		cancellationTokenSource.Cancel();
	   }
	  }

	  public class FileUploaderService : IFileUploaderService
	  {
	   public void FilesPathsUploader()
	   {
		if (cancellationTokenSource.IsCancellationRequested)
		{
		 break;
		}
	   }
	  }
	  
	  
	 // Инфраструктура
	  Сервера (1-2 т.р./мес. на 1 сервер) у облачного провайдера для:
	  - БД
	  - брокер сообщений
	  - бекенд в нескольких экземплярах
	  - фронтенд
	  - БД для аналитики ClickHouse
	  - мониторинг на Prometeus/Grafana - на отдельном сервере и вывод в UI всех метрик

	  Домен .ru (1-3т.р./год)
	  
	  Хранилище файлов в облаке (1рубль/мес):
	  - S3 - при загрузке файла он копируется еще в 2 хранилища для надежности
	  
	  Бесплатно:
	  - GitLab для хранения кода и CI/CD
	  - HTTPS - сертификаты можно сделать бесплатно на 3-6 месяцев и потом перевыпускать
	  - система аналитики данных - сколько пользователей в день регистрируется и т.д.
		яндекс-метрика и google-analytic
	  - система алерта, если сервис не работает (отвалился ssl-сертификат, проблема с DNS, не работает хостинг и т.д.)
	  
	  Для большого проекта:
	  - защита от DDOS
	  - CDN - чтобы все файлы копировались, например, в США, чтобы пользователи из США не слали запросы через весь мир и не ждали долго ответ
	  - емейл/смс - рассылки
	  - kubernetes
	  - vault для хранения секретов
	  - preprod для тестирования, копирующий всю инфраструктуру
	  - сервис для логов (Elastic Search, Kibana)
	  - балансировщик нагрузки (есть в k8s)
	  
	  
	 Фильтры в ASP.NET Core
	 
	=> лямбда-функции 
 
*/


/*
 
 #region Архитектура и контракты

	// Когда микросервисы оправданы, а когда нет?
	Микросервисы решают организационную и эксплуатационную проблему, а не техническую
	- независимый деплой
	- независимое масштабирование
	- независимые команды
	
	Если команда одна и релизится раз в спринт целиком, распределённая система только добавит
	- сетевых отказов
	- распределённых транзакций
	- стоимости observability
	
	Разумный путь — модульный монолит с чёткими границами доменов,
	из которого выносится то, что реально требует отдельного жизненного цикла:
	- высокая нагрузка на узкий сценарий
	- отдельный SLA
	- отдельный темп релизов
	
	Решение принимается по границам домена и по частоте изменений

	// Как вы режете монолит? С чего начинаете?
	Начинаем с карты доменов и анализа связности: что меняется вместе, где границы транзакций, кто владеет данными
	Первым выносим то, что даёт быструю пользу и слабо связано по данным — обычно интеграционный или отчётный контур
	Дальше strangler fig: перед монолитом ставится фасад/шлюз, новый сервис берёт на себя часть маршрутов,
	старый код остаётся живым до полного перевода трафика и отключается последним
	Разделение общей БД: пока два сервиса пишут в одну таблицу - это не микросервисы, а распределённый монолит

	// Как обеспечиваете обратную совместимость и версионируете API?
	Только аддитивные изменения:
	- новые поля опциональны
	- старые не удаляются и не меняют смысл
	- обязательность не ужесточается
	
	В gRPC это дисциплина номеров полей в proto — номер никогда не переиспользуется
	Ломающее изменение = новая версия эндпоинта, две версии живут параллельно, старая гасится по факту нулевого трафика, а не по календарю
	Контрактные тесты в CI, чтобы несовместимость ловилась до прода, а не потребителем
	Обновление токена через http-фильтры, чтобы не ломать существующих потребителей

	// Зачем BFF, если есть API Gateway?
	Шлюз решает инфраструктурные задачи — маршрутизация, аутентификация, rate limiting
	BFF решает задачу конкретного клиента:
	- агрегирует вызовы нескольких доменных сервисов
	- отдаёт ровно ту модель, которая нужна экрану
	- снимает с клиента знание о внутренней декомпозиции

#endregion 

#region Тестирование и качество

	**В: Как у вас реально устроена пирамида тестов?**
	Суть ответа: Основание — быстрые юнит-тесты на бизнес-логику и правила предметной области: миллисекунды, без внешних зависимостей, запускаются на каждый коммит. Средний слой, самый ценный в микросервисах, — интеграционные тесты сервиса целиком: поднимаем приложение через WebApplicationFactory, реальную БД в контейнере, заглушки чужих HTTP через WireMock; проверяем связку контроллер—логика—EF—SQL. Вершина — небольшое число e2e на критичные сценарии, потому что они медленные и хрупкие. Пропорции не догма: там, где основная сложность в интеграциях, средний слой закономерно тяжелее классического треугольника.
	Опереться на кейс: EGAR — юнит- и интеграционные тесты на xUnit и WireMock как стандарт команды на проектах МТС B2B и БКС; АФТ-сценарии на Cucumber у Ильи.
	Не говорить: «пишем только юнит-тесты, интеграционные долго гонять».

	**В: Что мокаете, а что нет?**
	Суть ответа: Мокаем то, чем не владеем и что дорого или недетерминированно: внешние HTTP-API, платёжные шлюзы, отправку писем, время и генерацию идентификаторов. Не мокаем то, что определяет корректность нашего кода: собственную БД и ORM — мок репозитория проверяет мок, а не SQL, который EF на самом деле сгенерирует. InMemory-провайдер EF Core для проверки запросов не годится: у него другая семантика, он не ловит ни ошибки трансляции LINQ, ни ограничения БД. Брокер тоже лучше поднимать реальный в контейнере, чем имитировать.
	Опереться на кейс: EGAR — WireMock для внешних интеграций (в том числе ELMA365), реальная Postgres в интеграционных тестах доменных сервисов.
	Не говорить: «мокаем DbContext, так тесты быстрее».

	**В: Зачем Testcontainers и WireMock, если есть моки?**
	Суть ответа: Потому что большая часть дефектов живёт на стыках, а не внутри классов: неверная миграция, отличающееся поведение провайдера, нарушение ограничения уникальности, неправильная сериализация, таймаут внешнего API. Testcontainers поднимает настоящие PostgreSQL, Kafka, RabbitMQ на время теста, тест сам управляет их жизненным циклом, и на CI это воспроизводится без ручной подготовки стенда. WireMock даёт контролируемое поведение чужого API, включая сценарии ошибок, задержек и 500-х, которые на живом стенде не воспроизвести. Итог — тесты, которые ловят реальные проблемы и не зависят от общего окружения.
	Опереться на кейс: EGAR — интеграционные тесты с WireMock на проектах МТС и БКС, где интеграции были основной зоной риска.
	Не говорить: «интеграционные тесты нестабильны, поэтому мы их отключили».

	**В: Как тестируете межсервисные контракты?**
	Суть ответа: Односторонних тестов недостаточно: потребитель проверяет свои ожидания против заглушки, но это не гарантирует, что продюсер их выполняет. Поэтому контракт фиксируется артефактом — proto или OpenAPI-схема в общем пакете — и в CI проверяется обеими сторонами: у продюсера тест подтверждает соответствие реализации схеме, у потребителя заглушка генерируется из той же схемы, а не пишется руками. Для событий проверяется совместимость схемы сообщения при изменении. Такой подход ловит несовместимость на мерже, а не на общем стенде за день до релиза.
	Опереться на кейс: EGAR — межсервисное взаимодействие доменных сервисов на МТС B2B и БКС, где контракты согласовывались между несколькими командами.
	Не говорить: «проверяем интеграцию руками на стенде перед релизом».

	**В: Как тестируете консьюмеров брокера?**
	Суть ответа: На двух уровнях. Обработчик отделён от инфраструктуры приёма, поэтому его логика покрывается обычными юнит-тестами: подали сообщение — проверили эффект. Инфраструктурный уровень — интеграционный тест с реальным брокером в контейнере: публикуем сообщение, ждём результата, проверяем состояние. Обязательные сценарии — именно те, что ломаются на проде: повторная доставка того же сообщения (проверка идемпотентности), некорректное сообщение (уход в DLQ без бесконечного цикла), падение в середине обработки. Отдельная дисциплина — детерминированное ожидание вместо Thread.Sleep, иначе тест станет мигающим.
	Опереться на кейс: EGAR — сервисы с Kafka и RabbitMQ на проектах БКС и альтернативных финансовых инструментов.
	Не говорить: «консьюмеры проверяем вручную».

	**В: Какой порог покрытия и что он значит?**
	Суть ответа: Ориентир по бизнес-логике — 70–80%, но цифра сама по себе не значит ничего: покрытие показывает, что строка выполнилась, а не что поведение проверено. Тест без осмысленных утверждений даёт покрытие и нулевую ценность. Поэтому смотрим на покрытие как на индикатор непокрытых зон, а качество оцениваем иначе: обязательные тесты на найденный баг перед его исправлением, ревью тестов наравне с кодом, отсутствие мигающих тестов. Гнаться за 100% вредно — растёт стоимость поддержки на тривиальных участках.
	Опереться на кейс: EGAR — ревью кода коллег и требование покрытия на проектах МТС и БКС, онбординг новых членов команды с введением в тестовый стандарт.
	Не говорить: «у нас покрытие 90%, значит с качеством всё хорошо».

#endregion 

#region Производительность, gRPC и CQRS

	**В: Как ищете узкое место?**
	Суть ответа: Начинаю с измерения, а не с догадок. Сверху вниз: метрики и трейсы показывают, в каком сервисе и на какой операции растёт latency, дальше — профилирование: dotnet-counters для быстрой картины (CPU, GC, пул потоков, очередь запросов), dotnet-trace и dotMemory/PerfView для деталей по времени и аллокациям, план запроса — для БД. Отдельно смотрю, не является ли проблема не CPU, а ожиданием: блокировки, голодание пула потоков, лимиты соединений. Замеряю до и после изменения на сопоставимой нагрузке; оптимизация без числа — не оптимизация.
	Опереться на кейс: рефакторинг модуля депозитарного учёта — производительность выросла на 50% при одновременном добавлении gRPC- и CQRS-слоёв; вынос общего кода в NuGet-пакеты сократил время сборки решения на 20%.
	Не говорить: «переписали на LINQ покрасивее, стало быстрее».

	**В: Типичные ошибки с async/await?**
	Суть ответа: Главная — sync-over-async: .Result и .Wait() на асинхронном вызове, что блокирует поток пула и под нагрузкой приводит к голоданию, а исторически в ASP.NET — к дедлоку. Дальше: async void вне обработчиков событий, где исключение убивает процесс; отсутствие CancellationToken на всём пути, из-за чего отменённый клиентом запрос продолжает жить; создание задач через Task.Run внутри уже асинхронного веб-запроса — это не ускоряет, а лишь перекладывает работу между потоками. В библиотечном коде — ConfigureAwait(false), в ASP.NET Core контекста синхронизации нет, так что там это не критично. Плюс ValueTask для горячих путей, где результат обычно готов синхронно.
	Опереться на кейс: оптимизация серверной части и вывод модуля из монолита, где переход на сквозную асинхронность и отмену дал часть выигрыша.
	Не говорить: «async везде делает приложение быстрее» — он про пропускную способность, а не про скорость одной операции.

	**В: Что делаете с аллокациями и GC?**
	Суть ответа: Сначала выясняю, действительно ли GC — проблема: доля времени в GC, число gen2-сборок, попадания в Large Object Heap. Типичные источники — лишние промежуточные коллекции и ToList, конкатенация строк в циклах, боксинг, замыкания в горячих путях, буферы на каждый запрос. Инструменты: Span<T> и Memory<T> для работы с фрагментами без копирования, ArrayPool и RecyclableMemoryStream для переиспользования буферов, StringBuilder и string.Create, структуры там, где это оправдано. Всё это применяется точечно на горячих путях: в обычной бизнес-логике такая микрооптимизация только ухудшает читаемость.
	Опереться на кейс: оптимизация алгоритмов и бизнес-логики модуля при выводе из монолита; там же — экстремальные подходы применялись только к участкам, подтверждённым профилировщиком.
	Не говорить: «везде используем Span, так быстрее».

	**В: Как строите кэширование?**
	Суть ответа: Уровни: локальный in-memory (быстрый, но у каждого инстанса свой, поэтому возможна рассинхронизация), распределённый Redis (общий, но сетевой round-trip), плюс HTTP-кэширование на границе. Ключевой вопрос всегда не «как положить», а «как инвалидировать»: по TTL — самое простое и предсказуемое; по событию через брокер — точнее, но требует надёжной доставки; write-through — при контроле над записью. Обязательно защищаться от cache stampede: блокировка на пересчёт или вероятностное раннее обновление, иначе истечение популярного ключа даст залп запросов в БД. И кэш не должен быть источником истины — сервис обязан работать при пустом кэше.
	Опереться на кейс: у Михаила — кэширование для ускорения загрузки клиента в TSC Cloud; в депозитарном контуре кэш применялся к справочным данным с редкой сменой.
	Не говорить: «кэшируем всё, что можно» — без стратегии инвалидации это источник неверных данных.

	**В: Как проводите нагрузочное тестирование?**
	Суть ответа: Сначала определяем цель в числах: ожидаемый RPS, профиль нагрузки, целевые p95/p99 и допустимая доля ошибок — без этого тест не имеет критерия. Инструменты — k6 или NBomber, сценарии повторяют реальный профиль, а не один эндпоинт. Прогоняем несколько видов: пиковую нагрузку, длительную выносливость для выявления утечек и роста памяти, и стресс до отказа, чтобы понять, как система деградирует — важно, чтобы она деградировала предсказуемо, а не падала. Стенд должен быть сопоставим с продом по ресурсам и данным, иначе цифры вводят в заблуждение. Результат — не «выдержали», а зафиксированный потолок и найденное узкое место.
	Опереться на кейс: работа с высоконагруженными модулями корпоративного ПО НРД/МосБиржи и оптимизация отзывчивости после рефакторинга (двукратный рост отзывчивости фронта и отказ от медленных решений на бэке).
	Не говорить: «прогнали 1000 запросов локально, всё хорошо».

	**В: Зачем в вашем случае gRPC и CQRS?**
	Суть ответа: gRPC дал строгий контракт и дешёвый транспорт между сервисами после декомпозиции: proto как единственный источник истины, кодогенерация клиента, HTTP/2 и бинарный протокол вместо JSON. CQRS разделил модели чтения и записи: команды идут через доменную логику и валидацию, запросы — по оптимизированному пути напрямую к данным, без лишних абстракций. Это дало возможность оптимизировать чтение независимо от записи. Важная оговорка: CQRS не обязывает заводить отдельные хранилища и event sourcing — в нашем случае это разделение моделей в рамках одной БД, что покрывает большинство задач без роста сложности.
	Опереться на кейс: вывод модуля из монолита с добавлением gRPC- и CQRS-слоёв — +50% производительности при сохранении простоты расширения; MediatR как основа разделения команд и запросов.
	Не говорить: «CQRS — это обязательно event sourcing и две базы».

	**В: Что нового в .NET 10 вы реально используете?**
	Суть ответа: Честная рамка — в проде основная масса сервисов на .NET 8 и 9 как на LTS, .NET 10 применяем на новых модулях и в рамках плановой миграции, часть проектов уже собирается под 10. Что даёт переход: рост производительности рантайма и JIT без изменения кода, продолжение улучшений в AOT и времени старта, развитие минимальных API и валидации, улучшения в System.Text.Json и LINQ. Миграция для нас — рутинная процедура: обновление TFM, прогон тестов, проверка совместимости пакетов, замер до и после. Мы регулярно ходим по версиям от .NET 6 к 10, поэтому переход на 10 у заказчика — вопрос недель, а не риска.
	Опереться на кейс: собственные проекты на .NET 6.0–10.0; в команде .NET 10 заявлен у шестерых — Астафьев, Черменев, Лосев, Комаров, Козырь, Бисяев.
	Не говорить: перечислять фичи .NET 10 по анонсу как «активно используем в продакшене» — на уточняющий вопрос это разваливается за одну минуту. Лучше сказать «на 10 переводим, в проде сейчас 8/9» — это сильнее, чем разоблачённое преувеличение.

#endregion 

#region Брокеры сообщений

	- Kafka vs RabbitMQ — когда что?
	  - Kafka — лог событий, реплей, высокий throughput, партиционирование по ключу
	  - RabbitMQ — маршрутизация задач, приоритеты, DLQ, «команда → исполнитель»
	
	- Какие гарантии доставки обеспечиваете?
	  - at-least-once как рабочий дефолт
	  - exactly-once как свойство всей цепочки
	
	- Идемпотентность консьюмера и дедупликация
	  - ключ идемпотентности
	  - таблица обработанных сообщений
	  - TTL
	  - что делать при повторе на середине обработки
	
	- Transactional outbox
	  - атомарность «записали в БД + отправили в брокер», почему нельзя просто publish после commit, кто вычитывает outbox
	
	- событийное взаимодействие нового .NET-контура со Scala-монолитом через Kafka с обратной совместимостью под FeatureToggle

#endregion

#region Распределённые данные

	- Saga и компенсации, почему не распределённые транзакции?
	  - 2PC не масштабируется и завязывает сервисы друг на друга; оркестрация vs хореография
	  - компенсирующие операции проектируются заранее.
	- Согласованность между сервисами
	  eventual consistency как осознанный выбор, где допустима, а где нужен строгий инвариант
	- Как разделяли общую БД монолита?
	  Прямой кейс Слинко (Т-Банк) и Комарова (МКБ)

#endregion

 
*/
