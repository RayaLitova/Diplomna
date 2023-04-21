using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class SavingManager : MonoBehaviour
{
    static string saveName = "";
    static string saveFile;
    public static GameData gameData = null;
    static GameData loader;

    private void Start()
    {
        if (gameData == null)
        {
            gameData = new GameData();
            gameData.Load("HerbItems", Resources.LoadAll<Usable>("HerbItems/"));
            gameData.Load("Tea", Resources.LoadAll<Usable>("Tea/"));
            CraftTea.FillColors();
        }

        if (LoadScene.GetCurrentSceneName() == "MainMenu")
            return;
        if (LoadScene.GetCurrentSceneName() == "TeaShop" && !gameData.areRecipesGenerated)
            StartCoroutine(gameData.GenerateRecipes(Resources.LoadAll<Food>("Tea/")));
        LoadGameData();
        SaveGameData();
    }
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
            loader = JsonUtility.FromJson<GameData>(fileContents);
        }
    }

    public void writeFile()
    {
        string jsonString = JsonUtility.ToJson(gameData);
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

    public void LoadGameData(Text name)
    {
        saveName = name.text;
        saveFile = /*Application.persistentDataPath +*/ "E:/_GameSaves/" + saveName + ".data";
        LoadGameData();
        LoadScene.Load(gameData.currScene);
    }

    public void LoadGameData()
    {
        readFile();

        //...
        gameData.dungeonLevel = loader.dungeonLevel;
        gameData.currScene = loader.currScene;
        for (int i = 0; i < loader.types.Count; i++)
        {
            gameData.Items[loader.types.ElementAt(i)][loader.names.ElementAt(i)] = loader.counts.ElementAt(i);
        }
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

    public bool areRecipesGenerated = false;

    public void Load(string name, Usable[] items)
    {
        Items[name] = new();
        foreach (var obj in items)
            Items[name].Add(obj.name, 0);
    }

    public IEnumerator GenerateRecipes(Food[] items)
    {
        areRecipesGenerated = true;
        foreach (var e in items)
        {
            e.GenerateRecipe();
            yield return null;
        }
    }
    
}
