using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Andrew Mabry (Drew)
public class AmmoPickup : MonoBehaviour
{
    //declaring variables
    public int ammoAmount = 5;
    public bool hadEntered = false;
    public PickupRespawn spawn;

    private void OnTriggerEnter(Collider other)
    {
        //checking if the other object is the player
        if (other.gameObject.tag == "Player")
        {
            //getting the script information from the player
            Throw ammo = other.gameObject.GetComponent<Throw>();
            //checking if the player is at max ammo
            if (ammo.curAmmo < ammo.maxAmmo)
            {
                //calling the GainAmmo method passing the ammoAmount variable as an argument
                ammo.GainAmmo(ammoAmount);
                //getting the code for the item's respawn point then settign the got item bool to true
                PickupRespawn respawn = spawn.GetComponent<PickupRespawn>();
                respawn.itemGot = true;
                //removing the pickup
                Destroy(this.gameObject);
            }
               
        }
        if(other.gameObject.tag == "Enemy")
        {
            //getting the script information from the player
            EnemyThrow enemyAmmo = other.gameObject.GetComponent<EnemyThrow>();
            //checking if the player is at max ammo
            if (enemyAmmo.curAmmo < enemyAmmo.maxAmmo)
            {
                //calling the GainAmmo method passing the ammoAmount variable as an argument
                enemyAmmo.GainAmmo(ammoAmount);
                //getting the code for the item's respawn point then settign the got item bool to true
                PickupRespawn respawn = spawn.GetComponent<PickupRespawn>();
                respawn.itemGot = true;
                //removing the pickup
                Destroy(this.gameObject);
            }
        }
    }
}
