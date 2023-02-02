using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cannons;
    [SerializeField] private GameObject boxes;
    [SerializeField] private SpawnProjectiles[] ammoMagazins;

    public bool isGameOver { get; private set; } = false;
    public int timeToStart { get; private set; } = 3;
    public int timeToEnd { get; private set; } = 60;
    public int lives { get; private set; } = 3;
    public int score { get; private set; } = 0;
    public int stars { get; private set; } = 0;
    public int[] requiredScoreForStars { get; private set; }

    private int levelIndexThrehold = 2;
    private int currentLevel;
    
    [SerializeField] private float percentForOneStar = 0.6f;
    [SerializeField] private float percentForTwoStars = 0.75f;
    [SerializeField] private float percentForThreeStars = 0.9f;

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
        if (lives <= 0 && !isGameOver) GameOver();       
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

    private int[] CalculateRequiredScores()
    {
        int approximateBestScore = 0;
        float error = 0.1f;
        for (int i = 0; i < ammoMagazins.Length; i++)
        {
             approximateBestScore += Convert.ToInt32(ammoMagazins[i].amountProjectile / 3);
        }
        approximateBestScore = Mathf.FloorToInt(approximateBestScore * (1 - error));

        return new int[] 
        {
            Mathf.FloorToInt(approximateBestScore * percentForOneStar),
            Mathf.FloorToInt(approximateBestScore * percentForTwoStars),
            Mathf.FloorToInt(approximateBestScore * percentForThreeStars),
        };
    }

    private void CalculateStars()
    {
        requiredScoreForStars = CalculateRequiredScores();
        for (int i = requiredScoreForStars.Length - 1; i > -1; i--)
        {
            if (score > requiredScoreForStars[i])
            {
                stars = i + 1;
                break;
            }
        }
    }

    private void UpdateLevelData(int index)
    {
        if (data.levels.Exists(level => level.index == index+1))
        {
            LevelStruct level = data.levels[index];
            
            level.maxScore = level.maxScore < score? score: level.maxScore;
            level.stars = level.stars < stars ? stars : level.stars;

            data.levels[index] = level;
        }
        else
        {
            data.levels.Add(new LevelStruct
            {
                index = index+1,
                maxScore = score,
                stars = stars
            });
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

        CalculateStars();

        EventBus.onGameOver?.Invoke();

        // Need to check if player have at least one star
        if (stars > 0)
        {
            UpdateMaxUnlockedLevel();
        }

        UpdateLevelData(currentLevel-levelIndexThrehold);

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
