using UnityEngine;
// Inflingir daño con el lapiz
public class Arma2_Adicional : MonoBehaviour
{
    public int golpe;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.RecibirGolpe(golpe, this.gameObject);
            }
        }
    }
}
