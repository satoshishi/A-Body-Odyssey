using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 消化器官ごとの状態遷移の管理
/// </summary>
public class StageManager : MonoBehaviour
{
   // public AudioSource heart;
    public SoundEventController sound;
    public MoveController move;
    public BodyMapController narration;
    public Light light;
    public SyoukaController syouka;
    public StimulusController stimulus;

    //各巡回数を管理するDictionary
    private Dictionary<SCENE_TYPE, SCENE_TYPE> FULL_STAGE;
    private Dictionary<SCENE_TYPE, SCENE_TYPE> SEVEN_STAGE;
    private Dictionary<SCENE_TYPE, SCENE_TYPE> SIX_STAGE;
    private Dictionary<SCENE_TYPE, SCENE_TYPE> FOUR_STAGE;

    private List<Dictionary<SCENE_TYPE, SCENE_TYPE>> STAGES;
    //public STAGE_SIZE size = STAGE_SIZE.FULL;

//    private bool Is_Already_Decide_Stage_Size = false;

    //巡回するサイズ
  /*  public enum STAGE_SIZE
    {
        FULL = 0,
        SEVEN = 1,
        SIX = 2,
        FOUR = 3
    };*/

    //ステージの種類
    public enum SCENE_TYPE
    {
        EAT = 0,
        SYOKUDOU = 1,
        I = 2,
        SYOUTYOU_1 = 3,
        SYOUTYOU_2 = 4,
        SYOUTYOU_3 = 5,
        DAITYOU_1 = 6,
        DAITYOU_2 = 7,
        DAITYOU_3 = 8,
        DAITYOU_4 = 9,
        DISCHARGE = 10,
        NOON,
    };

    [SerializeField]
    public List<SettingData> datas;//各ステージの設定データ

    void Start()
    {
     //   SceneManager.LoadScene("Player", LoadSceneMode.Single);
      //  SceneManager.LoadScene("Arduino", LoadSceneMode.Additive);

        InitStages();
 //  ChangeStage(SCENE_TYPE.NOON);
        ChangeStage(SCENE_TYPE.EAT);
    }

    void Update()
    {
        /*移動する臓器の数を指定*/
        /*その後摂取シーンに移行*/
       // if (!Is_Already_Decide_Stage_Size)
        //    DecideStageSize();

        /*Rキーを押したら初期シーンにリセット*/
        if (Input.GetKeyUp(KeyCode.R))
            ResetStage();

        if (Input.GetKeyUp(KeyCode.Escape)) 
            Application.Quit();

    }


    public void ResetStage()
    {
        SceneManager.LoadScene("Player", LoadSceneMode.Single);
    }


    /// <summary>
    /// シーンの数を指定
    /// </summary>
  /*  void DecideStageSize()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            size = StageManager.STAGE_SIZE.FULL;
            Is_Already_Decide_Stage_Size = true;
            StartCoroutine(GameObject.Find("Screen").GetComponent<TheataMoiveController>().EatMovieEvent());
            StartCoroutine(player.InitGravityAverage());
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            size = StageManager.STAGE_SIZE.SEVEN;
            Is_Already_Decide_Stage_Size = true;
            StartCoroutine(GameObject.Find("Screen").GetComponent<TheataMoiveController>().EatMovieEvent());
            StartCoroutine(player.InitGravityAverage());
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            size = StageManager.STAGE_SIZE.SIX;
            Is_Already_Decide_Stage_Size = true;
            StartCoroutine(GameObject.Find("Screen").GetComponent<TheataMoiveController>().EatMovieEvent());
            StartCoroutine(player.InitGravityAverage());
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            size = StageManager.STAGE_SIZE.FOUR;
            Is_Already_Decide_Stage_Size = true;
            StartCoroutine(GameObject.Find("Screen").GetComponent<TheataMoiveController>().EatMovieEvent());
            StartCoroutine(player.InitGravityAverage());
        }
    }*/



    /// <summary>
    /// 各巡回位置管理listの遷移順を設定
    /// </summary>
    void InitStages()
    {
        FULL_STAGE = new Dictionary<SCENE_TYPE, SCENE_TYPE>()
         {     
            {SCENE_TYPE.NOON,SCENE_TYPE.EAT},
            {SCENE_TYPE.EAT,SCENE_TYPE.SYOKUDOU},
            {SCENE_TYPE.SYOKUDOU,SCENE_TYPE.I},
            {SCENE_TYPE.I,SCENE_TYPE.SYOUTYOU_1},
            {SCENE_TYPE.SYOUTYOU_1,SCENE_TYPE.SYOUTYOU_2},
            {SCENE_TYPE.SYOUTYOU_2,SCENE_TYPE.SYOUTYOU_3},
            {SCENE_TYPE.SYOUTYOU_3,SCENE_TYPE.DAITYOU_1},
            {SCENE_TYPE.DAITYOU_1,SCENE_TYPE.DAITYOU_2},
            {SCENE_TYPE.DAITYOU_2,SCENE_TYPE.DAITYOU_3},
            {SCENE_TYPE.DAITYOU_3,SCENE_TYPE.DAITYOU_4},
            {SCENE_TYPE.DAITYOU_4,SCENE_TYPE.DISCHARGE}
        };
        /*
                SEVEN_STAGE = new Dictionary<SCENE_TYPE, SCENE_TYPE>()
                {
                    {SCENE_TYPE.NOON,SCENE_TYPE.EAT},
                    {SCENE_TYPE.EAT,SCENE_TYPE.SYOKUDOU},
                    {SCENE_TYPE.SYOKUDOU,SCENE_TYPE.I},
                    {SCENE_TYPE.I,SCENE_TYPE.SYOUTYOU_1},
                    {SCENE_TYPE.SYOUTYOU_1,SCENE_TYPE.DAITYOU_1},
                    {SCENE_TYPE.DAITYOU_1,SCENE_TYPE.DAITYOU_4},
                    {SCENE_TYPE.DAITYOU_4,SCENE_TYPE.DISCHARGE}
                };

                SIX_STAGE = new Dictionary<SCENE_TYPE, SCENE_TYPE>()
                {
                    {SCENE_TYPE.NOON,SCENE_TYPE.EAT},
                    {SCENE_TYPE.EAT,SCENE_TYPE.I},
                    {SCENE_TYPE.I,SCENE_TYPE.SYOUTYOU_1},
                    {SCENE_TYPE.SYOUTYOU_1,SCENE_TYPE.DAITYOU_1},
                    {SCENE_TYPE.DAITYOU_1,SCENE_TYPE.DAITYOU_4},
                    {SCENE_TYPE.DAITYOU_4,SCENE_TYPE.DISCHARGE}
                };

                FOUR_STAGE = new Dictionary<SCENE_TYPE, SCENE_TYPE>()
                {
                    {SCENE_TYPE.NOON,SCENE_TYPE.EAT},
                    {SCENE_TYPE.EAT,SCENE_TYPE.SYOUTYOU_1},
                    {SCENE_TYPE.SYOUTYOU_1,SCENE_TYPE.DAITYOU_4},
                    {SCENE_TYPE.DAITYOU_4,SCENE_TYPE.DISCHARGE}
                };*/

        STAGES = new List<Dictionary<SCENE_TYPE, SCENE_TYPE>>()
        {
            FULL_STAGE,
           // SEVEN_STAGE,
           // SIX_STAGE,
           // FOUR_STAGE
        };
        
    }

    /// <summary>
    /// 指定されたステージの次の遷移ステージを返す
    /// </summary>
    /// <param name="now_type"></param>
    /// <returns></returns>
    public SCENE_TYPE GetNextType(SCENE_TYPE now_type)
    {
        //return STAGES[(int)size][now_type];
        return STAGES[0][now_type];
    }

    /// <summary>
    /// 指定されたステージの次のステージを実行する
    /// </summary>
    /// <param name="now_type"></param>
    public void ChangeStage(SCENE_TYPE now_type/*, SCENE_TYPE next_type*/)
    {
        SCENE_TYPE next_type = GetNextType(now_type);
        SettingData next_data = datas[(int)next_type];
        SceneManager.LoadScene(next_data.scene_name, LoadSceneMode.Additive);//次のシーンのロード

        switch (next_type)
        {
            case SCENE_TYPE.EAT:
                stimulus.StopAll(StimulusController.Stimulus_Type.HERATBEAT);
                stimulus.StopAll(StimulusController.Stimulus_Type.STIMULUS);
                light.intensity = next_data.intensity;//ライト
                light.range = next_data.light_range;
                sound.SetBGM(SoundEventController.BgmType.EAT);
                break;
                
            case SCENE_TYPE.SYOUTYOU_1:
                syouka.UpdateSyoukaEvent(SCENE_TYPE.SYOUTYOU_1);
                UpdateStageParameter(now_type, next_type, next_data);
                break;

            case SCENE_TYPE.DAITYOU_1:
                syouka.UpdateSyoukaEvent(SCENE_TYPE.DAITYOU_1);
                UpdateStageParameter(now_type, next_type, next_data);
                break;
            /*食道の場合匍匐はさせず自動で移動させる*/
            case SCENE_TYPE.SYOKUDOU:
            case SCENE_TYPE.I:
            case SCENE_TYPE.SYOUTYOU_2:
            case SCENE_TYPE.SYOUTYOU_3:
            case SCENE_TYPE.DAITYOU_2:
            case SCENE_TYPE.DAITYOU_3:
            case SCENE_TYPE.DAITYOU_4:
                UpdateStageParameter(now_type, next_type, next_data);
                break;

            case SCENE_TYPE.DISCHARGE:
                stimulus.StopAll(StimulusController.Stimulus_Type.HERATBEAT);
                stimulus.StopAll(StimulusController.Stimulus_Type.STIMULUS);
                sound.Stop();//soundを全部止める
                sound.SetBGM(SoundEventController.BgmType.DISCHARGE);
                sound.Play(SoundEventController.AudioType.BGM);
                move.Init();//移動もさせないように
                SceneManager.UnloadScene(datas[(int)(now_type)].scene_name);//前シーンの削除
                break;

        }


        //  sound.StopSound();//生理音を停止
        // stimulus.Stop();//触覚刺激のピッチを変更
       // sound.Pitch(SoundEventController.AudioType.HEARTBEAT, next_data.heart_pitch);//心拍の音のピッチを変更
        stimulus.Pitch(next_data.heart_pitch, StimulusController.Stimulus_Type.HERATBEAT);
       // narration.PlayNarattion(next_data.type);//ナレーションの出力

    }

    private void UpdateStageParameter(SCENE_TYPE now_type, SCENE_TYPE next_type, SettingData next_data)
    {
        SceneManager.UnloadScene(datas[(int)(now_type)].scene_name);//前シーンの削除
        move.Init(next_data.path_name, true,next_data.speed);//移動パラメータの初期化
        light.intensity = next_data.intensity;//ライトの初期化
        light.range = next_data.light_range;//
        sound.Init(next_data.heart_pitch, next_data.bgm, next_data.is_use_physiological, true);
        sound.SetBGM(next_data.bgm);
        stimulus.Pitch(next_data.heart_pitch, StimulusController.Stimulus_Type.HERATBEAT);
        stimulus.Play(StimulusController.Stimulus_Type.HERATBEAT);
    }
}
