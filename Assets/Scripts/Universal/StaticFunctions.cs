using System.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StaticFunctions
{
    public static bool CheckForMatch<T>(T[] a, T[] b, int size)
    {
        List<T> tmp = new();
        tmp.AddRange(b);
        for (int i = 0; i < size; i++)
        {
            if (!tmp.Contains(a[i]))
                return false;
            tmp.Remove(a[i]);
        }
        return true;
    }

    public static bool CheckForMatch<T>(Dictionary<T[], int>.KeyCollection a, T[] b, int size)
    {
        foreach (var e in a)
        {
            if (CheckForMatch(e, b, size))
                return true;
        }
        return false;
    }

    public static T[] GetMatch<T>(Dictionary<T[], int>.KeyCollection a, T[] b, int size)
    {
        foreach (var e in a)
        {
            if (CheckForMatch(e, b, size))
                return e;
        }
        return null;
    }

    public static bool CheckForMatch<T>(List<T[]> a, T[] b, int size)
    {
        foreach (var e in a)
        {
            if (CheckForMatch(e, b, size))
                return true;
        }
        return false;
    }
    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

    public static string RemoveClones(this string input)
    {
        int index = input.Length;
        for (int i = 0; i < input.Length; i++)
        {
            if (input.ToCharArray()[i] == '(')
            {
                index = i;
                break;
            }
        }
        return input.Substring(0, index);
    }

    public static string AddWhitespaces(this string input)
    {
        return Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");
    }

    public static string GetFileName(this string input)
    {
        int nameEndindex = input.Length;
        int nameStartIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input.ToCharArray()[i] == '\\' || input.ToCharArray()[i] == '/')
            {
                nameStartIndex = i;
            }
            else if (input.ToCharArray()[i] == '.')
            {
                nameEndindex = i;
            }
        }
        if (nameStartIndex == 0)
            nameStartIndex = -1;
        return input.Substring(nameStartIndex + 1, nameEndindex - nameStartIndex - 1);
    }

}
