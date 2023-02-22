using UnityEngine;
using System.Linq;
using System;

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

}
