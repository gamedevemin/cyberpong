using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PaddleIntro : MonoBehaviour
{
     [SerializeField]
    [Header("Hareket Ayarları")]
    public static int speed = 10; // PADDLE HIZI

    [Header("SANAL DUVARLAR")]
    public float minX; // SANAL SOL DUVAR
    public float maxX; // SANAL SAG DUVAR

    [Header("Transform")]
    public static Transform paddleTransform; 
    private Vector3 newPaddleScale;
    public int paddleUzunlugu;

    void Start()
    {
        paddleTransform = GetComponent<Transform>(); // VARSAYILAN PADDLE TRANSFORMU
        newPaddleScale = paddleTransform.localScale; // VARSAYILAN PADDLE UZUNLUGU
        transform.localScale = new Vector3(paddleUzunlugu, 0.07f, 0); // YENİ PADDLE  UZUNLUĞU
    }

    void Update()
    {
        // MOUSE İLE OYNA
        float mouseP = Input.GetAxis("Mouse X") * speed *  Time.unscaledDeltaTime;
        float newPositionX = transform.position.x + mouseP;
        newPositionX = Mathf.Clamp(newPositionX, minX, maxX);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        //
    }
}