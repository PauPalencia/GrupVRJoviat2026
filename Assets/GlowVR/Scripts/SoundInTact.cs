using UnityEngine;
using System.Collections;

//Control sound in tact, script using for particle and toon shader

public class SoundInTact : MonoBehaviour {

    public float BPMs = 0; //Beats per Minutes
    public float WantedSecs = 0; //End of beat
    public Color colorStart;  //Start of color
    public Color colorEnd; //End of color
    public Color Result; //End of color
    public float duration = 2.0F;
    public bool isToonShader = false; //if any shader blink then true

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //BPMs += Time.deltaTime;

        float lerp = Mathf.PingPong(Time.time, BPMs) / BPMs;
        Result = Color.Lerp(colorStart, colorEnd, lerp);
        if (!isToonShader)
        GetComponent<Renderer>().material.SetColor("_TintColor", Result);
        if (isToonShader)
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Result);

    }
}
