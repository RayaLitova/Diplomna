using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWall : MonoBehaviour
{
    [SerializeField] Mesh doorWayMesh;
    public void ChangeToDoorWay()
    {
        GetComponent<MeshFilter>().mesh = doorWayMesh;
    }
}
