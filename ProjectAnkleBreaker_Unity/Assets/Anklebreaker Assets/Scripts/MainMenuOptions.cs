using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOptions : MonoBehaviour
{
    //This houses all the main menu & Pause Menu funtionality

    [SerializeField] GameObject FocusOnButton;
    [SerializeField] GameObject resetButton;
    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject GameplayCanvas;
    [SerializeField] GameObject EndGameCanvas;

    #region Main Modes
    public void LoadLevel(string LevelName) 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelName);
        
        
    }

    public void Options() 
    { 
    
    }

    public void QuitGame() 
    {
        Application.Quit();
        Debug.Log("Thanks for Playing, Game has quit");
    }
    #endregion

    #region Options
    public void ResolutionSettings() 
    { }
    #endregion

    #region Pause Menu
    public void PauseGame() 
    {
        Time.timeScale = 0;
        GameplayCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(FocusOnButton);

    }

    public void EndGameScreen() 
    {
        Time.timeScale = 0;
        GameplayCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(resetButton);

    }

    public void UnPauseGame() 
    {
        Time.timeScale = 1;
        GameplayCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    

    #endregion


    public void Start()
    {
        //GameplayCanvas.SetActive(true);
        //PauseCanvas.SetActive(false);
        if(GameplayCanvas != null)
        {
            GameplayCanvas.SetActive(true);
            PauseCanvas.SetActive(false);
            EndGameCanvas.SetActive(false);
        }
    }
}
