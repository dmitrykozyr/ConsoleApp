namespace Education.Patterns.Behavioral.Memento;

// Этот паттерн — основа функции Ctrl+Z в любом текстовом редакторе
// для отмены последнего действия

// Просим друга запомнить номер, что диктуют по телефону
// Он запоминает
// Нам диктуют новый и старый мы забываем
// Можем попросить друга напомнить его
public class MementoPattern
{
    public void Start()
    {
        var man = new Man();
        var friend = new Friend();

        // Получаем номер телефона
        man.PhoneNumber = "000-000-0000";

        // Просим друга запомнить номер
        friend.PhoneNumber = man.Save();

        // Получаем другой номер, старый забываем
        man.PhoneNumber = "777-777-7777";

        // Просим друга напомнить старый номер
        man.Restore(friend.PhoneNumber);
    }
}
