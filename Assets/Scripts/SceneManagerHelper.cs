using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHelper : MonoBehaviour
{
    public static SceneManagerHelper Instance;

    public string victoryMessage;

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
        SceneManager.LoadScene("Menu");
    }

    public void StartGame() {
        SceneManager.LoadScene("Minigame");
    }
}
