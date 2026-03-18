using UnityEngine;
using UnityEngine.UI;

public class MuteToggle : MonoBehaviour
{
  [Header("UI References")]
  [SerializeField] private Image buttonImage;
  [SerializeField] private Sprite muteSprite;
  [SerializeField] private Sprite unmuteSprite;

  private bool isMuted = true;
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    if (!buttonImage)
    {
      buttonImage = GetComponent<Image>();
      AudioListener.pause = isMuted;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void ToggleAudio()
  {
    isMuted = !isMuted;
    AudioListener.pause = isMuted;
    if (isMuted)
    {
      buttonImage.sprite = muteSprite;
    }
    else
    {
      buttonImage.sprite = unmuteSprite;
    }
  }
}
