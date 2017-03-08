using UnityEngine;
using System.Collections;

public class SyoukaController : MonoBehaviour {

    public Animator anim;
    public StimulusController stimulus;
   // public MoveManager player;
    public SoundEventController sound;
    

	// Use this for initialization
	void Start ()
    {
        Init();
	}

    public void UpdateSyoukaEvent(StageManager.SCENE_TYPE type)
    {
        switch(type)
        {
            case StageManager.SCENE_TYPE.SYOUTYOU_1:
                StartCoroutine(UpdateSyoukaParameter());
                anim.SetBool("IS_ADMIT_SYOUKA", true);
                break;
            case StageManager.SCENE_TYPE.DAITYOU_1:
                StopEvent();
                StartCoroutine(UpdateKoukaParameter());
                anim.SetBool("IS_ADMIT_KOUKA", true);
                break;
        }
    }

    public void Init()
    {
        StopEvent();

        anim.SetBool("IS_ADMIT_SYOUKA", false);
        anim.SetBool("IS_ADMIT_KOUKA", false);

        anim.SetBool("IS_ADMIT_INIT",true);
        anim.SetBool("IS_ADMIT_INIT", false);
    }

    public IEnumerator UpdateSyoukaParameter(int time=20)
    {
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            int normalize =  500+(4500*(time-0) / (20-0));
            sound.UpdatePitch(normalize);
         //   stimulus.UpdateStimulusPitch(normalize);
        }
    }

    public IEnumerator UpdateKoukaParameter(int time=20)
    {
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            int normalize = 5000 - (4500 * (time - 0) / (20 - 0));
            sound.UpdatePitch(normalize);
           // stimulus.UpdateStimulusPitch(normalize);
        }

    }

    public void StopEvent()
    {
        StopAllCoroutines();
    }
}
