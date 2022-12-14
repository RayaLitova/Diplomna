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
    [SerializeField] float stoppingDistance = 55f;

    private EnemyAnimationController animationController;
    private AudioSource audioSource;
    private bool isBossMusicPlaying = false;

    public float walkSpeedMultiplier = 1;
    void Start()
    {
        animationController = GetComponent<EnemyAnimationController>();
        character = GameObject.Find("Kgirls01").transform;
        rooms = GameObject.Find("Rooms").GetComponent<GetCurrentRoom>();
        startPosition = transform.position;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (attackTime > Time.time)
            return;

        GetComponent<EnemyAttack>().FinishExecution();

        if (Vector3.Distance(new Vector3(character.position.x, transform.position.y, character.position.z), transform.position) <= stoppingDistance)
        {
            animationController.WalkAnimation(false);
            if (attackCooldown > Time.time)
                return;
            GetComponent<EnemyAttack>().StartExecution();
            attackTime = Time.time + 2f;
            attackCooldown = Time.time + 3f;
            return;
        }
        else if (Vector3.Distance(character.position, transform.position) > stoppingDistance && rooms.CheckRooms(character) != null && rooms.CheckRooms(character) == rooms.CheckRooms(transform))
        {
            animationController.WalkAnimation(true);
            transform.LookAt(new Vector3(character.position.x, transform.position.y, character.position.z)); // fix rotating on y axis
            transform.position += transform.forward * walkSpeedMultiplier;
            if (gameObject.tag == "Boss" && !isBossMusicPlaying)
            {
                audioSource.enabled = false;
                audioSource.clip = Resources.Load<AudioClip>("Audio/Boss");
                audioSource.enabled = true;
                isBossMusicPlaying = true;
            }
        }
        else if (Vector3.Distance(startPosition, transform.position) < stoppingDistance)
        {

            animationController.WalkAnimation(false);
        }
        else
        {

            animationController.WalkAnimation(true);
            transform.LookAt(new Vector3(startPosition.x, transform.position.y, startPosition.z)); // fix rotating on y axis
            transform.position += transform.forward * walkSpeedMultiplier;

            if (gameObject.tag == "Boss" && isBossMusicPlaying)
            {
                audioSource.enabled = false;
                audioSource.clip = Resources.Load<AudioClip>("Audio/Dungeon");
                audioSource.enabled = true;
                isBossMusicPlaying = false;
            }
        }

    }

    private void OnDestroy()
    {
        if (gameObject.tag != "Boss")
            return;

        audioSource.enabled = false;
        audioSource.clip = Resources.Load<AudioClip>("Audio/Dungeon");
        audioSource.enabled = true;
    }
}
