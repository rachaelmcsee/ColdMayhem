using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    Transform eyes;
    public Quaternion tilt;
    public float angle;
    public float angleSpeed = 5f;
    public float heightDif = 0;

    //getting information form the enemy movement and sight scripts in order to judge distance
    public EnemyMovement movement;
    public EnemySight sight;

    private void Start()
    {
        eyes = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        //this code will tilt up the angel of the shot based on distance
        if (sight.targetVisible)
        {
            //checking the height difference of the enemy
            heightDif = movement.target.position.y - transform.position.y;
            //desiding the angel of aim
            angle = (heightDif / movement.playerDis) + (movement.playerDis * .005f);
            tilt = Quaternion.LookRotation(new Vector3(movement.direction.x, angle, movement.direction.z));
            eyes.rotation = Quaternion.Slerp(eyes.rotation, tilt, Time.deltaTime * angleSpeed);
        }
        
    }
}
