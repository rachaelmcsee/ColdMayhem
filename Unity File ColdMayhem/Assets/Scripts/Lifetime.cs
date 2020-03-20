using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifeTime = 1;
    float timer = 0;
    public ParticleSystem thisParticle;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= lifeTime)
        {
            thisParticle.Stop();
            if(timer >= lifeTime + 2)
                Destroy(this.gameObject);
        }
    }
}
