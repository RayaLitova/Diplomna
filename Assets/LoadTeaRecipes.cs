using UnityEngine;
using UnityEngine.UI;

public class LoadTeaRecipes : MonoBehaviour
{
    private void Start()
    {
        foreach (var e in Resources.LoadAll<Food>("Tea/"))
            if(e.isRecipeKnown)
                AddTea(e);
    }

    public void AddTea(Food tea)
    {
        GameObject recipe = Resources.Load<GameObject>("CraftingUI/Recipe");
        recipe.transform.Find("ConstSlot").Find("Item").GetComponent<Display>().DisplayObj(tea);
        recipe.transform.Find("Name").GetComponent<Text>().text = tea.name;
        recipe.transform.Find("Description").GetComponent<Text>().text = tea.description;
        recipe.GetComponent<FillRecipeInSlots>().item = tea;
        GameObject obj = Instantiate(recipe, transform.GetChild(0).GetChild(0));
        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - (138 * (transform.GetChild(0).GetChild(0).childCount - 1)), obj.transform.position.z);
    }
}
