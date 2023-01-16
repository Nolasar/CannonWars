using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI cooldownToStartText;
    [SerializeField] private GameObject startTimerPanel;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();

        scoreText.text = $"Score: {gameManager.score}";
        livesText.text = $"Lives: {gameManager.lives}";

        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(gameManager.timeToStart < 0)
        {
            startTimerPanel.SetActive(false);
            scoreText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(true);
        }
    }
    private void OnEnable()
    {
        EventBus.onCorrectBoxTouch += UpdateScore;
        EventBus.onCooldownToStartChanges += UpdateCooldownToStartGame;
        EventBus.onWrongBoxTouch += UpdateLives;
    }
    private void OnDisable()
    {
        EventBus.onCorrectBoxTouch -= UpdateScore;
        EventBus.onCooldownToStartChanges -= UpdateCooldownToStartGame;
        EventBus.onWrongBoxTouch -= UpdateLives;


    }
    private void UpdateScore()
    {
        scoreText.text = $"Score {gameManager.score}";
    }
    private void UpdateLives()
    {
        livesText.text = $"Lives {gameManager.lives}";
    }

    private void UpdateCooldownToStartGame()
    {
        cooldownToStartText.text = gameManager.timeToStart.ToString();
    }
    
}
