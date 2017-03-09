using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BodyMapController : MonoBehaviour
{
    /*public List<Image> syoutyou;
    public List<Image> daityou;*/

    public List<Image> map;

    [SerializeField]
    public List<UIPos> ui_pos;

    public RectTransform canvas;
    public GameObject body;
    public Image player;

    void Start()
    {
        // PlayNarattion(StageManager.SCENE_TYPE.DAITYOU_1);
        Init();

    }

    public void Init()
    {
        foreach (Image m in map)
        {
            m.color = new Color(1, 1, 1, 1);
        }
        canvas.localScale = new Vector3(0, 0, 0);
        body.SetActive(false);
    }

    public void PlayNarattion(StageManager.SCENE_TYPE type)
    {
        if (type == StageManager.SCENE_TYPE.EAT || type == StageManager.SCENE_TYPE.DISCHARGE)
            return;

            Init();

        SkeletonMap(type);
        UpdatePlayerPos(type);
        PickUp();
    }

    public void UpdatePlayerPos(StageManager.SCENE_TYPE type)
    {
        //  Debug.Log(ui_pos[(int)type - 1].position);

        player.rectTransform.localPosition = ui_pos[(int)type - 1].position;
        player.rectTransform.sizeDelta = ui_pos[(int)type - 1].delta;
        player.rectTransform.localRotation = Quaternion.Euler(0, ui_pos[(int)type - 1].rotate_y, ui_pos[(int)type - 1].rotate_z);
    }


    public void PickUp()
    {
        Enable();

        iTween.ValueTo(
          this.gameObject,
          iTween.Hash(
              "time", 1f,
              "from", 0f,
              "to", 5f,
              "onupdate", "UpdateScale",
              "easeType", iTween.EaseType.easeOutBounce,
              "oncomplete", "PickDown"
          )
      );
    }

    public void PickDown()
    {

        iTween.ValueTo(
          this.gameObject,
          iTween.Hash(
              "delay", 4f,
              "time", 1f,
              "from", 5f,
              "to", 0f,
              "easeType", iTween.EaseType.easeOutBounce,
              "onupdate", "UpdateScale",
              "oncomplete", "Desable"
          )
      );
    }


    private void UpdateScale(float newValue)
    {
        canvas.localScale = new Vector3(newValue, newValue, newValue);
    }

    private void Enable()
    {
        body.SetActive(true);
       // move.IsAdmitMoveType(MoveManager.MOVE_TYPE.ZENDOU, false);
    }
    private void Desable()
    {
        body.SetActive(false);
        //move.IsAdmitMoveType(MoveManager.MOVE_TYPE.ZENDOU, true);
    }

    /// <summary>
    /// 透明度の更新処理
    /// </summary>
    /// <param name="now_pos"></param>
    private void SkeletonMap(StageManager.SCENE_TYPE now_pos)
    {
        foreach (Image m in map)
        {
            if (!m.name.Equals(now_pos.ToString()))
                m.color = new Color(1, 1, 1, 0.2f);
        }
    }

}
