using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    [SerializeField]
    
    [Header("Paddle")]
    private Vector3 paddleScale;
    public GameObject paddleObject;
    public static int hayattakiBallSayisi = 1;
    public GameObject ballPrefabi;

    [Header("Geçiş & Final Ekranı")]
    public GameObject finalScreen;
    private bool sceneTransitionInitiated = false;
    public float transitionTimer = 0f; // Geçişin başlaması için gecikme sayacı
    public float fadeSpeed = 5f;
    
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
        if(hayattakiBallSayisi >= 5)
        {   // Paddle X ekseninde 2000 olana kadar growSpeed hızında X eksenindeki boyutunu arttırdım
            if (paddleScale.x < targetScaleX)
                {
                    paddleScale.x += growSpeed * Time.deltaTime;
                    
                    
                    paddleObject.transform.localScale = paddleScale;

                }
                //
        }
       
        //

        // FİNAL EKRANINA GEÇİŞ EKRANI BAŞLATILIYOR
        if (hayattakiBallSayisi > 1)
        { // TRANSPARAN BİR ŞEKİLDE EKRANI KAPLAMAYI BEKLEYEN FİNAL EKRANINA GEÇİŞ EKRANININ TRANSPARAN DEĞERİNİ ARTTIRIYORUM
            SpriteRenderer fsr = finalScreen.GetComponent<SpriteRenderer>();
            Color col = fsr.color;
            col.a += fadeSpeed * Time.deltaTime;
            fsr.color = col;
            //

            // BİR SÜRE BEKLEDİKTEN SONRA FİNAL EKRANININ GELMESİNİ SAĞLIYORUM
            if (!sceneTransitionInitiated)
            {   
                UnityEngine.Debug.Log("Transition Timer: " + transitionTimer);
                transitionTimer += Time.deltaTime;
                if (transitionTimer >= 5) // BU KADAR ZAMAN BEKLENİYOR FİNAL EKRANINA GEÇMEDEN ÖNCE
                {
                    UnityEngine.Debug.Log("Geçiş başlatılıyor");
                    sceneTransitionInitiated = true;                    
                    transitionBackground.SetActive(true); // FİNAL EKRANI AÇILIYOR
                    FindObjectOfType<SceneLoader>().StartSceneTransition(); // DİĞER SAHNEYE GEÇMEK İÇİN SceneLoader.cs'deki ilgili fonksiyon burada çalıştırılıyor
                    MusicManager.instance.StopMusic(); 
                }
            }
            //

            
        }
        //

        if(hayattakiBallSayisi == 0)
        {
            CreateBall();
            
        }
    }
    void CreateBall()
        {
               
               GameObject newBall = Instantiate(ballPrefabi, Vector3.zero, Quaternion.identity);
               Ball.ballOlusturmaSayaci = 0;
               hayattakiBallSayisi++;
        }
}
    

