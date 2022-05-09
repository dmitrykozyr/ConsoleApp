#region JWTToken
Тестирем проект в Postman

Генерация токена
https://localhost:7269/api/login
Postman -> Body > raw -> JSON
    {
        "Username": "User1",
        "Password": "pass_1"
    }

Эндпоинт, не требующий авторизации и аутентификации
https://localhost:7269/api/user/public

Эндпоинт, требующий токен юзера с ролью 'Administrator'
Вставляем его в Postman -> Auth -> Bearer Token
https://localhost:7269/api/user/admins

#endregion
