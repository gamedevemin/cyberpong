using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEngine.Rendering;
public class Ball : MonoBehaviour
{
    [SerializeField]

    [Header("Topun Hareket Ayarları")]
    private int speed; // BALL HIZI
    public int maxSpeed = 20;
    private Vector2 direction; // YÖN
    public Rigidbody2D rb;

    [Header("Top Yönetimi")]
    public GameObject BallPrefabi;
    public static int ballOlusturmaSayaci;

    [Header("Paddle Etkileşimi")]
    private Paddle paddleScript;


    void Start()
    {
        speed = 6; // VARSAYILAN HIZ
        rb = GetComponent<Rigidbody2D>(); // RB2D
        rb.freezeRotation = true; // BALL'IN ROTASYON DEĞERLERİ DONDURULUR BU SAYEDE X,Y,Z YÖNLERİNE SAVRULMAZ CARPISMALARDA

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
            LevelManager1.hayattakiBallSayisi--;
            Destroy(gameObject);
            
        } 
        //
        if (ballOlusturmaSayaci >= 4) CreateBall(); // YENİ BALL OLUŞMASI
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
        
        // PADDLE İLE ETKİLEŞİM
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
        //

    }
    //

    // EKRANDA YENİ BALL SPAWN EDEN FONKSİYON
    void CreateBall(){
    
        GameObject newBall = Instantiate(BallPrefabi, Vector3.zero, Quaternion.identity);
        ballOlusturmaSayaci = 0;
        LevelManager1.hayattakiBallSayisi++;
    }
    //

   
   
}


