using UnityEngine;

public class lv2Starter : MonoBehaviour
{
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        if (audioSource != null && startClip != null)
        {
            audioSource.clip = startClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Ses çalmak için gerekli bileşenler eksik!");
        }
    }
}
