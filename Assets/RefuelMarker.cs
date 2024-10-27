using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelMarker : MonoBehaviour
{
    public float bounceForce = 5f; // Adjust bounce force
    public float groundYLevel = 0f; // Y level for bouncing

    private Rigidbody rb;
    public Camera targetCamera;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        if (transform.position.y <= groundYLevel && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, bounceForce, rb.velocity.z);
        }
        transform.LookAt(targetCamera.transform);
    }

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
