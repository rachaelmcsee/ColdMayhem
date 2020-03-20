using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//some of this script was learned from the youtuber Brackeys

public class HP : MonoBehaviour
{
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
        currentHP = maxHP;
        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
        if(hpDisplay != null)
            hpDisplay.text = "HP: " + currentHP.ToString();

        hpFill.color = hpGradient.Evaluate(1f);
    }
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Snowball")
        {
            currentHP -= 10;
            if (hpDisplay != null)
            {
                hpDisplay.text = "HP: " + currentHP.ToString();
                hpBar.value = currentHP;
                hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
            }
                
        }
        
        //checking to see if the player is alive
        if (currentHP <= 0)
        {
            Instantiate(KO_Particle, this.transform.position, this.transform.rotation);
            Invoke("Death", .5f);
        }
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
