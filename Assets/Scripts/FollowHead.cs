using UnityEngine;

public class FollowHead : MonoBehaviour
{
    [Header("References")]
    public Transform head; // CenterEyeAnchor del OVR Rig

    [Header("Settings")]
    public float distance = 1.5f;
    public float heightOffset = -0.1f;
    public float followSpeed = 5f;
    public bool followRotation = true;

    void Start()
    {
        // Si no asignas la cabeza, intenta encontrarla automáticamente
        if (head == null)
        {
            Camera cam = Camera.main;
            if (cam != null)
                head = cam.transform;
        }
    }

    void LateUpdate()
    {
        if (head == null) return;

        // Posición objetivo frente a la cabeza
        Vector3 forward = head.forward;
        forward.y = 0; // evita inclinación vertical
        forward.Normalize();

        Vector3 targetPosition =
            head.position +
            forward * distance +
            Vector3.up * heightOffset;

        // Suavizado
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * followSpeed
        );

        // Rotación hacia la cabeza
        if (followRotation)
        {
            Vector3 lookDirection = transform.position - head.position;
            lookDirection.y = 0;

            Quaternion targetRotation =
                Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * followSpeed
            );
        }
    }
}