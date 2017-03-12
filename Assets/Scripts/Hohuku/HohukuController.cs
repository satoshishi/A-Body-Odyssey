using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 匍匐前進の手の動きを判定するスクリプト
/// </summary>
public class HohukuController : MonoBehaviour
{
    public enum HandState
    {
        IDLE,
        GROUND,
        HOHUKU
    }

    public enum HandType
    {
        RIGHT,
        LEFT,
    }

    [SerializeField]
    private HandType type;
    [SerializeField]
    private SoundEventController sound;
    /*  [SerializeField]
      private bool is_use_debug_move;*/

    public Transform hand;
    public Transform mat;
    public MatCalibration calibration;
    public MoveController move;
    public StimulusController stimulus;
    public HohukuController reverse_hand;

    [SerializeField]
    private HandState state;
    public HandState State
    {
        private set { state = value; }
        get { return state; }
    }

    /// <summary>
    /// マットと手の距離(深さ)
    /// </summary>
    private float depth;
    public float DepthDistance
    {
        set { depth = value; }
        get { return (depth - (mat.position.y)*1.1f); }
    }

    /// <summary>
    /// 手が接地した初期位置
    /// </summary>
    private float start_pos;
    public float StartPos
    {
        private set { start_pos = value; }
        get { return start_pos; }
    }

    /// <summary>
    /// 現在の手の引き位置
    /// </summary>
    private float now_pull_hand_pos;
    public float NowPullHandPos
    {
        private set { now_pull_hand_pos = value; }
        get { return now_pull_hand_pos + (calibration.PullAreaMin - StartPos); }
    }

    private float before_pull_area;
    public float BeforePullArea
    {
        set { before_pull_area = value; }
        get { return before_pull_area; }
    }

    /// <summary>
    /// マットに手が接地しているかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsGround()
    {
           Debug.Log("depth area is " + calibration.RightHandDepth +
               "now area is " + DepthDistance);
       /* Debug.Log("hand " + hand.localPosition +
            " mat " + mat.localPosition);*/
        return DepthDistance <= (type == HandType.RIGHT ? calibration.RightHandDepth : calibration.LeftHandDepth);
    }

    public bool IsMoveForward()
    {
        /*     Debug.Log("before " + BeforePullArea +
                 " pull " + PullArea());*/

        return BeforePullArea < PullArea() && (PullArea() >= 0.1f);
    }

    /// <summary>
    /// 現在の手の位置(0.00~1.00)
    /// calibrationされた範囲内で手の接地した位置からの値
    /// </summary>
    /// <returns></returns>
    public float PullArea()
    {
        /*  Debug.Log("nowpull : " + NowPullHandPos +
              " min : " + calibration.PullAreaMin +
              " nax : " + calibration.PullAreaMax);*/
        float area = Mathf.Ceil((NowPullHandPos - calibration.PullAreaMin) / (calibration.PullAreaMax - calibration.PullAreaMin) * 100f) / 100f;

        return area < 0 ? 0 : area > 1 ? 1 : area;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Debug.Log("ready");
            move.Move();
            sound.Play(SoundEventController.AudioType.MOVE);
            stimulus.UpdateStrength(0.5f);
        }
        else
        {
            DepthDistance = hand.position.y;
            UpdateHandLogic();
        }
    }

    public void UpdateHandLogic()
    {
        switch (State)
        {
            case HandState.IDLE:
                if (IsGround())
                {
                    State = HandState.GROUND;
                    StartPos = transform.localPosition.z;
                }
                break;
            case HandState.GROUND:
                if (!IsGround())
                {
                    if (reverse_hand.State != HandState.HOHUKU)
                    {
                        stimulus.StopAll();
                        sound.Stop(SoundEventController.AudioType.MOVE);
                    }
                    State = HandState.IDLE;
                    break;
                }

                NowPullHandPos = transform.localPosition.z;

                if (IsMoveForward())
                    State = HandState.HOHUKU;

                BeforePullArea = PullArea();

                break;
            case HandState.HOHUKU:

                NowPullHandPos = transform.localPosition.z;

                if (IsMoveForward())
                {
                    move.Move();
                    sound.Play(SoundEventController.AudioType.MOVE);
                    stimulus.UpdateStrength(PullArea());
                }
                else State = HandState.GROUND;

                BeforePullArea = PullArea();

                break;
        }
    }
}
