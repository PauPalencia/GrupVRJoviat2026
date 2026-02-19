using UnityEngine;

public class ContadorCrearSecuencia : MonoBehaviour
{
    [Header("Número de esta placa")]
    public int plateNumber;

    private bool alreadyTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Player"))
        {
            alreadyTriggered = true;

            ContadorSeciencaia gm = ContadorSeciencaia.Instance;

            if (gm != null)
            {
                gm.RegisterNumber(plateNumber);
            }
            else
            {
                Debug.LogError("GameManager no encontrado en la escena.");
            }
        }
    }
}