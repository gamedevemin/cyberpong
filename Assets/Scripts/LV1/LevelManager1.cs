using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;
using System;


public class LevelManager1 : MonoBehaviour
{
    [SerializeField]
    
    private bool spacePress; // Space'e 1 kez basıldı mı kontrolü
    private bool mouse0Press; // Mouse 0'a 1 kez basıldı mı kontrolü
    private bool mouse1Press; // Mouse 1'e 1 kez basıldı mı kontrolü
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
    public float fadeSpeed; // Patlama (Beyaz Ekran) Hızı
    
    [Header("FİNAL için Büyüme Ayarları")]
    public int targetScaleX = 2000; // HEDEF PADDLE X SCALE DEĞERİ -GEREĞİNDEN FAZLA-
    public float growSpeed = 0.001f; // PADDLE'IN HEDEF X SCALE DEĞERİNE ULAŞMA HIZI


    public GameObject transitionBackground;    
    void Start()
    {    
        GameObject newBall = Instantiate(ballPrefabi, Vector3.zero, Quaternion.identity);
        transitionBackground.SetActive(false); // SAHNE GEÇİŞİNDE KAPLAYACAK OLAN GÖRÜNTÜ 
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

        if(hayattakiBallSayisi > 900) StartCoroutine(ballsBigger());
        if(hayattakiBallSayisi > 8) StartCoroutine(ArtardaBallOlustur()); 
        if (hayattakiBallSayisi > 905)
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

        // Oyun başındaki bilgilendirmelerin ekrandan silinmesi
        if(Input.GetMouseButtonDown(0)) mouse0Press = true;
        if(Input.GetMouseButtonDown(1)) mouse1Press = true;
        if(Input.GetKeyDown(KeyCode.Space)) spacePress = true;
        if(Input.GetKeyDown(KeyCode.Escape) || (mouse0Press && mouse1Press && spacePress)) SkillsInfoCanvas.SetActive(false);
        //

    }

    IEnumerator ArtardaBallOlustur()
    {
        while (hayattakiBallSayisi < 700)
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

    IEnumerator ballsBigger()
    {
        List<GameObject> balls = GameObject.FindGameObjectsWithTag("Ball").ToList();
    
        foreach (int i in Enumerable.Range(0, 8)) // 8 kez dönecek: 0-7
        {
            foreach (GameObject ball in balls)
            {
                ball.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
    
                
            }
    
            yield return new WaitForSeconds(1f); // her adım arasında bekle (toplam 0.8 saniyede büyür)
        }
    }
    
}    
    

