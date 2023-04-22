using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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

    public static FoodColor[] colors = new FoodColor[]
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

    public static Dictionary<FoodColor, Color> UIcolors = new Dictionary<FoodColor, Color>()
    {
        { FoodColor.Blue, new Color(0, 0, 255)},
        { FoodColor.LightBlue, new Color(3, 69, 252)},
        { FoodColor.Navy, new Color(1, 34, 125)},
        { FoodColor.Red, new Color(255, 0, 0)},
        { FoodColor.Pink, new Color(255, 66, 183)},
        { FoodColor.Yellow, new Color(255, 191, 0)},
        { FoodColor.Green, new Color(24, 161, 0)},
        { FoodColor.Lime, new Color(139, 255, 135)},
        { FoodColor.ForestGreen, new Color(8, 51, 1)},
        { FoodColor.Purple, new Color(98, 0, 255)},
        { FoodColor.Lilac, new Color(181, 135, 255)},
        { FoodColor.Eggplant, new Color(44, 2, 92)},
        { FoodColor.Teal, new Color(12, 145, 78)},
        { FoodColor.Turquoise, new Color(60, 201, 130)},
        { FoodColor.YellowGreen, new Color(160, 222, 38)},
        { FoodColor.Orange, new Color(222, 115, 38)},
        { FoodColor.Brown, new Color(94, 57, 2)},
        { FoodColor.LightBrown, new Color(158, 95, 0)},
        { FoodColor.DarkBrown, new Color(33, 20, 0)},
        { FoodColor.Gray, new Color(140, 140, 140)},
    };

    public static new List<KeyValuePair<FoodColor, FoodColor>> lightColors = new()
        {
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.LightBlue, FoodColor.Blue),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Pink, FoodColor.Red),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Lime, FoodColor.Green),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Lilac, FoodColor.Purple),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Turquoise, FoodColor.Teal),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.LightBrown, FoodColor.Brown),
        };

    public static List<KeyValuePair<FoodColor, FoodColor>> darkColors = new()
        {
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Navy, FoodColor.Blue),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.ForestGreen, FoodColor.Green),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.Eggplant, FoodColor.Purple),
            new KeyValuePair<FoodColor, FoodColor>(FoodColor.DarkBrown, FoodColor.Brown),
        };

    public static List<KeyValuePair<FoodColor, Herb.HerbColor[]>> colorCombinations = new()
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

    public List<Usable[]> occupiedRecipes = new();

    public Herb[] GenerateRecipe()
    {
        this.recipe = new Herb[3];
        foreach (var e in Resources.LoadAll<Food>("Tea/"))
        {
            if (e.recipe[0] != null)
                occupiedRecipes.Add(e.recipe);
        }
        
        var lookupD = darkColors.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        var lookupL = lightColors.ToLookup(kvp => kvp.Key, kvp => kvp.Value);

        List<Herb.HerbColor> recipe = new();

        if (colors.Contains(color))
        {
            recipe = GetRecipe();
            if (recipe.Count < 3)
            {
                if (recipe.Count == 1 && CraftTea.herbColorCounts[recipe.First()] >= 3 && 
                    CraftTea.CombinationAvailable(new Herb.HerbColor[]{ recipe.First(), recipe.First(), recipe.First()}))
                {
                    recipe.Add(recipe.First());
                    recipe.Add(recipe.First());
                    CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.First(), recipe.First() });
                }
                else if (recipe.Count == 1 && CraftTea.herbColorCounts[recipe.First()] == 2 && 
                    (CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.White }) || CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.Gray })))
                {
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.White });//to ensure both are getting called
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.Gray });

                    recipe.Add(recipe.First());
                    int a = UnityEngine.Random.Range(0, 2);

                    if (a == 0 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.White }))
                    {
                        recipe.Add(Herb.HerbColor.White);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.White });

                    }
                    else if (CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.Gray }))
                    {
                        recipe.Add(Herb.HerbColor.Gray);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.Gray });

                    }
                    else //else if failed (a == 1)
                    {
                        recipe.Add(Herb.HerbColor.White);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.First(), Herb.HerbColor.White });
                    }
                }
                else if (recipe.Count == 1 && CraftTea.herbColorCounts[recipe.First()] == 1 &&
                    ((CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White }) || 
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray }) ||
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.Gray }))))

                {
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White });
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray });
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.Gray });

                    int a = UnityEngine.Random.Range(0, 3);

                    if (a == 0 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White }))
                    {
                        recipe.Add(Herb.HerbColor.White);
                        recipe.Add(Herb.HerbColor.White);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White });

                    }
                    else if (a == 1 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray }))
                    {
                        recipe.Add(Herb.HerbColor.Gray);
                        recipe.Add(Herb.HerbColor.Gray);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray });

                    }
                    else if (CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.Gray }))
                    {
                        recipe.Add(Herb.HerbColor.White);
                        recipe.Add(Herb.HerbColor.Gray);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.Gray });

                    }
                    else //else if failed (a == 2)
                    {
                        if (CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White }))
                        {
                            recipe.Add(Herb.HerbColor.White);
                            recipe.Add(Herb.HerbColor.White);
                            CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White });

                        }
                        else if (CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray }))
                        {
                            recipe.Add(Herb.HerbColor.Gray);
                            recipe.Add(Herb.HerbColor.Gray);
                            CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray });

                        }
                    }
                }
                else if (recipe.Count == 2 &&
                    (CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.First() }) ||
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.ElementAt(1) })))
                {
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.First() });
                    CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.ElementAt(1)});

                    int a = UnityEngine.Random.Range(0, 2);
                    if (a == 0 && CraftTea.herbColorCounts[recipe.First()] > 1 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.First() }))
                    {
                        recipe.Add(recipe.First());
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.First() });

                    }
                    else if (CraftTea.herbColorCounts[recipe.ElementAt(1)] > 1 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.ElementAt(1) }))
                    {
                        recipe.Add(recipe.ElementAt(1));
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.ElementAt(1) });
                    }
                    else // else if failed (a == 1)
                    {
                        recipe.Add(recipe.First());
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), recipe.First() });
                    }
                }
            }
        }
        else
        {
            try
            {

                recipe = GetRecipe(lookupL[color].First());
                if (recipe.Count < 3)
                {
                    if (recipe.Count == 1 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White }))
                    {
                        recipe.Add(Herb.HerbColor.White);
                        recipe.Add(Herb.HerbColor.White);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.White, Herb.HerbColor.White });

                    }
                    else if(CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), Herb.HerbColor.White }))
                    {
                        recipe.Add(Herb.HerbColor.White);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), Herb.HerbColor.White });
                    }

                }
            }
            catch (InvalidOperationException)
            {
                recipe = GetRecipe(lookupD[color].First());
                if (recipe.Count < 3)
                {
                    if (recipe.Count == 1 && CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray }))
                    {
                        recipe.Add(Herb.HerbColor.Gray);
                        recipe.Add(Herb.HerbColor.Gray);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), Herb.HerbColor.Gray, Herb.HerbColor.Gray });

                    }
                    else if(CraftTea.CombinationAvailable(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), Herb.HerbColor.Gray }))
                    {
                        recipe.Add(Herb.HerbColor.Gray);
                        CraftTea.IncrementUsedCombinations(new Herb.HerbColor[] { recipe.First(), recipe.ElementAt(1), Herb.HerbColor.Gray });
                    }

                }
            }
        }
        return FillRecipe(recipe);
    }

    public List<Herb.HerbColor> GetRecipe(FoodColor color)
    {
        var lookup = colorCombinations.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        List<Herb.HerbColor> recipe = new();

        recipe.AddRange(lookup[color].ElementAt(UnityEngine.Random.Range(0, lookup[color].Count())));

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
        if (StaticFunctions.CheckForMatch(occupiedRecipes, recipe, 3))
        {
            for (int i = 0; i < 3; i++)
            { 
                var tmp = FindHerb(recipe[i].color, recipe);

                if (tmp != null)
                    recipe[i] = tmp;
                if (!StaticFunctions.CheckForMatch(occupiedRecipes, recipe, 3))
                    break;
            
            }
        }
        return recipe;
    }

    public Herb FindHerb(Herb.HerbColor color, Herb[] occupied = null)
    {
        Herb[] herbs = Resources.LoadAll<Herb>("HerbItems/");
        Herb result = null;
        int startIndex = UnityEngine.Random.Range(0, herbs.Length);
        for (int i = startIndex; i < herbs.Length; i++)
        {
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
                if (herbs[i].color == color && occupied != null && !occupied.Contains(herbs[i]))
                {
                    result = herbs[i];
                    break;
                }
            }
        }
        return result;
    }
    public override void Execute()
    {
        if (Time.time < cooldownTimer)
            return;

        SavingManager.gameData.Items["Tea"][name]--;
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
        if (isRecipeKnown)
            return;
        SavingManager.SetRecipeKnown(this);
        FindObjectOfType<LoadTeaRecipes>().AddTea(this);
    }
}
