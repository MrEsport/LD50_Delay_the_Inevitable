using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Luca_Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HTP()
    {
        Debug.Log("C'est le tuto mais flemme");
    }
}
