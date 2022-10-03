using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticFunctions
{
    public static IEnumerator timer(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
