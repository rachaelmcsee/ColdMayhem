using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//by Andrew Mabry (Drew)
//some of this script was learned from the youtuber Brackeys

public class HP : MonoBehaviour
{
    //animation refference
    public Animator thisAnimator;
    //this is an empty that holds everything to make the pivit point easy to move.
    public GameObject empty;
    //set this to the tag of the other team
    public string opponentTag;

    //Declaring variables
    public int maxHP = 30;
    public int currentHP;
    public ParticleSystem KO_Particle;
    public Text hpDisplay;
    string temp;
    //getting the variables for the health bar
    public Slider hpBar;
    public Gradient hpGradient;
    public Image hpFill;

    //making an offset for the death particles
    public float particleOffset = 0;
    Vector3 particleSpawn;

    //making variables used for game info and to store lives
    GameInfo info;
    int yourLives;
    public GameObject respawnCam;

    //making a bool to state that the enemy is dead so that they won't try to respawn 2 times
    bool isDead = false;

    //this is a button that can return to the main menu when the game is over
    public GameObject menuButton;

    //this will refference the movement code
    public Movement movement;

    private void Start()
    {
        //setting the initail values that are based off of public variables and refferences
        if(thisAnimator == null)
        {
            thisAnimator = gameObject.GetComponent<Animator>();
        }
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
        if(movement == null)
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
            else
            {
                //checking if the collision was a Yetiball
                if (other.gameObject.tag == "Yetiball")
                {
                    //taking appropriat damage
                    currentHP -= 15;
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
            }
        }
        else
        {
            //checking if the collision was a snowball
            if (other.gameObject.tag == "Snowball" && !movement.isOver)
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
            else
            {
                //checking if the collision was a Yetiball
                if (other.gameObject.tag == "Yetiball")
                {
                    //taking appropriat damage
                    currentHP -= 15;
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
        //freezing animation
        thisAnimator.speed = 0f;
        //making the position for the particle spawn
        particleSpawn = transform.position;
        particleSpawn.y += particleOffset;
        //creating particles for the death and then calling on the death method on a delay
        Instantiate(KO_Particle, particleSpawn, transform.rotation);
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

        if(yourLives > 0 && this.tag == "Enemy")
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
            info.enemyChoice = Random.Range(0, 2);
            farthestSpawn.GetComponent<RespawnScript>().Respawn(info.enemyCharacters[info.enemyChoice]);
        }
        else
        {
            //if the enemy is out of lives then the player will get a victory display message
            if(this.tag == "Enemy")
            {
                Text victoryText = GameObject.FindGameObjectWithTag("Victory").GetComponent<Text>();
                if(victoryText != null)
                    victoryText.text = "Victory";
                //this finds the player and sets their is over variables to trues so the player can't move
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if(player != null)
                    player.GetComponent<Movement>().isOver = true;

                //setting the players return to main menu button to active
                player.GetComponent<HP>().menuButton.SetActive(true);
                //setting the LookAround isOver to true
                GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
                if(mainCam != null)
                    mainCam.GetComponent<LookAround>().isOver = true;
            }
        }
        
        if(this.tag == "Player")
        {
            respawnCam.SetActive(true);
            //unlocking the curser and making it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //deleting the player or entity
        if (empty != null)
            Destroy(empty);
        Destroy(gameObject);
    }
}
