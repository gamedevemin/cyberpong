using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;

    void Start()
    {
        // Zaten varsa kendini yok et (tekil yapı sağlanır)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;

        }

        // Singleton kur
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Ses kaynağını al
        audioSource = GetComponent<AudioSource>();

        // Müzik çalmıyorsa başlat
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
    void Update()
    {
       
        Debug.Log(instance);
        Debug.Log(audioSource.isPlaying);
        Debug.Log(AudioListener.pause);
    }

    // Müzik durdurma
    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // Müzik başlatma
    public void StartMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
