using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;



public class PointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBeingDragged = false;
    private CanvasGroup canvasGroup;
    private UI_ItemsManage itemsUI;

    private Vector3 initialPosition;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemsUI = GameObject.Find("ItemSlots").GetComponent<UI_ItemsManage>();
        initialPosition = transform.position;
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
        isBeingDragged = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingDragged = false;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;

        string key = itemsUI.getClosestItemSlot(transform.position);
        string oldKey = UI_ItemsManage.itemsTmp.Where(pair => pair.Value == GetComponent<DisplayItem>())
            .Select(pair => pair.Key.ToString()).FirstOrDefault();

        transform.position = initialPosition;
        Debug.Log(key + " " + oldKey);
        if (key == null)
        {
            if (GetComponent<ItemActivation>().enabled == true)
            {
                GetComponent<ItemActivation>().enabled = false;
                GetComponent<DisplayItem>().Remove();
            }
            return;
        }
        moveSkill(key, oldKey);
    }

    public void moveSkill(string to, string from)
    {
        
        if (from == null) // Move from skill menu (or for swap)
        {
            UI_ItemsManage.itemsTmp[to].Display(gameObject.GetComponent<DisplayItem>().GetItem());
            UI_ItemsManage.itemsTmp[to] = gameObject.GetComponent<DisplayItem>();
            UI_ItemsManage.items[to] = UI_ItemsManage.itemsTmp[to];

        }
        else // Move from action bar
        {
            if (UI_ItemsManage.itemsTmp[to] == null) // New slot is empty
            {
                UI_ItemsManage.itemsTmp[from].Remove();
                UI_ItemsManage.itemsTmp[from] = null;
                UI_ItemsManage.items[from] = UI_ItemsManage.itemsTmp[from];

                UI_ItemsManage.itemsTmp[to] = gameObject.GetComponent<DisplayItem>();
                UI_ItemsManage.items[to] = UI_ItemsManage.itemsTmp[to];
            }
            else // Swap with skill from new slot
            {
                UI_ItemsManage.items[to].Display(UI_ItemsManage.itemsTmp[from].GetItem());
                UI_ItemsManage.items[to] = UI_ItemsManage.itemsTmp[from];
                UI_ItemsManage.itemsTmp[to].gameObject.GetComponent<PointerEvents>().moveSkill(from, null);
                UI_ItemsManage.itemsTmp[to] = UI_ItemsManage.items[to];
            }
        }
    }
}
