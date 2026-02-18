using UnityEngine;
using System.Collections;

public class Dont_destroy : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }


    }
}
