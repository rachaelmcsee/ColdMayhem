using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andrew Mabry
//I got this code from Brackeys on how to make an hp bar I added the part about finding the camera so that when the player respawns the new camera is found
public class BillboardScript : MonoBehaviour
{

    public Transform cam;

    private void LateUpdate()
    {
        if(cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        transform.LookAt(transform.position + cam.forward);
    }
}
