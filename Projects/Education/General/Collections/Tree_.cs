namespace Education.General.Collections;

public class TreeData
{
    public int Id { get; init; }

    public TreeData[]? Child { get; init; }
}

public class Tree
{
    public void Main_()
    {
        // Инициализация дерева
        //       1
        //   2       3
        // 4   5   6
        var treeData = new TreeData
        {
            Id = 1,
            Child =
            [
                new TreeData
                {
                    Id = 2,
                    Child =
                    [
                        new TreeData
                        {
                            Id = 4
                        },
                        new TreeData
                        {
                            Id = 5
                        }
                    ]
                },
                new TreeData
                {
                    Id = 3,
                    Child =
                    [
                        new TreeData
                        {
                            Id = 6
                        }
                    ]
                }
            ]
        };

        GetAllLeafsRecursive(treeData);

        GetAllLeafsIterative(treeData);
    }

    // Обход дерева при помощи рекурсии
    static IEnumerable<int> GetAllLeafsRecursive(TreeData? root)
    {
        if (root is null)
        {
            yield break;
        }

        // Если это лист — отдаем Id
        if (root.Child is null || root.Child.Length == 0)
        {
            yield return root.Id;
        }
        else
        {
            // Если есть дети — идем вглубь
            foreach (var child in root.Child)
            {
                foreach (var leafId in GetAllLeafsRecursive(child))
                {
                    yield return leafId;
                }
            }
        }
    }

    // На очень глубоких деревьях (тысячи уровней) рекурсия может привести к StackOverflowException
    // В таких случаях используют Stack, где процесс происходит в куче, а не в стеке вызовов
    static IEnumerable<int> GetAllLeafsIterative(TreeData root)
    {
        if (root is null)
        {
            yield break;
        }

        // В стеке храним узлы, которые еще нужно проверить
        // Если заменить Stack на Queue, алгоритм превратится из обхода в глубину в обход в ширину
        var stack = new Stack<TreeData>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            // Достаем верхний элемент
            var current = stack.Pop();

            // Возвращаем Id, если нет детей
            if (current.Child is null || current.Child.Length == 0)
            {
                yield return current.Id;
            }
            else
            {
                // Если есть дети — закидываем их в стек для дальнейшей проверки
                // Чтобы сохранить порядок слева направо, детей лучше пушить в обратном порядке
                // Обратный цикл for нужен, чтобы в стеке первый ребенок оказался сверху и обработался первым
                for (int i = current.Child.Length - 1; i >= 0; i--)
                {
                    if (current.Child[i] != null)
                    {
                        stack.Push(current.Child[i]);
                    }
                }
            }
        }
    }
}
