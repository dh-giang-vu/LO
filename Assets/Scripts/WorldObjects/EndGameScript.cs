using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    LightSource[] allLights = null;
    public float endGameMark = 100f;
    // Start is called before the first frame update
    void Start()
    {
        allLights = FindObjectsOfType<LightSource>();
    }

    // Update is called once per frame
    void Update()
    {
        allLights = FindObjectsOfType<LightSource>();
        if (CheckWin()) {
            
        }
    }

    public bool CheckWin() {
        float score = 0f;
        foreach (LightSource element in allLights)
        {
            // score += element.getScore();
        }
        if (score > endGameMark) {
            return true;
        }
        return false;
    }
}
