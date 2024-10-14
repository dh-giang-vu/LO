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
    [SerializeField] private Avatar chopping;
    [SerializeField] private GameObject pickaxePrefab;
    [SerializeField] private GameObject axePrefab;

    private GameObject pickaxeInstance = null;
    private GameObject pickaxeSpawnPoint;
    private GameObject axeInstance = null;
    private GameObject axeSpawnPoint = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.avatar = idle;
        pickaxeSpawnPoint = FindChildByName(this.transform, "PickaxeSpawnPoint");
        axeSpawnPoint = FindChildByName(this.transform, "AxeSpawnPoint");
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

    public void PlayGatheringAnimation(string animationType)
    {
        if (animationType == "mining")
        {
            pickaxeInstance = Instantiate(pickaxePrefab, pickaxeSpawnPoint.transform);
            animator.avatar = mining;
            animator.SetTrigger("startMining");

        }
        else if (animationType == "chopping")
        {
            axeInstance = Instantiate(axePrefab, axeSpawnPoint.transform);
            animator.avatar = chopping;
            animator.SetTrigger("startChopping");
        }
        else
        {
            animator.avatar = gathering;
            animator.SetTrigger("startGathering");
        }
    }

    public void OnGatheringEnd()
    {
        if (pickaxeInstance != null)
        {
            Destroy(pickaxeInstance);
        }
        if (axeInstance != null)
        {
            Destroy(axeInstance);
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
