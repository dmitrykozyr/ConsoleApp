Amazing User, [10.04.2024 12:12]
ASP.NET Core Web API

- в "launchSettings.json" описаны настройки запуска проекта (адреса, по которым будет запускаться приложение)
- "appsettings.json" файл конфигурации приложения в формате json
- "appsettings.Development.json" - версия файла конфигурации приложения, которая используется в процессе разработки


//####################### ОТПРАВКА И ПОЛУЧЕНИЕ JSON #######################

https://metanit.com/sharp/aspnet6/2.10.php

// Отправка JSON. Метод WriteAsJsonAsync
Для отправки JSON можно использовать методы объекта HttpResponse:
- WriteAsJson()
- WriteAsJsonAsync()

Позволяют сериализовать переданные объекты в JSON и
для заголовка "content-type"
устанавливает значение "application/json; charset=utf-8":

    var builder = WebApplication.CreateBuilder();
    var app = builder.Build();

    app.Run(async (context) =>
    {
        Person tom = new("Tom", 22);
        await context.Response.WriteAsJsonAsync(tom);
    });

    app.Run();

    public record Person(string Name, int Age);

Клиенту отправляется объект типа Person,
который представляет класс-record, но это может быть и обычный класс

Можно было воспользоваться стандартным методом WriteAsync():

     app.Run(async (context) =>
     {
         var response = context.Response;
         response.Headers.ContentType = "application/json; charset=utf-8";
         await response.WriteAsync("{\"name\":\"Tom\",\"age\":37}");
     });

// Получение JSON. Метод ReadFromJsonAsync
Для получения из запроса объекта в формате JSON,
в классе HttpRequest определен метод ReadFromJsonAsync()
Позволяет сериализовать данные в объект определенного типа

Сздадим в проекте папку html, в которой определим новый файл index.html.

В файле index.html определим код:

<!DOCTYPE html>
<html>
<head>
    <meta charset = "utf-8"/>
    <title> METANIT.COM </title>
</ head>
<body>
    <h2> User form </h2>
    <div id = "message" ></div>
    <div>
        <p> Name: <br/>
            <input name = "userName" id = "userName"/>
        </p>
        <p> Age: <br/>
            <input name = "userAge" id = "userAge" type = "number"/>
        </p>
        <button id = "sendBtn"> Send </button>
    </div>
    <script>
        document.getElementById("sendBtn").addEventListener("click", send);
        async function send()
        {
            const response = await fetch("/api/user", {
                method: "POST",
                headers: { "Accept": "application/json", "Content-Type": "application/json" },
                body: JSON.stringify({
                    name: document.getElementById("userName").value,
                    age: document.getElementById("userAge").value
                })
            });
            const message = await response.json();
            document.getElementById("message").innerText = message.text;
        }
    </script>
</body>
</html >

По нажатию на кнопку с помощью функции 'fetch()' по адресу "/api/user"
будет отправляться объект со свойствами 'name' и 'age',
значения которых берутся из полей формы
В ответ от сервера веб-страница получает объект в формате JSON
со свойством 'text', которое хранит сообщение от сервера

В файле 'Program.cs' определим код для получения данных, отправляемых веб-страницей:

    var builder = WebApplication.CreateBuilder();
    var app = builder.Build();

    app.Run(async (context) =>
    {
        var response = context.Response;
        var request = context.Request;
        if (request.Path == "/api/user")
        {
            var message = "Некорректные данные";   // содержание сообщения по умолчанию
            try
            {
                // пытаемся получить данные JSON
                var person = await request.ReadFromJsonAsync<Person>();
                if (person != null) // если данные сконвертированы в Person
                    message = $"Name: {person.Name}  Age: {person.Age}";
            }
            catch { }
            // отправляем пользователю данные
            await response.WriteAsJsonAsync(new { text = message });
        }
        else
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/index.html");
        }
    });

    app.Run();

    public record Person(string Name, int Age);

Если обращение идет по адресу "/api/user" - получим данные в формате JSON
При обращениях по другим адресам посылаем веб-страницу 'index.html'

Метод 'ReadFromJsonAsync()' десериализует полученные данные в объект определенного типа
В данном случае типа 'Person':

    var person = await request.ReadFromJsonAsync<Person>();
    if (person != null) // если данные сконвертированы в Person
        message = $"Name: {person.Name}  Age: {person.Age}";

Результат вызова этого метода - значение переменной 'person' будет представлять объект 'Person'

Если данные запроса не представляют объект JSON,
либо если метод 'ReadFromJsonAsync()' не смог связать данные запроса со свойствами класса 'Person' -
вызов этого метода сгенерирует исключение

Поэтому вызов метода помещается в 'try..catch'
Это узкое место, далее от него избавимся

В конце в ответ посылаем анонимный объект,
который сериализуется в JSON с некоторым сообщением, которое хранится в свойстве 'text'
При получении этого сообщения оно выводится на веб-страницу

Проверять на наличие JSON в запросе можно с помощью метода 'HasJsonContentType()'
Возвращает 'true', если клиент прислал JSON

    if (request.HasJsonContentType())
    {
        var person = await request.ReadFromJsonAsync<Person>();
        if (person != null)
            responseText = $"Name: {person.Name}  Age: {person.Age}";
    }

// Настройка сериализации
При получении данных в формате JSON можем столкнуться с проблемами,
как когда вынуждены были поместить вызов метода 'ReadFromJsonAsync' в 'try..catch'

Если не введем в поля формы никаких значений - стандартный механизм привязки значений не сможет связать данные запроса со свойством 'Age', тогда получим исключение

Аналогичный пример, когда данные JSON не соответствуют определению типа,
в который надо выполнить десериализацию:

    const response = await fetch("/api/user", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            userName: "Tom",
            userAge: 22
        })
    });

Здесь названия свойств отправляемого объекта не соответствуют названиям свойств типа 'Person' в C#
Но объект Person все равно будет создан, а его свойства получат значения по умолчанию

Другой пример - отправляемые данные не соответствуют по типу:

    const response = await fetch("/api/user", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: "Tom",
            age: "twenty-two"
        })
    });

Здесь свойство 'age' представляет строку и не сможет быть сконвертировано в значение типа 'int'
При отправке на сервере возникнет 'System.Text.Json.JsonException'

В обоих примерах можно
- обрабатывть исключения
- встраивать дополнительные middleware для отлова подобных ситуаций и тд

Одно из решений - настройка сериализации/десериализации с помощью параметра типа 'JsonSerializerOptions', которое может передаваться в метод 'ReadFromJsonAsync()'

 ReadFromJsonAsync<T>(JsonSerializerOptions options);

Изменим код 'Program.cs':

    using System.Text.Json;
    using System.Text.Json.Serialization;
  
    var builder = WebApplication.CreateBuilder();
    var app = builder.Build();

    app.Run(async (context) =>
    {
        var response = context.Response;
        var request = context.Request;
        if (request.Path == "/api/user")
        {
            var responseText = "Некорректные данные";   // содержание сообщения по умолчанию

            if (request.HasJsonContentType())
            {
                // определяем параметры сериализации/десериализации
                var jsonoptions = new JsonSerializerOptions();
                // добавляем конвертер кода JSON в объект типа Person
                jsonoptions.Converters.Add(new PersonConverter());
                // десериализуем данные с помощью конвертера PersonConverter
                var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
                if (person != null)
                    responseText = $"Name: {person.Name}  Age: {person.Age}";
            }
            await response.WriteAsJsonAsync(new { text = responseText });
        }
        else
        {
            response.ContentType = "text/html; charset=utf-8";
            await response.SendFileAsync("html/index.html");
        }
    });

    app.Run();

    public record Person(string Name, int Age);
    public class PersonConverter : JsonConverter<Person>
    {
        public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var personName = "Undefined";
            var personAge = 0;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName?.ToLower())
                    {
                        // если свойство age и оно содержит число
                        case "age" when reader.TokenType == JsonTokenType.Number:
                            personAge = reader.GetInt32();  // считываем число из JSON
                            break;
                        // если свойство age и оно содержит строку
                        case "age" when reader.TokenType == JsonTokenType.String:
                            string? stringValue = reader.GetString();
                            // пытаемся конвертировать строку в число
                            if (int.TryParse(stringValue, out int value))
                            {
                                personAge = value;
                            }
                            break;
                        case "name":    // если свойство Name/name
                            string? name = reader.GetString();
                            if (name != null)
                                personName = name;
                            break;
                    }
                }
            }
            return new Person(personName, personAge);
        }
        // сериализуем объект Person в JSON
        public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("name", person.Name);
            writer.WriteNumber("age", person.Age);

            writer.WriteEndObject();
        }
    }

Пройдемся по коду конвертера 'Person' в JSON.

// Определение конвертера для сериализации/десериализации объекта в JSON
Класс конвертера для сериализации/десериализации объекта определенного типа в JSON должен наследоваться от класса 'JsonConverter<T>'
Абстрактный класс 'JsonConverter' типизируется типом, для объекта которого надо выполнить сериализацию/десериализацию

В коде выше такой реализацией является класс 'PersonConverter'

При наследовании класса 'JsonConverter' необходимо реализовать его абстрактные методы:
- Read() выполняет десериализацию из JSON в Person
- Write() выполняет сериализацию из Person в JSON

Метод 'Write()' принмает:
- Utf8JsonWriter - объект, который записывает данные в JSON
- Person - объект, который надо сериализовать
- JsonSerializerOptions - дополнительные параметры сериализации

    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", person.Name);
        writer.WriteNumber("age", person.Age);
        writer.WriteEndObject();
    }

С помощью объекта 'Utf8JsonWriter' открываем запись объекта в формате JSON:

    writer.WriteStartObject();

Записываем данные объекта 'Person':

    writer.WriteString("name", person.Name);
    writer.WriteNumber("age", person.Age);

Завершаем запись объекта:

    writer.WriteEndObject();

Метод 'Read()' принимает:
- Utf8JsonReader - объект, который читает данные из JSON
- Type - тип, в который надо выполнить конвертацию
- JsonSerializerOptions - дополнительные параметры сериализации

Результатом метода 'Read()' должен быть десериализованный объект,
в данном случае объект типа 'Person'

Определяем данные объекта Person по умолчанию, которые будут применяться,
если в процессе десериализации произойдут проблемы:

    var personName = "Undefined";
    var personAge = 0;

В цикле считываем каждый токен в строке JSON с помощью метода 'Read()' объекта 'Utf8JsonReader':

    while (reader.Read())

Если считанный токен представляет название свойства,
считываем его и считываем следующий токен:

    if (reader.TokenType == JsonTokenType.PropertyName)
    {
        var propertyName = reader.GetString();
        reader.Read();

        После этого можем узнать, как называется свойство и какое значение оно имеет:

        switch (propertyName?.ToLower())
        {

Регистр символов название свойства может отличаться(например, "Age", "age" или "AGE"),
поэтому приводим название свойства к нижнему регистру

Например, ожидаем, что JSON будет содержать свойство с именем 'age',
которое будет хранить число, для его получения применяем следующий блок 'case':

    case "age" when reader.TokenType == JsonTokenType.Number:
        personAge = reader.GetInt32();
        break;

То есть если свойство называется 'age' и представляет число 'JsonTokenType.Number',
вызываем метод 'eader.GetInt32()'

Но свойство 'age' также может содержать строку, например, '23'
Она может конвертироваться в число, для подобного случая добавляем дополнительный блок case:

    case "age" when reader.TokenType == JsonTokenType.String:
        string? stringValue = reader.GetString();
        if (int.TryParse(stringValue, out int value))
        {
            personAge = value;
        }
        break;

        Подобным образом считываем из JSON значение для свойства 'Name':

    case "name":
        string? name = reader.GetString();
        if (name != null)
            personName = name;

Полученными данными инициализируем объект 'Person' и возвращаем его из метода:

    return new Person(personName, personAge);

Таким можем проверить:
- какие свойства имеет объект JSON
- какие значения они несут и принять решения, передавать эти значения в объект 'Person'

Если в присланном JSON не будет нужных свойств, или свойство age будет содержать строку,
которая не конвертируется в число, объект 'Person' все равно будет создан

Чтобы использовать конвертер JSON, его надо добавить в коллекцию конвертеров:

    var jsonoptions = new JsonSerializerOptions();
    jsonoptions.Converters.Add(new PersonConverter());
    var person = await request.ReadFromJsonAsync<Person>(jsonoptions);

