using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ballPrefab;

    [Header("Ball Management")]
    private int activeBallCount = 0;

    // Ball oluştur
    public void CreateBall()
    {
        GameObject newBall = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
        activeBallCount++;
    }

    // Ball yok olunca çağır
    public void BallDestroyed()
    {
        activeBallCount = Mathf.Max(0, activeBallCount - 1); // Asla negatif olmasın
    }

    // Gerekirse dışarıdan ulaş
    public int GetActiveBallCount() => activeBallCount;
}
