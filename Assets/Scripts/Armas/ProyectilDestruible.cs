using UnityEngine;

// Script que añadir a los proyectiles que se destruyan al impactar contra un enemigo
public class ProyectilDestruible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
