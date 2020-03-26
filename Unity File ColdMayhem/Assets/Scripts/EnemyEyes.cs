using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    Transform eyes;
    public Quaternion tilt;
    public float angle;
    public float angleSpeed = 5f;

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
            angle = movement.playerDis * .001f;
            tilt = Quaternion.LookRotation(new Vector3(movement.direction.x, angle, movement.direction.z));
            eyes.rotation = Quaternion.Slerp(eyes.rotation, tilt, Time.deltaTime * angleSpeed);
        }
        
    }
}
