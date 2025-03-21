using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    //Cuenta atras
    public TextMeshProUGUI cuentaAtras;
    public float startTime = 120f;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = startTime;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            cuentaAtras.text = timeLeft.ToString("0");
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        cuentaAtras.text = "Te has quedado sin tiempo :(";
    }
}
