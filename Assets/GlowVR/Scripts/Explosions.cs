using UnityEngine;
using System.Collections;

//This script using on shark object and sphere like a Flower prefab

public class Explosions : MonoBehaviour {

    public Transform Explo;
    private Renderer Mtl;
    public Color color_start;
    public Color color_end;
    public Color Shooting_Color;
    public float Life = 100;
    public int scoreBonus = 1;
    public int score = 50;
    public TextMesh Scores;
    public string NameAnim;
    public AudioClip[] Kick_Sounds;
    public AudioClip[] Explosions_Sounds;
    public GameObject Cam;
    public bool Other = false;
    public Transform ParentOther;

    void Start()
    {
        //Scores = GetComponent<TextMesh>();
        Mtl = GetComponent<Renderer>();
        Cam = GameObject.Find("Main Camera");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Life -= 10;
            Mtl.material.SetColor("_Color", Shooting_Color);
            Instantiate(Explo, other.transform.position, other.transform.rotation);
            Destroy(other);
            StartCoroutine(CooldownColor(0.2F));
            if (Life <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(Explosions_Sounds[Random.Range(0, Explosions_Sounds.Length)]);
                Life = 0;
                score = score * scoreBonus;
                Scores.text = "+" + score;
                if (!Other)
                    GetComponent<Animation>().Play(NameAnim);
                if (Other)
                    Scores.GetComponent<Animation>().Play(NameAnim);
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                StartCoroutine(DestroyAfter(2));
                Cam.GetComponent<AudioReverbFilter>().room = 0;
                Cam.GetComponent<InZoneSpawn>().NumKills += 1;
                Cam.GetComponent<InZoneSpawn>().ShowKilling = true;
                Cam.GetComponent<InZoneSpawn>().NewPoint = score;
                StartCoroutine(CooldownSound(0.2f));
                Camera.main.gameObject.GetComponent<InZoneSpawn>().EnergyTime += 2;
                //Life = 0;
            }
            GetComponent<AudioSource>().PlayOneShot(Kick_Sounds[Random.Range(0, Kick_Sounds.Length)]);

        }
    }

    IEnumerator CooldownColor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Mtl.material.SetColor("_Color", color_end);
    }

    IEnumerator CooldownSound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Cam.GetComponent<AudioReverbFilter>().room = -10000;
    }

    IEnumerator DestroyAfter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!Other)
                Destroy(gameObject);
        if (Other)
            Destroy(ParentOther.gameObject);
    }

}
