using UnityEngine;

/// <summary>
/// Este script gestiona la creación y edición de UN único cubo en VR.
/// Permite:
///  - Crear el cubo
///  - Entrar en modo edición
///  - Escalarlo
///  - Rotarlo
///  - Reposicionarlo
///  - Salir del modo edición
/// Todo se controla mediante los mandos VR (Oculus).
/// </summary>
public class CubeEditManager : MonoBehaviour
{
    // ================================
    // REFERENCIAS A LOS MANDOS VR
    // ================================

    [Header("Referencias de mandos")]
    
    // Mando izquierdo (LTouch)
    // Se usa para:
    //  - Reposicionar el cubo
    //  - Salir del modo edición (botón X)
    public Transform leftController;

    // Mando derecho (RTouch)
    // Se usa para:
    //  - Crear el cubo
    //  - Escalar
    //  - Rotar
    public Transform rightController;

    // ================================
    // PARÁMETROS DE EDICIÓN
    // ================================

    [Header("Parámetros de edición")]

    // Velocidad a la que el cubo cambia de tamaño
    public float scaleSpeed = 0.5f;

    // Velocidad de rotación en grados por segundo
    public float rotationSpeed = 90f;

    // ================================
    // VARIABLES INTERNAS
    // ================================

    // Referencia al cubo actualmente creado
    // Solo puede existir UNO
    private GameObject currentCube;

    // Indica si el usuario está en modo edición
    // true  -> se puede escalar y rotar
    // false -> el cubo queda fijo
    private bool editMode = false;

    // ================================
    // UPDATE PRINCIPAL
    // ================================

    void Update()
    {
        // Gestión del botón A:
        //  - Crear cubo
        //  - Reposicionar cubo
        HandleAButton();

        // Gestión del botón X:
        //  - Salir del modo edición
        HandleExitEdit();

        // Si NO estamos en modo edición
        // o si el cubo no existe todavía,
        // no permitimos ninguna modificación
        if (!editMode || currentCube == null)
            return;

        // Escalado mediante joystick derecho
        HandleScaling();

        // Rotación mediante joystick izquierdo
        HandleRotation();
    }

    // =====================================================
    // BOTÓN A (RTouch)
    // =====================================================
    // Comportamiento:
    //  - Si no hay cubo → lo crea y entra en edición
    //  - Si ya hay cubo → lo reposiciona
    // =====================================================
    void HandleAButton()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            // Si todavía no existe ningún cubo
            if (currentCube == null)
            {
                CreateCube();
                editMode = true;

                Debug.Log("🟢 Cubo creado y modo edición activado");
            }
            // Si el cubo ya existe
            else {
                if (editMode) {
                    RepositionCube();
                    Debug.Log("📍 Cubo reposicionado");
                }
            }
        }
    }

    // =====================================================
    // CREACIÓN DEL CUBO
    // =====================================================
    void CreateCube()
    {
        // Se crea un cubo primitivo de Unity
        currentCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Se coloca delante del mando derecho
        // para que el usuario lo vea claramente al crearlo
        currentCube.transform.position =
            rightController.position + rightController.forward * 0.3f;

        // Escala inicial pequeña para VR
        currentCube.transform.localScale = Vector3.one * 0.1f;
    }

    // =====================================================
    // REPOSICIONAR CUBO
    // =====================================================
    void RepositionCube()
    {
        
        // El cubo se mueve exactamente a la posición
        // del mando izquierdo
        currentCube.transform.position = leftController.position;
    }

    // =====================================================
    // BOTÓN X (LTouch) → SALIR DE EDICIÓN
    // =====================================================
    void HandleExitEdit()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            // Se desactiva el modo edición
            // El cubo queda fijo en su estado actual
            if (editMode == false)
            {
                editMode = true;
                Debug.Log("🟢 Modo edición activado");
            }
            else
            {
                editMode = false;
                Debug.Log("🔴 Modo edición desactivado");
            }
        }
    }

    // =====================================================
    // ESCALADO DEL CUBO
    // =====================================================
    // Joystick derecho:
    //  - Arriba    → aumenta tamaño
    //  - Abajo     → reduce tamaño
    // =====================================================
    void HandleScaling()
    {
        if (editMode)
        {
            // Se lee el joystick derecho
            Vector2 scaleInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

            // Se ignora el ruido del joystick
            if (Mathf.Abs(scaleInput.y) > 0.1f)
            {
                // Cantidad de escala aplicada este frame
                float scaleAmount = scaleInput.y * scaleSpeed * Time.deltaTime;

                // Se escala de forma uniforme en los tres ejes
                currentCube.transform.localScale += Vector3.one * scaleAmount;
            }
        }
        
    }

    // =====================================================
    // ROTACIÓN DEL CUBO
    // =====================================================
    // Joystick izquierdo:
    //  - Horizontal    → rotación Y (Yaw)
    //  - Vertical      → rotación X (Pitch)
    // =====================================================
    void HandleRotation()
    {
        if (editMode)
        {
            // Se lee el joystick izquierdo
            Vector2 rotateInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            // Zona muerta para evitar vibraciones
            if (rotateInput.magnitude < 0.2f)
                return;

            // Se compara qué eje se está usando más
            float absX = Mathf.Abs(rotateInput.x);
            float absY = Mathf.Abs(rotateInput.y);

            // Rotación horizontal (izquierda / derecha)
            if (absX > absY)
            {
                currentCube.transform.Rotate(
                    Vector3.up,
                    rotateInput.x * rotationSpeed * Time.deltaTime,
                    Space.World
                );
            }
            // Rotación vertical (arriba / abajo)
            else
            {
                currentCube.transform.Rotate(
                    Vector3.right,
                    -rotateInput.y * rotationSpeed * Time.deltaTime,
                    Space.World
                );
            }
        }
    }
}
