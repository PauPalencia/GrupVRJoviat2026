using UnityEngine;

public class CrearSecuencia : MonoBehaviour
{
    [Header("Número del token (suelo)")]
    [SerializeField, Range(1, 16)] private int numeroToken = 1;

    // Secuencia objetivo configurable directamente en código.
    // Ajusta estos valores según la demo que quieras validar.
    public static readonly int[] SecuenciaObjetivo = { 1, 2, 3, 4 };

    public int NumeroToken => numeroToken;

    private void OnValidate()
    {
        numeroToken = Mathf.Clamp(numeroToken, 1, 16);
    }
}
