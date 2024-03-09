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
    if (Input.GetMouseButton(0) && !GameManager.GM.IsReelable && !GameManager.GM.IsCasting)
    {
        Debug.Log("Preparing Cast");
        GameManager.GM.IsCharging = true;
        if (GameManager.GM.IsCharging)
        {
          chargeAmount += Time.deltaTime * chargeRate;

          if (chargeAmount > maxCharge || chargeAmount < 0) chargeRate = -chargeRate;
          chargeSlider.value = chargeAmount;
        }
    }

    if (Input.GetMouseButtonUp(0) && GameManager.GM.IsCharging && !GameManager.GM.IsCasting)
    {
        Debug.Log("Released");
        GameManager.GM.IsCharging = false;
        GameManager.GM.IsCasting = true;
        //the distance math should be figured out in the bobber. The casting is JUST the action.
        CastBobber(chargeAmount, maxCharge);
    }
  }

  void CastRod()
  {
    if (GameManager.GM.IsCasting)
    {
      //this literally only exists to manage the text right now which is useless.
      float _currentDistanceTXT = Vector2.Distance(bobber.castStartPosition, bobber.transform.position);
      distanceText.text = $"{_currentDistanceTXT:F2}m";
    }
  }

  void RodReel()
  {
    if (Input.GetMouseButtonUp(0) && GameManager.GM.IsReelable && !GameManager.GM.IsCasting)
    {
      chargeAmount = 0;
      bobber.ResetPosition();
      GameManager.GM.IsCasting = false;
      GameManager.GM.IsCharging = false;
    }
  }

  private void CastBobber(float _charge, float _maxCharge)
  {
    Debug.Log("Casting Bobber");
    //we coudl just straight call the bobber, but we may want additional bobber details later.
    bobber.ApplyCastForce(_charge, _maxCharge);
  }
}
