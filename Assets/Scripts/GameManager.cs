using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cannons;
    [SerializeField] private GameObject boxes;

    public bool isGameOver { get; private set; } = false;
    public float playTimer { get; private set; } = 60;
    public int timeToStart { get; private set; } = 3;
    public int timeToEnd { get; private set; } = 60;
    public int lives { get; private set; } = 3;
    public int score { get; private set; } = 0;

    private void Start()
    {
        StartCoroutine(GameStartCooldown());
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

    IEnumerator GameEndCooldown()
    {
        while (timeToEnd > -1)
        {
            yield return new WaitForSeconds(1);
            // Update UI
            EventBus.onCooldownToEndChanges?.Invoke();
            timeToEnd -= 1;
            if (timeToEnd < 0)
            {
                GameOver();
            }
        }
    }
}
