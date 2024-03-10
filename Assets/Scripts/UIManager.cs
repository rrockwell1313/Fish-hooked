using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
  //create singleton
  public static UIManager UI { get; set; }

  private Slider chargeSlider;
  private TextMeshProUGUI distanceText;

  // Start is called before the first frame update
  private void Awake()
  {
    if (UI == null)
    {
      UI = this;
      DontDestroyOnLoad(gameObject);
    }
    else if (UI != this)
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    // Assign the UI components if not done already
    if (chargeSlider == null)
      chargeSlider = GameObject.Find("ChargeSlider").GetComponent<Slider>();

    if (distanceText == null)
      distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();

    // Optional: Handle initialization or update the UI state if needed
    UpdateUIInitialState();
  }

  private void OnEnable()
  {
    // Subscribe to GameManager's event when the UIManager is enabled
    GameManager.OnCastingStateChange += UpdateUIForCastingState;
  }

  private void OnDisable()
  {
    // Unsubscribe from GameManager's event when the UIManager is disabled to avoid memory leaks
    GameManager.OnCastingStateChange -= UpdateUIForCastingState;
  }

  public void UpdateUIForCastingState(bool isCasting)
  {
    // Enable or disable the chargeSlider based on the isCasting state
    Debug.Log("Casting state changed: " + isCasting);
    chargeSlider.enabled = !isCasting;
  }

  public void UpdateChargeSlider(float value)
  {
    if (chargeSlider != null)
      chargeSlider.value = value;
  }

  public void UpdateDistanceText(string text)
  {
    if (distanceText != null)
      distanceText.text = text;
  }

  private void UpdateUIInitialState()
  {
    // Initialize your UI elements with default values or states if necessary
  }

  // ... Your other UIManager methods
}

