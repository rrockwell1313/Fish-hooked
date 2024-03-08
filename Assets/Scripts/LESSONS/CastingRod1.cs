using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CastingRod1 : MonoBehaviour
{
  public BobberBehavior bobber; // Assign your bobber game object here
  private Rigidbody2D bobberRigidbody;
  public Slider chargeSlider;
  public TextMeshProUGUI distanceText;

  public float maxCharge = 100f;
  public float maxCastDistance = 10f;
  public float maxCastForce = 15f;
    
  private float chargeRate = 100f; // Adjust as needed for speed of charging
  private float chargeAmount = 0f;
  private float distanceToTravel;
    
  private bool isCharging = false;
  private bool isCasting = false;
  private bool isReelable = false;

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
    // Ensure the bobber has a Rigidbody2D component
    //if (bobber != null)
    //{
    //  bobberRigidbody = bobber.GetComponent<Rigidbody2D>();
    //  castStartPosition = bobber.transform.position; // Remember start position
    //  originalScale = bobber.transform.localScale;

    //  if (bobberRigidbody == null)
    //  {
    //    Debug.LogError("Bobber does not have a Rigidbody2D component.");
    //  }
    //}
    //else
    //{
    //  Debug.LogError("Bobber does not exist.");
    //}

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
          // Oscillate charge amount here, example logic only
          chargeAmount += Time.deltaTime * chargeRate;

          //Check charge amount and go backwards if at max.
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
        //CastBobber(chargeAmount, distanceToTravel);
        bobber.ApplyCastForce(chargeAmount, maxCharge);
            
      }
    }
  }

  void CastRod()
  {
    if (isCasting)
    {
      float currentDistance = Vector2.Distance(castStartPosition, bobber.transform.position);
      float progress = currentDistance / distanceToTravel;
      bobber.UpdateScale(progress);
      // Scale the bobber based on its progress.
      //float scaleMultiplier = CalculateScaleMultiplier(progress);
      //bobber.transform.localScale = originalScale * scaleMultiplier;
      // Cast the rod

      Debug.Log("Casting");
      float currentDistanceTXT = Vector2.Distance(castStartPosition, bobber.transform.position);
      distanceText.text = $"{currentDistanceTXT:F2}m";

      if (Vector2.Distance(castStartPosition, bobber.transform.position) >= maxCastDistance || bobberRigidbody.velocity.magnitude < 0.01f)
      {
        // Reset only when the bobber has stopped or reached its maximum distance
        isCasting = false;
        isReelable = true;
        // Additional logic to handle the bobber reset, if needed, could go here
      }

      if (Vector2.Distance(castStartPosition, bobber.transform.position) >= distanceToTravel)
      {
        bobberRigidbody.velocity = Vector2.zero; // Stop the bobber's movement
        bobberRigidbody.isKinematic = true; // Optionally make it kinematic to fully stop physics interactions
      }
      else
      {
        bobberRigidbody.isKinematic = false;
      }
    }
  }

  void RodReel()
  {
    if (Input.GetMouseButton(0) && isReelable)
    {
      Debug.Log("Resetting");
      // Reset bobber to start position or any other reset logic
      bobber.transform.position = castStartPosition;
      bobberRigidbody.velocity = Vector2.zero;
      chargeAmount = 0;
      // Allow new charge/cast cycle
      bobberRigidbody.isKinematic = false;
      isReelable = false;
    }
  }

  private void CastBobber(float charge, float distanceToTravel)
  {
    //Take the maxCastForce and compare it to the charge and max charge to calculate the force.
    bobber.ApplyCastForce(charge, distanceToTravel);
    //if (bobberRigidbody != null)
    //{
    //  // Calculate the proportion of the max force based on the current charge
    //  float forceMagnitude = (charge / maxCharge) * maxCastForce; // Adjust this calculation as needed
    //  Vector2 force = new Vector2(0, forceMagnitude);
    //  bobberRigidbody.AddForce(force, ForceMode2D.Impulse);
    //  Debug.Log("Force: " + force);
    //}


  }

  //float CalculateScaleMultiplier(float progress)
  //{
  //  if (progress <= 0.5f)
  //  {
  //    // Scale up from original to 2x original
  //    return Mathf.Lerp(1f, 2f, progress * 2);
  //  }
  //  else
  //  {
  //    // Scale down from 2x original back to original
  //    return Mathf.Lerp(2f, 1f, (progress - 0.5f) * 2);
  //  }
  //}
}
