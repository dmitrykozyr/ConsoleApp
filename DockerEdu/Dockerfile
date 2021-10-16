#FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1903 AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
#WORKDIR /src										# указываем рабочую дирректорию
#COPY ["DockerEdu/DockerEdu.csproj", "DockerEdu/"]	# копируем файл проекта
#RUN dotnet restore "DockerEdu/DockerEdu.csproj"		# выполняем команду - скачать все зависимости из NuGet
#COPY . .
#WORKDIR "/src/DockerEdu"
#RUN dotnet build "DockerEdu.csproj" -c Release -o /app/build		# запускаем билд проекта
#
#FROM build AS publish
#RUN dotnet publish "DockerEdu.csproj" -c Release -o /app/publish	# делаем, чтобы все зависимости лежали внутри контейнера
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "DockerEdu.dll"]				# точка входа

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY ../engine/examples ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
