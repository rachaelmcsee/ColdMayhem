﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    //declaring a variable to store the reference to the game info so they can change their character
    GameInfo info;
    public GameObject main;
    public GameObject characterSelect;

    //a variable to store the back button
    public GameObject back;
    private void Start()
    {
        info = GameObject.FindGameObjectWithTag("Info").GetComponent<GameInfo>();
    }

    //making a method to change the selected character
    public void Select(int characterInt)
    {
        info.playerChoice = characterInt;
        back.SetActive(true);
        characterSelect.SetActive(false);
        main.SetActive(true);
    }
}
