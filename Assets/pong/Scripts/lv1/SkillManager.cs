using UnityEngine;
using System.Collections;


/* 

EKRANDA SKİLLERİN SPAWN OLMASI İÇİN YAZILAN SINIFTASIN.

*/
public class SkillManager : MonoBehaviour
{
    // Paddle obj.
    [Header("Paddle Settings")]
    [Tooltip("Skill uygulanacak Paddle.")]
    public GameObject paddle;

    // Spawn ayarları.
    [Header("Skill Spawn Settings")]
    [Tooltip("Skill prefab'ları.")]
    public GameObject[] skillPrefabs;
    [Tooltip("Min bekleme (sn).")]
    public float minSpawnDelay = 3f;
    [Tooltip("Görsel prefab.")]
    public GameObject visualEffectPrefab;
    [Tooltip("Max bekleme (sn).")]
    public float maxSpawnDelay = 6f;
    
    private Rigidbody2D paddleRb; // Rb cache.

 
    void Start()
    {
        paddleRb = paddle.GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnSkillObjects());
    }


    void Update() { }

    // SKİLL SPAWN
    IEnumerator SpawnSkillObjects()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay); // 'delay' KADAR SÜRE CALISMAYI DURDUR

            Vector3 spawnPos = GetRandomSpawnPosition(); 

            // Rastgele skill SPAWN ET
            int i = Random.Range(0, skillPrefabs.Length);
            GameObject skillInstance = Instantiate(skillPrefabs[i], spawnPos, Quaternion.identity);
            //

            // SKİLL GÖRSELİ İLE GÖZÜKECEK GÖRSELİ SPAWN ET
            if (visualEffectPrefab != null)
            {
                GameObject visualInstance = Instantiate(visualEffectPrefab, Vector3.zero, Quaternion.identity, skillInstance.transform);
                visualInstance.transform.localPosition = new Vector3(0.6f, 0.3f, 0f);
            }
            //
        }
    }
    //

    // Kamera bazlı rastgele konum.
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
        yield return new WaitForSeconds(5);     // Efekt süresince bekle.
        Paddle.speed = 10;               // Orijinal hıza dön.
    }  
    

    public static IEnumerator SkillBig()
    {
        Vector3 originalScale = Paddle.paddleTransform.localScale;  // Orijinal boyutu sakla.
        Paddle.paddleTransform.localScale = new Vector3(originalScale.x + 1f, originalScale.y, originalScale.z);
        yield return new WaitForSeconds(5);       // Efekt süresince bekle.
        Paddle.paddleTransform.localScale = originalScale;          // Orijinal boyuta dön.
    } 
    //
}