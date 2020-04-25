using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    //making variables to keep track of lives
    public int playerLives = 3;
    public int enemyLives = 3;

    //variables to hold the character prefabs
    public GameObject[] playerCharacters;
    public GameObject[] enemyCharacters;

    public int playerChoice = 0;
    public int enemyChoice = 0;


}
