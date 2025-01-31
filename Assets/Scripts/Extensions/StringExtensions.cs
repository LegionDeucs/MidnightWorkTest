using System;

public static partial class Extensions
{
    public static string Remove(this string sourceString, string removeString)
    {
        var index = sourceString.IndexOf(removeString);
        var cleanPath = (index < 0) ?
            sourceString :
            sourceString.Remove(index, removeString.Length);

        return cleanPath;
    }

    public static string UppercaseWords(this string value)
    {
        var array = value.ToCharArray();

        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
                array[0] = char.ToUpper(array[0]);
        }

        for (var i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                    array[i] = char.ToUpper(array[i]);
            }
        }

        return new string(array);
    }

    public static string DeclOfNum(this int number, string title1, string title2, string title5, string format = "{0} {1}")
    {
        var cases = new int[] { 2, 0, 1, 1, 1, 2 };
        var titles = new string[] { title1, title2, title5 };
        var title = titles[(number % 100 > 4 && number % 100 < 20) ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
        return String.Format(format, number, title);
    }
}