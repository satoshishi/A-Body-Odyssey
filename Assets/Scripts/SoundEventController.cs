using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// サウンド処理
/// </summary>
public class SoundEventController : MonoBehaviour
{

    void Start()
    {
        Init();
    }

    #region 全体

    public void Init()
    {
        move_player.mute = true;
        physiological_player.pitch = 0.4f;
        bgm_player.pitch = 0.3f;
        pitch.cutoffFrequency = 5000;
       // move_player.pitch = 0.4f;
    }

    public void StopSound()
    {
        StopAllCoroutines();
        move_player.mute = true;
    }

    public void UpdatePitch(float move, float physiological, float bgm)
    {
        pitch.cutoffFrequency = move;
        physiological_player.pitch = physiological;
        bgm_player.pitch = bgm;
    }

    public void UpdatePitch(float move)
    {
        pitch.cutoffFrequency = move;
    }

    #endregion

    #region sound

    public AudioSource physiological_player;//生理音

    public IEnumerator PlayPhysiologicalOneShot(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (clip != null)
            physiological_player.PlayOneShot(clip);
    }

    #endregion

    #region BGN

    public AudioSource bgm_player;//bgm

    public void SetBGM(AudioClip clip)
    {
        if (clip == null)
        {
            bgm_player.enabled = false;
            return;
        }
        if (bgm_player.clip.name.Equals(clip.name) && bgm_player.isPlaying)
            return;

        bgm_player.enabled = true;
        bgm_player.clip = clip;
        bgm_player.Play();
    }

    #endregion

    #region move

    public AudioSource move_player;//移動音
    public AudioLowPassFilter pitch;//2000~5000hz

    public IEnumerator PlayMoveRequestTime(float delay)
    {
        move_player.mute = false;
        yield return new WaitForSeconds(delay);
        move_player.mute = true;
    }

    public void TouchEvent()
    {
        StartCoroutine(PlayMoveRequestTime(0.3f));
    }

    public void IsMuteMoveSound(bool result)
    {
        move_player.mute = result;
    }

    #endregion
}
