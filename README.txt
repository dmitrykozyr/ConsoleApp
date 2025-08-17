// ��������� DOCKER ----------------------------------------------------------------------------------

��������� ��� ������ Keycloak � Docker:
	docker run -p 127.0.0.1:8080:8080 -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.3.3 start-dev

���� ������ ������, ��� ������ ���� ����������, ����� ����������, ���� �� ����� ���������:
	netstat -ano | findstr :8080	������ PID ��������
	tasklist | findstr <PID>		���������� ��� �������� �� PID
	taskkill /PID <PID> /F			��������� ������� �� PID

��� ������� ������ ����, ��������, 8081
����� ������� ���������� �������, ��������� � UI:
	http://localhost:8081/

// ������ � UI Keycloak ------------------------------------------------------------------------------

������� Realm:	
	Realm name:		MyRealm

������� User:	
	Email verifie: 	ON
	Username: 		MyUser
	Email:			myuser@test.com
	First name:		MyUser
	Last name:		MyUser

	������ ������� Credentials -> Set password:	
		Password: 	123
		Temporary: 	Off

������ - ��� ����������, ������� ����� ���������� � Keycloak
������� ���������� ������� � �������� �������������� ����� ��������� ����� Web-���������:	
	Create client:	
		Client type: 			OpenID Connect
		Client ID:				public-client
		Client authentication:	OFF
		Standard flow:			true
		Direct access grants:	true
		Valid redirect URIs:	https://www.keycloak.org/app/* (�������� ���������� �� ����� Keycloak)
		Web origins: 			https://www.keycloak.org

	��������� �� �������� ���������� https://www.keycloak.org/app/:	
		Keycloak URL:	http://localhost:8081
		Realm:			MyRealm
		Client:			public-client

	�� ��������� �������� ������ ����� � ������ ��� �����:	
		myuser@test.com
		123

	�����, ��� �������������� ������ �������

������� ����������������� �������:
	Create client:	
		Client type: 			OpenID Connect
		Client ID:				confidential-client
		Client authentication:	ON
		Standard flow:			true
		Direct access grants:	true
		Service accounts roles:	true

		� Credentials ��������:
		Client Secret: 			'.................'

// ��������� POSTMAN ---------------------------------------------------------------------------------

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
		refresh_token	[refresh_token �� �������� �������]

Access Token Confidential (��� �������������� ������-������):
	POST http://localhost:8081/realms/MyRealm/protocol/openid-connect/token
		grant_type		client_credentials
		client_id		confidential-client
		client_secret	[Client Secret '.................']
		scope			openid
