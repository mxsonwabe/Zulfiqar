using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeTrail : MonoBehaviour
{
  [SerializeField] private Camera mainCamera;
  [SerializeField] private float depthFromCamera;

  TrailRenderer trailRenderer;
  bool isPointerHeld;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (mainCamera == null)
      mainCamera = Camera.main;
    trailRenderer = GetComponent<TrailRenderer>();
    trailRenderer.enabled = false;
    depthFromCamera = 8.0f;
  }

  // Update is called once per frame
  void Update()
  {
    // Set the first time occurrance of the trail
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      Vector3 startPos = GetPosition();
      trailRenderer.transform.position = startPos;
      trailRenderer.Clear();
      trailRenderer.enabled = true;
      isPointerHeld = true;
    }
    // Set the trial renderer to track the mouse point
    if (isPointerHeld)
    {
      trailRenderer.transform.position = GetPosition();
    }
    // Set the trail renderer off when the user releases the mouse
    if (Mouse.current.leftButton.wasReleasedThisFrame)
    {
      isPointerHeld = false;
    }
  }

  Vector3 GetPosition()
  {
    // convert the mouse pointer position to a game-world (3D) point-position
    Vector2 screenPos = Mouse.current.position.ReadValue();
    Vector3 screenPosWithDepth = new Vector3(screenPos.x, screenPos.y, depthFromCamera);
    return mainCamera.ScreenToWorldPoint(screenPosWithDepth);
  }
}
