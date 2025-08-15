	Запускаем сервер Keycloak в Docker



	docker run -p 127.0.0.1:8081:8080 -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.3.3 start-dev

	http://localhost:8081/admin/master/console/#/MyRealm/clients

	Create realm:
	
		'MyRealm'

	Create user:
	
		Email verified 	ON
		Username 		MyUser
		Email			myuser@test.com
		First name		MyUser
		Last name		MyUser

	Credentials -> Set password:
	
		Password: 	123
		Temporary: 	Off
	
	# Клиент 1
	Создаем клиента (приложение, которое будет обращаться к Keycloak)
	Создадим клиента, у которого аутентификация будет проходить через Web-интерфейс:
	Create client:	
	
		Client type 			OpenID Connect
		Client ID				public-client
		Client authentication	OFF
		Standard flow			true
		Direct access grants	true
		Valid redirect URIs		https://www.keycloak.org/app/* (тестовое приложение на сайте Keycloak)
		Web origins 			https://www.keycloak.org

	Переходим на тестовое приложение https://www.keycloak.org/app/:	
	
		Keycloak URL	http://localhost:8081
		Realm			MyRealm
		Client			public-client

	На следующей странице вводим логин и пароль дл входа:
	
		myuser@test.com
		123

	# Клиент 2
	Create client:	
	
		Client type 			OpenID Connect
		Client ID				confidential-client
		Client authentication	ON
		Direct access grants	true
		Service accounts roles	true
		Standard flow			true
		Direct access grants	true

		В Credentials копируем:
		Client Secret 			'yb9qiluVu2vIVHrcJhf8laJIaxbZgkRm'