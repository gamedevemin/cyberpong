using UnityEngine;
using System.Collections.Generic;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    public static int speed = 10;
    public float minX = -7.5f;
    public float maxX = 7.5f;
    public GameObject score;
    public  SpriteRenderer sr;
    public static Transform paddleTransform;
    public int targetScaleX = 2000; // Ulaşmak istediğin X ölçeği
    public float growSpeed = 0.001f;    // Ne kadar hızlı uzasın?
    public GameObject top;
    public GameObject finalScreen;
    private bool sceneTransitionInitiated = false;
    public float transitionTimer = 0f; // Geçişin başlaması için gecikme sayacı
    public float fadeSpeed = 5f;
    public GameObject transitionBackground;
    public  Color[] colors = new Color[]
    {
        new Color(3f/255f, 252f/255f, 252f/255f),
        new Color(201f/255f, 42f/255f, 255f/255f),
        new Color(255f/255f, 26f/255f, 255f/255f),
        new Color(0f/255f, 255f/255f, 127f/255f),
        new Color(0f/255f, 255f/255f, 255f/255f),
        new Color(255f/255f, 147f/255f, 0f/255f),
        new Color(255f/255f, 105f/255f, 180f/255f)
    };

    // Duvarlarla olan temasları tutmak için liste:
    public List<GameObject> currentWallCollisions = new List<GameObject>();

    void Start()
    {
        paddleTransform = GetComponent<Transform>();
        transform.localScale = new Vector3(500, 5, 0);
        sr = GetComponent<SpriteRenderer>();  // SpriteRenderer'ı başlangıçta atayın
        transitionBackground.SetActive(false);

        
    }

    void Update()
    {
        // MOUSE İLE OYNA
        float mouseP = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float newPositionX = transform.position.x + mouseP;
        newPositionX = Mathf.Clamp(newPositionX, minX, maxX);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

        // PADDLE BOYUTUNUN DEĞİŞİMİ VE TANIMLANMASI
        
        Vector3 paddleScale = transform.localScale;
        if (paddleScale.x < 2000)
        {
            paddleScale.x += growSpeed * Time.deltaTime;
            transform.localScale = paddleScale;
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


        if (Ball.hayattakiBallSayisi >= 6)
        {
            // finalScreen'in SpriteRenderer'ını al
            SpriteRenderer fsr = finalScreen.GetComponent<SpriteRenderer>();
            // Mevcut rengi kopyala ve alpha'yı arttır
            Color col = fsr.color;
            col.a += fadeSpeed * Time.deltaTime;
            fsr.color = col;

            // BURADA: Geçiş tetiklenmeden önce 4 saniye bekle
            if (!sceneTransitionInitiated)
            {
                transitionTimer += Time.deltaTime;
                if (transitionTimer >= 2f)
                {
                    sceneTransitionInitiated = true;
                    // SceneLoader'ı çağır: Sahne geçişi başlatılır.

                    FindObjectOfType<SceneLoader>()?.TransitionScene();
                    transitionBackground.SetActive(true);

                }
            }
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