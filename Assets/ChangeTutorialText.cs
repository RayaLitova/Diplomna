using UnityEngine;
using UnityEngine.UI;

public class ChangeTutorialText : MonoBehaviour
{
    public static bool DungeonTutorialPassed = false;
    public static bool CityTutorialPassed = false;
    public static bool TavernTutorialPassed = false;

    private string[] tutorialTextsDungeon = new string[] { 
        "Text1",
        "Text2",
        "Text3"
    };

    private string[] tutorialTextsCity = new string[] {
        "t",
        "t",
        "t"
    };

    private string[] tutorialTextsTavern = new string[] {
        "a",
        "a",
        "a"
    };
    private int current = 0;
    private Text textRef;
    
    void Start()
    {
        if (CheckIfTutorialIsPassed())
        {
            Destroy(gameObject);
            return;
        }
        textRef = transform.GetChild(0).Find("Text").GetComponent<Text>();
        textRef.text = GetText(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            current++;
            if (current == GetTextArrLength())
            { 
                FinishTutorial();
                return;            
            }
            textRef.text = GetText(current);
        }
    }

    public void FinishTutorial()
    {
        SetTutorialPassed();
        Destroy(gameObject);
    }

    private string GetText(int textNum)
    {
        string text = tutorialTextsDungeon[textNum];
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            text = tutorialTextsCity[textNum];
        else if(LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            text = tutorialTextsTavern[textNum];

        return text;
    }

    private int GetTextArrLength()
    {
        int text = tutorialTextsDungeon.Length;
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            text = tutorialTextsCity.Length;
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            text = tutorialTextsTavern.Length;

        return text;
    }

    private bool CheckIfTutorialIsPassed()
    {
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            return CityTutorialPassed;
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            return TavernTutorialPassed;
        return DungeonTutorialPassed;
    }

    private void SetTutorialPassed()
    {
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            CityTutorialPassed = true;
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            TavernTutorialPassed = true;
        DungeonTutorialPassed = true;
    }
}

