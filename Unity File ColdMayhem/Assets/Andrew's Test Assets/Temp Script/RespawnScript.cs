using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Andrew Mabry (Drew)
public class RespawnScript : MonoBehaviour
{
    //setting a variable so that you can insert a prefab for who needs to respawn
    public GameObject character;
    public float delay = 3;

    //making a method to call to spawn in the character on a delay
    public void Respawn(GameObject prefab)
    {
        character = prefab;
        Invoke("Spawn", delay);
    }

    void Spawn()
    {
        //spawning the character
        Instantiate(character, this.transform.position, this.transform.rotation);
    }
    //making a method to check the enemy distance from the spawn using their tag
    public float checkDis(string enemyTag)
    {
        //making a variable to store the distance from the target
        float distance;
        //finding the target
        GameObject enemy = GameObject.FindGameObjectWithTag(enemyTag);

        //checking if thier are any targets
        if(enemy != null)
        {
            //calculating the distance
            distance = Vector3.Distance(this.transform.position, enemy.transform.position);
        }
        else
        {
            //setting the distance to a default value if their are no enemies on the field
            distance = 1;
        }
        
        //sending back the distance to where it was called
        return distance;
    }
}
