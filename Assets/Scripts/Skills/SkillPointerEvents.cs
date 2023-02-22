using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System;


public class SkillPointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBeingDragged = false;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private UI_skillsManage skillsUi;

    private bool isFromMenu = false;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        skillsUi = GameObject.Find("Skills").GetComponent<UI_skillsManage>();
    }

    private void FixedUpdate()
    {
        if (isBeingDragged)
        {
            Vector3 position = Input.mousePosition;
            position.z = 100.0f; //plane to camera distance
            transform.position = position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (transform.parent.name != "Skills") //For skill menu
        {
            GameObject dub = Instantiate(gameObject, transform.parent);
            dub.transform.SetSiblingIndex(2);
            isFromMenu = true;
        }
        isBeingDragged = true;
        canvasGroup.alpha = 0.5f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingDragged = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
        string key = skillsUi.getClosestSkillSlot(transform.position);
        string oldKey = null;

        foreach (string k in UI_skillsManage.Skills.Keys)
        {
            if (UI_skillsManage.Skills[k] != null && 
                UI_skillsManage.Skills[k].gameObject.name.RemoveClones() == gameObject.name.RemoveClones())
            {
                oldKey = k;
                break;
            }
        }
        if (isFromMenu && oldKey != null) //if the user is trying to place the same skill twice
        {
            RemoveSkill(oldKey);
            oldKey = null;
        }
        if (key == null)
        {
            if (oldKey != null)
                RemoveSkill(oldKey);

            Destroy(gameObject);
            return;
        }
        moveSkill(key, oldKey);
    }

    public void RemoveSkill(string from)
    {
        UI_skillsManage.Skills[from].DestroySkill();
        Destroy(UI_skillsManage.Skills[from].gameObject);
        UI_skillsManage.Skills[from] = null;
    }

    public void moveSkill(string to, string from)
    {
        transform.SetParent(skillsUi.gameObject.transform);
        transform.localScale = new Vector3(0.87f, 0.87f, 0.87f); //scale to fit in slot
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);//size to fit in slot
        try
        {
            transform.GetComponent<SkillParticlesInstantiate>().enabled = true; // if particles already exist
        }
        catch (Exception) { };
        transform.GetComponent<UI_Skill_Execution>().enabled = true;
        rectTransform.anchoredPosition = skillsUi.SkillSlotAnchoredPosition[to];

        if (from == null) // Move from skill menu (or for swap)
        {
            if (UI_skillsManage.Skills[to] != null && isFromMenu)
            {
                UI_skillsManage.Skills[to].DestroySkill();
                Destroy(UI_skillsManage.Skills[to].gameObject);
            }
            UI_skillsManage.Skills[to] = gameObject.GetComponent<UI_Skill_Execution>();
            UI_skillsManage.ReduceCooldown(to);
        }
        else // Move from action bar
        {
            if (UI_skillsManage.Skills[to] == null) // New slot is empty
            {
                UI_skillsManage.Skills[from] = null;
                UI_skillsManage.Skills[to] = gameObject.GetComponent<UI_Skill_Execution>();
            }
            else // Swap with skill from new slot
            {
                var tmp = UI_skillsManage.Skills[from];
                UI_skillsManage.Skills[to].gameObject.GetComponent<SkillPointerEvents>().moveSkill(from, null);
                UI_skillsManage.Skills[to] = tmp;
            }
        }
        isFromMenu = false;
    }
}
