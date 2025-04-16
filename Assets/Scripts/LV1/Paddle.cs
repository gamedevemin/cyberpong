using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    [Header("Hareket Ayarları")]
    public static int speed = 10;

    [Header("SANAL DUVARLAR")]
    public float minX;
    public float maxX;

    [Header("Skor & Görsel Bileşenler")]
    public GameObject score;
    public SpriteRenderer sr;

    [Header("Transformlar & Objeler")]
    public static Transform paddleTransform;
    public GameObject top; // ÜST DUVAR
    private Vector3 newPaddleScale;

   

    [Header("Paddle Renkleri ve Uzunlugu")]
    public Color[] colors = new Color[]
    {
        new Color(3f/255f, 252f/255f, 252f/255f),
        new Color(201f/255f, 42f/255f, 255f/255f),
        new Color(255f/255f, 26f/255f, 255f/255f),
        new Color(0f/255f, 255f/255f, 127f/255f),
        new Color(0f/255f, 255f/255f, 255f/255f),
        new Color(255f/255f, 147f/255f, 0f/255f),
        new Color(255f/255f, 105f/255f, 180f/255f)
    };

    public int paddleUzunlugu;

    private List<GameObject> currentWallCollisions = new List<GameObject>();

    void Start()
    {
        paddleTransform = GetComponent<Transform>();
        newPaddleScale = paddleTransform.localScale;
        transform.localScale = new Vector3(paddleUzunlugu, 0.07f, 0); // PADDLE BAŞLANGIÇ UZUNLUĞU
        sr = GetComponent<SpriteRenderer>();  // PADDLE'IN SPRİTERENDERER'INI DEGİSKENE ATAMAK
    }

    void Update()
    {
        // MOUSE İLE OYNA
        float mouseP = Input.GetAxis("Mouse X") * speed *  Time.unscaledDeltaTime;
        float newPositionX = transform.position.x + mouseP;
        newPositionX = Mathf.Clamp(newPositionX, minX, maxX);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        //


        if(LevelManager1.hayattakiBallSayisi < 4)
        {
            transform.localScale = new Vector3(paddleUzunlugu, 0.07f, 0);
            minX = -9.84f;
            maxX = 9.84f;
        }

        if(LevelManager1.hayattakiBallSayisi > 3 && LevelManager1.hayattakiBallSayisi < 5)
        {
            transform.localScale = new Vector3(paddleUzunlugu + 2f, 0.07f, 0);
            minX = -10.82f;
            maxX = 10.82f;
        }

        if(LevelManager1.hayattakiBallSayisi > 5 && LevelManager1.hayattakiBallSayisi < 8)
        {
            transform.localScale = new Vector3(paddleUzunlugu + 6f, 0.07f, 0);
            minX = -12.87f;
            maxX = 12.87f;
        }

        // Temasta olan duvarların rengini paddle rengine eşitliyoruz
        foreach (GameObject wall in currentWallCollisions)
        {
            if (wall != null)
            {
                wall.GetComponent<SpriteRenderer>().color = sr.color;
            }
        }

        if(currentWallCollisions.Count == 2)
        {
            currentWallCollisions.Add(top);
            top.tag="Paddle";
        }
        if(currentWallCollisions.Count < 2)
        {
            top.tag="Wall";

        } 
    }


    // PADDLE - DUVAR ETKİLEŞİMİ
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "right" || collision.gameObject.name == "left")
        {
            // Eğer duvar listede yoksa ekleyip
            if (!currentWallCollisions.Contains(collision.gameObject))
            {
                currentWallCollisions.Add(collision.gameObject);
                // //////////
                collision.gameObject.tag = "Paddle";
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "right" || collision.gameObject.name == "left")
        {
            // Duvar temas kesildiğinde, paddle etiketi yerine Wall etiketine geri çevir ve rengini de geri çevir
            if (currentWallCollisions.Contains(collision.gameObject))
            {
                currentWallCollisions.Remove(collision.gameObject);
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                collision.gameObject.tag = "Wall";
            }
            //
        }
    }
}