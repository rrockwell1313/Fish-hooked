using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CastingRod : MonoBehaviour
{
  public BobberBehavior bobber;

  public float maxCharge = 100f;
  private float chargeRate = 100f;
  private float chargeAmount = 0f;
  
  void Update()
  {
    MouseInput();
    CastRod();
  }

  void MouseInput()
  {

    if (Input.GetMouseButton(0))//Holding
    {
      switch (GameManager.GM.currentState)
      {
        case GameState.Default:
          GameManager.GM.ChangeState(GameState.Charging);
          break;

        case GameState.Charging:
          ChargeRod();
          break;

        default:
          break;
      }
    }

    if (Input.GetMouseButtonUp(0))//Let go
    {
      switch (GameManager.GM.currentState)
      {
        case GameState.Charging:
          CastBobber(chargeAmount, maxCharge);
          GameManager.GM.ChangeState(GameState.Casting);
          break;

        case GameState.Reelable:
          RodReel();
          break;

        default:
          break;
      }
    }
    
  }

  void ChargeRod()
  {
      chargeAmount += Time.deltaTime * chargeRate;
      if (chargeAmount > maxCharge || chargeAmount < 0) chargeRate = -chargeRate;
      UIManager.UI.UpdateChargeSlider(chargeAmount);
  }

  void CastRod()
  {
    if (GameManager.GM.currentState == GameState.Casting)
    {
      Debug.Log("Casting Rod");
      float _currentDistanceTXT = Vector2.Distance(bobber.castStartPosition, bobber.transform.position);
      UIManager.UI.UpdateDistanceText( $"{_currentDistanceTXT:F2}m");
    }
  }

  void RodReel()
  {
      chargeAmount = 0;
      UIManager.UI.UpdateChargeSlider(chargeAmount);
      bobber.ResetPosition();
  }

  void CastBobber(float _charge, float _maxCharge)
  {
    bobber.ApplyCastForce(_charge, _maxCharge);
  }
}
