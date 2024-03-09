using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  #region Fishing Rod States
  public bool IsCharging { get; set; }
  public bool IsCasting { get; set; }
  public bool IsReelable { get; set; }
  #endregion

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }
}
