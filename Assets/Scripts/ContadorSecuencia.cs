using System.Collections.Generic;
using UnityEngine;

public class ContadorSecuencia : MonoBehaviour
{
    [Header("Secuencia objetivo (configurable en Inspector)")]
    [SerializeField] private List<int> secuenciaObjetivo = new List<int> { 1, 2, 3, 4 };

    [Header("Acción al completar secuencia")]
    [SerializeField] private Color colorAlCompletar = Color.green;

    [Header("Depuración")]
    [SerializeField] private bool mostrarLogs = true;

    private readonly List<int> secuenciaLeida = new List<int>();
    private int indiceEsperado;

    public IReadOnlyList<int> SecuenciaLeida => secuenciaLeida;
    public bool SecuenciaCompletada { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        RevisarEntrada(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        RevisarEntrada(collision.gameObject);
    }

    private void RevisarEntrada(GameObject objeto)
    {
        if (SecuenciaCompletada)
        {
            return;
        }

        if (!TokenSequenceUtils.TryGetTokenNumber(objeto, out int numero))
        {
            return;
        }

        secuenciaLeida.Add(numero);

        if (secuenciaObjetivo == null || secuenciaObjetivo.Count == 0)
        {
            return;
        }

        if (numero == secuenciaObjetivo[indiceEsperado])
        {
            indiceEsperado++;

            if (mostrarLogs)
            {
                Debug.Log($"[ContadorSecuencia] Correcto: {numero}. Progreso {indiceEsperado}/{secuenciaObjetivo.Count}.");
            }

            if (indiceEsperado >= secuenciaObjetivo.Count)
            {
                SecuenciaCompletada = true;
                int tokensColoreados = TokenSequenceUtils.PaintAllTokens(colorAlCompletar);

                if (mostrarLogs)
                {
                    Debug.Log($"[ContadorSecuencia] Secuencia completada. Renderers coloreados: {tokensColoreados}.");
                }
            }

            return;
        }

        indiceEsperado = numero == secuenciaObjetivo[0] ? 1 : 0;

        if (mostrarLogs)
        {
            Debug.Log($"[ContadorSecuencia] Número {numero} fuera de secuencia. Reinicio de progreso a {indiceEsperado}/{secuenciaObjetivo.Count}.");
        }
    }
}
