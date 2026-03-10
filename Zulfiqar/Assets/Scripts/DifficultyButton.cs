using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
  Button button;
  GameManager gameManager;
  [SerializeField] private int dLevel;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    button = GetComponent<Button>();
    button.onClick.AddListener(SetDifficulty);
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void SetDifficulty()
  {
    Debug.Log($"{gameObject.name} button was clicked!");
    gameManager.StartGame(dLevel);
  }
}
