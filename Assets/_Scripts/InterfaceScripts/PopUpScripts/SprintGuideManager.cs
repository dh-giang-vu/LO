using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            objectToDisable.SetActive(false);
            objectToEnable.SetActive(true);
        }       
    }
}
