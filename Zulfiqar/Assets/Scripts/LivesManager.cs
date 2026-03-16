using UnityEngine;

public class LivesManager : MonoBehaviour
{
  GameManager gameManager;
  
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    if (!gameManager)
    {
      Debug.LogError($"No Target RigidBody, Or Game Manager found.", this);
      enabled = false;
      return;
    }
  }

    // Update is called once per frame
    void Update()
    {
        
    }
}
