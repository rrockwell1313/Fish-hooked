using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;


public enum GameState
{
  Default,
  Casting,
  Reelable,
  Charging,
  Fishing,
  Hooked,
  Snapped
}
public class GameManager : MonoBehaviour
{
  public GameState currentState;

  public static GameManager GM { get; private set; }
  public static event Action<GameState> OnStateChange;

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
    currentState = GameState.Default;//set default state.
  }

  private void Start()
  {
    OnStateChange += HandleStateChange;
  }

  public void ChangeState(GameState newState)
  {
    Debug.Log($"Changing State to {newState}");
    currentState = newState;
    OnStateChange(newState);
  }

  private void HandleStateChange(GameState newState)
  {
    UIManager.UI.UpdateUIState(newState);
  }
}
