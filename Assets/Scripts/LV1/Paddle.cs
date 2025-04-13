using UnityEngine;
using System.Collections.Generic;

public class Paddle : MonoBehaviour
{
    [SerializeField]

    [Header("Hareket Ayarları")]
    public static int speed = 10;

    [Header("SANAL DUVARLAR")]
    public float minX = -7.5f; // PADDLE'A SANAL SOL DUVAR-ENGEL
    public float maxX = 7.5f; // PADDLE'A SANAL SAĞ DUVAR-ENGEL

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


    private List<GameObject> currentWallCollisions = new List<GameObject>();

    void Start()
    {
        paddleTransform = GetComponent<Transform>();
        newPaddleScale = paddleTransform.localScale;
        transform.localScale = new Vector3(500, 5, 0); // PADDLE BAŞLANGIÇ UZUNLUĞU
        sr = GetComponent<SpriteRenderer>();  // PADDLE'IN SPRİTERENDERER'INI DEGİSKENE ATAMAK
    }

    void Update()
    {
        // MOUSE İLE OYNA
        float mouseP = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float newPositionX = transform.position.x + mouseP;
        newPositionX = Mathf.Clamp(newPositionX, minX, maxX);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        //

        // PADDLE BOYUTUNUN DEĞİŞKENLİĞİ
        /* if(Ball.hayattakiBallSayisi > 3 && Ball.hayattakiBallSayisi < 6)
        {
            newPaddleScale.x = 700;
        } */
        //

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

    // PADDLE COLLİSİYONLARI
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
            // Duvar temas kesildiğinde, liste üzerinden çıkar ve rengi resetle (örneğin beyaz)
            if (currentWallCollisions.Contains(collision.gameObject))
            {
                currentWallCollisions.Remove(collision.gameObject);
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                collision.gameObject.tag = "Wall";
            }
        }
    }
}