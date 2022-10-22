using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_info : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float crit;
    [SerializeField] string[] effectFlags;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject.Find("Kgirls01").GetComponent<Animator>().SetBool("Hit", false);
        //GameObject.Find("Kgirls01").transform.Find().gameObject.active = false;
    }
}
