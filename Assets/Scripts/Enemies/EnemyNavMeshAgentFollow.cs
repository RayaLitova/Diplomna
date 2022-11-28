using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshAgentFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform character;
    [SerializeField] private GetCurrentRoom rooms;

    private Vector3 startPosition;
    private float attackTime;
    private float attackCooldown = 0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (attackTime > Time.time)
            return;

        GetComponent<EnemyAttack>().FinishExecution();
        if (rooms.CheckRooms(character) != null && rooms.CheckRooms(character) == rooms.CheckRooms(transform))
            agent.SetDestination(character.position);
        else
            agent.SetDestination(startPosition);
        Debug.Log(attackCooldown);
        if (attackCooldown < Time.time && Vector3.Distance(character.position, transform.position) <= agent.stoppingDistance)
        {
            transform.LookAt(character.position);
            GetComponent<EnemyAttack>().StartExecution();
            attackTime = Time.time + 2f;
            attackCooldown = Time.time + 5f;
        }
    }
}
