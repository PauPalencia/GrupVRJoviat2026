using UnityEngine;
using System.Collections;

//Script only for Flower prefab

public class AnimationFlower : MonoBehaviour {

    public float AnimTime,fade = 3.6f; //
    private Animation Flower;
    public bool push = false;
    public float KillLeaf, Life;
    public bool is_leaf=false; //if only script placed on leafs then is_leaf must be true
    private GameObject Cam;
    public GameObject Holder;
    public GameObject Plane;
    public Color ColorStart, ColorEnd, Shooting_Color;
    private Renderer Mtl;
    private bool Killed=false;
    public AudioClip[] Kick_Sounds;
    public AudioClip[] Explosions_Sounds;
    public int scoreBonus = 1; //bonus multiplier
    public int score = 50;
    public TextMesh Scores;
    public Transform Explo;

    // Use this for initialization
    void Start () {
        Flower = GetComponent<Animation>();
        Cam = GameObject.Find("Main Camera");
        if (is_leaf)
        {
            Mtl = GetComponent<Renderer>();
            Mtl.material.SetColor("_Color", ColorStart);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
            if (is_leaf)
            {
                Life -= 10;
                Mtl.material.SetColor("_Color", Shooting_Color); //White color when hitting
                StartCoroutine(CooldownColor(0.2F)); //Invert color to start color
                Instantiate(Explo, other.transform.position, other.transform.rotation); //Explosion over hitting
                if (Life <= 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(Explosions_Sounds[Random.Range(0, Explosions_Sounds.Length)]); //Play Explosion sounds
                    score = score * scoreBonus;                //Final score can be multiply with score bonus defines in inspector
                    //Scores.text = "+" + score;                For Score animation need store Score UI Text in to Flower Prefab, example like a Sphere prefab
                    Killed = true; //if killed = true then send this state in to InZoneSpawn script defined on Main Camera
                    StartCoroutine(CooldownColor(0.2F)); //Set hit color on leaf as black
                    Cam.GetComponent<InZoneSpawn>().NumKills += 1; // for UI statistics current Kill 
                    Cam.GetComponent<InZoneSpawn>().ShowKilling = true; //Animating KILLING text in InZoneSpawn script placed on Main Camera
                    Holder.GetComponent<AnimationFlower>().KillLeaf += 1; //if killed one of 4 leafs
                    GetComponent<Collider>().enabled = false; //disable collider
                    Cam.GetComponent<InZoneSpawn>().NewPoint = score; // Score counter
                    Cam.GetComponent<AudioReverbFilter>().room = 0; //Set the audio effect from background music for play delay effect of current sound
                    StartCoroutine(CooldownSound(0.2f)); //Invert delay sound effect
                    Life = 0;
                }
            }

        GetComponent<AudioSource>().PlayOneShot(Kick_Sounds[Random.Range(0, Kick_Sounds.Length)]); //current kick sound

    }

        // Update is called once per frame
        void Update () {
        if (!is_leaf)
        {
            fade -= Time.deltaTime;
            AnimTime = Flower["AbstractFlower_Anim"].time;
            if (AnimTime >= 3.6f && !push)
                Flower["AbstractFlower_Anim"].speed = 0;
            if (fade <= 0) fade = 0;
            Plane.GetComponent<Renderer>().material.SetColor("_TintColor", new Color (1,1,1,fade)); //for circle explosion effect

            if (KillLeaf >= 4)
            {
                KillLeaf = 0;
                push = true;
            }

            //If all of 4 leafs has been killed playing advanced animation

            if (push)
            {
                Holder.GetComponent<RotateObject>().enabled = true;
                Flower["AbstractFlower_Anim"].speed = 1;
                if (AnimTime >= 4.3f)
                    Flower["AbstractFlower_Anim"].speed = 0;
            }
        }

    }

    IEnumerator CooldownColor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Mtl.material.SetColor("_Color", ColorStart);
        if (Killed)
            Mtl.material.SetColor("_Color", ColorEnd);
    }

    IEnumerator CooldownSound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Cam.GetComponent<AudioReverbFilter>().room = -10000;
    }
}
