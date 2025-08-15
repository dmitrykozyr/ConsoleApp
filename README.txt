	��������� ������ Keycloak � Docker



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
	
	# ������ 1
	������� ������� (����������, ������� ����� ���������� � Keycloak)
	�������� �������, � �������� �������������� ����� ��������� ����� Web-���������:
	Create client:	
	
		Client type 			OpenID Connect
		Client ID				public-client
		Client authentication	OFF
		Standard flow			true
		Direct access grants	true
		Valid redirect URIs		https://www.keycloak.org/app/* (�������� ���������� �� ����� Keycloak)
		Web origins 			https://www.keycloak.org

	��������� �� �������� ���������� https://www.keycloak.org/app/:	
	
		Keycloak URL	http://localhost:8081
		Realm			MyRealm
		Client			public-client

	�� ��������� �������� ������ ����� � ������ �� �����:
	
		myuser@test.com
		123

	# ������ 2
	Create client:	
	
		Client type 			OpenID Connect
		Client ID				confidential-client
		Client authentication	ON
		Direct access grants	true
		Service accounts roles	true
		Standard flow			true
		Direct access grants	true

		� Credentials ��������:
		Client Secret 			'yb9qiluVu2vIVHrcJhf8laJIaxbZgkRm'