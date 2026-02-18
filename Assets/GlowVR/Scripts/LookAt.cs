using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

    private GameObject Cam;

	// Use this for initialization
	void Start () {
        Cam = GameObject.Find("Main Camera");

    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Cam.transform);
	}
}
