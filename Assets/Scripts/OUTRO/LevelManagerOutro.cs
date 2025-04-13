using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelManagerLast : MonoBehaviour
{
    [SerializeField] private float delayBeforeQuit = 10f; // Oyun kapama sayacı
    void Start()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(delayBeforeQuit);
        Application.Quit();
    }

}
