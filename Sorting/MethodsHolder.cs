using System;
using System.Collections.Generic;

namespace Sorting
{
    internal static class MethodsHolder
    {

        private static void Swap(int[] array, int i, int j)
        {
            var m = array[i];
            array[i] = array[j];
            array[j] = m;
        }

        /// <summary>
        /// Алгоритм состоит из повторяющихся проходов по сортируемому массиву.
        /// За каждый проход элементы последовательно сравниваются попарно и, если порядок в паре неверный, выполняется обмен элементов.
        /// Проходы по массиву повторяются N-1 раз или до тех пор, пока на очередном проходе не окажется, что обмены больше не нужны,
        /// что означает — массив отсортирован. При каждом проходе алгоритма по внутреннему циклу, очередной наибольший элемент массива ставится
        /// на своё место в конце массива рядом с предыдущим «наибольшим элементом», а наименьший элемент перемещается на одну позицию
        /// к началу массива («всплывает» до нужной позиции, как пузырёк в воде — отсюда и название алгоритма).
        /// Источник: https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D1%80%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B0_%D0%BF%D1%83%D0%B7%D1%8B%D1%80%D1%8C%D0%BA%D0%BE%D0%BC
        /// </summary>
        internal static List<Tuple<int, int>> BubbleSort(int[] array)
        {
            var swaps = new List<Tuple<int, int>>();
            for (var j = 0; j < array.Length - 1; j++)
            {
                var flag = false;
                for (var i = 0; i < array.Length - j - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        flag = true;
                        //
                        swaps.Add(new Tuple<int, int>(i, i + 1));
                    }
                }
                if (!flag) break;
            }
            return swaps;
        }

        /// <summary>
        /// Сортировка перемешиванием, или Шейкерная сортировка, или двунаправленная (англ. Cocktail sort) — разновидность пузырьковой сортировки.
        /// Анализируя метод пузырьковой сортировки, можно отметить два обстоятельства.
        /// Во-первых, если при движении по части массива перестановки не происходят, то эта часть массива уже отсортирована и, 
        /// следовательно, её можно исключить из рассмотрения.
        /// Во-вторых, при движении от конца массива к началу минимальный элемент «всплывает» на первую позицию, 
        /// а максимальный элемент сдвигается только на одну позицию вправо.
        /// Эти две идеи приводят к следующим модификациям в методе пузырьковой сортировки.
        /// Границы рабочей части массива(то есть части массива, где происходит движение) устанавливаются в месте последнего обмена на каждой итерации.
        /// Массив просматривается поочередно справа налево и слева направо.
        /// Источник: https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D1%80%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B0_%D0%BF%D0%B5%D1%80%D0%B5%D0%BC%D0%B5%D1%88%D0%B8%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%D0%BC
        /// </summary>
        internal static List<Tuple<int, int>> ShakerSort(int[] array)
        {
            var swaps = new List<Tuple<int, int>>();

            int left = 0,
                right = array.Length - 1,
                count = 0;

            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    count++;
                    if (array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        //
                        swaps.Add(new Tuple<int, int>(i, i + 1));
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    count++;
                    if (array[i - 1] > array[i])
                    {
                        Swap(array, i - 1, i);
                        //
                        swaps.Add(new Tuple<int, int>(i - 1, i));
                    }
                }
                left++;
            }
            return swaps;
        }

        /// <summary>
        /// Сортировка вставками (англ. Insertion sort) — алгоритм сортировки, в котором элементы входной последовательности просматриваются по одному,
        /// и каждый новый поступивший элемент размещается в подходящее место среди ранее упорядоченных элементов
        /// Источник: https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D1%80%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B0_%D0%B2%D1%81%D1%82%D0%B0%D0%B2%D0%BA%D0%B0%D0%BC%D0%B8
        /// </summary>
        internal static List<Tuple<int, int>> InsertionSort(int[] array)
        {
            var swaps = new List<Tuple<int, int>>();
            for (var i = 1; i < array.Length; i++)
            {
                var x = array[i];
                var j = i;
                while (j >= 1 && array[j - 1] > x)
                {
                    //
                    array[j] = array[j - 1];
                    swaps.Add(new Tuple<int, int>(j, j - 1));
                    j = j - 1;
                }
                array[j] = x;
            }
            //
            return swaps;
        }

        /// <summary>
        /// Быстрая сортировка, сортировка Хоара (англ. quicksort)
        /// Источник: https://ru.wikipedia.org/wiki/%D0%91%D1%8B%D1%81%D1%82%D1%80%D0%B0%D1%8F_%D1%81%D0%BE%D1%80%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B0
        /// </summary>
        internal static List<Tuple<int, int>> QuickSort(int[] array)
        {
            var swaps = new List<Tuple<int, int>>();
            QuickSort(array, 0, array.Length - 1, swaps);
            //
            return swaps;
        }

        private static void QuickSort(int[] array, int lo, int hi, List<Tuple<int, int>> swaps)
        {
            if (lo < hi)
            {
                var p = Partition(array, lo, hi, swaps);
                QuickSort(array, lo, p, swaps);
                QuickSort(array, p + 1, hi, swaps);
            }
        }

        private static int Partition(int[] array, int low, int high, List<Tuple<int, int>> swaps)
        {
            var pivot = array[(low + high) / 2];
            var i = low - 1;
            var j = high + 1;
            while (true)
            {
                do
                {
                    i++;
                } while (array[i] < pivot);
                do
                {
                    j--;
                } while (array[j] > pivot);
                if (i >= j)
                    return j;
                Swap(array, i, j);
                //
                swaps.Add(new Tuple<int, int>(i, j));
            }
        }

        /// <summary>
        /// Сортировка расчёской (англ. comb sort)
        /// Источник: https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D1%80%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B0_%D1%80%D0%B0%D1%81%D1%87%D1%91%D1%81%D0%BA%D0%BE%D0%B9
        /// </summary>
        internal static List<Tuple<int, int>> CombSort(int[] array)
        {
            var swaps = new List<Tuple<int, int>>();
            double fakt = 1.2473309; // фактор уменьшения
            var step = array.Length - 1;

            while (step >= 1)
            {
                for (int i = 0; i + step < array.Length; ++i)
                {
                    if (array[i] > array[i + step])
                    {
                        Swap(array, i, i + step);
                        //
                        swaps.Add(new Tuple<int, int>(i, i + step));
                    }
                }
                step = (int)(step / fakt);
            }
            // досортировка пузырьком
            swaps.AddRange(BubbleSort(array));
            //
            return swaps;
        }

        /// <summary>
        /// Сортировка выбором (Selection sort)
        /// Источник: https://ru.wikipedia.org/wiki/%D0%A1%D0%BE%D1%80%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%BA%D0%B0_%D0%B2%D1%8B%D0%B1%D0%BE%D1%80%D0%BE%D0%BC
        /// </summary>
        /// <returns></returns>
        internal static List<Tuple<int, int>> SelectionSort(int[] array)
        {
            var swaps = new List<Tuple<int, int>>();
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[min])
                    {
                        min = j;
                    }
                }
                Swap(array, i, min);
                //
                swaps.Add(new Tuple<int, int>(i, min));
            }
            //
            return swaps;
        }

    }
}
