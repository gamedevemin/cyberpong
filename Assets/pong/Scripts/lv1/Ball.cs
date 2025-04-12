using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
public class Ball : MonoBehaviour
{
    [SerializeField]

    public int speed = 10; // BALL HIZ
    public Vector2 direction; /// Topun hareket yönünü belirler (başlangıçta rastgele atanır, çarpışmada yansıtılır)
    public Rigidbody2D rb; 
    private static int ballOlusturmaSayaci; // PADDLE İLE BALL CARPISTIKCA ARTAN VE BİR DEĞERE ULASTIGINDA YENİ BALL OLUSTURAN DEGİSKEN
    public GameObject BallPrefabi; // BALL PREFABI
    public static int hayattakiBallSayisi; // EKRANDAKİ BALL SAYISI
    private Paddle paddleScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // NESNENİN FİZİKSEL OLARAK BİR HAREKETİ KISITLANIR

        // BALL'A YÖN VE HIZ ATAMALARI
        transform.position = Vector3.zero; // Ball'ı sıfır noktasına atar
        direction = Random.insideUnitCircle.normalized; // Rastgele bir yönü -birim vektör- bir değişkene atamak
        rb.linearVelocity = direction * speed; // Ball'a yön ve o yöndeki hızı tanımlanır
        //

        GameObject paddleObject = GameObject.Find("Paddle");
        if(paddleObject != null)
        {
            paddleScript = paddleObject.GetComponent<Paddle>();
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

        // YENİ BALL OLUŞMASI
        if (ballOlusturmaSayaci == 4)
        {
            UnityEngine.Debug.Log("BOS: "+ballOlusturmaSayaci);
            UnityEngine.Debug.Log("HBS:"+ hayattakiBallSayisi);
            CreateBall();
            UnityEngine.Debug.Log("BOS: "+ballOlusturmaSayaci);
            UnityEngine.Debug.Log("HBS:"+ hayattakiBallSayisi);


            
           
        }
        //
    }

    // BALL COLLİSİON İLE COLLİSİONLARIN ETKİLEŞİMİ
    void OnCollisionEnter2D(Collision2D collision)
    {
        // BALL'IN SEKMESİ
        if(collision.gameObject.tag == "Paddle" || collision.gameObject.tag == "Wall")
        {
        direction = Vector2.Reflect(direction, collision.contacts[0].normal); // BALL'IN TERS YÖNÜNÜ DEĞİŞKENE ATAMAK
        rb.linearVelocity = direction * speed; // BALL'IN TERS YÖNE YÖNELMESİNİ SAĞLAMAK
        speed++;} // BALL'I HIZLANDIRMAK

        // EKRANA BALL EKLEMEK İÇİN PADDLE' İLE BALL'IN KAÇ KEZ CARPISTIGINI KONTROL ETMEK
        if(collision.gameObject.tag == "Paddle")
        { 
            ballOlusturmaSayaci++;
            // SKOR ARTIŞI
            int currentScore = int.Parse(paddleScript.score.GetComponent<TextMesh>().text);
            currentScore++;
            paddleScript.score.GetComponent<TextMesh>().text = currentScore.ToString();

            // PADDLE RENK DEĞİŞİMİ
            int randomIndex = Random.Range(0, paddleScript.colors.Length);
            paddleScript.sr.color = paddleScript.colors[randomIndex];
        }
        //
        
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


        // BALL BİR SKİLL'İ TAŞIMAYA BAŞLARSA O SKİLL'İN TAGİNİ VE RENGİNİ ALIYOR ARDINDAN SKİLL VARLIGINI YOK EDİYOR
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

    // EKRANDA YENİ BALL SPAWN EDEN FONKSİYON
    void CreateBall(){
        GameObject newBall = Instantiate(BallPrefabi, Vector3.zero, Quaternion.identity);
        ballOlusturmaSayaci = 0;
        hayattakiBallSayisi++;
    }
    //


   
}


