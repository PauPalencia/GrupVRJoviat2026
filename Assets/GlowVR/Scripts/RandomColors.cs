using UnityEngine;
using System.Collections;

//Script using for Sphere prefab

public class RandomColors : MonoBehaviour {

    public float TimeSet,RandTime;
    private bool invertColor = false;
    private Renderer Mtl;
    public Color color_start, color_end, Result;
    public float duration = 2.0F;
    //private bool activeColor = false;

    // Use this for initialization
    void Start () {

        RandTime = Random.Range(5, 10);
        Mtl = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        TimeSet += Time.deltaTime;

        
        if (TimeSet>= RandTime)
        {
            if (!invertColor)
            {
                GetComponent<Collider>().enabled = true;
                Mtl.material.SetColor("_Color", color_end);
                //activeColor = true;

            }
            if (invertColor)
            {
                GetComponent<Collider>().enabled = false;
                Mtl.material.SetColor("_Color", color_start);
                //activeColor = false;

            }

            invertColor = !invertColor;
            TimeSet = 0;
        }


    }
}
