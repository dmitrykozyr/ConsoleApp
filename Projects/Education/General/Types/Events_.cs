namespace Education.General.Types;

public class Events_
{
    /*
        Позволяют объектам уведомлять другие объекты, что что-то произошло
        Например событие уничтожения врага в игре
        В других классах подписываемся на это событие
        и выполняем определенные действия в этих классах, когда событие происходит

        Делегат определяет сигнатуру метода, который будет вызываться при возникновении события

        Событие объявляется с использованием делегата и используется для вызова методов подписчиков

        Подписчик - метод, который подписывается на событие и будет вызван, когда событие произойдет

        Пример:
        1. Определяем делегат ClickEventHandler, принимающий два параметра: отправитель события и объект EventArgs
        2. На основе делегата объявляем событие Clicked
        3. Метод OnClick генерирует событие Clicked, если на него есть подписчики
        4. В методе Main создаем экземпляр Button, подписываемся на событие Clicked и указываем метод-обработчик Button_Clicked
        5. Метод Button_Clicked вызывается, когда происходит событие Clicked
    */

    public class Button
    {
        public delegate void ClickEventHandler(object sender, EventArgs e);

        public event ClickEventHandler? Clicked;

        public void OnClick()
        {
            // Проверяем, есть ли подписчики на событие
            if (Clicked is not null)
            {
                // Генерируем событие
                Clicked(this, EventArgs.Empty);
            }
        }
    }

    public class Program
    {
        static void Main_()
        {
            var button = new Button();

            // Подписка на событие
            button.Clicked += Button_Clicked;

            // Имитация нажатия кнопки
            button.OnClick();
        }

        private static void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Кнопка была нажата!");
        }
    }
}
