using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetector : MonoBehaviour
{
  // camera reference to convert screen to world coordinates
  [SerializeField] private Camera mainCamera;

  private bool isPointerHeld = false;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (!mainCamera)
      mainCamera = Camera.main;
  }

  // Update is called once per frame
  void Update()
  {
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      isPointerHeld = true;
    }
    if (Mouse.current.leftButton.wasReleasedThisFrame)
    {
      isPointerHeld = false;
    }
    if (isPointerHeld)
    {
      DetectSwipe();
    }
  }

  void DetectSwipe()
  {
    // Get the current position of the mouse in screen space
    Vector2 screenPos = Mouse.current.position.ReadValue();
    // Convert the current pos of the mouse in screen-position to a ray going into the 3D world
    Ray ray = mainCamera.ScreenPointToRay(screenPos);

    // fire the ray and check if it hits anything
    if (Physics.Raycast(ray, out RaycastHit hit, 100f))
    {
      if (hit.collider.TryGetComponent<Target>(out var target))
      {
        target.ProcessHit();
      }
    }
  }
}
