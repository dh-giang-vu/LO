using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideController : MonoBehaviour
{
    // Assign the Next and Previous buttons in the Inspector
    public Button nextButton;
    public Button previousButton;

    // Parent object containing all the child objects
    public Transform parentObject;

    // Track the current child index
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initially, enable only the first child, disable the rest
        UpdateChildVisibility();

        // Add listeners for button clicks
        nextButton.onClick.AddListener(GoToNextChild);
        previousButton.onClick.AddListener(GoToPreviousChild);
    }

    // This function is called when the Next button is pressed
    private void GoToNextChild()
    {
        if (currentIndex < parentObject.childCount - 1)
        {
            currentIndex++;
            UpdateChildVisibility();
        }
    }

    // This function is called when the Previous button is pressed
    private void GoToPreviousChild()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateChildVisibility();
        }
    }

    // Update which child is active based on the currentIndex
    private void UpdateChildVisibility()
    {
        // Loop through all children of the parent object
        for (int i = 0; i < parentObject.childCount; i++)
        {
            // Enable the current child and disable others
            parentObject.GetChild(i).gameObject.SetActive(i == currentIndex);
        }
    }
}
