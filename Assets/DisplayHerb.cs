using UnityEngine;
using UnityEngine.UI;

public class DisplayHerb : MonoBehaviour
{
    private Herb herb;
    public void Display(Herb herbRef = null)
    {
        if (herbRef != null)
            herb = herbRef;

        Image image = GetComponent<Image>();
        image.sprite = herb.icon;
        transform.rotation = new Quaternion(0, 0, 1, 0); //z = 180
        image.color = new Color(image.color.r, image.color.g, image.color.r, 1f);
        UpdateCount();
    }

    public void UpdateCount()
    {
        if(herb == null)
            return;
        transform.parent.Find("Count").GetComponent<Text>().text = herb.count.ToString();
    }

    public bool CheckHerb(Herb h)
    {
        return h == herb;
    }

}
