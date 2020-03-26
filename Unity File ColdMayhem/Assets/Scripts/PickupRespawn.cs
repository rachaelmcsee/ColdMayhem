using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Andrew Mabry (Drew)
public class PickupRespawn : MonoBehaviour
{
    //creating variables
    public GameObject pickup;
    public float respawnTime = 30;
    float time;
    public bool itemGot = false;

    private void Start()
    {
        //spawning the item at this position
        GameObject itemCopy = Instantiate(pickup, transform.position, transform.rotation);

        //getting the code from the new object and setting the spawn to this spawn point
        AmmoPickup itemCode = itemCopy.GetComponent<AmmoPickup>();
        itemCode.spawn = this;
    }
    private void Update()
    {
        //if the item is got it starts the respawn timer
        if (itemGot)
        {
            time += Time.deltaTime;
            //if the time reaches respawn time then the item will be instantiated
            if(time >= respawnTime)
            {
                //spawning the item at this position
                GameObject itemCopy = Instantiate(pickup, transform.position, transform.rotation);
                
                //getting the code from the new object and setting the spawn to this spawn point
                AmmoPickup itemCode = itemCopy.GetComponent<AmmoPickup>();
                itemCode.spawn = this;
                //resetting values
                time = 0;
                itemGot = false;
            }

        }
    }
}
