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
                }
                break;
            case MenuController.ButtonType.Options:
                break;
            case MenuController.ButtonType.Restart:
                break;
            case MenuController.ButtonType.Exit:
                Application.Quit();
                break;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
