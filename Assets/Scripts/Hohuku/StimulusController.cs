using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触覚刺激を操作するスクリプト
/// </summary>
public class StimulusController : MonoBehaviour
{
    /// <summary>
    /// チャンネル
    /// </summary>
    public enum Stimulus_Ch
    {
        FIVE = 0,
        SIX = 1,
        SEVEN = 2,
        EIGHT = 3
    };

    public enum Stimulus_Type
    {
        STIMULUS,
        HERATBEAT
    }

    public struct Stimulus
    {
        public AudioSource source;
        public bool IsAlreadyPlaying { get; set; }
        public Stimulus_Ch ch;
        public Stimulus_Type type;

        public Stimulus(AudioSource _source, Stimulus_Ch _ch, Stimulus_Type _type)
        {
            source = _source;
            ch = _ch;
            type = _type;
            IsAlreadyPlaying = false;
        }
    }

    [SerializeField]
    private List<AudioSource> stimulus_source;
    [SerializeField]
    private List<AudioSource> heartbeat_source;
    [SerializeField]
    private SoundEventController sound;

    private List<Stimulus> Stimuluses_Move;
    [SerializeField]
    private List<Stimulus> Stimuluses_HeartBeat;

    // Use this for initialization
    void Awake()
    {
        Stimuluses_Move = new List<Stimulus>();
        Stimuluses_HeartBeat = new List<Stimulus>();

        for (int i = 0; i < stimulus_source.Count; i++)
        {
            Stimuluses_Move.Add(new Stimulus(stimulus_source[i], (Stimulus_Ch)i, Stimulus_Type.STIMULUS));
            Stimuluses_HeartBeat.Add(new Stimulus(heartbeat_source[i], (Stimulus_Ch)i, Stimulus_Type.HERATBEAT));
        }

        Init();
    }

    public void Init()
    {
        StopAll(Stimulus_Type.HERATBEAT);
        StopAll(Stimulus_Type.STIMULUS);
    }

    /// <summary>
    /// 刺激開始
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ch"></param>
    public void Play(Stimulus_Type type, Stimulus_Ch ch)
    {
        Stimulus target = type == Stimulus_Type.STIMULUS ? Stimuluses_Move[(int)ch] : Stimuluses_HeartBeat[(int)ch];

        if (!target.IsAlreadyPlaying)
        {
        //    Debug.Log(ch + " "+ target.IsAlreadyPlaying);
            target.source.Play();
            target.IsAlreadyPlaying = true;

            if (target.type == Stimulus_Type.STIMULUS)
                Stimuluses_Move[(int)ch] = target;
            else Stimuluses_HeartBeat[(int)ch] = target;

        }
    }

    /// <summary>
    /// 刺激停止
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ch"></param>
    public void Stop(Stimulus_Type type, Stimulus_Ch ch)
    {

        Stimulus target = type == Stimulus_Type.STIMULUS ? Stimuluses_Move[(int)ch] : Stimuluses_HeartBeat[(int)ch];

        if (target.IsAlreadyPlaying)
        {
            target.source.Stop();
            target.IsAlreadyPlaying = false;

            if (target.type == Stimulus_Type.STIMULUS)
                Stimuluses_Move[(int)ch] = target;
            else Stimuluses_HeartBeat[(int)ch] = target;

        }
    }

    public void StopAll(Stimulus_Type type = Stimulus_Type.STIMULUS)
    {

        for (int i = 0; i <= (int)Stimulus_Ch.EIGHT; i++)
        {
            if (type == Stimulus_Type.STIMULUS)
                Stop(Stimulus_Type.STIMULUS, (Stimulus_Ch)i);
            else Stop(Stimulus_Type.HERATBEAT, (Stimulus_Ch)i);
        }

    }

    public void Pitch(float pitch, Stimulus stimulus)
    {
        stimulus.source.pitch = pitch;
    }

    public void Pitch(float pitch, Stimulus_Type type)
    {
        List<Stimulus> audio = type == Stimulus_Type.STIMULUS ? Stimuluses_Move : Stimuluses_HeartBeat;

        foreach (Stimulus s in audio)
            Pitch(pitch, s);
    }

    public const float FULL_STIMULUS_BELLY = 0.4f;

    /// <summary>
    /// 移動量に応じて刺激の強さを調整する
    /// 移動量が0~0.5で腹を刺激(0.25でmax,0.5でmin)
    /// 移動量が0.25=0.75で足を刺激(0.5でmax,0.75でmin)
    /// </summary>
    /// <param name="strength"></param>
    public void UpdateStrength(float strength)
    {
        StopAllCoroutines();

        if (strength >= 1)
        {
            Stimuluses_Move[(int)Stimulus_Ch.FIVE].source.volume = 0f;
            Stimuluses_Move[(int)Stimulus_Ch.SIX].source.volume = 0f;
            Stimuluses_Move[(int)Stimulus_Ch.SEVEN].source.volume = 0f;
            Stimuluses_Move[(int)Stimulus_Ch.EIGHT].source.volume = 0f;
        }
        /*else*/
        if (strength >= FULL_STIMULUS_BELLY)
        {
            Play(Stimulus_Type.STIMULUS, Stimulus_Ch.SIX);
            Play(Stimulus_Type.STIMULUS, Stimulus_Ch.EIGHT);

            //移動量が0.75を超えたら腹の刺激はしない
            if (strength <= 0.65f)
            {
                //脚部刺激強める
                Stimuluses_Move[(int)Stimulus_Ch.SIX].source.volume = ((strength - FULL_STIMULUS_BELLY) / FULL_STIMULUS_BELLY);
                Stimuluses_Move[(int)Stimulus_Ch.EIGHT].source.volume = ((strength - FULL_STIMULUS_BELLY) / FULL_STIMULUS_BELLY);
            }
            else
            {
                //脚部刺激弱める
                Stimuluses_Move[(int)Stimulus_Ch.SIX].source.volume = 2.0f - ((strength - FULL_STIMULUS_BELLY) / FULL_STIMULUS_BELLY);
                Stimuluses_Move[(int)Stimulus_Ch.EIGHT].source.volume = 2.0f - ((strength - FULL_STIMULUS_BELLY) / FULL_STIMULUS_BELLY);
            }
            //腹部刺激弱める
            Stimuluses_Move[(int)Stimulus_Ch.FIVE].source.volume = 2.0f - (strength / FULL_STIMULUS_BELLY);
            Stimuluses_Move[(int)Stimulus_Ch.SEVEN].source.volume = 2.0f - (strength / FULL_STIMULUS_BELLY);
        }
        else
        {

            Play(Stimulus_Type.STIMULUS, Stimulus_Ch.FIVE);
            Play(Stimulus_Type.STIMULUS, Stimulus_Ch.SEVEN);

            Stimuluses_Move[(int)Stimulus_Ch.FIVE].source.volume = (strength / FULL_STIMULUS_BELLY);
            Stimuluses_Move[(int)Stimulus_Ch.SEVEN].source.volume = (strength / FULL_STIMULUS_BELLY);
        }
        /*    Debug.Log("腹部 " + StimulusSource[(int)Stimulus_Ch.FIVE].volume +
                " 脚部 " + StimulusSource[(int)Stimulus_Ch.SIX].volume);*/

        StartCoroutine(StopEvent());

    }

    public IEnumerator StopEvent()
    {
        int frame = 0;

        while (frame <= 8)
        {
            frame++;
            yield return null;
        }
        StopAll();
        sound.Stop(SoundEventController.AudioType.MOVE);
    }

}