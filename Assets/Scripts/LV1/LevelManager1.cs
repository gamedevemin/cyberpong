using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.InputSystem;
public class LevelManager1 : MonoBehaviour
{
    [SerializeField]
    
    private bool spacePress;
    private bool mouse0Press;
    private bool mouse1Press;
    [Header("Paddle")]
    private Vector3 paddleScale;
    public GameObject SkillsInfoCanvas;
    public GameObject paddleObject;
    public static int hayattakiBallSayisi = 1;
    public GameObject ballPrefabi;

    [Header("Geçiş & Final Ekranı")]
    public GameObject finalScreen;
    private bool sceneTransitionInitiated = false;
    public float transitionTimer = 0f; // Geçişin başlaması için gecikme sayacı
    public float fadeSpeed;
    
    [Header("FİNAL için Büyüme Ayarları")]
    public int targetScaleX = 2000; // HEDEF PADDLE X SCALE DEĞERİ
    public float growSpeed = 0.001f; // PADDLE'IN HEDEF X SCALE DEĞERİNE ULAŞMA HIZI


    public GameObject transitionBackground;    
    void Start()
    {    
        GameObject newBall = Instantiate(ballPrefabi, Vector3.zero, Quaternion.identity);
        transitionBackground.SetActive(false); // SAHNE GEÇİŞİ AMAÇLI EKRANI KAPLAYACAK OLAN GÖRÜNTÜ
        paddleScale = paddleObject.transform.localScale;
    }

    void Update()
    {   // FİNAL aşamasına geçiş için paddle'ın boyu kontrolsüzce uzamasını sağladım
        if(hayattakiBallSayisi >= 8)
        {   // Paddle X ekseninde 2000 olana kadar growSpeed hızında X eksenindeki boyutunu arttırdım
            if (paddleScale.x < targetScaleX)
                {
                    paddleScale.x += growSpeed * Time.deltaTime;                   
                    paddleObject.transform.localScale = paddleScale;
                }
                //
        }
        //

        if(hayattakiBallSayisi > 0) StartCoroutine(ArtardaBallOlustur()); 
       
        if (hayattakiBallSayisi > 1000)
        { // TRANSPARAN BİR ŞEKİLDE EKRANI KAPLAMAYI BEKLEYEN FİNAL EKRANINA GEÇİŞ EKRANININ TRANSPARAN DEĞERİNİ ARTTIRIYORUM

            StartCoroutine(IEStopMusic());
            SpriteRenderer fsr = finalScreen.GetComponent<SpriteRenderer>();
            Color col = fsr.color;
            col.a += fadeSpeed * Time.deltaTime;
            fsr.color = col;
            //

            // BİR SÜRE BEKLEDİKTEN SONRA FİNAL EKRANININ GELMESİNİ SAĞLIYORUM
            if (!sceneTransitionInitiated)
            {   
                transitionTimer += Time.deltaTime;
                if (transitionTimer >= 5) // BU KADAR ZAMAN BEKLENİYOR FİNAL EKRANINA GEÇMEDEN ÖNCE
                {
                    sceneTransitionInitiated = true;                    
                    transitionBackground.SetActive(true); // FİNAL EKRANI AÇILIYOR
                    FindObjectOfType<SceneLoader>().StartSceneTransition(); // DİĞER SAHNEYE GEÇMEK İÇİN SceneLoader.cs'deki ilgili fonksiyon burada çalıştırılıyor
                    
                }
            }
            //

        }
        //

        if(hayattakiBallSayisi == 0) CreateBall();


        if(Input.GetMouseButtonDown(0)) mouse0Press = true;
        if(Input.GetMouseButtonDown(1)) mouse1Press = true;
        if(Input.GetKeyDown(KeyCode.Space)) spacePress = true;
        if(Input.GetKeyDown(KeyCode.Escape) || (mouse0Press && mouse1Press && spacePress)) SkillsInfoCanvas.SetActive(false);
    
    }

    IEnumerator ArtardaBallOlustur()
    {
        while (hayattakiBallSayisi < 500)
        {
            Instantiate(ballPrefabi, Vector3.zero, Quaternion.identity);
            hayattakiBallSayisi++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CreateBall()
        {
               GameObject newBall = Instantiate(ballPrefabi, Vector3.zero, Quaternion.identity);
               Ball.ballOlusturmaSayaci = 0;
               hayattakiBallSayisi++;
        }

    public static IEnumerator IEStopMusic()
    {
        AudioSource musicSource = MusicManager.instance.GetComponent<AudioSource>();
        float startVolume = musicSource.volume;
    
        float duration = 2f; // 2 saniyede kısılsın
        float elapsed = 0f;
    
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = startVolume; // Bir sonraki sahne için eski haline getir (istersen sıfırda da bırakabilirsin)
    }

}    
    

