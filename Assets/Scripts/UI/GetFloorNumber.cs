using UnityEngine;
using UnityEngine.UI;

public class GetFloorNumber : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = GenerateDungeon.dungeonLevel.ToString();
    }
}
