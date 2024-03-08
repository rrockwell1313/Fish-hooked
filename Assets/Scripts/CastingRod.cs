using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CastingRod : MonoBehaviour
{
  public BobberBehavior bobber;
  public Slider chargeSlider;
  public TextMeshProUGUI distanceText;

  public float maxCharge = 100f;
  public float maxCastDistance = 10f;
  public float maxCastForce = 15f;
    
  private float chargeRate = 100f; // Adjust as needed for speed of charging
  private float chargeAmount = 0f;
    
  private bool isCharging = false;
  private bool isCasting = false;
  private bool isReelable = false;

  //Boobber Parts
  private float distanceToTravel;
  private Rigidbody2D bobberRigidbody;
  private Vector2 castStartPosition;
  private Vector3 originalScale;

  #region Changes to be made
    //*
    //I need to now seperate the components. We need all UI components moved out and put into a UI manager. 
    //I need to make the bob behavior outside of the casting method. 
    //Once we have componentized everything from here, we can add the bobbing method to the bobb to let it move in the water.
    //Then we need a state machine to keep track of what state the casting, and bobber is in to begin the fish system.
    //*//
  #endregion

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
        distanceToTravel = (chargeAmount / maxCharge) * maxCastDistance;
        Debug.Log("Distance: " + distanceToTravel);
        CastBobber(chargeAmount, distanceToTravel);
            
      }
    }
  }

  void CastRod()
  {
    if (isCasting)
    {
      bobber.UpdateScale(distanceToTravel);
      Debug.Log("Casting");
      float currentDistanceTXT = Vector2.Distance(castStartPosition, bobber.transform.position);
      distanceText.text = $"{currentDistanceTXT:F2}m";

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

  private void CastBobber(float charge, float distanceToTravel)
  {
    bobber.ApplyCastForce(charge, maxCharge);
  }
}
