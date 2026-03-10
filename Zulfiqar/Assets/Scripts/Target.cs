using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour, IPointerDownHandler
{
  Rigidbody rb;
  private float xRange = 4f;
  private float ySpawnPos = -2f;
  private float minSpeed = 12f;
  private float maxSpeed = 18f;
  private float maxTorque = 4f;
  GameManager gameManager;
  [SerializeField] private ParticleSystem explosionParticle;
  [SerializeField] private int targetPoints;
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
      gameManager.UpdateScore(-10);
    }
    if (gameManager.Score < 0)
      gameManager.GameOver();
    Destroy(gameObject);
  }
  public void OnPointerDown(PointerEventData eventData)
  {
    Debug.Log($"Destroyed Object w/ event data:\n{eventData.ToString()}");
    //int value = gameObject.name.Replace("(Clone)", "") switch
    //{
    //  "Ball_1" => 5,
    //  "Ball_2" => 10,
    //  "Ball_3" => 15,
    //  "Bomb" => -15,
    //  _ => 0
    //};
    gameManager.UpdateScore(targetPoints);
    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    if (gameManager.Score < 0)
    {
      gameManager.GameOver();
    }
    Destroy(gameObject);
  }
}
