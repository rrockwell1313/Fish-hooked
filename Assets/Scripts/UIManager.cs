using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;
public class UIManager : MonoBehaviour
{
  public static UIManager UI { get; set; }

  private Slider chargeSlider;
  private TextMeshProUGUI distanceText;

 
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

  }

  private void OnEnable()
  {
    //when GM runs this, we run update.
    GameManager.OnStateChange += UpdateUIState;
  }

  private void OnDisable()
  {
    GameManager.OnStateChange -= UpdateUIState;
  }

  public void UpdateUIState(GameState newState)
  {
    ResetUI();//reset all existing UI before starting new ones.
    switch (newState)
    {
      case GameState.Default:
        UpdateChargeSlider(0);
        UpdateDistanceText("0.00m");
        UpdateDefaultUI(true);
        break;
      
      case GameState.Casting:
      break;
      
      case GameState.Charging:
        UpdateChargingUI(true);
      break;
      
      case GameState.Reelable:
      break;
      
      default:
      break;

    }
  }
  public void ResetUI()
  {
    UpdateCastingUI(false);
    UpdateDefaultUI(false);
  }

  #region Activate UI States

  public void UpdateChargingUI(bool value)
  {
    chargeSlider.gameObject.SetActive(value);
  }
  public void UpdateCastingUI(bool value)
  {

  }

  public void UpdateDefaultUI(bool value)
  {
    chargeSlider.gameObject.SetActive(value);
  }

  #endregion

  #region Update UI Values
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
  #endregion
}

