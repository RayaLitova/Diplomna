using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavMeshAgentFollow : MonoBehaviour
{
    private Transform character;
    private GetCurrentRoom rooms;

    private Vector3 startPosition;
    private float attackTime;
    private float attackCooldown = 0f;
    [SerializeField] float stoppingDistance = 60f;

    private EnemyAnimationController animationController;
    void Start()
    {
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

        if (attackCooldown < Time.time && Vector3.Distance(character.position, transform.position) <= stoppingDistance + 5f)
        {
            animationController.WalkAnimation(false);
            transform.LookAt(character.position);
            GetComponent<EnemyAttack>().StartExecution();
            attackTime = Time.time + 2f;
            attackCooldown = Time.time + 3f;
            return;
        }

        if (Vector3.Distance(character.position, transform.position) > stoppingDistance && rooms.CheckRooms(character) != null && rooms.CheckRooms(character) == rooms.CheckRooms(transform))
        {
            animationController.WalkAnimation(true);
            transform.position = Vector3.MoveTowards(transform.position, character.position, 1f);
            transform.LookAt(character.position);
        }
        else
        {
            if (Vector3.Distance(startPosition, transform.position) < stoppingDistance)
                return;
            animationController.WalkAnimation(true);
            transform.position = Vector3.MoveTowards(transform.position, startPosition, 1f);
            transform.LookAt(startPosition);
        }

        
    }
}
