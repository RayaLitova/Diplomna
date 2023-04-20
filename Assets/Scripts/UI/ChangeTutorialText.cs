using UnityEngine;
using UnityEngine.UI;

public class ChangeTutorialText : MonoBehaviour
{
    public static bool DungeonTutorialPassed = false;
    public static bool CityTutorialPassed = false;
    public static bool TavernTutorialPassed = false;
    public static bool TrainingRoomTutorialPassed = false;

    private string[] tutorialTextsDungeon = new string[] { 
        "Use WASD to move around",
        "You can also use Shift to dash in a certain direction",
        "Look out to not exit the safe zone, marked by this big blue wall",
        "Before you exit you should select your abilities",
        "Press K to open the skill menu",
        "Hold Alt to show your cursor",
        "Now you can drag and drop abilities into the left part of your action bar",
        "After you've placed them they can be executed with QER",
        "Be careful with your choices, you cannot change them once you've exited the safe zone",
        "You can also use special items",
        "Press I to open your inventory",
        "There you'll see all magic items",
        "You can only place the ones that you've obtained in you item bar (upper left corner)",
        "During your trial in the dungeon, enemies will drop more items",
        "This will be indicated with a blue diamond on top of your character's head",
        "The items are reseted every time you enter the dungeon",
        "The red circle in the middle of your action bar is your health",
        "Once it's emptied your trial ends and you're sent back to the city",
        "To pass the dungeon level you have to conquer all normal enemies in your path",
        "Also you have to beat the final boss which will open the portal back to the city",
        "The path to the boss was shown in the starting cutscene",
        "Good luck!"
    };

    private string[] tutorialTextsCity = new string[] {
        "This is the main city",
        "Here all the citizens are your friends",
        "You can walk around and see the village",
        "There are tow particular houses that you're allowed to enter - the tavern and the training room",
        "The tavern is located right behind the well on the main square",
        "The training room is located on the other side of the main square from the tavern."
    };

    private string[] tutorialTextsTavern = new string[] {
        "This is the tavern",
        "Here you can listen to the cat's beautiful voice and also talk to the bartender, which will send you to your next dungeon"
    };

    private string[] tutorialTextsTrainingRoom = new string[] {
        "This is the training room",
        "Here you can test and improve your skills on the training dummies",
        "You have access to all your skills, which can be found in the skill menu, opened with the key 'K', and all available items, which are located in your inventory, opened with the key 'I'"
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
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            text = tutorialTextsTavern[textNum];
        else if (LoadScene.GetCurrentSceneName() == "TargetDummyRoom")
            text = tutorialTextsTrainingRoom[textNum];

        return text;
    }

    private int GetTextArrLength()
    {
        int len = tutorialTextsDungeon.Length;
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            len = tutorialTextsCity.Length;
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            len = tutorialTextsTavern.Length;
        else if (LoadScene.GetCurrentSceneName() == "TargetDummyRoom")
            len = tutorialTextsTrainingRoom.Length;

        return len;
    }

    private bool CheckIfTutorialIsPassed()
    {
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            return CityTutorialPassed;
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            return TavernTutorialPassed;
        else if (LoadScene.GetCurrentSceneName() == "TargetDummyRoom")
            return TrainingRoomTutorialPassed;
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

