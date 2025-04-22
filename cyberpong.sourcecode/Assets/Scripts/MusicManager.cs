using UnityEngine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class MusicManager : MonoBehaviour
{

    /* 

        ARKADA CALAN MÜZİĞİN KONTROL EDİLDİĞİ SINIF. YORUM SATIRLARI İÇİN GÜNCELLEME BEKLİYOR.

    */


    public static MusicManager instance;
    private AudioSource audioSource;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {

        if (instance != null && instance != this) // Bir başka ses oynatılıyorsa
        {
            Destroy(gameObject); // Music
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

    // Müzik durdurma
    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
   
    }

    // Müzik başlatma
    public void StartMusic()
    { 
        if (audioSource != null && !audioSource.isPlaying) audioSource.Play();
    }

    public static IEnumerator FadeOutMusic()
    {
        AudioSource source = MusicManager.instance.GetComponent<AudioSource>();
        while (source.volume > 0.1f)
        {
            source.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}