using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///　消化器官ごとの設定データ
/// </summary>
[CreateAssetMenu()]
public class SettingData : ScriptableObject
{
    public string path_name;//アニメーションの名前
    public string scene_name = "syokudou";//シーンの名前
    public SoundEventController.BgmType bgm;//bgm;
    public bool is_use_physiological;//生理音を使うか
    public float walk_stimules_pitch = 1.34f;//ほふく音のピッチ
    public float heart_pitch = 0.4f;//心音のピッチ
    public float light_range = 8.73576f;//ライトの範囲
    public float intensity = 1.8f;//ライトの明るさ
    public float speed; //移動速度
   // public StageManager.SCENE_TYPE type;
}
