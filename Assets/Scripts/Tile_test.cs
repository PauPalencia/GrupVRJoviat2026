using UnityEngine;

public class Tile_test : MonoBehaviour
{
    [Header("ID de la baldosa")]
    [SerializeField] private int tileID;

    [Header("Panel")]
    [SerializeField] private Renderer panelRenderer;

    [Header("Materiales")]
    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialVerde;
    [SerializeField] private Material materialRojo;

    private void Start()
    {
        if (panelRenderer == null)
            panelRenderer = transform.Find("panel")?.GetComponent<Renderer>();

        ResetearColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pies"))
        {
            GameManagerSecuencia.Instance.RegistrarPaso(this, tileID);
        }
    }

    public void PonerVerde()
    {
        if (panelRenderer != null && materialVerde != null)
            panelRenderer.material = materialVerde;
    }

    public void PonerRojo()
    {
        if (panelRenderer != null && materialRojo != null)
            panelRenderer.material = materialRojo;
    }

    public void ResetearColor()
    {
        if (panelRenderer != null && materialNormal != null)
            panelRenderer.material = materialNormal;
    }
}