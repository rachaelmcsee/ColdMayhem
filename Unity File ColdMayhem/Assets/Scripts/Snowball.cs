using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Andrew Mabry (Drew)
public class Snowball : MonoBehaviour
{

    //declaring variables
    public Collider snowballCol;
    public GameObject thisObject;
    public GameObject particleImpact;
    public Transform thisPos;

    //activating code if the snowball colides with anything
    private void OnCollisionEnter(Collision collision)
    {
        //creating a particle affect on colision to make the snowball look like it exploded
        Instantiate(particleImpact, thisPos.position, thisPos.rotation);
        //deleting the snowball on impact
        Destroy(thisObject);
    }
}
