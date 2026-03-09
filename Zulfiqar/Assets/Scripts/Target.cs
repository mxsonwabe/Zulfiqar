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
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    if (!rb)
    {
      Debug.LogError($"No Target RigidBody", this);
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
    //if (transform.position.y < (ySpawnPos - 4))
    //{
    //  Destroy(gameObject);
    //}
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

  //private void OnMouseDown()
  //{
  //  Destroy(gameObject);
  //}

  private void OnTriggerEnter(Collider other)
  {
    Destroy(gameObject);
  }
  public void OnPointerDown(PointerEventData eventData)
  {
    Destroy(gameObject);
    Debug.Log($"Destroyed Object w/ event data:\n{eventData.ToString()}");
  }
}
