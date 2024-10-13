using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Avatar idle;
    [SerializeField] private Avatar running;
    [SerializeField] private Avatar gathering;
    [SerializeField] private Avatar mining;
    [SerializeField] private GameObject pickaxePrefab;

    private GameObject pickaxeInstance = null;
    [SerializeField]private GameObject pickaxeSpawnPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.avatar = idle;
        pickaxeSpawnPoint = FindChildByName(this.transform, "PickaxeSpawnPoint");
    }

    public void SetMovementAnimation(bool isMoving, bool isSprinting)
    {

        if (isMoving)
        {
            animator.avatar = running;
        }
        else
        {
            animator.avatar = idle;
        }
        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isSprinting", isSprinting);
    }

    public void PlayGatheringAnimation(bool isMining)
    {
        if (!isMining)
        {
            animator.avatar = gathering;
            animator.SetTrigger("startGathering");
        }

        if (isMining)
        {
            pickaxeInstance = Instantiate(pickaxePrefab, pickaxeSpawnPoint.transform);
            animator.avatar = mining;
            animator.SetTrigger("startMining");

        }
    }

    public void OnGatheringEnd()
    {
        if (pickaxeInstance != null)
        {
            Destroy(pickaxeInstance);
        }
        animator.avatar = idle;
        SendMessageUpwards("StopGathering");
        Debug.Log("OnGatheringEnd");
    }

    private GameObject FindChildByName(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child.gameObject;
            }

            // Recursively search in child's children
            GameObject result = FindChildByName(child, childName);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }
}
