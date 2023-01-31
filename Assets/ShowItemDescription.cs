using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemDescription : MonoBehaviour
{
   

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
