using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//Andrew Mabry
//I moddified most of this code from the youtuber Brackeys

public class EnemyMovement : MonoBehaviour
{
    //animation variables
    public Animator thisAnimator;
    public EnemyThrow thisThrow;
    bool isWalking;

    //declaring variables
    public Transform target;
    public float toFar = 12f;
    public NavMeshAgent agent;
    public float playerDis;
    public Vector3 direction;
    Quaternion lookDirection;
    public float lookSpeed = 4f;
    string targetTag = "Player";
    public GameObject targetObject;
    bool isDead = false;
    HP hp;
    //variables for the targets velocity
    Vector3 velocity;
    Vector3 curPos;
    Vector3 lastPos;
    //variables for checking if this character is moving
    Vector3 thisCurPos;
    Vector3 thisLastPos;

    //this is used to tell if the enemy is moving
    bool isStill = false;

    private void Start()
    {
        //getting references to the animator and EnemyThrow script
        thisAnimator = this.gameObject.GetComponent<Animator>();
        thisThrow = this.gameObject.GetComponent<EnemyThrow>();

        //getting HP info
        hp = GetComponent<HP>();
        ChangeDistance(toFar);
        //calls on the AquireTarget method every 1 second and CalcVelocity
        InvokeRepeating("AquireTarget", 0, 1);
        InvokeRepeating("CalcVelocity", 1, .1f);
        //calculating this characters velocity
        InvokeRepeating("thisVelocity", 0, .1f);
    }
    void Update()
    {
        if(target != null)
        {
            //checking the distance from the player
            playerDis = Vector3.Distance(target.position, transform.position);
        }
        
        

        if (hp.currentHP <= 0)
            isDead = true;

        //seeing if the target is too far and if so then moving to the player and if not then making sure that the AI is facing them
        if(playerDis > agent.stoppingDistance && !isDead)
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

        //makes animation decisions
        if (isWalking)
        {
            if (thisThrow.isCharging)
            {
                thisAnimator.SetBool("isWalkCharging", true);
            }
            else
            {
                thisAnimator.SetBool("isWalkCharging", false);

            }
            thisAnimator.SetBool("isWalking", true);
        }
        else
        {
            thisAnimator.SetBool("isWalking", false);
            thisAnimator.SetBool("isWalkCharging", false);
            thisAnimator.SetBool("isCharging", thisThrow.isCharging);
        }

    }

    public void AquireTarget()
    {
        //checks if the AI no longer has a target
        if(target == null)
        {
            targetObject = GameObject.FindGameObjectWithTag(targetTag);
            if(targetObject != null)
                target = targetObject.transform;
        }
    }

    //creating a method to look at the target
    void FaceTarget()
    {
        //getting the direction, converting it inter a usable angle value, then rotating the AI at a fixed rate
        if(velocity != null && target != null && !isStill)
        {
            direction = ((target.position + (velocity * Time.deltaTime * (playerDis * .3f))) - transform.position).normalized;
        }
        else
        {
            if(target !=null)
                direction = (target.position - transform.position).normalized;
        }
        
        lookDirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * lookSpeed);
    }

    public void ChangeDistance(float dist)
    {
        agent.stoppingDistance = dist;
    }

    //using this to get a rough approximation of velocity of the target
    void CalcVelocity()
    {
        //confirms that there is a target
        if(target != null)
        {
            if (lastPos == null)
            {
                lastPos = target.position;
            }
            else
            {
                lastPos = curPos;
            }
            curPos = target.position;

            velocity = (curPos - lastPos) / Time.deltaTime;

            if(curPos == lastPos)
            {
                isStill = true;
            }
            else
            {
                isStill = false;
            }
        }
        
    }
    //this method is used to calculate this characters velocity to see if they are moving
    void thisVelocity()
    {
        if (thisCurPos != null)
        {
            thisLastPos = thisCurPos;
        }
        thisCurPos = transform.position;

        if (thisLastPos != null)
        {
            if(thisCurPos == thisLastPos)
            {
                isWalking = false;
            }
            else
            {
                isWalking = true;
            }
        }
        else
        {
            isStill = false;
        }
    }
    //this is used for animation that is called by EnemyThrow
    public void Throw()
    {
        if (isWalking)
        {
            thisAnimator.SetTrigger("WalkingThrow");
        }
        else
        {
            thisAnimator.SetTrigger("Throw");
        }

    }
}
