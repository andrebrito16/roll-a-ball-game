using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject playButton;
    public string teste = "teste";
    public enum ButtonType
    {
        Play,
        Restart,
        Options,
        Exit
    }

    public delegate void MenuAction(ButtonType buttonType);
    public static event MenuAction OnMenuButtonClicked;
    public void MenuButtonClicked(ButtonType buttonType)
    {
        OnMenuButtonClicked?.Invoke(buttonType);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnButtonClicked(playButton);
        }
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
