using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject main;
    public GameObject theCrew;
    public GameObject theTuto;
    public void Play()
    {
        SceneManager.LoadScene("Enemy");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HTP()
    {
        theTuto.SetActive(true);   
        main.SetActive(false);   
    }

    public void crew()
    {
        theCrew.SetActive(true);   
        main.SetActive(false);   
    }
    
    public void backToMain()
    {
        main.SetActive(true);
        theCrew.SetActive(false);
        theTuto.SetActive(false);
    }
}
