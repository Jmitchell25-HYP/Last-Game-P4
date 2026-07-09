using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;


    public void Update()
    { if (Keyboard.current.escapeKey.wasPressedThisFrame)
      {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
      }
       
          
      
    }



    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



}
