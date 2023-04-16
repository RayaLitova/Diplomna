using UnityEngine.UI;

public class DisplayHerb : Display
{ 
    public override void DisplayObj(Usable objRef = null)
    {
        base.DisplayObj(objRef);
        UpdateCount();
    }

    public void UpdateCount()
    {
        if(obj == null)
            return;
        transform.parent.Find("Count").GetComponent<Text>().text = ((Herb)obj).count.ToString();
    }

    public bool CheckHerb(Herb h)
    {
        return h == (Herb)obj;
    }
}
