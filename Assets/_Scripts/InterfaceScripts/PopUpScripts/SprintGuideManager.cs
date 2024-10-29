using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }       
    }
}
