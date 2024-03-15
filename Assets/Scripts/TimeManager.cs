using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }
    public float timeRemaining = 30f; // Starting time in seconds
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI gameOverText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Tip: Uncomment this line to persist the GameTimer object across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitFor2SecondsAndLoadMenu()
    {
        yield return new WaitForSeconds(2);
        SceneManagerHelper.Instance.LoadVictoryScene("Game Over");
    }

    private void Start()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                PlayerController.Instance.gameHasStarted = false;
                
                gameOverText.text = "Game Over";
                gameOverText.gameObject.SetActive(true);
                StartCoroutine(WaitFor2SecondsAndLoadMenu());
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreaseTime(float amount)
    {
        timeRemaining += amount;
    }
}
