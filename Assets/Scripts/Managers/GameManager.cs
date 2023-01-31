using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cannons;
    [SerializeField] private GameObject boxes;

    public bool isGameOver { get; private set; } = false;
    public int timeToStart { get; private set; } = 3;
    public int timeToEnd { get; private set; } = 61;
    public int lives { get; private set; } = 3;
    public int score { get; private set; } = 0;
    public static int maxUnlockLevel { get; private set; } = 1;

    private int currentLevel;
    private DataManager data;
    private void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        data = DataManager.instance;
        StartCoroutine(GameStartCooldown());

        if (boxes.activeInHierarchy) boxes.SetActive(false);
        if (cannons.activeInHierarchy) cannons.SetActive(false);
    }

    private void Update()
    {
        if (lives < 0) GameOver();
    }
    private void OnEnable()
    {
        // Increase score when projectile interact with correct box
        EventBus.onCorrectBoxTouch += UpdateScore;
        // Decrease player's lives when projectile interact with wrong box
        EventBus.onWrongBoxTouch += UpdateLives;
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        EventBus.onCorrectBoxTouch -= UpdateScore;
        EventBus.onWrongBoxTouch -= UpdateLives;
    }

    private void UpdateLives()
    {
        lives--;
    }
    private void UpdateScore()
    {
        score++;
    }

    private void UpdateMaxUnlockedLevel()
    {
        if (data.maxUnlockedLevel < currentLevel)
        {
            data.maxUnlockedLevel++;
        }
    }
    
    // Timer to start of the game
    IEnumerator GameStartCooldown()
    {
        while(timeToStart > -1)
        {    
            yield return new WaitForSeconds(1);
            // Update UI
            EventBus.onCooldownToStartChanges?.Invoke();
            timeToStart -= 1;
            if (timeToStart < 0)
            {
                StartGame();
            }
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        StopAllCoroutines();

        if (boxes.activeInHierarchy) boxes.SetActive(false);

        if (cannons.activeInHierarchy) cannons.SetActive(false);

        EventBus.onGameOver?.Invoke();

        // Need to check if player have at least one star
        UpdateMaxUnlockedLevel();

        data.SaveToFile();
    }

    private void StartGame()
    {
        isGameOver = false;

        StopCoroutine(GameStartCooldown());

        StartCoroutine(GameEndCooldown());

        if (!boxes.activeInHierarchy) boxes.SetActive(true);

        if (!cannons.activeInHierarchy) cannons.SetActive(true);

        EventBus.onGameStart?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator GameEndCooldown()
    {
        while (timeToEnd > -1)
        {
            // Update UI
            EventBus.onCooldownToEndChanges?.Invoke();
            timeToEnd -= 1;
            if (timeToEnd < 0)
            {
                GameOver();
            }
            yield return new WaitForSeconds(1);
        }
    }

}
