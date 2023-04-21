using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Food", menuName = "Food")]
public class Food : Executable
{
    public enum FoodType 
    { 
        Tea,
        Potion,
        Food,
        Length
    }
    public FoodType type;

    public int bonusATK = 0;
    public int bonusDEF = 0;
    public int bonusCrit = 0;
    public int bonusAgility = 0;
    public int bonusMaxHealth = 0;
    public int bonusRegen = 0;
    public float cooldownReduction = 0f;
    public int healing = 0;

    public enum FoodColor
    {
        Blue,
        LightBlue,
        Navy,
        Red,
        Pink,
        Yellow,
        Green,
        Lime,
        ForestGreen,
        Purple,
        Lilac,
        Eggplant,
        Teal,
        Turquoise,
        YellowGreen,
        Orange,
        Brown,
        LightBrown,
        DarkBrown,
        Gray,
        Length
    }

    private static FoodColor[] colors = new FoodColor[]
        {
            FoodColor.Blue,
            FoodColor.Red,
            FoodColor.Yellow,
            FoodColor.Green,
            FoodColor.Purple,
            FoodColor.Teal,
            FoodColor.YellowGreen,
            FoodColor.Orange,
            FoodColor.Brown,
            FoodColor.Gray
        };

    private static new List<KeyValuePair<FoodColor, FoodColor>> lightColors = new()
        {
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.LightBlue, FoodColor.Blue),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Pink, FoodColor.Red),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Lime, FoodColor.Green),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Lilac, FoodColor.Purple),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Turquoise, FoodColor.Teal),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.LightBrown, FoodColor.Brown),
        };

    private static List<KeyValuePair<FoodColor, FoodColor>> darkColors = new()
        {
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Navy, FoodColor.Blue),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.ForestGreen, FoodColor.Green),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Eggplant, FoodColor.Purple),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.DarkBrown, FoodColor.Brown),
        };

    private static List<KeyValuePair<FoodColor, Herb.HerbColor[]>> colorCombinations = new()
    {
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Blue, new Herb.HerbColor[1]{Herb.HerbColor.Blue} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Red, new Herb.HerbColor[1]{ Herb.HerbColor.Red }),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Yellow, new Herb.HerbColor[3]{Herb.HerbColor.Yellow, Herb.HerbColor.White, Herb.HerbColor.White } ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Yellow, new Herb.HerbColor[3]{Herb.HerbColor.Yellow, Herb.HerbColor.Gray, Herb.HerbColor.Gray } ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Green, new Herb.HerbColor[1]{Herb.HerbColor.Green} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Green, new Herb.HerbColor[2]{Herb.HerbColor.Yellow, Herb.HerbColor.Blue} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Purple, new Herb.HerbColor[2]{Herb.HerbColor.Blue, Herb.HerbColor.Red} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Teal, new Herb.HerbColor[2]{Herb.HerbColor.Blue, Herb.HerbColor.Green} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.YellowGreen, new Herb.HerbColor[2]{Herb.HerbColor.Yellow, Herb.HerbColor.Green} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Orange, new Herb.HerbColor[2]{Herb.HerbColor.Yellow, Herb.HerbColor.Red} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Orange, new Herb.HerbColor[3]{Herb.HerbColor.Yellow, Herb.HerbColor.Red, Herb.HerbColor.White} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Orange, new Herb.HerbColor[3]{Herb.HerbColor.Yellow, Herb.HerbColor.Red, Herb.HerbColor.Gray} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Brown, new Herb.HerbColor[2]{Herb.HerbColor.Green, Herb.HerbColor.Red} ),
        new KeyValuePair<FoodColor, Herb.HerbColor[]>(FoodColor.Gray, new Herb.HerbColor[2]{Herb.HerbColor.Gray, Herb.HerbColor.White} ),
    };

    public FoodColor color;
    public Herb[] recipe = new Herb[3];
    public bool isRecipeKnown = false;

    public Herb[] GenerateRecipe()
    {
        var lookupL = darkColors.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        var lookupD = lightColors.ToLookup(kvp => kvp.Key, kvp => kvp.Value);

        List<Herb.HerbColor> recipe = new();

        if (colors.Contains(color))
        {
            recipe = GetRecipe();
            if (recipe.Count < 3)
            {
                if (recipe.Count == 1 && CraftTea.herbColorCounts[recipe.First()] >= 3)
                {
                    recipe.Add(recipe.First());
                    recipe.Add(recipe.First());
                }
                else if (recipe.Count == 1 && CraftTea.herbColorCounts[recipe.First()] == 2)
                {
                    recipe.Add(recipe.First());
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            recipe.Add(Herb.HerbColor.White);
                            break;
                        case 1:
                            recipe.Add(Herb.HerbColor.Gray);
                            break;
                    }
                }
                else if (recipe.Count == 1 && CraftTea.herbColorCounts[recipe.First()] == 1)
                {
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            recipe.Add(Herb.HerbColor.White);
                            recipe.Add(Herb.HerbColor.White);
                            break;
                        case 1:
                            recipe.Add(Herb.HerbColor.Gray);
                            recipe.Add(Herb.HerbColor.Gray);
                            break;
                        case 2:
                            recipe.Add(Herb.HerbColor.White);
                            recipe.Add(Herb.HerbColor.Gray);
                            break;
                    }
                }
                else if (recipe.Count == 2)
                {
                    if (Random.Range(0, 2) == 0 && CraftTea.herbColorCounts[recipe.First()] > 1)
                        recipe.Add(recipe.First());
                    else
                        recipe.Add(recipe.ElementAt(1));
                }
            }
        }
        else if (lookupL[color] != null)
        {
            recipe = GetRecipe(lookupL[color].ElementAt(0));
            if (recipe.Count < 3)
            {
                for (int i = 0; i < (3 - recipe.Count); i++)
                    recipe.Add(Herb.HerbColor.White);  
                
            }
        }
        else if(lookupD[color] != null)
        {
            recipe = GetRecipe(lookupD[color].ElementAt(0));
            if (recipe.Count < 3)
            {
                for (int i = 0; i < (3 - recipe.Count); i++)
                    recipe.Add(Herb.HerbColor.Gray);

            }
        }
        return FillRecipe(recipe);
    }

    public List<Herb.HerbColor> GetRecipe(FoodColor color)
    {
        var lookup = colorCombinations.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        List<Herb.HerbColor> recipe = new();

        recipe.AddRange(lookup[color].ElementAt(Random.Range(0, lookup[color].Count())));

        return recipe;
    }
    public List<Herb.HerbColor> GetRecipe()
    {
        return GetRecipe(color);
    }


    public Herb[] FillRecipe(List<Herb.HerbColor> colors)
    {
        for (int i = 0; i < 3; i++)
        {
            Herb temp;
            temp = FindHerb(colors.ElementAt(i), recipe);
            recipe[i] = temp;
        }
        return recipe;
    }

    public Herb FindHerb(Herb.HerbColor color, Herb[] occupied = null)
    {
        Debug.Log(color);
        Debug.Log(occupied.Length);
        Herb[] herbs = Resources.LoadAll<Herb>("HerbItems");
        Herb result = null;
        int startIndex = Random.Range(0, herbs.Length);
        for (int i = startIndex; i < herbs.Length; i++)
        {
            Debug.Log(herbs[i]);
            if (herbs[i].color == color && occupied != null && !occupied.Contains(herbs[i]))
            {
                result = herbs[i];
                break;
            }
        }

        if (result == null)
        {
            for (int i = 0; i < startIndex; i++)
            {
                Debug.Log(herbs[i]);
                if (herbs[i].color == color && occupied != null && !occupied.Contains(herbs[i]))
                {
                    result = herbs[i];
                    break;
                }
            }
        }
        Debug.Log(result);
        return result;
    }
    public override void Execute()
    {
        if (Time.time < cooldownTimer)
            return;

        ApplyCooldown();
        CharacterStats characterStats = GameObject.Find("Player").GetComponentInParent<CharacterStats>();
        characterStats.AddBuff();
        characterStats.ATK += bonusATK;
        characterStats.DEF += bonusDEF;
        characterStats.Crit += bonusCrit;
        characterStats.Agility += bonusAgility;
        characterStats.MaxHealth += bonusMaxHealth;
        characterStats.HealthRegen += bonusRegen;
        characterStats.Health += healing;

        if (cooldownReduction != 0)
        {
            foreach (var v in FindObjectsOfType<Executable>())
            {
                v.ReduceCooldown(cooldownReduction);
            }
        }
        FindObjectOfType<MonoBehaviour>().StartCoroutine(timer());
    }

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(cooldown);
        ReverseEffect();
    }

    public void ReverseEffect()
    {
        CharacterStats characterStats = GameObject.Find("Player").GetComponentInParent<CharacterStats>();
        characterStats.ATK -= bonusATK;
        characterStats.DEF -= bonusDEF;
        characterStats.Crit -= bonusCrit;
        characterStats.Agility -= bonusAgility;
        characterStats.MaxHealth -= bonusMaxHealth;
        characterStats.HealthRegen -= bonusRegen;
        characterStats.ReduceBuffs();

        if (cooldownReduction != 0)
        {
            foreach (var v in FindObjectsOfType<Executable>())
            {
                v.ReduceCooldown(-cooldownReduction);
            }
        }
    }

    public void UnlockRecipe()
    {
        isRecipeKnown = true;
        FindObjectOfType<LoadTeaRecipes>().AddTea(this);
        //more
    }
}
