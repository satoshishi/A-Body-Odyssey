using UnityEngine;
using System.Collections;

public class SceneRestartManager : MonoBehaviour
{
    public bool Is_Already_Restart = false; 

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
