using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("Ses Dosyası")]
    private AudioClip startClip;

    private AudioSource audioSource;
    public SceneLoader sceneLoader; 


    void Start()
    {
        if (audioSource != null && startClip != null)
        {
            audioSource.clip = startClip;
            audioSource.Play();            
        }
        else
        {
            Debug.LogWarning("Ses çalmak için gerekli bileşenler eksik!");
        }
        sceneLoader.StartSceneTransition(); // Sonraki sahneye geçiş
    }

    void Update()
    {
        if(!audioSource.isPlaying) MusicManager.instance.StartMusic(); // audioSource'deki ses bittiğinde oyun müziğinin başlatılması
    }
}
