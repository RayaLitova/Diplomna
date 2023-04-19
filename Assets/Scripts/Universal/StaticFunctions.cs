using System.Linq;
using System;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StaticFunctions
{
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
            if (input.ToCharArray()[i] == '\\' )
            {
                nameStartIndex = i;
            }
            else if (input.ToCharArray()[i] == '.')
            {
                nameEndindex = i;
            }
        }

        return input.Substring(nameStartIndex + 1, nameEndindex - nameStartIndex - 1);
    }

}
