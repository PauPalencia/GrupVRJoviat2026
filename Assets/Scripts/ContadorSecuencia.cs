using UnityEngine;

public class ContadorSecuencia : MonoBehaviour
{
    public static ContadorSecuencia Instance;

    private bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterNumber(int number)
    {
        if (gameOver) return;

        Debug.Log("Número tocado: " + number);

        if (number > 100)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        gameOver = true;

        Debug.Log("GAME OVER - Número mayor a 100");

        // Buscar todas las placas en la escena
        ContadorCrearSecuencia[] plates = FindObjectsOfType<ContadorCrearSecuencia>();

        foreach (ContadorCrearSecuencia plate in plates)
        {
            plate.SetGameOverColor();
        }
    }
}