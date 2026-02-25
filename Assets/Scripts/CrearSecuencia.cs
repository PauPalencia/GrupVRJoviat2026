using UnityEngine;

public class CrearSecuencia : MonoBehaviour
{
    [Header("Número del token (suelo)")]
    [SerializeField, Range(1, 16)] private int numeroToken = 1;

    public int NumeroToken => numeroToken;

    private void OnValidate()
    {
        numeroToken = Mathf.Clamp(numeroToken, 1, 16);
    }
}
