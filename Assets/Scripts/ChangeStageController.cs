using UnityEngine;
using System.Collections;

public class ChangeStageController : MonoBehaviour {

    private bool Is_Admit_StageChange=false;
    private StageManager stage;
    private FadeController fade;
    public StageManager.SCENE_TYPE stage_type;

    void Start()
    {
        fade = GameObject.Find("Fade").transform.GetComponent<FadeController>();
        stage = GameObject.Find("StageManager").transform.GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Is_Admit_StageChange)
            StartCoroutine(ChangeSceneEvent());
    }

    /// <summary>
    /// シーン切り替え処理
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeSceneEvent()
    {
        Is_Admit_StageChange = false;

        //排泄イベントでない場合はフェードアウトしてシーン切り替え
        if (stage.GetNextType(stage_type) != StageManager.SCENE_TYPE.DISCHARGE)
        {
            fade.Fadeout(0.5f, 0, FadeController.FADE_COLOR_TYPE.BLACK);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            fade.Fadeout(0.1f, 0, FadeController.FADE_COLOR_TYPE.WHITE, 1, true, 2f);
            yield return new WaitForSeconds(0.5f);
        }
        stage.ChangeStage(stage_type);
    }

    /// <summary>
    /// 　各消化器官で設けているアクシデントイベントを実行する
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            Is_Admit_StageChange = true;
    }
}
