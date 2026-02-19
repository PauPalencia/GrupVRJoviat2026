using UnityEngine;

public class Tile_test : MonoBehaviour
{
    // ==============================
    // STRUCT INTERNA
    // ==============================
    [System.Serializable]
    public struct TokenID
    {
        public int x;
        public int y;

        public TokenID(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }

    // ==============================
    // VARIABLES
    // ==============================

    [Header("Panel")]
    [SerializeField] private Renderer panelRenderer;

    [Header("Materiales")]
    [SerializeField] private Material materialNormal;
    [SerializeField] private Material materialActivo;

    [Header("ID")]
    public TokenID id;

    // ==============================
    // INICIO
    // ==============================

    private void Start()
    {
        if (panelRenderer == null)
        {
            // Busca autom�ticamente el hijo llamado "panel"
            panelRenderer = transform.Find("panel")?.GetComponent<Renderer>();
        }

        if (panelRenderer != null && materialNormal != null)
        {
            panelRenderer.material = materialNormal;
        }
    }

    // ==============================
    // TRIGGER
    // ==============================

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("m'ha entrat el gameobject amb tag: " + other.tag);
        if (other.CompareTag("Player"))
        {
            if (panelRenderer != null && materialActivo != null)
            {
                panelRenderer.material = materialActivo;
            }

           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panelRenderer != null && materialNormal != null)
            {
                panelRenderer.material = materialNormal;
            }

            Debug.Log("Player SALE del Tile con ID: " + id);
        }
    }
}