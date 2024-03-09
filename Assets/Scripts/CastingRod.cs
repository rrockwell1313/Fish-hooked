using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CastingRod : MonoBehaviour
{
  //this is our request for other components. Maybe its something else we can do?
  public BobberBehavior bobber;

  //this goes into the UI manager
  public Slider chargeSlider;
  public TextMeshProUGUI distanceText;

  //this actually is correct.
  public float maxCharge = 100f;
  private float chargeRate = 100f; // Adjust as needed for speed of charging
  private float chargeAmount = 0f;
  
  //move all these to state machine.
  private bool isCharging = false;
  private bool isCasting = false;
  private bool isReelable = false;




  void Start()
  {

  }

  void Update()
  {
    ChargeCast();
    CastRod();
    RodReel();
  }

  void ChargeCast()
  {
    //this SHOULD be happening as we hold things down forever.
    if (Input.GetMouseButton(0) && !isReelable) // Or use Input.GetMouseButtonDown(0) for left click
    {
      if (!isCasting)
      {
        isCharging = true;
        if (isCharging)
        {
          chargeAmount += Time.deltaTime * chargeRate;

          if (chargeAmount > maxCharge || chargeAmount < 0) chargeRate = -chargeRate;
          Debug.Log("Charge: " + chargeAmount);
          chargeSlider.value = chargeAmount;
        }
      }
    }

    if (Input.GetMouseButtonUp(0) && isCharging && !isCasting)
    {
      if (isCharging)
      {
        Debug.Log("Release");
        isCharging = false;
        isCasting = true;
        //the distance math should be figured out in the bobber. The casting is JUST the action.
        CastBobber(chargeAmount, maxCharge);
            
      }
    }
  }

  void CastRod()
  {
    if (isCasting)
    {
      bobber.UpdateScale();
      Debug.Log("Casting");
      float _currentDistanceTXT = Vector2.Distance(bobber.castStartPosition, bobber.transform.position);
      distanceText.text = $"{_currentDistanceTXT:F2}m";
    }
  }

  void RodReel()
  {
    if (Input.GetMouseButton(0) && isReelable)
    {
      Debug.Log("Resetting");
      chargeAmount = 0;
      isReelable = false;
      bobber.ResetPosition();
    }
  }

  private void CastBobber(float _charge, float _maxCharge)
  {
    //we coudl just straight call the bobber, but we may want additional bobber details later.
    bobber.ApplyCastForce(_charge, _maxCharge);
  }
}
