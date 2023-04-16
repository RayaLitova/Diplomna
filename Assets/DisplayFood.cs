using UnityEngine.UI;

public class DisplayFood : Display
{
    public override void DisplayObj(Usable objRef = null)
    {
        base.DisplayObj(objRef);
        UpdateCount();
    }

    public void UpdateCount()
    {
        if (obj == null)
            return;
        transform.parent.Find("Count").GetComponent<Text>().text = ((Food)obj).count.ToString();
    }
}
