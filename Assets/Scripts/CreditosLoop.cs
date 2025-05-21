using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditosLoop : MonoBehaviour
{ 
    public RectTransform creditsText; // El texto que se mueve
 
    public float resetY = -500f; // Y inicial (abajo del todo)
    public float endY = 500f;    // Y final (cuando desaparece por arriba)

    private float currentSpeed;

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    void Start()
    {
        creditsText.anchoredPosition = new Vector2(0, resetY);
    }

    void Update()
    {
        creditsText.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;

        if (creditsText.anchoredPosition.y >= endY)
        {
            creditsText.anchoredPosition = new Vector2(0, resetY);
        }
    }
}
