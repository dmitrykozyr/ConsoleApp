Event Sourcing — это способ хранения данных, где сохраняем не текущее состояние,
а все изменения (события), которые с этим состоянием происходили

Вместо «что есть сейчас» храним «что происходило»



Представм банковский счёт.

Обычный подход:

	Храним значение:

		Баланс = 1500₽


Event Sourcing:

	Храним историю, а значение считаем:

		+1000₽ (пополнение)
		+700₽  (пополнение)
		-200₽  (покупка)

		Баланс = сумма всех событий = 1500₽


Принцип работы

	1. Есть события (Events)

		Это факты, которые уже произошли:
		- UserRegistered
		- MoneyDeposited
		- MessageSent
		- OrderCreated

		События неизменяемы, их нельзя редактировать

	2. Состояние = результат событий

		Не храним:

			Balance = 1500

		А считаем:

			balance = events.Sum(e => e.Amount);

	3. Добавление данных = новое событие

		Вместо:

			UPDATE balance = 2000

		Делаем:

			+500₽ (новое событие)










//!
CREATE TABLE IF NOT EXISTS events (
    id BIGSERIAL PRIMARY KEY,
    aggregate_id UUID NOT NULL,
    event_type TEXT NOT NULL,
    event_data JSONB NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE INDEX IF NOT EXISTS ix_events_aggregate ON events (aggregate_id, id);
