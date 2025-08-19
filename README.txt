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

Create client:	
	Client ID:				public-client
	Standard flow:			true
	Direct access grants:	true
	Implicit flow			true


	Valid redirect URIs:	https://localhost:7118/* (�� ����� ������ ����������� ���/����������)
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



