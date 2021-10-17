using System;

namespace DockerEdu
{
    class Program
    {
        // Установить докер с офф сайта и расширение в VSCode
        // Чтобы создать Dockerfile, в VS нажимаем пкм на проекте -> Add -> Docker Support

        // docker
        // docker version
        // docker image ls      cмотрим id сгенерированного image(образа) ''
        // docker images
        // docker build .
        // docker run [name of image]
        // docker push          запушить репозиторий в hub.docker.com, там есть только 
        //                      один приватный бесплатный репозиторий, пушить необязательно
        // docker ps            список контейнеров
        // docker ps -a         список запущенных контейнеров
        // docker rm [container id]                 удалить контейнер
        // docker container prune                   удалить все контейнеры
        // docker run -p 3000:3000 10a68f3b51b7     запускаем контейнер на основе образа и указываем два порта:
        //                                          первый - локальный, второй - какой порт из контейнера
        //                                          хотим замапить на локальный
        // docker stop [container id]       остановить контейнер
        // docker attach [container id]     переключиться на указанный контейнер
        // docker Logs [container id]       логи
        // docker rmi [image id]            удалить image
        // docker image prune               удалить все неиспользуемые image
        // docker build . -t logsapp        t - присвоить тег нашему image, чтобы обращаться не по id

        // Запушить image в docker hub
        // docker tag [old name] [docker hub account name]/[new name]   переименовать image
        // docker push [docker hub account name]/[new name]:[tag]       запушить

        // docker run -d -p 3000:3000 --name logsapp --rm [image id]
        // -d не блокировать терминал после запуска
        // -p порт
        // --rm удалить контейнер после первой остановки

        // Если нужны настройки для работы с Node.js, гуглим docker hub node
        // С сайта https://hub.docker.com/_/node скопировать в терминал команду docker pull node

        // Когда создаем image, то фиксируем в нем код. Если потом изменим код, то нужно остановить
        // контейнеры и сделать билд, тем самым создав новый image, а с ним создаем через run новый контейнер


        // Ошибка при запуске:
        // docker client must be run with elevated privileges to connect.: Get docker_engine/v1.24/version": 
        // open //./pipe/docker_engine: The system cannot find the file specified
        // With Powershell:
        // Open Powershell as administrator
        // Launch command: & 'C:\Program Files\Docker\Docker\DockerCli.exe' -SwitchDaemon
        // OR, with cmd:
        // Open cmd as administrator
        // Launch command: "C:\Program Files\Docker\Docker\DockerCli.exe" -SwitchDaemon
        static void Main()
        {
            Console.WriteLine("Docker");
        }
    }
}
