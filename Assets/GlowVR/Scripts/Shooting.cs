using UnityEngine;
using System.Collections;

//Script for shooting 

public class Shooting : MonoBehaviour {

    public Rigidbody Bullet;
    public Transform Spawner;
    public float queueTime;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            queueTime += Time.deltaTime;
            if (queueTime >= 0.1f)
            {
                Rigidbody Bul;
                Bul = Instantiate(Bullet, Spawner.transform.position, Spawner.transform.rotation) as Rigidbody;
                Bul.velocity = transform.TransformDirection(Vector3.forward * 1000);
                queueTime = 0;
            }
        }
            
	}
}
