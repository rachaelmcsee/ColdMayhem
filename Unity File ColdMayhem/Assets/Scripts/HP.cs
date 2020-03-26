using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//by Andrew Mabry (Drew)
//some of this script was learned from the youtuber Brackeys

public class HP : MonoBehaviour
{
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

    private void Start()
    {
        //setting the initail values that are based off of public variables
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
        if (currentHP <= 0)
        {
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
        //deleting the player or entity
        Destroy(gameObject);
    }
}
