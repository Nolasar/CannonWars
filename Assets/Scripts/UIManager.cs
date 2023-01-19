using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Score and lives of player showing while game on
    [Header("Texts: ")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    // Cooldown to start/end of the game
    [SerializeField] private TextMeshProUGUI cooldownToStartText;
    [SerializeField] private TextMeshProUGUI cooldownToEndText;
    // Final score showing on game over panel
    [SerializeField] private TextMeshProUGUI finalScoreText;
    // Panel for start and end game
    [Header("Panels: ")]
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
        cooldownToEndText.text = $"Timer remaining: {gameManager.timeToEnd}";
        // Activate cooldown panel
        startTimerPanel.SetActive(true);
        // Hide player stats while cooldown on
        GameStatsVisibility(false);
    }

    private void OnEnable()
    {
        // Subscribe to events, udpate UI contidition when game conditions in game manager changing
        // Update score text when a projectile interact with correct box
        EventBus.onCorrectBoxTouch += UpdateScoreText;
        // When cooldown to start was changed( in game manager ), update appropriate text
        EventBus.onCooldownToStartChanges += UpdateCooldownToStartGameText;
        // When cooldown to end was changed( in game manager ), update appropriate text
        EventBus.onCooldownToEndChanges += UpdateCooldownToEndGameText;
        // Update lives text when a projectile interact with wrong box
        EventBus.onWrongBoxTouch += UpdateLivesText;
        // Show appropriate panel when game is over
        EventBus.onGameOver += ShowGameOverPanel;
        // Hide appropriate panel when game starts
        EventBus.onGameStart += HideCooldownToStartPanel;
    }
    private void OnDisable()
    {
        // Unsubscribe from event when UI manager disable
        EventBus.onCorrectBoxTouch -= UpdateScoreText;
        EventBus.onCooldownToStartChanges -= UpdateCooldownToStartGameText;
        EventBus.onCooldownToEndChanges -= UpdateCooldownToEndGameText;
        EventBus.onWrongBoxTouch -= UpdateLivesText;
        EventBus.onGameOver -= ShowGameOverPanel;
        EventBus.onGameStart -= HideCooldownToStartPanel;
    }
    private void UpdateScoreText()
    {
        scoreText.text = $"Score {gameManager.score}";
    }
    private void UpdateLivesText()
    {
        livesText.text = $"Lives {gameManager.lives}";
    }

    private void UpdateCooldownToStartGameText()
    {
        cooldownToStartText.text = gameManager.timeToStart.ToString();
    }

    private void UpdateCooldownToEndGameText()
    {
        cooldownToEndText.text = $"Time remaining: {gameManager.timeToEnd}";
    }

    private void ShowGameOverPanel()
    {
        GameStatsVisibility(false);
        gameOverPanel.SetActive(true);
        finalScoreText.text = $"Final score: {gameManager.score}";
    }

    private void HideCooldownToStartPanel()
    {
        startTimerPanel.SetActive(false);
        GameStatsVisibility(true);
    }

    private void GameStatsVisibility(bool visibility)
    {
        scoreText.gameObject.SetActive(visibility);
        livesText.gameObject.SetActive(visibility);
        cooldownToEndText.gameObject.SetActive(visibility);
    }
    
}
