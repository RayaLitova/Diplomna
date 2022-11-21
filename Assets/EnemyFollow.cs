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

        if (Vector3.Distance(startPosition, character.position) < followDistance && Vector3.Distance(transform.position, character.position) > attackDistance)
        {
            GetComponent<EnemyAttack>().FinishExecution();
            Vector3 moveDir = new Vector3(character.position.x - transform.position.x, 0, character.position.z - transform.position.z);
            moveDir.Normalize();
            float magnitude = Mathf.Clamp01(moveDir.magnitude);
            transform.position = Vector3.MoveTowards(transform.position, character.position, Time.deltaTime);

        }
        else if (Vector3.Distance(startPosition, character.position) >= followDistance && Vector3.Distance(transform.position, startPosition) > 10)
        {
            Vector3 moveDir = new Vector3(startPosition.x - transform.position.x, 0, startPosition.z - transform.position.z);
            moveDir.Normalize();
            float magnitude = Mathf.Clamp01(moveDir.magnitude);
            transform.position += moveDir * magnitude;
            GetComponent<EnemyAttack>().StartExecution();
            interruptTimer = Time.time + 2.0f;
        }
        else
        {
            GetComponent<EnemyAttack>().FinishExecution();
        }
    }
}
