﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I learned this code from the youtuber Brackeys
//Andrew Mabry (Drew)
public class LookAround : MonoBehaviour
{
    //declaring variables
    //controlling look speed
    public float sensitivity = 100f;
    //getting the character refference so that when you look side to side the player rotates
    public Transform playerBody;
    //using a variable for the x rotation
    float turn = 0f;

    public bool isOver = false;

    void Start()
    {
        //locking and hiding the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
            //determining how much the player moves the mouse
            float xLook = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float yLook = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            //making a limiter on looking down or up too far
            turn -= yLook;
            turn = Mathf.Clamp(turn, -90f, 90f);

            //rotating the camera and/or the player
            transform.localRotation = Quaternion.Euler(turn, 0f, 0f);
            playerBody.Rotate(Vector3.up * xLook);
        }
        else
        {
            //unlocking the mouse and making it visible again
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        

        
    }
}
