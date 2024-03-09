using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberBehavior : MonoBehaviour
{
  public Vector2 castStartPosition;
  private Vector3 originalScale;

  public float maxCastForce = 15f;
  public float maxCastDistance = 5f;

  private float currentDistance;
  private Rigidbody2D rb;
  private float distance;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    originalScale = transform.localScale;
    castStartPosition = transform.localPosition;
  }

  void Update()
  {
      //nothing is needed yet.
  }

  public void ApplyCastForce(float _chargeAmount, float _maxCharge)
  {
    distance  = (_chargeAmount / _maxCharge) * maxCastDistance;
    float forceMagnitude = (_chargeAmount / _maxCharge) * maxCastForce;
    rb.AddForce(new Vector2(0, forceMagnitude), ForceMode2D.Impulse);
  }

  public void UpdateScale()
  {
    currentDistance = Vector2.Distance(castStartPosition, transform.position);

    float _progress = currentDistance / distance;
    float _scaleMultiplier = CalculateScaleMultiplier(_progress);

    transform.localScale = originalScale * _scaleMultiplier;
    // once distanceToTravel is reached, stop moving.
    if (currentDistance >= distance)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
  }

  float CalculateScaleMultiplier(float _progress)
  {
    //i need more knowledge about this.
    if (_progress <= 0.5f)
    {
      return Mathf.Lerp(1f, 2f, _progress * 2);
    }
    else
    {
      return Mathf.Lerp(2f, 1f, (_progress - 0.5f) * 2);
    }
  }

  public void ResetPosition()
  {
    transform.localPosition = castStartPosition;
    Debug.Log("Reel In");
  }
}
