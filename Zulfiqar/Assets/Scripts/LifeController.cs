using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour
{
  [SerializeField] float rotX, rotY, rotZ;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    rotX = 0; rotY = .5f; rotZ = 0;

  }
  // Update is called once per frame
  void Update()
  {
    transform.Rotate(rotX, rotY, rotZ);
  }

  public void ExplodeAndDestroy()
  {
    Debug.Log($"Explode and Destroy: {this}");
    Destroy(gameObject);
    //ParticleSystem effect = 
    //Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation, transform.parent);
    //StartCoroutine(PlayDestroyEffect(effect));
  }
  IEnumerator PlayDestroyEffect(ParticleSystem effect)
  {
    yield return new WaitUntil(() => !effect.IsAlive());
    Destroy(gameObject);
  }
}
