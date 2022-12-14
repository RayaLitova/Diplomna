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

    private EnemyAnimationController animationController;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<EnemyAnimationController>();
        character = GameObject.Find("Kgirls01").transform;
        rooms = GameObject.Find("Rooms").GetComponent<GetCurrentRoom>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (attackTime > Time.time)
            return;

        GetComponent<EnemyAttack>().FinishExecution();

        if (Vector3.Distance(character.position, transform.position) > agent.stoppingDistance && rooms.CheckRooms(character) != null && rooms.CheckRooms(character) == rooms.CheckRooms(transform))
        {
            //agent.SetDestination(character.position);
            transform.position = Vector3.MoveTowards(transform.position, character.position, 1f);
            animationController.WalkAnimation(true);
        }
        else
        {
            if (Vector3.Distance(startPosition, transform.position) < agent.stoppingDistance)
                return;
            //agent.SetDestination(startPosition);
            transform.position = Vector3.MoveTowards(transform.position, startPosition, 1f);
            animationController.WalkAnimation(true);
        }

        if (attackCooldown < Time.time && Vector3.Distance(character.position, transform.position) <= agent.stoppingDistance)
        {
            animationController.WalkAnimation(false);
            transform.LookAt(character.position);
            GetComponent<EnemyAttack>().StartExecution();
            attackTime = Time.time + 2f;
            attackCooldown = Time.time + 3f;
        }
    }
}
