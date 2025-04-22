using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("Sahne Ayarları")]
    [SerializeField] private string sceneToLoad;  // Yüklenecek sahnenin adı
    [SerializeField] private float delayBeforeTransition = 10f; // Sahne geçişinden önceki bekleme süresi (saniye)

    [Header("Geçiş Efekti Ayarları")]
    [SerializeField] private bool useTransitionMusic = true;  // Geçiş müziği çalacak mı?
    [SerializeField] private AudioClip transitionClip;  // Geçiş sırasında çalacak ses
    [SerializeField] private AudioSource transitionAudioSource;  // Geçiş müziği için kullanılan AudioSource


    // Bu metot sahneye geçişi başlatır, ama bekleme süresi ekler
    public void StartSceneTransition()
    {
        
        StartCoroutine(TransitionAfterDelay());
    }
    

    private IEnumerator TransitionAfterDelay()
    
    {
        // Eğer geçiş müziği aktifse, müzik çalacak
        if (useTransitionMusic && transitionClip != null && transitionAudioSource != null)
        {
            transitionAudioSource.clip = transitionClip;
            transitionAudioSource.Play();


        }
        yield return new WaitForSeconds(delayBeforeTransition);

        // Asenkron sahne yüklemesi başlatılır
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        // Yükleme %90’a kadar tamamlanana kadar bekle
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        // Sahneyi etkinleştir
        asyncLoad.allowSceneActivation = true;
    }
}
