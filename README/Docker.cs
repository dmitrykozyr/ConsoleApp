Установить докер с офф сайта и расширение в VSCode
Чтобы создать Dockerfile, в VS нажимаем пкм на проекте -> Add -> Docker Support
Когда создаем image - фиксируем в нем код
Если потом изменим код - нужно остановить контейнеры и сделать билд, создав новый image
C ним создаем через run новый контейнер


docker build --tag [имя image маленькими буквами] .	// создать image
                                                    // Точка означает текущую директорию
docker images                                       // список images
docker run -it -p 8080:8081 [имя или id image] --rm // запускаем контейнер на основе образа (image) и указываем два порта:
                                                    // - первый - локальный
                                                    // - второй - какой порт из контейнера хотим замапить на локальный
                                                    // rm - удалить контейнер после запуска
                                                    // it - запуск контейнера в интерактивном режиме, выйти из его через CTRL + D
                                                    // После запуска сервис станет доступен по адресу http://localhost:8080/
docker login                                        // залогиниться в DockerHub для дальнейшего пуша
docker push                                         // запушить репозиторий в DockerHub
docker rm [container id]                            // удалить контейнер
docker container prune                              // удалить все контейнеры
docker rmi [image id]                               // удалить image
docker image prune                                  // удалить все неиспользуемые image


// Dockerfile (без расширения)

    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    USER app
    WORKDIR /app
    EXPOSE 8080
    EXPOSE 8081

    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    ARG BUILD_CONFIGURATION=Release
    WORKDIR /src
    COPY ["JobSearcher/JobSearcher.csproj", "JobSearcher/"]
    COPY["TodoList.Domain/TodoList.Domain.csproj", "TodoList.Domain/"]
    RUN dotnet restore "./JobSearcher/JobSearcher.csproj"
    COPY . .
    WORKDIR "/src/JobSearcher"
    RUN dotnet build "./JobSearcher.csproj" -c $BUILD_CONFIGURATION -o /app/build

    FROM build AS publish
    ARG BUILD_CONFIGURATION=Release
    RUN dotnet publish "./JobSearcher.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost = false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "JobSearcher.dll"]


// docker-compose.yml

Если есть АПИ и БД на PosgreSQL, и они находятся в двух разных контейнерах - их нужно связать, это делем в этом файле
Здесь важны отступы через TAB

    version: "3.9"                          // версия docker-compose
    networks:                               // устанавливаем связь между контейнерами, чтобы они могли общаться
        deploy-guide-dev:
            driver: bridge                  // bridge - самый часто используемый тип сети
    services:
        app:                                // НАСТРОЙКА ПРИЛОЖЕНИЯ
            container_name: deploy-guide    // имя контейнера
            build:                          // указываем, как собирать образ
                context: .
                dockerfile: Dockerfile      // путь к Dockerfile, тут он находится там-же, где и docker-compose
            ports:
                -"80:80"                    // первый порт вводим в браузере для доступа к сервису
                                            // второй - внутренний, к нему приложение обращается внутри контейнера
            networks:
                - deploy-guide-dev          // название сети
            depends_on:                     // список зависимостей
                - postgres_db               // пока postgres_db не запустится - приложение не стартанет 
    
        postgres_db:                        // НАСТРОЙКА POSTGRESQL 
            container_name: postgres        // имя контейнера
            image: postgres: latest         // название образа, берется с DockerHub
            environment:                    // настройка подключения к БД
                POSTGRES_USER: postgres
                POSTGRES_PASSWORD: 123
                POSTGRES_DB: deploy-guide   // имя БД
            ports:
                - "5432:5432"               // стандартные порты PostgreSQL
            networks:
                - deploy-guide-dev
            volumes:                        // volumes нужен, чтобы записывать данные не внутрь контейнера,
                                            // а наружу на диск самого компьютера, чтобы данные не пропадали
                - postgres-data:/var/lib/postgresql/data
    volumes:                                // список всех volume
        postgres-data:                      // указываем вышеописанный volume
