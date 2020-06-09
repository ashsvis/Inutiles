using System;
using System.Collections.Generic;

namespace Sorting
{
    internal static class MethodsHolder
    {
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
                        var m = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = m;
                        flag = true;
                        //
                        swaps.Add(new Tuple<int, int>(i, i + 1));
                    }
                }
                if (!flag) break;
            }
            return swaps;
        }
    }
}
