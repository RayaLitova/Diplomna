using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections.Generic;

public class FillSavesNames : MonoBehaviour
{
    public static List<string> filePaths = new();
    private void Start()
    {
        filePaths.Clear();
        filePaths.AddRange(Directory.GetFiles(Application.persistentDataPath + "/" , "*.data"));
        for (int i = 0; i < transform.childCount; i++)
        {
            try
            {
                filePaths[i] = StaticFunctions.GetFileName(filePaths[i]);
                transform.GetChild(i).Find("Name").GetComponent<Text>().text = filePaths[i];
            }
            catch (Exception)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
