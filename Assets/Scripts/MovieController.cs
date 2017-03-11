using UnityEngine;
using System.Collections;
using RenderHeads.Media.AVProVideo;

/// <summary>
/// gear360の動画再生スクリプト
/// </summary>
public class MovieController : MonoBehaviour
{

    [SerializeField]
    private MediaPlayer media;
    [SerializeField]
    private FadeController fade;
    [SerializeField]
    private SoundEventController sound;
    [SerializeField]
    private bool IsAlreadyPlaying = false;
    [SerializeField]
    private StageManager stage;
    [SerializeField]
    private StageManager.SCENE_TYPE type;

    // Use this for initialization
    void Start()
    {
        fade = GameObject.Find("Fade").GetComponent<FadeController>();
        sound = GameObject.Find("Sounds").GetComponent<SoundEventController>();
        stage = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z) && !IsAlreadyPlaying)
        {
            StartCoroutine("Play");
        }
    }

    IEnumerator Play()
    {
        IsAlreadyPlaying = true;
        media.Play();

        yield return new WaitForSeconds(8f);
        fade.Fadeout(2f, 0f, FadeController.FADE_COLOR_TYPE.BLACK);
        yield return new WaitForSeconds(1f);
        sound.Play(SoundEventController.AudioType.BGM);
        yield return new WaitForSeconds(1f);
        stage.ChangeStage(type);
    }


}
