using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Globalization;

public class GameManager : MonoBehaviour
{
  int score = 0;
  float spawnRate = 1.25f;
  bool isGameActive;
  public int Score { get; private set; }
  [SerializeField] private Button resetButton;
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI gameOverText;
  [SerializeField] private GameObject titleScreen;
  [SerializeField] private List<GameObject> gameObjects;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    scoreText.text = $"Score: {score}";
  }

  IEnumerator SpawnTargets()
  {
    while (isGameActive)
    {
      yield return new WaitForSeconds(spawnRate);
      int idx = Random.Range(0, gameObjects.Count);
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
  }
}
