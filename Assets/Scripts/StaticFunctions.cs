using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;
using System;

public static class StaticFunctions
{
    public static IEnumerator waitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public static string RemoveWhitespace(this string input)
    {
        return new string(input.ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }
}
