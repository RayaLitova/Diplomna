using UnityEngine.EventSystems;
using System.Linq;

public class HerbPointerEvents : PointerEvents
{
    public override void OnPointerUp(PointerEventData eventData)
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
        if(SavingManager.gameData.Items["HerbItems"][GetComponent<Display>().Get().name] > 0)
            moveItem(key, oldKey);
    }
}
