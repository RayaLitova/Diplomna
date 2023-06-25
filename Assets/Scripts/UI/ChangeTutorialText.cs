using UnityEngine;
using UnityEngine.UI;

public class ChangeTutorialText : MonoBehaviour
{
    public static bool DungeonTutorialPassed = false;
    public static bool CityTutorialPassed = false;
    public static bool TavernTutorialPassed = false;
    public static bool TrainingRoomTutorialPassed = false;
    public static bool TeaShopTutorialPassed = false;

    private string[] tutorialTextsDungeon = new string[] { 
        "Use WASD to move around",
        "You can also use Shift to dash in a certain direction",
        "Look out to not exit the safe zone, marked by this big blue wall",
        "Before you exit you should select your abilities",
        "Press K to open the skill menu",
        "Hold Alt to show your cursor",
        "Now you can drag and drop abilities into the left part of your action bar",
        "After you've placed them they can be executed with Q, E and R",
        "Be careful with your choices, you cannot change them once you've exited the safe zone",
        "While you're still in the safe room you may select your teas",
        "You can find them in your food bag which can be opened by pressing B",
        "Teas are placed in the right part of your action bar and can be used with 1, 2 and 3", 
        "Everywhere in the dungeon you'll find herbs, which can be gathered by interacting with them using the key F",
        "You can see your herb bag by pressing H",
        "Later on you'll use these herbs to craft teas, so gather as many as you can",
        "You can also use magic items",
        "Press I to open your inventory",
        "There, you'll see all magic items",
        "You can only place the ones that you've obtained in you item bar (upper left corner)",
        "During your trial in the dungeon enemies will drop more items",
        "This will be indicated with a blue diamond on top of your character's head",
        "The items reset every time you enter the dungeon",
        "The red circle in the middle of your action bar is your health",
        "Once it's emptied your trial ends and you're sent back to the village",
        "To pass the dungeon level you have to complete the dungeon objectives in your upper right corner",
        "Also you have to beat the final boss which will open the portal back to the village",
        "The path to the boss was shown in the starting cutscene",
        "Good luck!"
    };

    private string[] tutorialTextsCity = new string[] {
        "This is the village",
        "Here all the citizens are your friends",
        "You can walk around and see the village",
        "There are three particular houses that you're allowed to enter - the tavern, the training room and the tea shop",
        "The tavern is located just behind the well",
        "The training room is on the opposite side of the main square", 
        "The the shop is located right next to the training room."
    };

    private string[] tutorialTextsTavern = new string[] {
        "This is the tavern",
        "Here you can listen to the cat's beautiful voice and also talk to the bartender, who will send you to your next dungeon"
    };

    private string[] tutorialTextsTrainingRoom = new string[] {
        "This is the training room",
        "Here you can test and improve your skills on the training dummies",
        "You have access to all your skills, which can be found in the skill menu, opened with the key K, and all available items, which are located in your inventory, opened with the key I"
    };

    private string[] tutorialTextsTeaShop = new string[] {
        "This is the tea shop",
        "Here you can craft teas",
        "Open the craft menu by pressing T and your herb bag with the key H",
        "Now you can drag and drop your herbs at the recipe slots",
        "After you have filled all the slots press the 'Generate' button below",
        "If your recipe is valid it will be shown in the left side of the menu",
        "Every tea recipe is a mix of herbs that, when combined, make the tea color"
        //"You can see all available teas by speaking with the shopkeeper"
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
        else if (LoadScene.GetCurrentSceneName() == "TeaShop")
            text = tutorialTextsTeaShop[textNum];

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
        else if (LoadScene.GetCurrentSceneName() == "TeaShop")
            len = tutorialTextsTeaShop.Length;

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
        else if (LoadScene.GetCurrentSceneName() == "TeaShop")
            return TrainingRoomTutorialPassed;
        return DungeonTutorialPassed;
    }

    private void SetTutorialPassed()
    {
        if (LoadScene.GetCurrentSceneName() == "CityScene")
            CityTutorialPassed = true;
        else if (LoadScene.GetCurrentSceneName() == "AdventureGuildScene")
            TavernTutorialPassed = true;
        else if (LoadScene.GetCurrentSceneName() == "TargetDummyRoom")
            TrainingRoomTutorialPassed = true;
        else if (LoadScene.GetCurrentSceneName() == "TeaShop")
            TeaShopTutorialPassed = true;
        DungeonTutorialPassed = true;
    }
}

