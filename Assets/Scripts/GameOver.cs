using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject goScreen;
    public GameObject leManagerQuiNestPasUneInstance;


    #region Singleton
    private static GameOver instance;
    public static GameOver Instance { get => instance; set => instance = value; }
    #endregion
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    
    
    public void callGameOver()
    {
        goScreen.SetActive(true);
        leManagerQuiNestPasUneInstance.SetActive(false);
        Score.Instance.stopScore = true;
        PlayerManager.Instance.player.activatePlayer(false);
    }

    public void retry()
    {
        SceneManager.LoadScene("Enemy", LoadSceneMode.Single);
    }
    
    public void menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
