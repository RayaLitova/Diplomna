using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StaticFunctions : MonoBehaviour
{
    public static IEnumerator waitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
