using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Andrew Mabry (Drew)

//this code is used to limit the lifetime of particle affects
public class Lifetime : MonoBehaviour
{
    //declaring variables
    public float lifeTime = 1;
    float timer = 0;
    public ParticleSystem thisParticle;

    // Update is called once per frame
    void Update()
    {
        //counting total time
        timer += Time.deltaTime;
        
        //if the total time is greater than the lifetime than particles will stop
        if(timer >= lifeTime)
        {
            thisParticle.Stop();
            //2 seconds after the particles stop the particle object is destroyed
            if(timer >= lifeTime + 2)
                Destroy(this.gameObject);
        }
    }
}
