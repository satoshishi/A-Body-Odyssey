using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{
    void Start()
    {
        (GetComponent<Renderer>().material.mainTexture as MovieTexture).Play();
    }

}
