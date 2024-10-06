using UnityEngine;
using UnityEngine.UI;

public class ImageButtonController : MonoBehaviour
{
    // Assign these through the Inspector
    public Button[] buttons;  // Array of buttons corresponding to images
    public GameObject parentObject;  // Parent object whose children will be toggled

    private void Start()
    {
        // Add listeners to each button
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    private void OnButtonClicked(Button clickedButton)
    {
        // Find the index of the clicked button in the array
        int index = System.Array.IndexOf(buttons, clickedButton);
        if (index >= 0)
        {
            // Enable the specific child based on the button clicked, skipping the buffer child
            ToggleChildren(index + 1); // Adding 1 to account for the buffer child
        }
    }

    private void ToggleChildren(int activeIndex)
    {
        // Iterate through all children of the parent object
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            Transform child = parentObject.transform.GetChild(i);
            // Enable the child at the activeIndex and disable all others
            child.gameObject.SetActive(i == activeIndex);
        }
    }
}
