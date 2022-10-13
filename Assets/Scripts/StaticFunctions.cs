using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StaticFunctions
{
    public static IEnumerator waitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public static bool timer(float sec)
    {
        float targetTime = Time.time + sec;
        float a = 0;
        while (Time.fixedDeltaTime < targetTime && a < 55)
        {
            a++;
            Debug.Log(Time.time);
        }
        return true;
    }
}
