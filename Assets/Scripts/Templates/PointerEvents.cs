using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBeingDragged = false;
    private CanvasGroup canvasGroup;
    private UI_manager manager;

    private Vector3 initialPosition;
    [SerializeField] string UI_manager_obj_name;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        manager = GameObject.Find(UI_manager_obj_name).GetComponent<UI_manager>();
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
        string key = manager.getClosestSlot(transform.position);
        string oldKey = manager.objects.Where(pair => pair.Value == GetComponent<Display>().Get())
            .Select(pair => pair.Key.ToString()).FirstOrDefault();

        transform.position = initialPosition;
        if (key == null)
        {
            if (oldKey != null)
            {
                manager.slotDisplay[oldKey].Remove();
                manager.objects[oldKey] = null;
            }
            return;
        }
        moveItem(key, oldKey);
    }

    public void moveItem(string to, string from = null)
    {

        if (from == null) // Move from menu (or for swap)
        {
            manager.objects[to] = GetComponent<Display>().Get();
            manager.slotDisplay[to].Activate(manager.objects[to], true);

        }
        else // Move from action bar
        {
            if (manager.objects[to] == null) // New slot is empty
            {
                manager.objects[to] = gameObject.GetComponent<Display>().Get();
                manager.slotDisplay[to].Activate(manager.objects[to], false);

                manager.slotDisplay[from].Remove();
                manager.objects[from] = null;
            }
            else // Swap with skill from new slot
            {
                Usable obj = manager.objects[from]; //save 
                manager.objects[from] = null; //condition for "new slot is empty"

                manager.slotDisplay[to].gameObject.GetComponent<PointerEvents>().moveItem(from, to); //clear new slot
                manager.objects[to] = obj;
                manager.slotDisplay[to].Activate(manager.objects[to], false);
            }
        }
    }
}
