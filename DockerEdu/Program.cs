using System;

namespace DockerEdu
{
    class Program
    {
        // Установить докер с офф сайта и расширение в VSCode
        // Чтобы создать Dockerfile, в VS нажимаем пкм на проекте -> Add -> Docker Support
        // - docker
        // - docker version
        // - docker image ls            cмотрим id сгенерированного image(образа) ''
        // - docker images
        // - docker build .
        // - docker run[name of image]
        // - docker push                запушить репозиторий в hub.docker.com, там есть только 
        //                              один приватный бесплатный репозиторий, пушить необязательно

        // Если нужны настройки для работы с Node.js, гуглим docker hub node
        // С сайта https://hub.docker.com/_/node скопировать в терминал команду docker pull node
        static void Main()
        {
            Console.WriteLine("Docker");
        }
    }
}
