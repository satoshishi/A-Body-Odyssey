using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///　消化器官ごとの設定データ
/// </summary>
[CreateAssetMenu()]
public class SettingData : ScriptableObject
{
    public string[] path_name=new string[4];//アニメーションの名前
    public string scene_name = "syokudou";//シーンの名前
    public AudioClip bgm;//bgm;
    public AudioClip physiological;
    public float walk_stimules_pitch = 1.34f;//ほふく音のピッチ
    public float heart_pitch = 0.4f;//心音のピッチ
    public float light_range = 8.73576f;//ライトの範囲
    public float intensity = 1.8f;//ライトの明るさ
    public float speed; //移動速度
   // public StageManager.SCENE_TYPE type;
}
