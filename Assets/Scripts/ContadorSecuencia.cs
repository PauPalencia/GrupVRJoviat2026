using System.Collections.Generic;
using UnityEngine;

public class ContadorSecuencia : MonoBehaviour
{
    [Header("Secuencia correcta")]
    [SerializeField] private List<int> secuenciaObjetivo = new List<int> { 2, 7, 10, 15 };

    [Header("Colores")]
    [SerializeField] private Color colorCorrecto = Color.green;
    [SerializeField] private Color colorIncorrecto = Color.red;

    private int indiceActual = 0;
    private bool secuenciaTerminada = false;

    private void OnTriggerEnter(Collider other)
    {
        if (secuenciaTerminada) return;

        if (!TokenSequenceUtils.TryGetTokenNumber(other.gameObject, out int numero))
            return;

        Debug.Log("Pisado: " + numero);

        // Si es el número correcto
        if (numero == secuenciaObjetivo[indiceActual])
        {
            indiceActual++;

            if (indiceActual >= secuenciaObjetivo.Count)
            {
                secuenciaTerminada = true;
                TokenSequenceUtils.PaintAllTokens(colorCorrecto);
                Debug.Log("SECUENCIA COMPLETADA");
            }
        }
        else
        {
            secuenciaTerminada = true;
            TokenSequenceUtils.PaintAllTokens(colorIncorrecto);
            Debug.Log("SECUENCIA INCORRECTA");
        }
    }
}