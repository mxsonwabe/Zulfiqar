using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
  Rigidbody rb;
  private float xRange = 4f;
  private float ySpawnPos = -2f;
  private float minSpeed = 12f;
  private float maxSpeed = 18f;
  private float maxTorque = 4f;
  GameManager gameManager;
  [SerializeField] private int targetPoints;
  [SerializeField] private ParticleSystem explosionParticle;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    rb = GetComponent<Rigidbody>();
    if (!rb || !gameManager)
    {
      Debug.LogError($"No Target RigidBody, Or Game Manager found.", this);
      enabled = false;
      return;
    }

    rb.AddForce(RandomForce(), ForceMode.Impulse);
    rb.AddTorque(RandomTorque(), ForceMode.Impulse);
    transform.position = RandomSpawnPos();
  }

  // Update is called once per frame
  void Update()
  {

  }

  Vector3 RandomForce()
  {
    return Vector3.up * Random.Range(minSpeed, maxSpeed);
  }
  Vector3 RandomSpawnPos()
  {
    return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
  }
  Vector3 RandomTorque()
  {
    return new Vector3(
      Random.Range(-maxTorque, maxTorque),
      Random.Range(-maxTorque, maxTorque),
      Random.Range(-maxTorque, maxTorque)
     );
  }

  private void OnTriggerEnter(Collider other)
  {
    if (gameObject.CompareTag("GoodTarget"))
    {
      gameManager.UpdateScore(-targetPoints);
    }
    if (gameManager.Score < 0)
      gameManager.LoseLife();
    Destroy(gameObject);
  }
  public void OnPointerDown(PointerEventData eventData)
  {
    //int value = gameObject.name.Replace("(Clone)", "") switch
    //{
    //  "Ball_1" => 5,
    //  "Ball_2" => 10,
    //  "Ball_3" => 15,
    //  "Bomb" => -15,
    //  _ => 0
    //};
    ProcessHit();
  }

  public void OnPointerEnter(PointerEventData eventData)
  {
    Debug.Log("OnPointerEnter");
    if (Pointer.current != null && Pointer.current.IsPressed())
    { 
      Debug.Log($"Mouse Pressed:\n{eventData}");
      ProcessHit();
    }
  }

  public void ProcessHit()
  {
    // Guard against being called after object is already destroyed
    if (gameObject == null) return;

    gameManager.UpdateScore(targetPoints);
    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    if (gameManager.Score < 0)
    {
      gameManager.LoseLife();
    }
    Destroy(gameObject);
  }
}
