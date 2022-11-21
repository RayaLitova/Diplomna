using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float followDistance;
    [SerializeField] private float attackDistance;
    private Vector3 startPosition;
    private CharacterController enemyController;
    private Transform character;

    private float interruptTimer = 0f;

    private void Start()
    {
        startPosition = transform.position;
        startPosition.y = 0;
        enemyController = GetComponent<CharacterController>();
        character = GameObject.Find("Kgirls01").transform;

    }
    private void FixedUpdate()
    {
        if (interruptTimer > Time.time)
            return;

        GetComponent<EnemyAttack>().FinishExecution();
        if (Vector3.Distance(startPosition, character.position) < followDistance && Vector3.Distance(transform.position, character.position) > attackDistance)
        {
            Debug.Log("Follow");
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, 2f);

        }
        else if (Vector3.Distance(startPosition, character.position) >= followDistance && Vector3.Distance(transform.position, startPosition) > 10)
        {
            Debug.Log(Vector3.Distance(startPosition, transform.position));
            transform.position = Vector3.MoveTowards(transform.position, startPosition, 1f);
        }
        else if (Vector3.Distance(transform.position, character.position) <= attackDistance)
        {
            GetComponent<EnemyAttack>().StartExecution();
            interruptTimer = Time.time + 2.0f;
        }
    }
}
