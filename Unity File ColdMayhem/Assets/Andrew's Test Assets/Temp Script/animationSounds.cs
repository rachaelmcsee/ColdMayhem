using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//by Andrew Mabry

public class animationSounds : MonoBehaviour
{
    //declaring public  variables for references
    public AudioClip[] steps;
    public AudioClip jump;
    public AudioClip land;
    //this is where the sound will play
    public AudioSource source;
    //making several methods to be called on animation events
    public void Step()
    {
        source.PlayOneShot(steps[Random.Range(0, steps.Length)]);
    }

    public void Jump()
    {
        source.PlayOneShot(jump);
    }
    public void Land()
    {
        source.PlayOneShot(land);
    }
}
