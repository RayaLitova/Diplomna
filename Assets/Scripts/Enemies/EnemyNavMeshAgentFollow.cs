using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshAgentFollow : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform character;
    private GetCurrentRoom rooms;

    private Vector3 startPosition;
    private float attackTime;
    private float attackCooldown = 0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GameObject.Find("Kgirls01").transform;
        rooms = GameObject.Find("Rooms").GetComponent<GetCurrentRoom>();
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
            if(Vector3.Distance(startPosition, transform.position) > agent.stoppingDistance) 
                agent.SetDestination(startPosition);

        if (Vector3.Distance(character.position, transform.position) <= agent.stoppingDistance)
        {
            transform.LookAt(character.position);
            GetComponent<EnemyAttack>().StartExecution();
            attackTime = Time.time + 2f;
            attackCooldown = Time.time + 5f;
        }
    }
}
