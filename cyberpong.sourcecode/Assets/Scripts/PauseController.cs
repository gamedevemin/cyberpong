using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject Pause;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(!Pause.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Pause.SetActive(true); // PAUSE
        }    
        else if (Pause.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Pause.SetActive(false); // CONTINUE
        } 
    }

    public void QuitButton() 
    {
        Application.Quit();
    }
    public void ContinueButton() 
    {
        Pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
