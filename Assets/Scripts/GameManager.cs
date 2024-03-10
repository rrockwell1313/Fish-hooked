using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  //Basically what this does is assign a object the ability to maintain existence in every scene, but only one of them.
  //in turn, with its global existence we can call and set any variable what we need from it without defining it in each script.
  //It makes it a little long, but allows us to always call a set location of states and data.
  public static GameManager GM { get; private set; }
  public static event Action<bool> OnCastingStateChange;


  #region Fishing States
  public bool IsCharging { get; set; } = false;
  //attatch the event to IsCasting
  public bool IsCasting { get; set; } = false;
  public bool IsReelable { get; set; } = false;
  #endregion

  #region Game States
  public bool IsPaused { get; set; }
  public bool IsPlaying { get; set; } = false;
  #endregion

  private void Awake()
  {
    //if it doesnt exist, make it. if it does, destroy it.
    if (GM == null)
    {
      GM = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Update()
  {
    //setup debugs to view all states.
    if (IsCasting)
    {
      Debug.Log("Casting");
    }

    if (IsCharging)
    {
      Debug.Log("Charging");
    }

    if (IsReelable)
    {
      Debug.Log("Reelable");
    }
  }

  public void ChangeCastingState(bool newCastingState)
  {
    if (IsCasting != newCastingState)
    {
      IsCasting = newCastingState;
      // Raise the event to notify subscribers
      OnCastingStateChange?.Invoke(IsCasting);
    }
  }
}
