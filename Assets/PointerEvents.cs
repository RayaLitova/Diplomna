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
        string oldKey = UI_ItemsManage.itemsTmp.Where(pair => pair.Value == GetComponent<DisplayItem>().GetItem())
            .Select(pair => pair.Key.ToString()).FirstOrDefault();

        transform.position = initialPosition;
        if (key == null)
        {
            if (oldKey != null)
            {
                UI_ItemsManage.itemSlotDisplayItem[oldKey].Remove();
                UI_ItemsManage.itemsTmp[oldKey] = null;
                UI_ItemsManage.items[oldKey] = null;
            }
            return;
        }
        moveSkill(key, oldKey);
    }

    public void moveSkill(string to, string from)
    {
        
        if (from == null) // Move from skill menu (or for swap)
        {
            UI_ItemsManage.itemsTmp[to] = GetComponent<DisplayItem>().GetItem();
            UI_ItemsManage.items[to] = UI_ItemsManage.itemsTmp[to];
            UI_ItemsManage.itemSlotDisplayItem[to].Activate(UI_ItemsManage.items[to], true);

        }
        else // Move from action bar
        {
            if (UI_ItemsManage.itemsTmp[to] == null) // New slot is empty
            {
                UI_ItemsManage.itemsTmp[to] = gameObject.GetComponent<DisplayItem>().GetItem();
                UI_ItemsManage.items[to] = UI_ItemsManage.itemsTmp[to];
                UI_ItemsManage.itemSlotDisplayItem[to].Activate(UI_ItemsManage.items[to], false);

                UI_ItemsManage.itemSlotDisplayItem[from].Remove();
                UI_ItemsManage.itemsTmp[from] = null;
                UI_ItemsManage.items[from] = null;
            }
            else // Swap with skill from new slot
            {
                Item item = UI_ItemsManage.items[from]; //save item
                UI_ItemsManage.itemsTmp[from] = null; //condition for "new slot is empty"

                UI_ItemsManage.itemSlotDisplayItem[to].gameObject.GetComponent<PointerEvents>().moveSkill(from, to); //clear new slot
                UI_ItemsManage.items[to] = item;
                UI_ItemsManage.itemsTmp[to] = UI_ItemsManage.items[to];
                UI_ItemsManage.itemSlotDisplayItem[to].Activate(UI_ItemsManage.items[to], false);
            }
        }
    }
}
