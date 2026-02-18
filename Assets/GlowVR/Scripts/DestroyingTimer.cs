using UnityEngine;
using System.Collections;

//Advanced destroying

public class DestroyingTimer : MonoBehaviour {

    private float Time_to_destroy;
    public float Seconds;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Time_to_destroy += Time.deltaTime;

        if (Time_to_destroy >= Seconds)
            DestroyImmediate(gameObject);

    }
}
