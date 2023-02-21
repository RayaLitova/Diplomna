using UnityEngine;
using UnityEngine.UI;

public class ShowDamagePopups : MonoBehaviour
{
    public static void ShowPopup(string type, int damage, Vector3 enemyPosition)
    {
        GameObject popup = Instantiate(Resources.Load<GameObject>("TextPopups/" + type), GameObject.Find("DamageNumbers").transform);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(enemyPosition);
        popup.transform.position = screenPosition + new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));
        if (type == "Miss")
            return;

        popup.GetComponent<Text>().text = damage.ToString();
    }
}
