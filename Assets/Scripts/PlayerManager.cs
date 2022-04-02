using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance; set => instance = value; }
    #endregion
    
    public PlayerController player;
    void Awake()
    {
            if (Instance == null)
                Instance = this;
    }
}
