using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransitionScene : MonoBehaviour
{

    private void OnEnable()
    {
        MenuController.OnMenuButtonClicked += OnMenuButtonAction;
    }

    private void OnDisable()
    {
        MenuController.OnMenuButtonClicked -= OnMenuButtonAction;
    }

    void OnMenuButtonAction(MenuController.ButtonType buttonType)
    {
        switch (buttonType)
        {
            case MenuController.ButtonType.Play:
                {
                    SceneManagerHelper.Instance.StartGame();
                    Debug.Log("Game has started!");
                }
                break;
            case MenuController.ButtonType.Options:
                Debug.Log("Options button was clicked!");
                // Handle options logic
                break;
            case MenuController.ButtonType.Restart:
                break;
            case MenuController.ButtonType.Exit:
                // Close the game
                Application.Quit();
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
