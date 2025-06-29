class CICD_
{
    #region Создание Docker Image и его пуш в DockerHub

        // В VS Code устанавливаем расширение Docker и в терминале вводим
        docker login

        // docker build -t          собираем Image
        // dockeredukozyr/testtask  логин и название Image в DockerHub
        // .\JobSearcherRepo\       путь к Dockerfile в проекте
        docker build -t dockeredukozyr/testtask .\JobSearcherRepo\

        // посмотреть список Image
        docker images

        // latest                   необязательное указание версии
        // docker push              загрузить Image
        // После пуша увидим Image в DockerHub
        // https://hub.docker.com/repository/docker/dockeredukozyr/testtask/general
        docker push dockeredukozyr/testtask:latest

        // удалить локальный образ
        docker image rm dockeredukozyr/testtask

        // выгрузить себе Image
        docker pull dockeredukozyr/testtask

        // 3001                     порт
        // 80                       порт из Dockerfile из поля EXPOSE
        // name                     имя контейнера создаваемного
        // dockeredukozyr/testtask  имя Image
        docker run -p 3001:80 --name testtask dockeredukozyr/testtask

        // Если локально установлен Docker, то сможем увидеть наш Image и запущенный контейнер

    #endregion

    #region Настройка GitLab

        /*
            Создаем репозиторий и в корне проекта создаем файл .gitlab-ci.yml со следующим содержимым:
            - stages    определяет стадии CI/CD (build, test, deploy)
            - variables нужно заменить your-dockerhub-username/your-image-name на имя пользователя и имя образа в DockerHub
            - image     указывает, что для выполнения задач будет использоваться официальный образ Docker
            - services  указывает, что нужно использовать Docker-in-Docker (dind) для работы с Docker внутри CI/CD
            - script    содержит команды, которые будут выполнены:
                • docker build  строит Docker образ из текущей директории
                • docker login  логинится в DockerHub - здесь используются переменные окружения для безопасности, которые нужно добавить в GitLab
                • docker push   загружает собранный образ в Docker Hub

            Для добавления переменных окружения в GitLab -> Settings -> CI/CD -> Variables добавим переменные:
            • Key: DOCKERHUB_USERNAME, Value: имя пользователя Docker Hub
            • Key: DOCKERHUB_PASSWORD, Value: пароль от Docker Hub

            Теперь при каждом коммите в репозиторий будет автоматически запускаться процесс сборки и загрузки образа в DockerHub
        */

        stages:
            - build
            - test
            - deploy

        variables:
            DOCKER_IMAGE: dockeredukozyr/testtask
            DOCKER_TAG: latest

        build-job:
            stage: build
            image: docker:latest
            services:
            - docker:dind
            script:
            - echo "Building the Docker image..."
            - docker build -t $DOCKER_IMAGE:$DOCKER_TAG .
            - echo "Logging in to Docker Hub..."
            - echo "$DOCKERHUB_PASSWORD" | docker login -u "$DOCKERHUB_USERNAME" --password-stdin
            - echo "Pushing the Docker image to Docker Hub..."
            - docker push $DOCKER_IMAGE:$DOCKER_TAG

        unit-test-job:
            stage: test
            script:
            - echo "Running tests 1"

        lint-test-job:
            stage: test
            script:
            - echo "Running tests 2"

        deploy-job:
            stage: deploy
            environment: production
            script:
            - echo "Deploying application"

    #endregion


}
