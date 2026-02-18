using UnityEngine;
using System.Collections;

//Rotating any objects

public class RotateObject : MonoBehaviour {

    public bool rot_x,rot_y,rot_z;
    public float speed = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (rot_y)
        transform.Rotate(0, speed * Time.deltaTime, 0);
        if (rot_x)
            transform.Rotate(speed * Time.deltaTime, 0, 0);
        if (rot_z)
            transform.Rotate(0, 0, speed * Time.deltaTime);

    }
}
