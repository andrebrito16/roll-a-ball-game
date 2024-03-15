using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
    public static SceneManagerHelper Instance;

    public string victoryMessage; // Variable to hold the victory message

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadVictoryScene(string message)
    {
        victoryMessage = message;
        SceneManager.LoadScene("Menu"); // Replace "VictoryScene" with the name of your victory scene
    }

    public void StartGame() {
        SceneManager.LoadScene("Minigame"); // Replace "Game" with the name of your game scene
    }
}
