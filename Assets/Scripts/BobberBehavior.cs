using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehavior : MonoBehaviour
{
  private Vector2 castStartPosition;
  private Vector3 originalScale;
  public float maxCastForce = 15f;

  private Rigidbody2D rb;
  private float distanceToTravel;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    originalScale = transform.localScale;
    castStartPosition = transform.localPosition;
  }

  public void ApplyCastForce(float charge, float maxCharge)
  {
    float forceMagnitude = (charge / maxCharge) * maxCastForce;
    rb.AddForce(new Vector2(0, forceMagnitude), ForceMode2D.Impulse);
  }

  public void UpdateScale(float progress)
  {
    float scaleMultiplier = CalculateScaleMultiplier(progress);
    transform.localScale = originalScale * scaleMultiplier;
  }

  float CalculateScaleMultiplier(float progress)
  {
    if (progress <= 0.5f)
    {
      return Mathf.Lerp(1f, 2f, progress * 2);
    }
    else
    {
      return Mathf.Lerp(2f, 1f, (progress - 0.5f) * 2);
    }
  }

  public void ResetPosition()
  {
    transform.localPosition = castStartPosition;
    Debug.Log("Reel In");
  }
}
