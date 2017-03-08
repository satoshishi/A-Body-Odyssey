using UnityEngine;
using System.Collections;

public class UnderWaterEventController : MonoBehaviour
{
    private bool Is_Already_Complete_UnderWaterEvent = false;
    private StimulusController stimulu;
    private SoundEventController sound;
    private FadeController fade;
   // private MoveManager move;

    void Start()
    {
     //   move = GameObject.Find("player").GetComponent<MoveManager>();
        sound = GameObject.Find("Sounds").GetComponent<SoundEventController>();
        fade = GameObject.Find("Fade").GetComponent<FadeController>();
        stimulu = GameObject.Find("Stimulus").GetComponent<StimulusController>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !Is_Already_Complete_UnderWaterEvent)
        {
       //     stimulu.Stop();
            sound.UpdatePitch(800, 0.2f, 0.2f);//水中音のピッチを下げる
                                               //     stimulu.UpdateHeartStmulesPitch(0.2f, 0.2f);//触覚刺激のピッチを変更。
            fade.Fadeout(0.4f, 0f, FadeController.FADE_COLOR_TYPE.YELLOW, 0.3f, false);//画面が黄ばむ
           // stimulu.UpdateStimulusPitch(500);
       /*     move.SelectMoveTypePathName(MoveManager.MOVE_TYPE.BACK);
            move.IsAdmitMoveType(MoveManager.MOVE_TYPE.BACK, true);
            move.IsAdmitMoveType(MoveManager.MOVE_TYPE.ZENDOU, false);
            move.ResetDistance();*/
        }

    }


}
