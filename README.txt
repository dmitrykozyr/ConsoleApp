// НАСТРОЙКА DOCKER ----------------------------------------------------------------------------------

Запускаем ДЕВ сервер Keycloak в Docker:
	docker run -p 127.0.0.1:8080:8080 -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.3.3 start-dev

Если выдает ошибку, что данный порт недоступен, можно посмотреть, каки он занят процессом:
	netstat -ano | findstr :8080	узнать PID процесса
	tasklist | findstr <PID>		посмотреть имя процесса по PID
	taskkill /PID <PID> /F			завершить процесс по PID

Или указать другой порт, например, 8081
После запуска локального сервера, переходим в UI:
	http://localhost:8081/

// РАБОТА В UI Keycloak ------------------------------------------------------------------------------

Создаем Realm:	
	Realm name:		MyRealm

Создаем User:	
	Email verifie: 	ON
	Username: 		MyUser
	Email:			myuser@test.com
	First name:		MyUser
	Last name:		MyUser

	Задаем парорль Credentials -> Set password:	
		Password: 	123
		Temporary: 	Off

Клиент - это приложение, которое будет обращаться к Keycloak
Создаем публичного клиента у которого аутентификация будет проходить через Web-интерфейс:	
	Create client:	
		Client type: 			OpenID Connect
		Client ID:				public-client
		Client authentication:	OFF
		Standard flow:			true
		Direct access grants:	true
		Valid redirect URIs:	https://www.keycloak.org/app/* (тестовое приложение на сайте Keycloak)
		Web origins: 			https://www.keycloak.org

	Переходим на тестовое приложение https://www.keycloak.org/app/:	
		Keycloak URL:	http://localhost:8081
		Realm:			MyRealm
		Client:			public-client

	На следующей странице вводим логин и пароль для входа:	
		myuser@test.com
		123

	Видим, что аутентификация прошла успешно

Создаем конфиденциального клиента:
	Create client:	
		Client type: 			OpenID Connect
		Client ID:				confidential-client
		Client authentication:	ON
		Standard flow:			true
		Direct access grants:	true
		Service accounts roles:	true

		В Credentials копируем:
		Client Secret: 			'.................'

// НАСТРОЙКА POSTMAN ---------------------------------------------------------------------------------

Access Token:
	POST http://localhost:8081/realms/MyRealm/protocol/openid-connect/token
		grant_type		password
		client_id		public-client
		scope			email openid
		username		myuser@test.com
		password		123

Refresh Token:
	POST http://localhost:8081/realms/MyRealm/protocol/openid-connect/token
		grant_type		refresh_token
		client_id		public-client
		refresh_token	[refresh_token из прошлого запроса]

Access Token Confidential (для взаимодействия сервер-сервер):
	POST http://localhost:8081/realms/MyRealm/protocol/openid-connect/token
		grant_type		client_credentials
		client_id		confidential-client
		client_secret	[Client Secret '.................']
		scope			openid
