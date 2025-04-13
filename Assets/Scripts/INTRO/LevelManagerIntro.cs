using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    public SceneLoader sceneLoader;  // SceneLoader script'inin referansı

    void Start()
    {
        // Oyun başladığında sahne geçişini başlat
        if (sceneLoader != null)
        {
            sceneLoader.StartSceneTransition();  // StartSceneTransition() metodunu çağır
        }
        else
        {
            Debug.LogError("SceneLoader script'ine referans verilmedi!");
        }
    }

}
