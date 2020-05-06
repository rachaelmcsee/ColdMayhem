using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCheck : MonoBehaviour
{

    GameInfo info;
    public GameObject start;
    public GameObject select;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.FindGameObjectWithTag("Info").GetComponent<GameInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(info.enemyLives <= 0)
        {
            start.SetActive(false);
            select.SetActive(false);
        }
    }
}
