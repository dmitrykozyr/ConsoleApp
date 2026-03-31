Event Sourcing — это способ хранения данных, где сохраняем не текущее состояние,
а все изменения (события), которые с этим состоянием происходили

События неизменяемы, их нельзя редактировать
Вместо «что есть сейчас» храним «что происходило»

Представм банковский счёт
При обычном подходе храним значение:

	Баланс = 1500₽


При Event Sourcing подходе храним историю, а значение считаем:

	+1000₽ (пополнение)
	+700₽  (пополнение)
	-200₽  (покупка)

	Баланс = сумма всех событий = 1500₽
	

Нужно выпонить в БД, чтобы программа заработала

	CREATE TABLE IF NOT EXISTS events (
		id BIGSERIAL PRIMARY KEY,
		aggregate_id UUID NOT NULL,
		event_type TEXT NOT NULL,
		event_data JSONB NOT NULL,
		created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
	);

	CREATE INDEX IF NOT EXISTS ix_events_aggregate ON events (aggregate_id, id);
