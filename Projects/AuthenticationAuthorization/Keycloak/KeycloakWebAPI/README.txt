// ��������� DOCKER ----------------------------------------------------------------------------------

��������� ��� ������ Keycloak � Docker:
	docker run -d --name keycloak -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin -p 8080:8080 quay.io/keycloak/keycloak:26.3.3 start-dev

���� ������ ������, ��� ������ ���� ����������, ����� ����������, ����� �� ����� ���������:
	netstat -ano | findstr :8080	������ PID ��������
	tasklist | findstr <PID>		���������� ��� �������� �� PID
	taskkill /PID <PID> /F			��������� ������� �� PID

��� ������� ������ ����, ��������, 8081
����� ������� ���������� �������, ��������� � UI:
	http://localhost:8081/

// ������ � UI Keycloak ------------------------------------------------------------------------------

������� Realm:	
	Realm name:		MyRealm

Create client:	
	Client ID:				public-client
	Client authentication	on
	Standard flow:			true
	Direct access grants:	true
	Implicit flow			true

	Valid redirect URIs:	https://localhost:7118/signin-oidc/* (�� ����� ������ ����������� ���/����������)
	Web origins: 			https://localhost:7118

������� User:	
	Email verifie: 	ON
	Username: 		MyUser
	Email:			myuser@test.com
	First name:		MyUser
	Last name:		MyUser

	������ ������� Credentials -> Set password:	
		Password: 	MyUser
		Temporary: 	Off

// ������ ������� ------------------------------------------------------------------------------------

��������� ������ � Keycloak � Docker ��� ����� .exe
��������� ������

� �������� �������� Authorize:
	Client ID:		public-client
	openid:			true
	profile:		true

�������� � ���� �������������� Keycloak:
	login:			user1
	password:		user1

�� ����������������� � ������ ����� ����� ������� � ��������
