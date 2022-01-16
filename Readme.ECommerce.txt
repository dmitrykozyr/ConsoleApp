Создание проекта: ASP.NET Core Web Application
Имя ECommerce.Api.Products
Выбираем API, убираем галочку Configure for HTTPS

Нажимаем на солюшене ПКМ -> Set startup project -> Выбираем четыре проекта как Startup, чтобы они все запускались при старте программы

Проект Products.Test создан на основе xUnit .Net Core
Добавляем ссылку на проект Products, т.к. будем его тестировать

CI/CD
Чтобы автоматизировать запуск тестов, создадим проект на https://azure.microsoft.com/en-us/services/devops/
https://dmitrykozyr.visualstudio.com/Ecommerce
https://www.linkedin.com/learning/azure-microservices-with-dot-net-core-for-developers/creating-an-azure-devops-project?autoAdvance=true&autoSkip=true&autoplay=true&resume=false&u=2113185
Нажимаем Repos -> Import -> Импортируем наш репозиторий
Нажимаем Pipelines -> New Pipeline -> Выбираем стандартное отображение -> Continue -> В поиске шаблонов ищем .Net Core
Добавляем Job типа Use .Net Core -> и перетаскиваем вверх -> Вводим версию 3.1.x
Выбираем Publish и убираем галочку Publish Web Projects
Нажимаем Save & queue -> Save and run
Чтобы включить CI, переходим на Pipelines -> Edit -> На вкладке Triggers галочка Enable continious integration -> Save & queue -> Save and run
Теперь билд будет запускаться при каждом коммите в мастер

Docker
Чтобы добавить возможность контейниризации, на каждом из четырех проектов нажимаем ПКМ -> Add -> Container Orchestrator Support -> Docker Compose -> Windows
После этого в папке с проектом создастся dockerfile, а в папке с солюшеном создастся docker-compose, где регистрируется каждый добавленный сервис
Так-же при запуске проектом по умолчанию будет docker-compose
Во всех четырех сервисах в dockerfile 1903 меняем 1803 для совместимости
В файле docker-compose.override.yml меняем порты для разных сервисов 600x
В сервисе Orders -> Properties -> launchSettings.json удаляем строку "sslPort": 0
До конца не разобрался -  при запуске docker-compose выдает ошибку
