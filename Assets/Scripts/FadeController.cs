using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// シーン切り替えの際のフェードアウト処理
/// </summary>
public class FadeController : MonoBehaviour
{
    [SerializeField]
     List<Fade> Fades;

    public Blur blur;

    private int color_id;
    private bool Is_Admit_FadeIn = true;
    private float fade_in_delay = 0f;

    public enum FADE_COLOR_TYPE
    {
        WHITE=0,
        BLACK=1,
        YELLOW=2
    };

    // Use this for initialization
    void Start()
    {
        Init();
    }

    /// <summary>
    /// 外部から呼び出す場合のフェードイン処理
    /// </summary>
    /// <param name="type"></param>
    /// <param name="time"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>

    private void FadeIn()
    {
        if (!Is_Admit_FadeIn)
            return;


        iTween.ValueTo(
           this.gameObject,
           iTween.Hash(
               "delay", fade_in_delay,
               "time", 3f,
               "from", 1f,
               "to", 0f,
               "onupdate", "UpdateToAlpha",
               "oncomplete", "Desable"
           )
       );

    }

    /// <summary>
    /// 指定の速度でフェードアウト
    /// </summary>
    public void Fadeout(float time, float from,FADE_COLOR_TYPE type, float to=1f,bool is_admit_fadein=true, float delay = 0f)
    {
        Init();
        color_id = (int)type;
        Is_Admit_FadeIn = is_admit_fadein;
        fade_in_delay = delay;

        iTween.ValueTo(
           this.gameObject,
           iTween.Hash(
               "delay",0,
               "time", time,
               "from", from,
               "to", to,
               "onstart","Enable",
               "onupdate", "UpdateToAlpha",
               "oncomplete", "FadeIn"
           )
       );

    }

    public void Enable()
    {
        Fades[color_id].Obj.SetActive(true);
        blur.enabled = true;
    }
    public void Desable()
    {
        Fades[color_id].Obj.SetActive(false);
        blur.enabled = false;
    }


    // private bool IsComplete = false;
    //   private float distance = 0f;
    // private bool IsComplete = false;

    /// <summary>
    /// 移動量に合わせてフェードイン
    /// </summary>
    /// <param name="path_name"></param>
    /* public void Fadeout(float percent, string name)
     {
         if (IsComplete)
             return;

         if (percent >= 0.9f)
         {
             IsComplete = true;
             Fadeout(2f,1, name);
         }
         if (percent - 0.5f <= 0f)
             percent = 0f;
         else percent -= 0.5f;

         if (name.Equals(WHITE))
             UpdateToAlphaTheWhite(percent);
         else UpdateToAlphaTheBlack(percent);
     }*/

    public void Init()
    {
        foreach (Fade f in Fades)
        {
            f.render = f.Obj.GetComponent<Renderer>();
            
            switch (f.type)
            {
                case FADE_COLOR_TYPE.BLACK:
                    f.render.material.color = new Color(0, 0, 0, 0);
                    break;
                case FADE_COLOR_TYPE.WHITE:
                    f.render.material.color = new Color(1, 1, 1, 0);
                    break;
                case FADE_COLOR_TYPE.YELLOW:
                    f.render.material.color = new Color(1, 1, 0, 0);
                    break;
            }

            f.Obj.SetActive(false);
        }

      //  blur.iterations = 0;
        //blur.enabled = false;
    }

    private void UpdateToAlpha(float newValue)
    {
        Fades[color_id].render.material.color = new Color(Fades[color_id].render.material.color.r, 
            Fades[color_id].render.material.color.g, 
            Fades[color_id].render.material.color.b, 
            newValue);
           
        blur.iterations = (int)(newValue * 10);
    }
}
