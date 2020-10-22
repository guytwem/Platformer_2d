using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    #region Variables
    public static bool isPaused;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    #endregion
    // Start is called before the first frame update
    #region Start
    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        

    }
    #endregion
    // Update is called once per frame
    #region Pausing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuActive(); 
        }
    }
    public void PauseMenuActive()
    {
        isPaused = !isPaused;
        Debug.Log(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pausePanel.SetActive(false);
            optionsPanel.SetActive(false);
        }
    }
    #endregion
}
