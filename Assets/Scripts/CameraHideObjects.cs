using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHideObjects : MonoBehaviour
{
    Transform player;

    List<Transform> currentlyInTheWay = new List<Transform>();
    List<Transform> alreadyTransparent = new List<Transform>();

    Material solidMaterial;
    Material transparentMaterial;
    void Start()
    {
        player = GameObject.Find("Kgirls01").transform;

        solidMaterial = Resources.Load<Material>("Materials/solid");
        transparentMaterial = Resources.Load<Material>("Materials/transparent");
    }

    void Update()
    {
        currentlyInTheWay.Clear();

        float cameraPlayerDistance = Vector3.Magnitude(transform.position - player.position);
        Ray ray1_forward = new Ray(transform.position, player.position - transform.position);
        Ray ray1_backward = new Ray(player.position, transform.position - player.position); //when the camera is inside an object

        var hits1_forward = Physics.RaycastAll(ray1_forward, cameraPlayerDistance);
        var hits1_backward = Physics.RaycastAll(ray1_backward, cameraPlayerDistance);

        foreach (var hit in hits1_forward) 
        {
            GameObject obj = hit.collider.gameObject;
            if (!currentlyInTheWay.Contains(obj.transform) && (obj.tag == "BackgroundObjects" || obj.tag == "Interactable"))
                currentlyInTheWay.Add(obj.transform);
        }

        /*foreach (var hit in hits1_backward)
        {
            GameObject obj = hit.collider.gameObject;
            if (!currentlyInTheWay.Contains(obj.transform) && (obj.tag == "BackgroundObjects" || obj.tag == "Interactable"))
                currentlyInTheWay.Add(obj.transform);
        }*/

        foreach (var obj in currentlyInTheWay)
        {
            if (!alreadyTransparent.Contains(obj))
            {
                Debug.Log(obj.name + "Make transparent");
                obj.GetComponent<MeshRenderer>().material = transparentMaterial;
                alreadyTransparent.Add(obj);
            }
        }

        for (int i = 0; i < alreadyTransparent.Count; i++)
        {
            if (!currentlyInTheWay.Contains(alreadyTransparent[i]))
            {
                Debug.Log(alreadyTransparent[i].name + "Make solid");
                alreadyTransparent[i].GetComponent<MeshRenderer>().material = solidMaterial;
                alreadyTransparent.Remove(alreadyTransparent[i]);
            }

        }
    }
}
