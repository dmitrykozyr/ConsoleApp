// НАСТРОЙКА DOCKER ----------------------------------------------------------------------------------

Запускаем ДЕВ сервер Keycloak в Docker:
	docker run -d --name keycloak -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin -p 8080:8080 quay.io/keycloak/keycloak:26.3.3 start-dev

Если выдает ошибку, что данный порт недоступен, можно посмотреть, каким он занят процессом:
	netstat -ano | findstr :8080	узнать PID процесса
	tasklist | findstr <PID>		посмотреть имя процесса по PID
	taskkill /PID <PID> /F			завершить процесс по PID

Или указать другой порт, например, 8081
После запуска локального сервера, переходим в UI:
	http://localhost:8081/

// РАБОТА В UI Keycloak ------------------------------------------------------------------------------

Создаем Realm:	
	Realm name:		MyRealm

Create client:	
	Client ID:				public-client
	Client authentication	on
	Standard flow:			true
	Direct access grants:	true
	Implicit flow			true

	Valid redirect URIs:	https://localhost:7118/signin-oidc/* (по этому адрему запускается АПИ/приложение)
	Web origins: 			https://localhost:7118

Создаем User:	
	Email verifie: 	ON
	Username: 		MyUser
	Email:			myuser@test.com
	First name:		MyUser
	Last name:		MyUser

	Задаем парорль Credentials -> Set password:	
		Password: 	MyUser
		Temporary: 	Off

// Запуск проекта ------------------------------------------------------------------------------------

Заупскаем сервер с Keycloak в Docker или через .exe
Запускаем проект

В сваггере нажимаем Authorize:
	Client ID:		public-client
	openid:			true
	profile:		true

Попаадем в окно аутентификации Keycloak:
	login:			user1
	password:		user1

Мы аутентифицированы и теперь можем слать запросы в сваггере
