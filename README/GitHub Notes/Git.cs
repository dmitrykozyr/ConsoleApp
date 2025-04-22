class A
{
    #region Откат миграции

        git checkout develop

        Remove-Migration

        Update-Database 2021080100_NameOfPreviousMigration

        // Проектом по умолчанию сделать проект, работающий с базой
        Add-Migration NameOfNewMigration

    #endregion

    #region Создание .bat файла

        // В .bat файле можно прописать команды git, после запуска выполнятся одна за другой
        // Комментарий ::
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

    #endregion

    #region Разница между merge и rebase
    /*
        1.Структура истории

            Git Merge cоздает новый коммит слияния (merge commit), который объединяет изменения из двух веток
            История сохраняется в виде "разветвленной" структуры, что позволяет видеть, когда и как происходило слияние
            Если есть ветка feature и вы сливаете её в main, то будет создан новый коммит, который объединяет обе ветки

            Git Rebase: перемещает или "переписывает" коммиты из одной ветки на вершину другой - это создает "линейную" историю
            Коммиты из ветки feature будут применены к самой последней версии ветки main, как будто они были созданы после последнего коммита в main
            Если выполнить rebase ветки feature на main, то коммиты из feature будут "переделаны" так, как если бы они были созданы после последнего коммита в main

        2. Использование

            Git Merge используется для объединения завершенных функций или исправлений в основную ветку
            Подходит для командной работы, когда важно сохранить историю всех изменений и их контекст

            Git Rebase часто используется для "чистки" истории перед слиянием, чтобы сделать её более линейной и понятной
            Полезен для обновления рабочей ветки с последними изменениями из основной ветки перед отправкой изменений в удаленный репозиторий

        3. Конфликты

            В Git Merge конфликты могут возникнуть при слиянии, и их нужно будет разрешать в контексте merge commit

            В Git Rebase конфликты могут возникнуть на каждом этапе применения коммитов. Вам нужно будет разрешать конфликты по мере их возникновения

        4. Исторические последствия

            Git Merge сохраняет полную историю изменений, включая все слияния

            Git Rebase изменяет историю, так как коммиты переписываются. Это может привести к проблемам, если другие разработчики уже начали использовать вашу ветку


            Если нужно сохранить полную историю изменений и контекст, используйте merge
            Если вы предпочитаете чистую и линейную историю, используйте rebase
            Важно помнить, что после выполнения rebase не следует делать push в удаленный репозиторий, если другие разработчики уже работают с этой веткой,
            так как это может вызвать проблемы с синхронизацией.
    */
    #endregion
        
    #region Merge

        // Merge - залить develop в свою ветку
        // -p удаляет удаленные ветки, которые больше не существуют на удаленном репозитории
        git checkout develop
        git pull -p
        git checkout<branch>
        git merge develop
        // если конфликт - разрешаем, потом git merge --continue
        git push -f

    #endregion

    #region Rebase

        git checkout develop
        git pull -p
        git checkout<branch>
        git rebase develop
        // если конфликт - разрешаем, потом git rebase --continue
        git push -f

    #endregion

    #region Объединить коммиты

        git rebase -i HEAD~5
        // в vim для ввода нажать I
        // напротив нужных коммитов вместо pick написать squash
        // закрыть vim: esc: wq
        // разрешить конфликты в VS    
        git rebase --continue
        // в vim закомментить # ненужные комментарии
        git push -f

    #endregion

    #region Cherry Pick

        // Если работаем в одной ветке, а нам нужно перенести себе в ветку изменения из другой ветки, то можно сделать rebase, но он перенесет все коммиты
        // Cherry pick позволяет перенести один коммит, нужно только указать его хеш
        // Перед этим переключаемся на ветку, в которую хотим влить изменения
        git cherry-pick<commit-hash>
        git cherry-pick<commit-hash-1> <commit-hash-2> <commit-hash-3>
        // Если возникнут конфликты во время cherry-pick, нужно разрешить их и продолжить процесс:
        git cherry-pick --continue

    #endregion

    #region Не пулятся изменения из develop

        // Проблема с локальным develop - гит перестал трекать изменения в девелопе
        // Зайти в SourceTree и удалить develop, потом сделать в консоли git checkout develop
        // git pull

    #endregion
    
    #region Если конфликты после пуша

        git fetch
        git merge origin/develop
        // Разрешаем конфликты в VisualStudio

    #endregion

    #region Подмодуль

        // Добавить подмодуль
        cd C:\Setup\repos\backend-common
        git config --global http.sslVerify false
        git submodule add https://git.mcb.ru/.../lp.api.directory
        git config --global http.sslVerify true
        git status
        git submodule update --remote

        // Загрузить себе данные из подмодуля
        git submodule update --init --remote

    #endregion

    #region Синхронизация с develop в процессе разработки

        git checkout develop
        git pull -p
        git checkout feature/JIRA-000
        git merge develop

    #endregion
}