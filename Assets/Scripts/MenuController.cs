using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Reference to the button canva
    public GameObject playButton;
    public string teste = "teste";
    public enum ButtonType
    {
        Play,
        Restart,
        Options,
        Exit
    }

    // Define a delegate with the specific signature including a ButtonType parameter
    public delegate void MenuAction(ButtonType buttonType);
    // Define an event based on that delegate
    public static event MenuAction OnMenuButtonClicked;

    // Method to call when a menu item is clicked, with the button type as a parameter
    public void MenuButtonClicked(ButtonType buttonType)
    {
        // Check if there are any subscribers
        OnMenuButtonClicked?.Invoke(buttonType);
    }

    public void OnButtonClicked(GameObject buttonGameObject)
{
    ButtonTypeHolder buttonTypeHolder = buttonGameObject.GetComponent<ButtonTypeHolder>();
    if (buttonTypeHolder != null)
    {
        MenuButtonClicked(buttonTypeHolder.buttonType);
    }
    else
    {
        Debug.LogWarning("ButtonTypeHolder component missing on the button GameObject", buttonGameObject);
    }
}
}
