using System.Collections.Generic;
using System.Linq;

using Random = UnityEngine.Random;

public static partial class Extensions
{
    public static T GetRandom<T>(this IEnumerable<T> collection)
    {
        var count = collection.Count();
        if (count == 0)
            return default(T);

        return collection.ElementAt(Random.Range(0, count));
    }

    public static void Populate<T>(this T[] arr, T value)
    {
        for (var i = 0; i < arr.Length; i++)
            arr[i] = value;
    }

    public static string Print<T>(this IList<T> list)
    {
        var res = "[ " + string.Join(", ", list.Select(s => $"{s}")) + " ]";
        return res;
    }

    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();

        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    public static int GetRandomIndex<T>(this List<T> list)
    {
        if (list.Count == 0)
            return -1;

        return Random.Range(0, list.Count);
    }

    public static int GetRandomIndexWithout<T>(this List<T> list, int exclusion = -1)
    {
        if (list.Count == 0)
            return -1;

        return RandomRangeWithout(0, list.Count, exclusion);
    }
}