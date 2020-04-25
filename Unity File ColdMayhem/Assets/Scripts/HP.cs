using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//by Andrew Mabry (Drew)
//some of this script was learned from the youtuber Brackeys

public class HP : MonoBehaviour
{
    //storing the prefab of this character
    public GameObject thisPrefab;
    //set this to the tag of the other team
    public string opponentTag;

    //Declaring variables
    public int maxHP = 100;
    public int currentHP;
    public ParticleSystem KO_Particle;
    public Text hpDisplay;
    string temp;
    //getting the variables for the health bar
    public Slider hpBar;
    public Gradient hpGradient;
    public Image hpFill;

    //making variables used for game info and to store lives
    GameInfo info;
    int yourLives;

    //making a bool to state that the enemy is dead so that they won't try to respawn 2 times
    bool isDead = false;

    private void Start()
    {
        //setting the initail values that are based off of public variables
        info = GameObject.FindGameObjectWithTag("Info").GetComponent<GameInfo>();
        currentHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
        if(hpDisplay != null)
            hpDisplay.text = "HP: " + currentHP.ToString();

        hpFill.color = hpGradient.Evaluate(1f);
    }
   
    private void OnCollisionEnter(Collision other)
    {
        //checking if the collision was a snowball
        if (other.gameObject.tag == "Snowball")
        {
            //taking appropriat damage
            currentHP -= 10;
            //if the person or object have a health display it adjusts accordingly
            if (hpDisplay != null)
            {
                hpDisplay.text = "HP: " + currentHP.ToString();   
            }
            //if the person or object have a health bar it adjusts accordingly
            if (hpBar != null)
            {
                hpBar.value = currentHP;
                hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
            }
                
        }
        
        //checking to see if the player is alive
        if (currentHP <= 0 && !isDead)
        {
            isDead = true;
            //calling on deathparticles on a delay to give time for the entity to stop befor playing the particles
            Invoke("DeathParticles", .5f);
        }
    }

    void DeathParticles()
    {
        //creating particles for the death and then calling on the death method on a delay
        Instantiate(KO_Particle, transform.position, transform.rotation);
        Invoke("Death", .5f);
    }
    void Death()
    {
        //setting temp variables for finding the most distance
        float farthestDis = 0;
        float dis;
        GameObject farthestSpawn = null;

        //reduces the lives of the appropriate team
        if (this.tag == "Enemy")
        {
            info.enemyLives -= 1;
            yourLives = info.enemyLives;
        }
        else
        {
            if(this.tag == "Player")
            {
                info.playerLives -= 1;
                yourLives = info.playerLives;
            }
        }

        if(yourLives > 0)
        {
            //finding all respawn locations then finding the farthest one from the enemy
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
            foreach (GameObject spawnPoint in spawnPoints)
            {
                //finding the distance of the spawn then checking if it is farther then the previous farthest if so assigning to the temp variables
                dis = spawnPoint.GetComponent<RespawnScript>().checkDis(opponentTag);
                if (dis > farthestDis)
                {
                    farthestDis = dis;
                    farthestSpawn = spawnPoint;
                }
            }

            farthestSpawn.GetComponent<RespawnScript>().Respawn(thisPrefab);
        }
        else
        {
            if(this.tag == "Enemy")
            {
                Text victoryText = GameObject.FindGameObjectWithTag("Victory").GetComponent<Text>();
                victoryText.text = "Victory";
            }
            else
            {
                
            }
        }
        

        //deleting the player or entity
        Destroy(gameObject);
    }
}
