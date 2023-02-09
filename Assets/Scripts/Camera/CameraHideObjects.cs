using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CameraHideObjects : MonoBehaviour
{
    Transform player;

    List<Transform> currentlyInTheWay = new List<Transform>();
    List<Transform> alreadyTransparent = new List<Transform>();

    Material solidMaterial;
    Material transparentMaterial;
    void Start()
    {
        player = GameObject.Find("Player").transform;

        solidMaterial = Resources.Load<Material>("Materials/solid");
        transparentMaterial = Resources.Load<Material>("Materials/transparent");
    }

    void Update()
    {
        currentlyInTheWay.Clear(); //clear list

        float cameraPlayerDistance = Vector3.Magnitude(transform.position - player.position);
        Ray ray = new Ray(transform.position, player.position - transform.position);
        var hits = Physics.RaycastAll(ray, cameraPlayerDistance);

        foreach (var hit in hits) 
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.name == "PortalActive") //fix portal texture issue
                continue;

            if (!currentlyInTheWay.Contains(obj.transform) && (obj.tag == "BackgroundObjects" || obj.tag == "Interactable"))
                currentlyInTheWay.Add(obj.transform);
        }

        foreach (var obj in currentlyInTheWay)
        {
            if (!alreadyTransparent.Contains(obj))
            {
                obj.GetComponent<MeshRenderer>().material = transparentMaterial; //make transparent
                alreadyTransparent.Add(obj);
            }
        }

        for (int i = 0; i < alreadyTransparent.Count; i++)
        {
            if (!currentlyInTheWay.Contains(alreadyTransparent[i]))
            {
                alreadyTransparent[i].GetComponent<MeshRenderer>().material = solidMaterial; //make solid
                alreadyTransparent.Remove(alreadyTransparent[i]);
            }

        }
    }
}
