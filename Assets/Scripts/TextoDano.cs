using UnityEngine;

public class TextoDano : MonoBehaviour
{
    public float tiempoDesaparicion = 1f;

    private void Start()
    {
        Destroy(gameObject, tiempoDesaparicion);
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
