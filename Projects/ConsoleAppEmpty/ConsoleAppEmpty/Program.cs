// Есть массив из 1 миллиона элементов, который нужно отсортировать по возрастанию
// Можно использовать многопоточность для разделения этой задачи на несколько подзадач
// и параллельного выполнения их на разных ядрах процессора

// Используем метод ParallelSort, который разделяет массив на чанки и запускает задачи для каждого чанка
// Каждая задача сортирует свой чанк с помощью метода SortChunk
// После завершения всех задач объединяем отсортированные чанки в один отсортированный массив
// с помощью метода MergeChunks

using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int[] array = Enumerable.Range(1, 1000000).ToArray();   // генерируем массив из 1 миллиона элементов
        int[] sortedArray = ParallelSort(array);                // сортируем массив с помощью многопоточности

        Console.WriteLine("Sorted array:");
        foreach (int number in sortedArray)
        {
            Console.Write(number + " ");
        }
    }

    static int[] ParallelSort(int[] array)
    {
        // получаем количество ядер процессора
        int cores = Environment.ProcessorCount;

        // вычисляем размер чанка для каждого потока
        int chunkSize = array.Length / cores;

        // создаем массив задач для каждого потока
        var tasks = new Task<int[]>[cores];

        for (int i = 0; i < cores; i++)
        {
            // вычисляем начальный индекс для текущего чанка
            int startIndex = i * chunkSize;

            // вычисляем конечный индекс для текущего чанка
            int endIndex = (i == cores - 1) ? array.Length : (i + 1) * chunkSize;

            // запускаем задачу для текущего чанка
            tasks[i] = Task.Run(() => SortChunk(array, startIndex, endIndex)); 
        }

        // ожидаем завершения всех задач
        Task.WaitAll(tasks);

        // объединяем отсортированные чанки в один отсортированный массив
        int[] sortedArray = MergeChunks(tasks.Select(t => t.Result).ToArray());

        return sortedArray;
    }

    static int[] SortChunk(int[] array, int startIndex, int endIndex)
    {
        int[] chunk = new int[endIndex - startIndex];

        // копируем чанк из основного массива
        Array.Copy(array, startIndex, chunk, 0, endIndex - startIndex);

        // сортируем чанк
        Array.Sort(chunk);

        return chunk;
    }

    static int[] MergeChunks(int[][] chunks)
    {
        int totalLength = chunks.Sum(c => c.Length);
        int[] mergedArray = new int[totalLength];

        int currentIndex = 0;

        foreach (int[] chunk in chunks)
        {
            // копируем отсортированный чанк в объединенный массив
            Array.Copy(chunk, 0, mergedArray, currentIndex, chunk.Length);
            currentIndex += chunk.Length;
        }

        // сортируем объединенный массив еще раз, чтобы убедиться, что он полностью отсортирован
        Array.Sort(mergedArray);

        return mergedArray;
    }
}

