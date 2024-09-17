using UnityEngine;

public class MenuCraft : MonoBehaviour
{
    public string messageToPrint = "Default Message";

    public void PrintMessage()
    {
        Debug.Log(messageToPrint);
    }
}