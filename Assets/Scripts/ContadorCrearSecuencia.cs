using UnityEngine;

public class ContadorCrearSecuencia : MonoBehaviour
{
    [Header("Número de esta placa")]
    public int plateNumber;

    private bool alreadyTriggered = false;
    private Renderer plateRenderer;

    private void Awake()
    {
        plateRenderer = GetComponentInChildren<Renderer>();    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyTriggered) return;

        if (other.CompareTag("Pies"))
        {
            alreadyTriggered = true;

            if (ContadorSecuencia.Instance != null)
            {
                ContadorSecuencia.Instance.RegisterNumber(plateNumber);
            }
            else
            {
                Debug.LogError("ContadorSecuencia no encontrado en la escena.");
            }
        }
    }

    public void SetGameOverColor()
    {
        if (plateRenderer != null)
        {
            plateRenderer.material.color = Color.red;
        }
    }
}