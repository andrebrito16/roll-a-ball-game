using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 0;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject gameOverTextObject;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;
    public bool gameHasStarted;
    public AudioSource pickupSound;
    public AudioSource enemyPickupSound;
    public AudioSource gameOverSound;

    private IEnumerator WaitFor3SecondsAndLoadMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManagerHelper.Instance.LoadVictoryScene("Congratulations! You win!");

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                    gameHasStarted = true;
                    timerIsRunning = true;
                    Debug.Log("Game has started!");
                }
                break;
            case MenuController.ButtonType.Options:
                Debug.Log("Options button was clicked!");
                break;
            case MenuController.ButtonType.Restart:
                RestartGame();
                break;
            case MenuController.ButtonType.Exit:
                Debug.Log("Exit button was clicked!");
                break;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
        countText.SetText("Score: " + count.ToString());
        timerIsRunning = true;
        gameHasStarted = true;
    }


    private void FixedUpdate()
    {
        if (!gameHasStarted) return;

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!gameHasStarted) return;

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            GameTimer.Instance.timeRemaining += 0.5f; // Increase time by 0.5 seconds

            pickupSound.Play();

            SetCountText();
        }

        if (other.gameObject.CompareTag("EnemyPickUp"))
        {
            other.gameObject.SetActive(false);
            count--;

            enemyPickupSound.Play();

            SetCountText();
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();

        if (count < 0) {
            gameOverSound.Play();
            gameOverTextObject.SetActive(true);
            timerIsRunning = false;
            gameHasStarted = false;

            StartCoroutine(WaitFor3SecondsAndLoadMenu());
        }

        if (count >= 12)
        {
            winTextObject.SetActive(true);
            timerIsRunning = false;
            gameHasStarted = false;

            StartCoroutine(WaitFor3SecondsAndLoadMenu());
        }
    }

    void Update()
    {
        if (!gameHasStarted) return;

        // Get position of the player
        Vector3 playerPosition = transform.position;
        
        if (playerPosition.y < -0.5f)
        {
            gameOverSound.Play();
            gameOverTextObject.SetActive(true);
            timerIsRunning = false;
            gameHasStarted = false;

            StartCoroutine(WaitFor3SecondsAndLoadMenu());
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            SceneManagerHelper.Instance.LoadVictoryScene("You have quit the game!");
            timerIsRunning = false;
            gameHasStarted = false;
        }

        if (timerIsRunning)
        {
            if (GameTimer.Instance.timeRemaining > 0)
            {
                GameTimer.Instance.timeRemaining -= Time.deltaTime;
                DisplayTime(GameTimer.Instance.timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                GameTimer.Instance.timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float miliseconds = Mathf.FloorToInt(timeToDisplay * 1000 % 1000);

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, miliseconds);
    }

    public void IncreaseTime(float amount)
    {
        GameTimer.Instance.timeRemaining += amount;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameTimer.Instance.timeRemaining = 30f;
        gameHasStarted = true;
        timerIsRunning = true;
    }
}
