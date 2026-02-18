using UnityEngine;
using System.Collections;

public class AnimationSpeed : MonoBehaviour {

    //This script control tentacles of big jellyfish like a world reaction by sound
     
    public string AnimString; //Name of animation for play
    public bool Action=false;
    public bool Move = false;
    public float Speed=0; //Sets in inspector

    // Use this for initialization
    void Start () {

        GetComponent < Animation >()[AnimString].speed = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
	
        if (Action)
        {
            Speed = 5;
            GetComponent<Animation>()[AnimString].speed = Speed;
            GetComponent<Animation>().CrossFade("Action");
            Move = true;
            Action = false;
        }

        if (Move)
        {
            Speed -= 2*Time.deltaTime;
            GetComponent<Animation>()[AnimString].speed = Speed;
            if (Speed <= 0.2f)
            {
                GetComponent<Animation>().CrossFade("Action");
                Move = false;
                Speed = 0.2f;
            }
        }
	}
}
