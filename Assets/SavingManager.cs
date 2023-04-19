using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

public class SavingManager : MonoBehaviour
{
    static string saveName = "Raya";
    static string saveFile;
    public static GameData gameData = new GameData();

    public bool ChangeSaveName()
    {
        saveName = GameObject.Find("InputData").GetComponent<Text>().text;
        Transform errMessages = GameObject.Find("ErrorMessages").transform;
        if (FillSavesNames.filePaths.Contains(saveName))
        {
            errMessages.GetChild(0).gameObject.SetActive(true);
            return false;

        }
        else if (saveName == "")
        {
            errMessages.GetChild(2).gameObject.SetActive(true);
            return false;
        }
        else if (FillSavesNames.filePaths.Count == 3)
        {
            errMessages.GetChild(1).gameObject.SetActive(true);
            return false;
        }
        return true;

    }

    public void readFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            gameData = JsonUtility.FromJson<GameData>(fileContents);
        }
    }

    public void writeFile()
    {
        string jsonString = JsonUtility.ToJson(gameData);
        Debug.Log(gameData.Items);
        File.WriteAllText(saveFile, jsonString);
    }

    public void SaveGameData()
    {
        gameData.currScene = LoadScene.GetCurrentSceneName();

        gameData.types.Clear();
        gameData.names.Clear();
        gameData.counts.Clear();
        foreach (var key in gameData.Items.Keys)
        {
            foreach (var k in gameData.Items[key].Keys)
            {
                gameData.types.Add(key);
                gameData.names.Add(k);
                gameData.counts.Add(gameData.Items[key][k]);
            }
        }
        //...
        writeFile();
    }

    public void LoadGameData()
    {
        readFile();
        //...
        for(int i = 0; i < gameData.types.Count; i++)
        {
            if (!gameData.Items.ContainsKey(gameData.types.ElementAt(i)))
                gameData.Items[gameData.types.ElementAt(i)] = new();
            gameData.Items[gameData.types.ElementAt(i)][gameData.names.ElementAt(i)] = gameData.counts.ElementAt(i);
        }
        LoadScene.Load(gameData.currScene);
    }

    public void StartNewGame()
    {
        if (!ChangeSaveName())
            return;
        saveFile = /*Application.persistentDataPath +*/ "E:/_GameSaves/" + saveName + ".data";
        writeFile();
        LoadScene.Load("DungeonScene");
    }

    public void DeleteSave(string saveName)
    {
        FillSavesNames.filePaths.Remove(saveName);
        File.Delete("E:/_GameSaves/" + saveName + ".data");
    }

    public void DeleteSave(GameObject nameField)
    {
        DeleteSave(nameField.transform.Find("Name").GetComponent<Text>().text);
        nameField.SetActive(false);
    }

    public void SaveAndExit()
    {
        SaveGameData();
        LoadScene.Load("MainMenu");
    }
}

[System.Serializable]
public class GameData
{
    public string currScene;
    public int dungeonLevel;

    public Dictionary<string, Dictionary<string, int>> Items = new(); //non serializable

    //for serialization
    public List<string> types = new();
    public List<string> names = new();
    public List<int> counts = new();
}
