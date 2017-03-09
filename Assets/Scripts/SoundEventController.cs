using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// サウンド処理
/// pitch操作
/// audioの有効無効
/// 生理音の実行
/// bgmの変更
/// </summary>
public class SoundEventController : MonoBehaviour
{
    public enum AudioType
    {
        MOVE = 0,
        HEARTBEAT = 1,
        BGM = 2,
        PHYSIOLOGICAL = 3
    };

    public enum BgmType
    {
        NOON = -1,
        NORMAL = 0,
        EAT = 1,
        DISCHARGE = 2,
        UNDERWATER = 3
    };

    public struct Audio
    {
        public AudioSource source;
        public bool IsAlreadyPlaying { get; set; }

        public AudioType type;

        public Audio(AudioSource _source, AudioType _type)
        {
            source = _source;
            type = _type;
            IsAlreadyPlaying = false;
        }

        /*    public void IsPlaying(bool result)
            {
                IsAlreadyPlaying = result;
            }*/
    }

    [SerializeField]
    private List<Audio> AudioGroups;

    [SerializeField]
    private List<AudioSource> _audio;

    [SerializeField]
    private List<AudioClip> bgm_clips;


    void Start()
    {
        AudioGroups = new List<Audio>();

        for (int i = 0; i < _audio.Count; i++)
            AudioGroups.Add(new Audio(_audio[i], (AudioType)i));

        Init();
    }

    public void Init(float picth = 1, BgmType bgm = BgmType.NOON, bool is_play_physiological = false, bool is_play_heratbeat = false)
    {
        Pitch(1f);
        Stop();

        if (bgm != BgmType.NOON)
        {
            SetBGM(bgm);
            Play(AudioType.BGM);
        }

        if (is_play_physiological)
            PlayPhysiological();

        if (is_play_heratbeat)
            Play(AudioType.HEARTBEAT);

    }

    public void Play(AudioType type)
    {
        Audio target = AudioGroups[(int)type];

        if (!target.IsAlreadyPlaying)
        {
            target.source.Play();
            target.IsAlreadyPlaying = true;
            AudioGroups[(int)type] = target;
        }
    }

    public void Play()
    {
        foreach (Audio a in AudioGroups)
            Play(a.type);
    }

    public void Stop(AudioType type)
    {
        Audio target = AudioGroups[(int)type];

        if (target.IsAlreadyPlaying)
        {
            target.source.Stop();
            target.IsAlreadyPlaying = false;
            AudioGroups[(int)type] = target;
        }
    }

    public void Stop()
    {
        foreach (Audio a in AudioGroups)
            Stop(a.type);
    }

    public void Pitch(AudioType type, float pitch)
    {
        if (type == AudioType.BGM && pitch == 1)
            AudioGroups[(int)type].source.pitch = 0.6f;

        AudioGroups[(int)type].source.pitch = pitch;
    }

    public void Pitch(float pitch)
    {
        foreach (Audio a in AudioGroups)
            Pitch(a.type, pitch);
    }

    public void PlayPhysiological()
    {
        AudioGroups[(int)AudioType.PHYSIOLOGICAL].source.Play(10);
    }

    public void SetBGM(BgmType type)
    {
        AudioGroups[(int)AudioType.BGM].source.clip = bgm_clips[(int)type];
    }
}
