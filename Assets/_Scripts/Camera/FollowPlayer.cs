using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 relativePosition = new Vector3(0, 6, -10);

    void Update()
    {
        transform.position = player.transform.position + relativePosition;
    }
}