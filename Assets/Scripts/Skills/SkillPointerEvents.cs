using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SkillPointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBeingDragged = false;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private UI_skillsManage skillsUi;
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
        }
        isBeingDragged = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingDragged = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
        string key = skillsUi.getClosestSkillSlot(transform.position);
        string oldKey = UI_skillsManage.SkillsTemp.Where(pair => pair.Value == gameObject.GetComponent<UI_Skill_Execution>())
            .Select(pair => pair.Key.ToString()).FirstOrDefault();

        if (key == null)
        {
            if (oldKey != null)
                GetComponent<UI_Skill_Execution>().DestroySkill();

            Destroy(gameObject);
            return;
        }
        moveSkill(key, oldKey);
    }

    public void moveSkill(string to, string from)
    {
        transform.SetParent(skillsUi.gameObject.transform);
        transform.localScale = new Vector3(0.8604978f, 0.8604978f, 0.8604978f); //scale to fit in slot
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(65, 65);//size to fit in slot
        transform.GetComponent<UI_Skill_Execution>().enabled = true; 
        transform.GetComponent<UI_Skill_Info>().enabled = true;

        rectTransform.anchoredPosition = skillsUi.SkillSlotAnchoredPosition[to];

        if (from == null) // Move from skill menu (or for swap)
        {
            UI_skillsManage.SkillsTemp[to] = gameObject.GetComponent<UI_Skill_Execution>();
            UI_skillsManage.Skills[to] = UI_skillsManage.SkillsTemp[to];

        }
        else // Move from action bar
        {
            if (UI_skillsManage.SkillsTemp[to] == null) // New slot is empty
            {
                UI_skillsManage.SkillsTemp[from] = null;
                UI_skillsManage.Skills[from] = UI_skillsManage.SkillsTemp[from];

                UI_skillsManage.SkillsTemp[to] = gameObject.GetComponent<UI_Skill_Execution>();
                UI_skillsManage.Skills[to] = UI_skillsManage.SkillsTemp[to];
            }
            else // Swap with skill from new slot
            {
                UI_skillsManage.Skills[to] = UI_skillsManage.SkillsTemp[from];
                UI_skillsManage.SkillsTemp[to].gameObject.GetComponent<SkillPointerEvents>().moveSkill(from, null);
                UI_skillsManage.SkillsTemp[to] = UI_skillsManage.Skills[to];
            }
        }
    }
}
