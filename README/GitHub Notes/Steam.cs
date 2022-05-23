//========================== SteamSDK ============================
// Для обновления выполняем шаги, отмеченные +
Скачиваем Steam SDK и разархивируем на диск E, чтобы путь к папке не содержал русских букв
+ В E:\sdk\tools\ContentBuilder\content создаем папки Windows и Linux, кидаем туда соответствующие билды
В E:\sdk\tools\ContentBuilder\scripts есть два текстовых файла
В названиях этих файлов и в них самих меняем 1000 на ID приложения (приложение уже должно быть зарегистрировано в Steam),
а 1001 — на ID хранилища (обычно, на 1 больше, чем ID приложения)
Если загружаем сборки для Windows и Linux, то дублируем файл depot_build и в него вписываем ID второго хранилища,
которое заранее нужно создать и опубликовать
Итого, имеем один файл app_build и два файла depot_build, в которых в строке ContentRoot задаем путь к сборкам
+ В E:\sdk\tools\ContentBuilder\builder запустить steamcmd и ввести login [логин], [пароль], [код], присланный на почту
+ Вводим run_app_build E:\sdk\tools\ContentBuilder\scripts\app_build_742470.vdf

//======================== Steamworks ============================
+ В Изменить настройки приложения->SteamPipe -> Сборки устанавливаем последнюю сборку как default
Если мы загрузили билды Windows и Linux, то их хранилища объединить в одну сборку и пометить ее как default
В Изменить настройки приложения->Установка->Общее настроить способ запуска для обоих билдов
В Изменить настройки приложения->Приложение->Общие и в Изменить страницу в магазине отметить поддерживаемые ОС
В Все связанные комплекты..->Комплекты->Включенные хранилища должны быть включены два хранилища — с билдом Windows и Linux

//======================== app_build_XXXXX0 ======================
"appbuild"
{
    "appid" "742470"
    "desc" "Your build description here"
    "buildoutput" "..\output\"
    "contentroot" "..\content\"
    "setlive"   ""
    "preview" "0"
    "local" ""
    "depots"
    {
        "742471" "depot_build_742471.vdf"
        "742472" "depot_build_742472.vdf"
    }
}

//================= depot_build_742471 (Windows) =================
"DepotBuildConfig"
{
    "DepotID" "742471"

    "ContentRoot" "E:\sdk\tools\ContentBuilder\content\Windows"

  "FileMapping"
  {
        "LocalPath" "*"
    "DepotPath" "."
    "recursive" "1"
  }

    "FileExclusion" "*.pdb"
}

//================= depot_build_XXXXX2 (Linux) ===================
Дублируем предыдущий файл, вместо XXXXX1 пишем XXXXX2 и путь меняем на E:\sdk\tools\ContentBuilder\content\Linux
https://www.youtube.com/watch?v=SoNH-v6aU9Q