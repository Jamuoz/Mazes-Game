using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManage : MonoBehaviour
{
    //public GameObject player;
    public GameObject panelPause;
    bool OpenMenu =false;
    //public SelectManage instance;
    bool pause=false;
    
    private void Awake()
    {
        //bloquea el cursor si esat en la escena nombrada y lo desbloquea si no
        if(SceneManager.GetActiveScene().name == "Maze" ) HideCursor();else ViewCursor();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !OpenMenu) 
        {
            PauseGame();
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

    public void UpdateMenuOpen(bool val){
        OpenMenu =val;
    }
    public void ViewCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HideCursor(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        TimeManager(); HideCursor();
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
        ViewCursor();
        OpenMenu =true;
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
