using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SavingManager : MonoBehaviour
{
    string saveName = "Raya";
    string saveFile;
    public static GameData gameData = new GameData();
    private void Start()
    {
        saveFile = /*Application.persistentDataPath +*/ "E:/" + saveName + ".data";
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
