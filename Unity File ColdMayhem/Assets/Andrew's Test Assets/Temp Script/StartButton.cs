using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    //making variables that can store the starting spawns for the enemy and player
    public RespawnScript playerRespawn;
    public RespawnScript enemyRespawn;

    //stores the game info script in the scene
    public GameInfo info;

    public GameObject respawnCam;
    public Text spawnText;
    public GameObject defeatText;
    private void Start()
    {
        respawnCam = GameObject.FindGameObjectWithTag("RespawnCam");
    }

    public void Spawn()
    {
        //if the lives are the same as when the game started then this is the begining of the game then the player and enemy will be spawned in
        if (info.playerLives == 3)
        {
            playerRespawn.PlayerSpawn(info.playerCharacters[info.playerChoice]);
            enemyRespawn.PlayerSpawn(info.enemyCharacters[info.enemyChoice]);
            spawnText.text = "Respawn";
        }
        else
        {
            //setting temp variables for finding the most distance
            float farthestDis = 0;
            float dis;
            GameObject farthestSpawn = null;

            //finding all respawn locations then finding the farthest one from the enemy
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
            foreach (GameObject spawnPoint in spawnPoints)
            {
                //finding the distance of the spawn then checking if it is farther then the previous farthest if so assigning to the temp variables
                dis = spawnPoint.GetComponent<RespawnScript>().checkDis("Enemy");
                if (dis > farthestDis)
                {
                    farthestDis = dis;
                    farthestSpawn = spawnPoint;
                }
            }

            farthestSpawn.GetComponent<RespawnScript>().PlayerSpawn(info.playerCharacters[info.playerChoice]);
            
            if(info.playerLives <= 1)
            {
                this.gameObject.SetActive(false);
            }
        }

        //setting the respawn camera to inactive after assigning it to the player
        GameObject.FindGameObjectWithTag("Player").GetComponent<HP>().respawnCam = respawnCam;
        if (info.playerLives <= 1)
        {
            defeatText.SetActive(true);
            this.gameObject.SetActive(false);
        }
        respawnCam.SetActive(false);
    }
}
