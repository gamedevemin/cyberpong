using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject Pause;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Pause.activeSelf && Input.GetKeyDown(KeyCode.Escape)) Pause.SetActive(true); // PAUSE
        else if (Pause.activeSelf && Input.GetKeyDown(KeyCode.Escape)) Pause.SetActive(false); // CONTINUE 


    }

    public void QuitButton() 
    {
        Application.Quit();
    }
    public void ContinueButton() 
    {
        Pause.SetActive(false);
    }
}
