using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalRegreso : MonoBehaviour
{
    public GameObject menuRegreso;

    // Start is called before the first frame update
    void Start()
    {
        menuRegreso.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            menuRegreso.SetActive(true);
            Time.timeScale = 0f;
            
        }
    }

    public void BotonSi()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicio");
    }

    public void BotonNo()
    {
       menuRegreso.SetActive(false);
       Time.timeScale = 1f;
        
    }
}
