using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraGuideManager : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable;
    [SerializeField] GameObject objectToEnable;

    private bool isRightClick;
    private bool isScrolled;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("right click");
                isRightClick = true;    
            }

        if (Input.mouseScrollDelta.y != 0)
            {
                isScrolled = true;
                Debug.Log("Scroll wheel used!");
            }
                    

        if (isRightClick && isScrolled)
            {
                objectToDisable.SetActive(false);
                objectToEnable.SetActive(true);
            }       
    }
}
