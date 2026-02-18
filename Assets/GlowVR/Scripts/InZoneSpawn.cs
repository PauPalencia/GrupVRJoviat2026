using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Main script for gameplay logic placed on main camera

public class InZoneSpawn : MonoBehaviour {

    public Transform[] Objects; //Configured abstraction prefabs
    public float SpawningTime,GPTime; //GPTime-GamePlayTime
    public Transform Cam,Player;
    public int NumKills,k; //k - number of every 10 combos
    public AudioClip[] Extensions;
    public AudioClip[] Backgrounds; //Background Songs
    public ParticleEmitter[] Part; //background effect sound particles
    private bool changeMusic = false;
    public GameObject[] Abstractions;
    public GameObject[] Supports; //activating when user getting more of 30 kills
    public Color colorStart, colorEnd;  //Background Colors
    private Transform Holder;
    public bool FogFading = false; //activating when user getting more of 30 kills
    public float FadingTime = 0.0007f;//activating when user getting more of 30 kills


    ///////////Interface///////////
    private float GettingScores, CurrentPoints, WantedPoints;
    public float NewPoint;
    public Text GettingText;
    public Text KillingText;
    public Text PointsText;
    public bool ShowKilling = false;

    ////////////////////////////// New mechanics

    public Image Energy,Target;
    public GameObject Alert, FadeInPanel;
    public float EnergyTime = 0; //time of energy if 0 then game over
    private float MaxEnergyTime = 30; //max time
    public int gameMode = 0;
    public GameObject SndMgr; //soundmanager
    public bool game_over = false;
    public AudioClip Break;
    private float timeBeforeTap = 0;



    // Use this for initialization
    void Start () {

        SndMgr = GameObject.Find("SoundManager");
        EnergyTime = MaxEnergyTime;

        if (gameMode == 0)
        for (int i = 0; i < 3; i++)
        {
            Holder = Instantiate(Objects[0], Player.transform.position + new Vector3(Random.Range(-400, 400), Random.Range(-200, 200), Random.Range(250, 600)), Objects[0].transform.rotation) as Transform;
            Holder.transform.parent = Player.transform;
        }
    }

    // We gets Holder and placing to Main Camera as parent
    void Update()
    {

        EnergyTime -= Time.deltaTime;

        Energy.gameObject.GetComponent<Image>().fillAmount = EnergyTime / MaxEnergyTime;
        if (EnergyTime <= 20)
            Alert.SetActive(true);
        else
            Alert.SetActive(false);

        if (game_over)
        {
            timeBeforeTap += Time.deltaTime;
            if (timeBeforeTap>=3)
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(0);
            }
        }

        if (!game_over)
        { 
            if (EnergyTime <= 0)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(Break);
                Energy.gameObject.SetActive(false);
                Target.gameObject.SetActive(false);
                FadeInPanel.SetActive(true);

                game_over = true;
            }

            else

            if (EnergyTime >= MaxEnergyTime)
                EnergyTime = MaxEnergyTime;

        SpawningTime += Time.deltaTime;
        GPTime += Time.deltaTime;

        if (NumKills < 10) //Current Kills
            if (SpawningTime >= 10) //spawning 5 objects every 10 seconds
            {
                for (int i = 0; i < 5; i++)
                {
                    Holder = Instantiate(Objects[0], Player.transform.position + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(300, 700)), Objects[0].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                }

                SpawningTime = 0;
            }

        if (NumKills >= 10 && NumKills < 20)
            if (SpawningTime >= 10)
            {
                for (int i = 0; i < 1; i++)
                {
                    Holder = Instantiate(Objects[0], Cam.transform.position + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(300, 700)), Objects[0].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[1], Cam.transform.position + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(300, 700)), Objects[1].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[5], Cam.transform.position + new Vector3(Random.Range(-700, 700), Random.Range(-300, 300), Random.Range(300, 700)), Objects[5].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                }

                SpawningTime = 0;
            }

        if (NumKills >= 20 && NumKills < 30)
            if (SpawningTime >= 10)
            {
                for (int i = 0; i < 1; i++)
                {
                    Holder = Instantiate(Objects[0], Cam.transform.position + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(300, 700)), Objects[0].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[1], Cam.transform.position + new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(300, 700)), Objects[1].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[5], Cam.transform.position + new Vector3(Random.Range(-700, 700), Random.Range(-300, 300), Random.Range(300, 700)), Objects[5].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[2], Cam.transform.position + new Vector3(Random.Range(1500, 1700), Random.Range(200, 500), Random.Range(1500, 2000)), Objects[2].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;

                }

                SpawningTime = 0;
            }


        if (NumKills >= 30)
        {

            if (!changeMusic)
            {

                FogFading = true;

                for (int i = 0; i < Abstractions.Length; i++)
                {
                    Abstractions[i].SetActive(true);
                }

                Supports[0].GetComponent<SoundInTact>().BPMs = 0.26F; //Blink speed of particles for music world reaction
                Supports[1].GetComponent<UVW_Animated>().enabled = true; //Medusa elevator animation

                changeMusic = true;
            }

            if (SpawningTime >= 10)
            {
                for (int i = 0; i < 1; i++)
                {
                    Holder = Instantiate(Objects[1], Cam.transform.position + new Vector3(Random.Range(-400, 400), Random.Range(-400, 400), Random.Range(150, 400)), Objects[1].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[0], Cam.transform.position + new Vector3(Random.Range(-400, 400), Random.Range(-400, 400), Random.Range(150, 400)), Objects[0].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                    Holder = Instantiate(Objects[2], Cam.transform.position + new Vector3(Random.Range(1500, 1700), Random.Range(200, 500), Random.Range(1500, 2000)), Objects[2].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                }

                for (int i = 0; i < 1; i++)
                {

                    Holder = Instantiate(Objects[4], Cam.transform.position + new Vector3(Random.Range(-400, 400), Random.Range(-400, 400), Random.Range(150, 400)), Objects[4].transform.rotation) as Transform;
                    Holder.transform.parent = Player.transform;
                }

                SpawningTime = 0;
            }
        }

        if (NumKills >= 10 * k)
        {
            for (int p = 0; p < Part.Length; p++)
            {
                Part[p].GetComponent<ParticleEmitter>().emit = true; //4 background particles wow effect
            }
            Supports[2].GetComponent<AnimationSpeed>().Action = true; //Tetacles of Medusa move
            Supports[3].GetComponent<AnimationSpeed>().Action = true;//Tetacles of Medusa move
            Supports[4].GetComponent<AnimationSpeed>().Action = true;//Tetacles of Medusa move
            StartCoroutine(CooldownParts(1)); //ending emit 4 background particles
            GetComponent<AudioSource>().PlayOneShot(Extensions[Random.Range(0, Extensions.Length)]); //Extension wow effect sound
            k += 1;

            GettingScores = 10 * k;
            GettingText.GetComponent<Animator>().Play("GettingText_Anim", -1, 0f); //Play main text
            GettingText.text = "GET: " + GettingScores + " KILLS";
            NumKills = 0;
        }


        if (ShowKilling)
        {
            WantedPoints += NewPoint;
            KillingText.text = "KILLS: " + NumKills;
            KillingText.GetComponent<Animator>().Play("KillText_Anim", -1, 0f);
            ShowKilling = false;
        }

        if (CurrentPoints <= WantedPoints)
        {
            CurrentPoints += NewPoint * 2 * Time.deltaTime;
        }

        PointsText.text = "POINTS: " + Mathf.FloorToInt(CurrentPoints); //Points counter UI

        if (FogFading)
        {
            FadingTime -= 0.0002f * Time.deltaTime;
            if (FadingTime <= 0.0001f)
            {
                FadingTime = 0.0001f;
            }
            RenderSettings.fogDensity = FadingTime;
        }
    }


    }

    IEnumerator CooldownParts(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int p = 0; p < Part.Length; p++)
        {
            Part[p].GetComponent<ParticleEmitter>().emit = false;
        }
    }

    //For changing background music

    /*IEnumerator FadeOutVolume(float valueTime)
    {
        yield return new WaitForSeconds(valueTime);
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = Backgrounds[1];
        GetComponent<AudioSource>().Play();

    }*/
}
