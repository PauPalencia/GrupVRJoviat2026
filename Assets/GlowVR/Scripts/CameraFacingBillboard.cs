using UnityEngine;
using System.Collections;


//Using for facing object to player

public class CameraFacingBillboard : MonoBehaviour
{
    public GameObject m_Camera;

    void Start()
    {
        m_Camera = GameObject.Find("TargetView");

    }

    void Update()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
}