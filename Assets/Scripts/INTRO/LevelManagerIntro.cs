using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    public SceneLoader sceneLoader;

    void Start()
    {
       
        if (sceneLoader != null)
        {
            sceneLoader.StartSceneTransition(); 
        }
        else
        {
            Debug.LogError("SceneLoader script'ine referans verilmedi!");
        }
    }

}
