using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManage : MonoBehaviour
{
    //public GameObject player;
    public GameObject panelPause;
    //public SelectManage instance;
    bool pause=false;
    // Start is called before the first frame update
    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //}
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Muerte" || SceneManager.GetActiveScene().name== "Victoria")
        {
            ViewCursor();
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            TimeManager();
        }
    }

    public void TimeManager()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void ViewCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void SelectedLife()
    {
        Debug.Log("funciona");
    }

    public void SelectMental()
    {
        Debug.Log("funciona");
    }

    public void SelectReanudar()
    {
        Debug.Log("funciona");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Maze");
    }
    public void OptionsMenu()
    {
        Debug.Log("funciona");
    }
    public void PauseGame()
    {
        TimeManager();
        ViewCursor();
        if (panelPause!=null)
        {
            panelPause.SetActive(!panelPause.activeSelf);
        }
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("MenuInicio");
        //ViewCursor();
    }

}
