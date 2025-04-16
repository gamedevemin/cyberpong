using UnityEngine;
using System.Collections;





public class SkillManager : MonoBehaviour
{
    public Paddle paddleObject;
    public GameObject DashActıveUI;
    private bool dashCooldown = false;
    public int dashUse = 2;
    public float dashDistance = 2.0f; 


    // Zaman yavaşlama oranı
    public float slowMotionFactor = 0.2f;
    // Normal hıza dönme oranı
    public float normalSpeed = 1f;

    [Header("Paddle")]
    public GameObject paddle;
    private Rigidbody2D paddleRb;

 
    void Start()
    {
        paddleRb = paddle.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
         // Space tuşuna basıldığında zamanı yavaşlat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = slowMotionFactor;  // Zamanı yavaşlat
        }

        // Space tuşuna basılmadığında zamanı normal hızda tut
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = normalSpeed;  // Zamanı eski haline getir
        }

        if (Input.GetMouseButtonDown(1) && !dashCooldown && dashUse != 0) // Sağ tık
        {
            float dashX = paddleObject.transform.position.x + dashDistance;
            dashX = Mathf.Clamp(dashX, paddleObject.minX, paddleObject.maxX);
            paddleObject.transform.position = new Vector3(dashX, paddleObject.transform.position.y, paddleObject.transform.position.z);
            dashUse--;
            StartCoroutine(DashCooldown());
        }
        else if (Input.GetMouseButtonDown(0) && !dashCooldown && dashUse != 0) // Sol tık
        {
            float dashX = paddleObject.transform.position.x - dashDistance;
            dashX = Mathf.Clamp(dashX, paddleObject.minX, paddleObject.maxX);
            paddleObject.transform.position = new Vector3(dashX, paddleObject.transform.position.y, paddleObject.transform.position.z);
            dashUse--;
            StartCoroutine(DashCooldown());
        }

        if(dashUse == 0) DashActıveUI.GetComponent<TextMesh>().text = "";
        if(dashUse == 1) DashActıveUI.GetComponent<TextMesh>().text = "-";
        if(dashUse == 2) DashActıveUI.GetComponent<TextMesh>().text = "- -";
    }   

    IEnumerator DashCooldown()
    {
        while(dashUse < 2)
        {
            yield return new WaitForSeconds(3);
            dashUse++;
        }
    }
    //

    // Kamera KADRAJI SINIRLARI İÇERİSİNDE rastgele konum.
    Vector3 GetRandomSpawnPosition()
    {
        Camera cam = Camera.main;
        float h = 2f * cam.orthographicSize;
        float w = h * cam.aspect;
        Vector3 c = cam.transform.position;
        return new Vector3(
            Random.Range(c.x - w / 2f, c.x + w / 2f),
            Random.Range(c.y - h / 2f, c.y + h / 2f),
            0f
        );
    }
    //

    
}