using System.Collections.Generic;
using UnityEngine;

public class GameManagerSecuencia : MonoBehaviour
{
    public static GameManagerSecuencia Instance;

    [Header("Secuencia correcta")]
    [SerializeField] private List<int> secuenciaCorrecta = new List<int> { 2, 7, 10, 15 };

    private int indiceActual = 0;
    private bool secuenciaTerminada = false;

    private Tile_test[] todasLasBaldosas;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        todasLasBaldosas = FindObjectsOfType<Tile_test>();
    }

    public void RegistrarPaso(Tile_test baldosa, int numero)
    {
        if (secuenciaTerminada)
            return;

        if (numero == secuenciaCorrecta[indiceActual])
        {
            indiceActual++;

            if (indiceActual >= secuenciaCorrecta.Count)
            {
                Victoria();
            }
        }
        else
        {
            Derrota();
        }
    }

    private void Victoria()
    {
        secuenciaTerminada = true;

        foreach (var baldosa in todasLasBaldosas)
            baldosa.PonerVerde();

        Debug.Log("SECUENCIA COMPLETADA");
    }

    private void Derrota()
    {
        secuenciaTerminada = true;

        foreach (var baldosa in todasLasBaldosas)
            baldosa.PonerRojo();

        Debug.Log("SECUENCIA INCORRECTA");
    }
}