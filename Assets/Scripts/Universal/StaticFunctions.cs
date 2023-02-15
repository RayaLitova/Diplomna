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

}
