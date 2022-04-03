using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private float scoreFloat;
    public bool stopScore;
    
    #region Singleton
    private static Score instance;
    public static Score Instance { get => instance; set => instance = value; }
    #endregion
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void FixedUpdate()
    {
        if (!stopScore)
        {
            scoreFloat ++;
            scoreText.text = (scoreFloat/60).ToString("F2");
        }
    }
}
