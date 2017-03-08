using UnityEngine;
using System.Collections;

/// <summary>
/// Thetaによるムービーの処理
/// </summary>
public class TheataMoiveController : MonoBehaviour
{
    // public GameObject zendou;
    // public StageManager manager;
    public FadeController fade;
    public StageManager.SCENE_TYPE type;
    public StageManager stage;
    private bool Is_Already_Start_Movie;

    public AudioSource voice;
    public AudioSource toilet;
    public AudioClip eat_clip;
    public AudioClip discharge_clip;
    public ParticleSystem water_top;
    public ParticleSystem water_bottom;

    void Start()
    {
        Is_Already_Start_Movie = false;
        fade = GameObject.Find("Fade").transform.GetComponent<FadeController>();
        stage = GameObject.Find("StageManager").transform.GetComponent<StageManager>();
        //water_top.Pause();
        //water_bottom.Pause();
        StartCoroutine("InitMovieEvent");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z) && !Is_Already_Start_Movie)
            PlayMovie();
    }

    public void PlayMovie()
    {
        if(!(GetComponent<Renderer>().material.mainTexture as MovieTexture).isPlaying)
        (GetComponent<Renderer>().material.mainTexture as MovieTexture).Play();
      //^  else (GetComponent<Renderer>().material.mainTexture as MovieTexture).

        if (type == StageManager.SCENE_TYPE.EAT)
            StartCoroutine("EatMovieEvent");
        else StartCoroutine("DisChargeMovieEvent");

        Is_Already_Start_Movie = true;
    }

    IEnumerator InitMovieEvent()
    {
        (GetComponent<Renderer>().material.mainTexture as MovieTexture).Play();
        yield return new WaitForSeconds(0.1f);

        if (type == StageManager.SCENE_TYPE.EAT)
            (GetComponent<Renderer>().material.mainTexture as MovieTexture).Pause();
        else StartCoroutine("DisChargeMovieEvent");
    }

    public IEnumerator EatMovieEvent()
    {
        yield return new WaitForSeconds(20f);
        fade.Fadeout(2, 0, FadeController.FADE_COLOR_TYPE.BLACK);
        yield return new WaitForSeconds(2f);
        voice.PlayOneShot(eat_clip);

        yield return new WaitForSeconds(1f);
        stage.ChangeStage(StageManager.SCENE_TYPE.EAT);
        //zendou.gameObject.SetActive(true);
        //gameObject.SetActive(false);
    }

    IEnumerator DisChargeMovieEvent()
    {
        yield return new WaitForSeconds(10.5f);
        toilet.PlayOneShot(discharge_clip);
        yield return new WaitForSeconds(0.5f);
        water_top.Play();
       //  yield return new WaitForSeconds(2f);
        water_bottom.Play();
    //    yield return new WaitForSeconds(12f);
     //   stage.ResetStage();
    }
}
