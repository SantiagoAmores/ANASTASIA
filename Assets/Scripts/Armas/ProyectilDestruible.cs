using UnityEngine;

// Script que añadir a los proyectiles que se destruyan al impactar contra un enemigo
public class ProyectilDestruible : MonoBehaviour
{
    public int golpe;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe(golpe);
            }
            Destroy(this.gameObject);
        }
    }
}
