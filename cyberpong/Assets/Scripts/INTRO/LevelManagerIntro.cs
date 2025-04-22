using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public SceneLoader sceneLoader;  // SceneLoader referansını Inspector’dan atıyoruz

    void Start()
    {
        // Oyun başladığında sahne geçişini tetikle
        if (sceneLoader != null)
        {
            sceneLoader.StartSceneTransition();  // SceneLoader varsa geçişi başlat
        }
        else
        {
            Debug.LogError("SceneLoader script'ine referans verilmedi!");  // Atanmamışsa konsola hata yaz
        }
    }

}
