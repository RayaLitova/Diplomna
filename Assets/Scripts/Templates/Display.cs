using UnityEngine;
using UnityEngine.UI;
public class Display : MonoBehaviour
{
    protected Usable obj = null;

    public virtual void DisplayObj(Usable objRef = null)
    { 
        if(objRef != null)
            obj = objRef;

        Image image = GetComponent<Image>();
        image.sprite = obj.icon;
        transform.rotation = new Quaternion(0, 0, 1, 0); //z = 180
        image.color = new Color(image.color.r, image.color.g, image.color.r, 1f);
    }

    public virtual void ShowDescription()
    {
        if (obj == null)
            return;

        Transform desc = transform.parent.parent.parent.GetChild(1); // Description game object
        desc.gameObject.SetActive(true);
        desc.GetChild(1).GetComponent<Text>().text = obj.name; // name
        desc.GetChild(2).GetComponent<Text>().text = obj.description; // description
    }

    public virtual void HideDescription()
    {
        transform.parent.parent.parent.GetChild(1).gameObject.SetActive(false);
    }

    public Usable Get()
    {
        return obj;
    }

    public virtual void Remove()
    {
        Image image = GetComponent<Image>();
        image.sprite = null;
        image.color = new Color(image.color.r, image.color.g, image.color.r, 0f);
        obj = null;
    }

    public virtual void Activate(Usable objRef = null, bool playParticles = false) //for items
    {
        DisplayObj(objRef);
    }
}

