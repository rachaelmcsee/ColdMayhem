using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    //declaring varibales
    RaycastHit hit;
    EnemyMovement moveScript;
    public bool targetVisible = false;
    public float range = 40f;
    //this is used as a buffer because the target is constantly be switched between true and false
    float delay = 0;

    public bool test = false;
    void Start()
    {
        //getting access to the EnemyMovement script to get target and direction info and repeating the SightCheck every little bit to reduce load on the game
        moveScript = GetComponent<EnemyMovement>();
        InvokeRepeating("SightCheck", .1f, .3f);
    }

    void SightCheck()
    {
        //checks to make sure that its current enemy is something that should be thrown at
        if (this.GetComponent<EnemyThrow>().isEnemy && moveScript.targetObject != null)
        {
            if (delay > 0)
            {
                delay -= .5f;
            }
            //sends out a ray infront of the enemy and sends the data back to the hit variable
            Physics.Raycast(transform.position, moveScript.direction, out hit, range);
            //checking the ray information for the target and then states if the ray hit the target or not
            if(hit.collider != null)
            {
                if (hit.collider.gameObject == moveScript.targetObject)
                {
                    delay = 1;
                    targetVisible = true;
                }
                else
                {
                    if (delay <= 0)
                    {
                        targetVisible = false;
                    }
                }
            }
            else
            {
                //this checks to see if the player exists if not then the visibility is set to false
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(player == null)
                {
                    targetVisible = false;
                }
            }
            if (moveScript.target == null)
            {
                targetVisible = false;
            }
            
        }
        

    }
}