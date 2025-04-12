using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("Sahne Ayarları")]
    [SerializeField] private string sceneToLoad;  // Yüklenecek sahnenin adı

    [Header("Geçiş Ses Ayarları")]
    [SerializeField] private AudioClip transitionClip;  // Sahne geçişi sırasında çalacak ses
    [SerializeField] private AudioSource audioSource;     // Ses kaynağı
    public GameObject transitionBackground;

    void Start()
    {
                    transitionBackground.SetActive(false);

    }
    /// <summary>
    /// Sahne geçişini başlatır: 4 saniye bekler, geçiş sesini oynatır ve ses tamamen bittiğinde sahneyi yükler.
    /// </summary>
    public void TransitionScene()
    {
        StartCoroutine(TransitionSceneRoutine());
    }

    private IEnumerator TransitionSceneRoutine()
    {
        // Arkaplanı aktif et (örneğin siyah ekran)
        transitionBackground.SetActive(true);
    
        // Geçiş sesi varsa oynat, sesin tamamlanmasını bekle
        if (audioSource != null && transitionClip != null)
        {
            audioSource.clip = transitionClip;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    
        // Asenkron sahne yüklemesini başlat
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad);
    
        // Yükleme tamamlanana kadar bekle (genellikle %90'a kadar yükleme yapar)
        yield return new WaitUntil(() => asyncOp.progress >= 0.9f);
    
        // Sahneyi aktive et
        asyncOp.allowSceneActivation = true;
    }
    

}
