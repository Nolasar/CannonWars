using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Score and lives of player showing while game on
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    // Cooldown to start of the game
    [SerializeField] private TextMeshProUGUI cooldownToStartText;
    // Final score showing on game over panel
    [SerializeField] private TextMeshProUGUI finalScoreText;
    // Panel for start and end game
    [SerializeField] private GameObject startTimerPanel;
    [SerializeField] private GameObject gameOverPanel;
    // Game manager instance
    private GameManager gameManager;

    private void Start()
    {
        // ref to game manager component
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        // initialize default values for score and lives
        scoreText.text = $"Score: {gameManager.score}";
        livesText.text = $"Lives: {gameManager.lives}";
        // Activate cooldown panel
        startTimerPanel.SetActive(true);
        // Hide player stats while cooldown on
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Subscribe to events, udpate UI contidition when game conditions in game manager changing
        EventBus.onCorrectBoxTouch += UpdateScoreUI;
        EventBus.onCooldownToStartChanges += UpdateCooldownToStartGameUI;
        EventBus.onWrongBoxTouch += UpdateLivesUI;
        EventBus.onGameOver += ShowGameOverPanel;
        EventBus.onGameStart += ShowGameUI;
    }
    private void OnDisable()
    {
        // Unsubscribe from event when UI manager disable
        EventBus.onCorrectBoxTouch -= UpdateScoreUI;
        EventBus.onCooldownToStartChanges -= UpdateCooldownToStartGameUI;
        EventBus.onWrongBoxTouch -= UpdateLivesUI;
        EventBus.onGameOver -= ShowGameOverPanel;
        EventBus.onGameStart -= ShowGameUI;
    }
    private void UpdateScoreUI()
    {
        scoreText.text = $"Score {gameManager.score}";
    }
    private void UpdateLivesUI()
    {
        livesText.text = $"Lives {gameManager.lives}";
    }

    private void UpdateCooldownToStartGameUI()
    {
        cooldownToStartText.text = gameManager.timeToStart.ToString();
    }

    private void ShowGameOverPanel()
    {
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Final score: {gameManager.score}";
    }

    private void ShowGameUI()
    {
        startTimerPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
    }
    
}
