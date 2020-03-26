using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//by Andrew Mabry (Drew)

public class EnemyThrow : MonoBehaviour
{


    //declarign variables
    public float chargePower = 1;
    public float throwStrength = 10f;
    public Transform releasePos;
    public GameObject snowball;
    Rigidbody snowballRB;
    int chargeAmount = 120;
    //determines how much longer after max charge you can hold the snowball and how long it takes to fully charge
    //remember that if you change the charge time or maxHold time then you have to change the movement penalty time!!!
    public float totalChargeTime = 2;
    public float maxHold = 2;
    public float holdTime = 0;
    //variables for the charge bar
    float chargeTime = 0;

    //making variables for the Ammo
    public int maxAmmo = 10;
    public int curAmmo = 0;
    bool hasAmmo = true;

    //creating a bool that can be viewed from the movement code
    public bool isCharging = false;

    //getting a refference to the Eyes transform


    //getting information form the enemy sight script in order to decide if the Enemy will try to fire
    public EnemySight sight;


    public bool test = false;
    private void Start()
    {
        //setting the current ammo to the max ammo and then getting a refference to the movement and sight scripts
        curAmmo = maxAmmo;

        InvokeRepeating("AttemptThrow", 0, .1f);
    }
    //this method attempt to throw a snowball by judging the distance and changing the angle
    void AttemptThrow()
    {
        
        //creating a series of if statements to judge the distance
        if (sight.targetVisible)
        {
            
            test = true;
            isCharging = true;
            
        }
        else
        {
            isCharging = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        //checking if they are holding the left mouse button
        if (isCharging && hasAmmo)
        {
            
            //using an if to see if the user has been holding the charge for less time than the total charge time
            if (chargeTime < totalChargeTime)
            {
                //increasing power of the throw
                chargePower += chargeAmount * Time.deltaTime;
                //counting the amount of time the player is charging the throw
                chargeTime += Time.deltaTime;
            }
            else
            {
                //using a second counter to make sure they don't hold a fully charged throw for more than the max hold time
                if (holdTime < maxHold)
                {
                    holdTime += Time.deltaTime;
                }
                else
                {
                    //forcing a throw if the user keeps holding
                    ThrowSnow();
                    //setting a bool variable to true make sure the player doesn't start charging again without releasing the mouse
                }
            }

        }
        else
        {
            //if they were charging it will call the throw command
            if (chargePower > 1)
            {
                ThrowSnow();
            }
        }
        

    }

    //learned this code from the youtuber Renaissance Coders on how to make a cannon I had to modify it for a charging affect and charging bar.
    public void ThrowSnow()
    {
        //setting the spawn of the snowball to the same rotation of the player camera
        //releasePos.rotation = transform.rotation;
        //spawning the snowball under the name snowballCopy
        GameObject snowballCopy = Instantiate(snowball, releasePos.position, releasePos.rotation);
        //getting the rigidBody of the copy then aplying force to the copy
        snowballRB = snowballCopy.GetComponent<Rigidbody>();
        snowballRB.AddForce(releasePos.forward * chargePower * throwStrength);
        //reseting variables so that the next shot does not get messed up
        chargePower = 1;
        holdTime = 0;
        chargeTime = 0;
        isCharging = false;
        //reducing ammo
        --curAmmo;
        //setting the has ammo to false if the current ammo drops to 0 or less
        if (curAmmo <= 0)
        {
            hasAmmo = false;
        }


    }

    //making a method that can be called on item pickup
    public void GainAmmo(int pickup)
    {
        //increases the ammo based on the pickup
        curAmmo += pickup;

        //if the current ammo is more than the max ammo then the current ammo will be set to the max
        if (curAmmo > maxAmmo)
            curAmmo = maxAmmo;
        hasAmmo = true;
    }
}
