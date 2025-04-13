using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class Ball : MonoBehaviour
{
    [SerializeField]
    public static int hayattakiBallSayisi;

    [Header("Topun Hareket Ayarları")]
    private int speed = 10;
    public int maxSpeed = 20;
    private Vector2 direction;
    public Rigidbody2D rb;

    [Header("Top Yönetimi")]
    public GameObject BallPrefabi;
    private static int ballOlusturmaSayaci;

    [Header("Paddle Etkileşimi")]
    private Paddle paddleScript;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // BALL'IN ROTASYON DEĞERLERİ DONDURULUR

        // BALL'A YÖN VE HIZ ATAMALARI
        transform.position = Vector3.zero; // BALL'I X, Y, Z'DE 0 POZİSYONUNA ATAR
        direction = Random.insideUnitCircle.normalized; // RASTGELE BİR X, Y DEĞERİ ATANIR BALL İSTİKAMETİ RASTGELE OLMASI İÇİN
        rb.linearVelocity = direction * speed; // Ball'a yön ve hız tanımlanır
        //

        GameObject paddleObject = GameObject.Find("Paddle"); // Paddle ADINDAKİ GAMEOBJECT ÇAĞIRILIR
        if(paddleObject != null)
        {
            paddleScript = paddleObject.GetComponent<Paddle>(); // Paddle NESNESİ "Paddle" GAMEOBJECT'İNDEKİ Paddle NESNESİNE DÖNÜŞTÜRÜLÜR
        }
        else
        {
            UnityEngine.Debug.LogError("Paddle isimli GameObject bulunamadı!");
        }
    }

    void Update()
    {
        // BALL YOK OLUŞ
        if (transform.position.y <= -5){
            Destroy(gameObject);
            hayattakiBallSayisi--;
        } 
        //

        if (ballOlusturmaSayaci == 4) CreateBall(); // YENİ BALL OLUŞMASI
    }

    // BALL COLLİSİON'I İLE DİĞER COLLİSİONLARIN ETKİLEŞİMİ SONUÇLARI
    void OnCollisionEnter2D(Collision2D collision)
    {
        // BALL'IN SEKMESİ VE HIZININ ARTMASI
        if(collision.gameObject.tag == "Paddle" || collision.gameObject.tag == "Wall")
        {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal); // BALL'IN TERS YÖNÜNÜ DEĞİŞKENE ATAMAK
        rb.linearVelocity = direction * speed; // BALL'IN TERS YÖNE YÖNELMESİNİ SAĞLAMAK
        speed = Mathf.Min(speed + 1, 50); 
        } 
        //

        
        if(collision.gameObject.tag == "Paddle")
        { 
            ballOlusturmaSayaci++; // SAYAŞ ARTIŞI

            // SKOR GÜNCELLEMESİ
            int currentScore = int.Parse(paddleScript.score.GetComponent<TextMesh>().text);
            currentScore++;
            paddleScript.score.GetComponent<TextMesh>().text = currentScore.ToString();
            //

            // PADDLE RENK DEĞİŞİMİ
            int randomIndex = Random.Range(0, paddleScript.colors.Length);
            paddleScript.sr.color = paddleScript.colors[randomIndex];
            //
        }
        
        // SKİLL TASIYAN BALL'LAR PADDLE'A DEĞDİĞİNDE RENKLERİ VE ETİKETLERİ ORJİNALE DÖNER
        if(collision.gameObject.CompareTag("Paddle") && 
           (gameObject.tag == "FastSkill" || gameObject.tag == "BigSkill"))
        {
            gameObject.tag = "Ball";
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //


    }
    //

    // BALL İLE CARPISAN COLLİSİONLARIN TAGLERİNE ÖZEL ETKİLEŞİMLERİN AYARLANMASI
    void OnTriggerEnter2D(Collider2D other)
    {
        var sr = GetComponent<SpriteRenderer>(); // RENK DEGİSTİRMEK İÇİN KULLANACAGIMIZ SPRİTERENDERER'I ZORUNDA OLMADIGIMIZ HALDE DEGİSKENE ATIYORUZ


        // BALL BİR SKİLL'İ TAŞIMAYA BAŞLARSA O SKİLL'İN TAGİNİ VE ÖZEL RENGİNİ ALIYOR ARDINDAN SKİLL GAMEOBJECT'İNİ YOK EDİYOR
        if (other.CompareTag("Fast"))
        {
            sr.color = new Color(3f/255f, 252f/255f, 252f/255f);
            gameObject.tag = "FastSkill";
            Destroy(other.gameObject); // SKİLL VARLIGINI YOK ET

        }

        if (other.CompareTag("Big"))
        {
            sr.color = new Color(0f/255f, 255f/255f, 127f/255f);
            gameObject.tag = "BigSkill";
            Destroy(other.gameObject); // SKİLL VARLIGINI YOK ET

        }
        //
    }
    //

    // EKRANDA YENİ BALL SPAWN EDEN FONKSİYON
    void CreateBall(){
        GameObject newBall = Instantiate(BallPrefabi, Vector3.zero, Quaternion.identity);
        ballOlusturmaSayaci = 0;
        hayattakiBallSayisi++;
                            UnityEngine.Debug.Log(hayattakiBallSayisi);

    }
    //


   
}


