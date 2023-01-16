using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cannons;
    [SerializeField] private GameObject boxes;
    public float playTimer = 60;
    public float timeToStart = 3.0f;
    public int lives = 3;
    public int score = 0;

    private void Start()
    {
        StartCoroutine(GameStartCooldown());       
    }

    private void OnEnable()
    {
        EventBus.onCorrectBoxTouch += UpdateScore;
        EventBus.onWrongBoxTouch += UpdateLives;
    }

    private void OnDisable()
    {
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

    IEnumerator GameStartCooldown()
    {
        while(timeToStart > -1)
        {    
            yield return new WaitForSeconds(1.0f);
            EventBus.onCooldownToStartChanges?.Invoke();
            timeToStart -= 1;
        }
        if (timeToStart < 0)
        {
            if (!boxes.activeInHierarchy) boxes.SetActive(true);

            if (!cannons.activeInHierarchy) cannons.SetActive(true);
        }
    }

    private void GameOver()
    {
        if (lives == 0)
        {

        }
    }
}
