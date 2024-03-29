﻿// Откат миграции
//git checkout develop
//Remove-Migration
//Update-Database 2021080100_NameOfPreviousMigration
// Проектом по умолчанию сделать проект, работающий с базой
//Add-Migration NameOfNewMigration

//============================= Git ==============================
// Можно созтаь .bat файл и прописать команды git, после запуска выполнятся одна за другой, коммент ::
init
status
add XXX
add -A              // Добавить все файлы
add -u              // Добавить только обновленные файлы, но не новые
commit dima.txt -m «..»
commit -am          // Добавить только обновленные файлы
checkout XXX.txt    // Отмена изменений в файле
clean -f            // Удалить недобавленные файлы
diff XXX..YYY       // Различия между коммитами
log
pull
push
reset —hard         // Отмена изменений во всех файлах
reset —soft HEAD~1  // Вернуть состояние как перед последним коммитом

// Объединить коммиты
git rebase -i HEAD~5
в vim для ввода нажать I
напротив нужных коммитов вместо pick написать squash
закрыть vim: esc: wq
разрешить конфликты в VS
git rebase --continue
в vim закомментить # ненужные комментарии
git push -f

// Rebase
git checkout develop
git pull
git checkout <branch>
git rebase develop
если конфликт - разрешаем, потом git rebase --continue
git push -f

// Не пулятся изменения из develop
// Проблема с локальным develop - гит перестал трекать изменения в девелопе
// Зайти в SourceTree и удалить develop, потом сделать в консоли git checkout develop
// git pull

// Добавить изменения в существующий коммит, чтобы не делать сквош
в SourceTree перевести изменения в staged
git commit --amend
закрыть vim: esc: wq
git push - f

// Cherry Pick
// Если работаем в одной ветке, а нам нужно перенести себе в ветку изменения из другой ветки, то можно сделать rebase, но он перенесет все коммиты
// Cherry pick позволяет перенести один конкретный коммит, нужно только указать его хеш
git cherry-pick 54debc8

// Force Push
// Git предотвращает перезапись истории центрального репозитория, отклоняя push-запросы, если нельзя выполнить их ускоренное слияние
// Если история удаленного репозитория отличается от вашей истории, необходимо загрузить удаленную ветку командой pull и выполнить ее слияние с локальной веткой командой merge, а затем выполнить команду push
// Флаг --force отменяет это поведение и подгоняет ветку удаленного репозитория под локальную ветку, удаляя вышестоящие изменения, которые могли быть внесены с момента последнего выполнения команды pull
// Использование push оправдано в том случае, когда вы понимаете, что только что опубликованные вами коммиты были неправильными и вы исправили их с помощью команды git commit --amend или интерактивного перебазирования
// При этом прежде, чем использовать опцию --force, вы должны быть уверен, что никто из команды не забирал эти коммиты с помощью команды pull

// Если конфликты после пуша
git fetch
git merge origin/develop
// Разрешаем конфликты в VisualStudio

//============================ Blender ===========================
k -> грань -> c     // Заблокировать нож под прямым углом
f                   // Объединить
p -> selection      // Отделить элемент модели в отдельную модель
n                   // Ссвойства
t                   // Инструменты
m                   // Переместить в другую коллекцию
~                   // Угол обора
r + x + 90          // Вращать объект по оси x на 90 градусов
d + лкм             // Рисовать 
d + пкм             // Стереть
file -> append      // Импортировать объект из другого .blend файла
i + i               // Если выбрать смежные полигоны и нажать I + I, то для каждого выделение будет происходить отдельно
g + g               // Объединить выбранную вершину/ребро с другой, потом сделать Remove Doubles
b                   // Выделение прямоугольником
с                   // Выделение кругом
1 2 3               // Вершины / грани / полигоны
subdivide           // Разбить полигон на мелкие несколько полигонов
ctrl + r            // Разрезать на две части
ctrl + alt + q      // Включить 4 режима отображения модели
ctrl + space        // Во весь экран
ctrl + b            // Добавить грани
alt + пкм на грани  // Выделить всю грань по кругу
h, alt + h          // Спрятать/отобразить выделение
shift + ~           // Полет камеры
alt + e             // Каждый полигон экструдится независимо от дугого
shift + d           // Дублировать объект
shift + c           // Центрировать курсор
ctrl + e -> bridge edge loops  // Соединение двух параллельных граней с большим числом точек

//============================== Риг =============================
- Выбираем модель человека, ctrl+a, all transforms - применить scale
- shift+a, armature - создать кость
- Справа выбираем вкладку armature, viewport display, in front - стображение кости сквозь модель
- На вкладке bones переименовываем все кости [name.L], которые будут иметь зеркальное отражение
- shift+s, cursor to world origin - переместить курсор в центрор
- Выбираем все кости, которые нужно зеркально отразить
- shift+d, enter, ctrl+m, x - сдклать дубликат и зеркально отразить по оси x
- Если группы костей - не один объект, выделяем их все, ctrl+j - объединить
- выделяем кость плеча и спины, ctrl+p, keep offset - соединить, аналогично с ногами и root bone
- В object mode выделяем модель, а потом и риг, ctrl+p, with automatic weigths
- Выбираем модель, переходим в WeightPaintMode, shift+lmb, выбираем кость и рисуем для нее веса