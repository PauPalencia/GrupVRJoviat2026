using UnityEngine;
using System.Collections;

//Destroying objects with time

public class Destroying : MonoBehaviour {

    public float TimeDestroy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TimeDestroy += Time.deltaTime;
        if (TimeDestroy >= 3) Destroy(gameObject);

    }
}
