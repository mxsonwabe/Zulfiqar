using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  float spawnRate = 1.25f;
  [SerializeField] private List<GameObject> gameObjects;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    StartCoroutine(nameof(SpawnTargets));
  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator SpawnTargets()
  {
    while (true)
    {
      yield return new WaitForSeconds(spawnRate);
      int idx = Random.Range(0, gameObjects.Count);
      Instantiate(gameObjects[idx]);
    }
  }
}
