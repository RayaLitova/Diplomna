using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills_UI : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keyCodes;
    public static Dictionary<string, GameObject> Skills;
    void Start()
    {
        keyCodes = new Dictionary<string, KeyCode>()
        {
            {"Action key 1", KeyCode.Q },
            {"Action key 2", KeyCode.E },
            {"Action key 3", KeyCode.R },
        };

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
