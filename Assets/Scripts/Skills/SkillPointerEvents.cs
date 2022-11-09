using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class SkillPointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBeingDragged = false;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (isBeingDragged)
        {
            Vector3 position = Input.mousePosition;
            position.z = 100.0f; //plane to camera distance
            transform.position = Camera.main.ScreenToWorldPoint(position);

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBeingDragged = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        UI_skillsManage skillsUi = transform.parent.GetComponent<UI_skillsManage>();
        isBeingDragged = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
        string key = skillsUi.getClosestSkillSlot(transform.position);
        string oldKey = UI_skillsManage.SkillsTemp.Where(pair => pair.Value == gameObject.GetComponent<UI_Skill_Info>())
            .Select(pair => pair.Key.ToString()).FirstOrDefault();
        if (key == null)
        {
            if (oldKey != null)
            {
                UI_skillsManage.Skills[oldKey] = null;
                UI_skillsManage.SkillsTemp[oldKey] = null;
            }
            Destroy(gameObject);
            return;
        }

        moveSkill(key, oldKey);
    }

    public void moveSkill(string to, string from)
    {
        UI_skillsManage skillsUi = transform.parent.GetComponent<UI_skillsManage>();
        if(from != null)
            UI_skillsManage.SkillsTemp[from] = null;
        rectTransform.anchoredPosition = skillsUi.SkillSlotAnchoredPosition[to];
        UI_skillsManage.Skills[to] = gameObject.GetComponent<UI_Skill_Info>();
        if (UI_skillsManage.SkillsTemp[to] != null)
            UI_skillsManage.SkillsTemp[to].gameObject.GetComponent<SkillPointerEvents>().moveSkill(from, to);
        UI_skillsManage.SkillsTemp[to] = UI_skillsManage.Skills[to];
    }



}
