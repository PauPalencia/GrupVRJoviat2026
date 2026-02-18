using UnityEngine;
using System.Collections;

//Script only for sphere prefab, using for random scaling of object and change the colors

public class SphereLogic : MonoBehaviour {

    public float ChangeBonusColor_Time;
    public float RandomTime;
    private Renderer Mtl;
    public Color color_start;
    public Color color_end;
    public Color start_variation_Outline;
    public Color end_variation_Outline;
    public int scoreBonus=1;
    private bool invert = true;


    // Use this for initialization
    void Start () {

        RandomTime = Random.Range(5, 20);
        Mtl = GetComponent<Renderer>();

    }
	
	// Update is called once per frame
	void Update () {

        ChangeBonusColor_Time += Time.deltaTime;

        if (ChangeBonusColor_Time >= RandomTime)
        {
            if (!invert) {
            Mtl.material.SetColor("_Color", color_end);
                GetComponent<Explosions>().color_end = color_end;
            Mtl.material.SetColor("_OutlineColor", start_variation_Outline);
                GetComponent<Animation>().Play("SphereScale");

                GetComponent<Explosions>().scoreBonus = scoreBonus;
        }
            if (invert)
            {
                Mtl.material.SetColor("_Color", GetComponent<Explosions>().color_start);
                GetComponent<Explosions>().color_end = GetComponent<Explosions>().color_start;
                Mtl.material.SetColor("_OutlineColor", end_variation_Outline);
                GetComponent<Animation>().Play("SphereScaleBack");
                scoreBonus = 1;
                GetComponent<Explosions>().scoreBonus = scoreBonus;
            }
            invert = !invert;
            ChangeBonusColor_Time = 0;
        }

        if (invert && !GetComponent<Animation>().IsPlaying("SphereScale") && !GetComponent<Animation>().IsPlaying("ScoreText"))
            GetComponent<Animation>().Play("SphereLines");

        if (!invert && !GetComponent<Animation>().IsPlaying("SphereScaleBack") && !GetComponent<Animation>().IsPlaying("ScoreText"))
            GetComponent<Animation>().Play("SphereLines");
    }
}
