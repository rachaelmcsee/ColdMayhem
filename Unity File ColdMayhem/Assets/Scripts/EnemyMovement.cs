﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Andrew Mabry
//I moddified most of this code from the youtuber Brackeys

public class EnemyMovement : MonoBehaviour
{
    //declaring variables
    public Transform target;
    public float toFar = 9f;
    NavMeshAgent agent;
    public float playerDis;
    public Vector3 direction;
    Quaternion lookDirection;
    public float lookSpeed = 3f;
    string targetTag = "Player";
    public GameObject targetObject;
    bool isDead = false;
    HP hp;

    private void Start()
    {
        //getting HP info
        hp = GetComponent<HP>();
        //getting the navMeshAgent and using it to set the stopping distance
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = toFar;
        //calls on the AquireTarget method every 1 second
        InvokeRepeating("AquireTarget", 0, 1);
    }
    void Update()
    {
        //checking the distance from the player
        playerDis = Vector3.Distance(target.position, transform.position);

        if (hp.currentHP <= 0)
            isDead = true;

        //seeing if the target is too far and if so then moving to the player and if not then making sure that the AI is facing them
        if(playerDis > toFar && !isDead)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            if(!isDead)
                FaceTarget();
        }
       if (isDead)
        {
            agent.Stop();
        }
    }

    void AquireTarget()
    {
        //checks if the AI no longer has a target
        if(target == null)
        {
            targetObject = GameObject.FindGameObjectWithTag(targetTag);
            target = targetObject.transform;
        }
    }

    //creating a method to look at the target
    void FaceTarget()
    {
        //getting the direction, converting it inter a usable angle value, then rotating the AI at a fixed rate
        direction = (target.position - transform.position).normalized;
        lookDirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * lookSpeed);
    }
}
