using UnityEngine;
using System;

/// <summary>
/// Este script monitoriza la conectividad de los dispositivos VR:
///  - Gafas (HMD)
///  - Mando izquierdo
///  - Mando derecho
///
/// Cuando se pierde o recupera la conexión,
/// se escribe un log con:
///  - Dispositivo afectado
///  - Estado (perdido / recuperado)
///  - Marca de tiempo
/// </summary>
public class VRConnectivityLogger : MonoBehaviour
{
    // ================================
    // ESTADOS PREVIOS (para evitar spam)
    // ================================

    private bool leftControllerConnected;
    private bool rightControllerConnected;
    private bool hmdPresent;

    // ================================
    // INICIALIZACIÓN
    // ================================

    void Start()
    {
        // Guardamos el estado inicial de cada dispositivo
        leftControllerConnected  = OVRInput.IsControllerConnected(OVRInput.Controller.LTouch);
        rightControllerConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTouch);
        hmdPresent               = OVRManager.isHmdPresent;

        // Log inicial (opcional pero útil)
        LogStatus("Sistema VR iniciado");
    }

    // ================================
    // UPDATE
    // ================================

    void Update()
    {
        CheckLeftController();
        CheckRightController();
        CheckHMD();
    }

    // ================================
    // COMPROBACIÓN MANDO IZQUIERDO
    // ================================

    void CheckLeftController()
    {
        bool isConnected = OVRInput.IsControllerConnected(OVRInput.Controller.LTouch);

        // Si el estado ha cambiado desde el último frame
        if (isConnected != leftControllerConnected)
        {
            if (!isConnected)
                LogStatus("❌ Mando IZQUIERDO desconectado");
            else
                LogStatus("✅ Mando IZQUIERDO reconectado");

            leftControllerConnected = isConnected;
        }
    }

    // ================================
    // COMPROBACIÓN MANDO DERECHO
    // ================================

    void CheckRightController()
    {
        bool isConnected = OVRInput.IsControllerConnected(OVRInput.Controller.RTouch);

        if (isConnected != rightControllerConnected)
        {
            if (!isConnected)
                LogStatus("❌ Mando DERECHO desconectado");
            else
                LogStatus("✅ Mando DERECHO reconectado");

            rightControllerConnected = isConnected;
        }
    }

    // ================================
    // COMPROBACIÓN HMD (GAFAS)
    // ================================

    void CheckHMD()
    {
        bool isPresent = OVRManager.isHmdPresent;

        if (isPresent != hmdPresent)
        {
            if (!isPresent)
                LogStatus("❌ HMD / GAFAS no detectadas");
            else
                LogStatus("✅ HMD / GAFAS detectadas");

            hmdPresent = isPresent;
        }
    }

    // ================================
    // FUNCIÓN DE LOG CON TIMESTAMP
    // ================================

    void LogStatus(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        Debug.Log($"[{timestamp}] {message}");
    }
}
