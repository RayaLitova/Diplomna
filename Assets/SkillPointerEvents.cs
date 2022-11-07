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
        Skills_UI skillsUi = transform.parent.GetComponent<Skills_UI>();
        isBeingDragged = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
        string key = skillsUi.getClosestSkillSlot(transform.position);
        string oldKey = Skills_UI.SkillsTemp.Where(pair => pair.Value == gameObject.GetComponent<UI_Skill_Info>())
            .Select(pair => pair.Key.ToString()).FirstOrDefault();
        if (key == null)
        {
            if (oldKey != null)
            {
                Skills_UI.Skills[oldKey] = null;
                Skills_UI.SkillsTemp[oldKey] = null;
            }
            Destroy(gameObject);
            return;
        }

        moveSkill(key, oldKey);
    }

    public void moveSkill(string to, string from)
    {
        Skills_UI skillsUi = transform.parent.GetComponent<Skills_UI>();
        if(from != null)
            Skills_UI.SkillsTemp[from] = null;
        rectTransform.anchoredPosition = skillsUi.SkillSlotAnchoredPosition[to];
        Skills_UI.Skills[to] = gameObject.GetComponent<UI_Skill_Info>();
        if (Skills_UI.SkillsTemp[to] != null)
            Skills_UI.SkillsTemp[to].gameObject.GetComponent<SkillPointerEvents>().moveSkill(from, to);
        Skills_UI.SkillsTemp[to] = Skills_UI.Skills[to];
    }



}
