using UnityEngine;
using System.Collections;


/* 

EKRANDA SKİLLERİN SPAWN OLMASI İÇİN YAZILAN SINIFTASIN.

*/
public class SkillManager : MonoBehaviour
{
    [Header("Paddle")]
    public GameObject paddle;

    [Header("Skill Spawn")]
    [Tooltip("Skill prefab'ları.")]
    public GameObject[] skillPrefabs;
    [Tooltip("Min bekleme (sn).")]
    public float minSpawnDelay = 3f;

    [Tooltip("Max bekleme (sn).")]
    public float maxSpawnDelay = 6f;
    
    private Rigidbody2D paddleRb;

 
    void Start()
    {
        paddleRb = paddle.GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnSkillObjects());
    }

    // SÜREKLİ SKİLL SPAWN
    IEnumerator SpawnSkillObjects()
    {
        while (true)
        {  
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay); // SPAWN ETMEDEN ÖNCE, SON SPAWNDAN SONRA BEKLENECEK SÜREYİ BELİRLE
            yield return new WaitForSeconds(delay); // 'delay' SÜRE BEKLE CALIŞMA

            Vector3 spawnPos = GetRandomSpawnPosition(); 

            int i = Random.Range(0, skillPrefabs.Length); // RASTGELE SAYİ SEÇ
            GameObject skillInstance = Instantiate(skillPrefabs[i], spawnPos, Quaternion.identity); // RASTGELE BİR SKİLL'İ SPAWN ET
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

    // SKİLLERİN NE İŞE YARAYACAĞI TANIMLANMASI
    public static IEnumerator SkillSpeed()
    {
        Paddle.speed = 30;
        yield return new WaitForSeconds(5);     // bekle
        Paddle.speed = 10;               // Orijinal hıza dön.
    }  
    

    public static IEnumerator SkillBig()
    {
        Vector3 originalScale = Paddle.paddleTransform.localScale;  // Orijinal boyutu sakla.
        Paddle.paddleTransform.localScale = new Vector3(originalScale.x + 1f, originalScale.y, originalScale.z);
        yield return new WaitForSeconds(5);       // bekle
        Paddle.paddleTransform.localScale = originalScale;          // Orijinal boyuta dön.
    } 
    //
}