using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;

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
            gameData.recipes = null;
            CraftTea.FillColors();
        }

        if (LoadScene.GetCurrentSceneName() == "MainMenu")
            return;

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
        if(LoadScene.GetCurrentSceneName() == "TeaShop")
            LoadRecipes();
        
        for (int i = 0; i < loader.types.Count; i++)
            gameData.Items[loader.types.ElementAt(i)][loader.names.ElementAt(i)] = loader.counts.ElementAt(i);
    }

    public void LoadRecipes()
    {
        Debug.Log("Load");
        gameData.recipes = new List<string>();
        gameData.recipes.Clear();
        gameData.recipes.AddRange(loader.recipes);
        foreach (var e in Resources.LoadAll<Food>("Tea/"))
        {
            int index = gameData.recipes.FindIndex(x => x == e.name);
            Debug.Log(e.name +" "+index);
            for (int i = 0; i < 3; i++)
            {
                e.recipe[i] = Resources.Load<Herb>("HerbItems/" + gameData.recipes.ElementAt(index + i + 1).RemoveWhitespace());
            }
            EditorUtility.SetDirty(e);
            AssetDatabase.SaveAssets();
        }
    }

    public void StartNewGame()
    {
        if (!ChangeSaveName())
            return;
        saveFile = /*Application.persistentDataPath +*/ "E:/_GameSaves/" + saveName + ".data";

        foreach (var e in Resources.LoadAll<Food>("Tea/")) //clear recipe
            e.recipe = new Herb[3];

        gameData.GenerateRecipes(Resources.LoadAll<Food>("Tea/"));
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

    public List<string> recipes = new(); // food name + 3 x herbs


    public void Load(string name, Usable[] items)
    {
        Items[name] = new();
        foreach (var obj in items)
            Items[name].Add(obj.name, 0);
    }

    public void GenerateRecipes(Food[] items)
    {
        recipes = new List<string>();
        foreach (var e in items)
        {
            recipes.Add(e.name);
            foreach (var i in e.GenerateRecipe())
                recipes.Add(i.name);
            
        }
    }
    
}
