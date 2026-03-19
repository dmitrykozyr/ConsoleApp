NuGet prometheus-net.AspNetCore

Grafana — это пустая картинная галерея
Prometheus — это склад с красками
Чтобы на стенах появились картины (графики), нужно подключить склад к галерее,
а потом сказать художнику, что рисовать
	 
Запуск (Docker)
	Самый простой способ поднять стек — файл docker-compose.yml
	Он создаст сразу и хранилище (Prometheus), и визуализатор (Grafana).
	 
	 yaml
	 services:
	   prometheus:
	  image: prom/prometheus
	  ports:
		- "9090:9090"
	  volumes:
		- ./prometheus.yml:/etc/prometheus/prometheus.yml

	   grafana:
	  image: grafana/grafana
	  ports:
		- "3000:3000" # Grafana будет доступна тут

Вход в интерфейс
	Открываем браузер: http://localhost:3000
	Логин/пароль по умолчанию: admin/admin
	 
Подключение источника данных (Data Source)
	Grafana должна узнать, откуда брать цифры
	На левой панели нажми на иконку шестеренки Connections -> Data Sources -> Add data source -> Prometheus
	В поле URL вводим: http://prometheus:9090 (если в докере) или http://localhost:9090
	Жмем Save & Test
	Если загорелась зеленая галочка — связь установлена
	 
Создание первого графика (Dashboard)
	Нарисуем график заказов из C# кода
	Жмем на квадратики (Dashboards) в меню слева -> New -> New Dashboard -> Add visualization
	Выбераем источник Prometheus
	В поле Query (запрос) вводим имя метрики, которую создали в C#: shop_orders_total
	В правой панели выбераем тип графика, например, Stat для одного числа или Time series для линии во времени
	Жмем Apply
	 
Готовые дашборды
	Разработчики уже нарисовали тысячи дашбордов на сайте Grafana Dashboards
	Ищем "ASP.NET Core"
	В Grafana жмем Dashboards -> New -> Import
	Вставляем ID, жмем Load
	Загрузится профессиональная панель мониторинга со всеми графиками сразу
	Теперь C# каждую секунду пишет в память: "Заказов стало 105"
	Prometheus раз в 15 секунд заходит в приложение и забирает число "105" к себе в историю
	Grafana каждую секунду спрашивает у Prometheus данные за последние 5 минут и рисует графики
	Можно настроить Alerting (оповещение), чтобы Grafana писала в Telegram, если заказы упали до нуля
