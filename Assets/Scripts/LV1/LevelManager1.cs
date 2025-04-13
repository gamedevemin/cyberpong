using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    [SerializeField]
    
    [Header("Paddle")]
    private Vector3 paddleScale;
    public GameObject paddleObject;

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
        transitionBackground.SetActive(false); // SAHNE GEÇİŞİ AMAÇLI EKRANI KAPLAYACAK OLAN GÖRÜNTÜ

        paddleScale = paddleObject.transform.localScale;
    }

    void Update()
    {   // FİNAL aşamasına geçiş için paddle'ın boyu kontrolsüzce uzamasını sağladım
        if(Ball.hayattakiBallSayisi >= 1)
        {   // Paddle X ekseninde 2000 olana kadar growSpeed hızında X eksenindeki boyutunu arttırdım
            if (paddleScale.x < targetScaleX)
                {
                    paddleScale.x += growSpeed * Time.deltaTime;
                    UnityEngine.Debug.Log("Büyüyor Paddle");
                    UnityEngine.Debug.Log(paddleScale.x);
                    paddleObject.transform.localScale = paddleScale;

                }
                //
        }
        //

        // FİNAL EKRANINA GEÇİŞ EKRANI BAŞLATILIYOR
        if (Ball.hayattakiBallSayisi > 8)
        { // TRANSPARAN BİR ŞEKİLDE EKRANI KAPLAMAYI BEKLEYEN FİNAL EKRANINA GEÇİŞ EKRANININ TRANSPARAN DEĞERİNİ ARTTIRIYORUM
            SpriteRenderer fsr = finalScreen.GetComponent<SpriteRenderer>();
            Color col = fsr.color;
            col.a += fadeSpeed * Time.deltaTime;
            fsr.color = col;
            //

            // BİR SÜRE BEKLEDİKTEN SONRA FİNAL EKRANININ GELMESİNİ SAĞLIYORUM
            if (!sceneTransitionInitiated)
            {
                transitionTimer += Time.deltaTime;
                if (transitionTimer >= 2f) // BU KADAR ZAMAN BEKLENİYOR FİNAL EKRANINA GEÇMEDEN ÖNCE
                {
                    sceneTransitionInitiated = true;                    
                    transitionBackground.SetActive(true); // FİNAL EKRANI AÇILIYOR
                    FindObjectOfType<SceneLoader>().StartSceneTransition(); // DİĞER SAHNEYE GEÇMEK İÇİN SceneLoader.cs'deki ilgili fonksiyon burada çalıştırılıyor
                    MusicManager.instance.StopMusic(); 
                }
            }
            //
        }
        //
    }
}
