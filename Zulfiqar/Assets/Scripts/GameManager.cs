using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class GameManager : MonoBehaviour
{
  int score = 0, currentTimeCount = 0, lives = 3;
  float spawnRate = 1.25f;
  bool isGameActive;
  bool isPaused = false;
  AudioSource audioSource;
  InputAction pauseAction;
  public int Score { get; private set; }
  [SerializeField] private Button resetButton;
  [SerializeField] LifeController[] livesUIController;
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI timerText;
  [SerializeField] private TextMeshProUGUI gameOverText;
  [SerializeField] private TextMeshProUGUI gameWonText;
  [SerializeField] private GameObject titleScreen;
  [SerializeField] private GameObject pausePanel;
  [SerializeField] private List<GameObject> gameObjects;
  [SerializeField] private int timeCounter = 60;
  //[SerializeField] private AudioClip backgroundAudio;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    if (!audioSource)
    {
      Debug.LogWarning($"NO AUDIO SOURCE FOUND IN GAMEM+MANAGER: {this}");
    }
    pauseAction = InputSystem.actions.FindAction("Player/Pause", true);
  }

  // Update is called once per frame
  void Update()
  {
    scoreText.text = $"Score: {score}";
    if (pauseAction.WasPressedThisFrame())
    {
      ChangePaused();
    }
  }

  IEnumerator GameTimer()
  {
    while (isGameActive)
    {
      yield return new WaitForSeconds(1f);
      if (currentTimeCount >= timeCounter)
        GameWon();
      if ((timeCounter - currentTimeCount) < 10)
        timerText.color = Color.green;
      timerText.text = $"Time: {timeCounter - currentTimeCount}";
      currentTimeCount++;
    }
  }
  IEnumerator SpawnTargets()
  {
    while (isGameActive)
    {
      yield return new WaitForSeconds(spawnRate);
      int idx = UnityEngine.Random.Range(0, gameObjects.Count);
      Instantiate(gameObjects[idx]);
    }
  }

  public void UpdateScore(int value)
  {
    score += value;
    Score = score;
  }

  public void GameOver()
  {
    isGameActive = false;
    gameOverText.gameObject.SetActive(true);
    resetButton.gameObject.SetActive(true);
    //Time.timeScale = 0f;
  }

  public void LoseLife()
  {
    if (!isGameActive) return;
    lives--;
    if (lives >= 0)
    {
      livesUIController[lives].ExplodeAndDestroy();
    }
    if (lives <= 0)
    {
      GameOver();
    }
    else
    {
      score = 10;
      Score = score;
    }
  }
  public void GameWon()
  {
    isGameActive = false;
    gameWonText.gameObject.SetActive(true);
    resetButton.gameObject.SetActive(true);
    //Time.timeScale = 0f;
  }

  public void IncreaseScore(GameObject enemyGameObj)
  {
    Debug.Log($"Game Object Hit: {enemyGameObj}");
    score++;
  }

  public void DescreaseScore(GameObject enemyGameObj)
  {
    if (!enemyGameObj)
      return;
    Debug.Log($"Game Object Missed: {enemyGameObj}");
    score--;
  }

  public void RestartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void StartGame(int dLevel)
  {
    spawnRate /= dLevel;
    isGameActive = true;
    titleScreen.SetActive(false);
    resetButton.gameObject.SetActive(false);
    gameOverText.gameObject.SetActive(false);
    StartCoroutine(nameof(SpawnTargets));
    StartCoroutine(nameof(GameTimer));
  }

  void ChangePaused()
  {
    isPaused = !isPaused;
    if (isPaused)
    {
      Time.timeScale = 0;
      pausePanel.SetActive(true);
    } else
    {
      Time.timeScale = 1f;
      pausePanel.SetActive(false);
    }
  }
}
