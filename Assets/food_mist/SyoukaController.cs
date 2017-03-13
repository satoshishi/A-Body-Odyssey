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
            float normalize = 0.5f + (0.5f * (time - 0) / (20 - 0));
            normalize = normalize > 1f ? 1f : normalize < 0.5f ? 0.5f : normalize;
            sound.Pitch(normalize);
            stimulus.Pitch(normalize,StimulusController.Stimulus_Type.STIMULUS);
        }
    }

    public IEnumerator UpdateKoukaParameter(int time=20)
    {
        while (time >= 0)
        {
            yield return new WaitForSeconds(1f);
            time--;
            float normalize = 1f - (0.5f * (time - 0) / (20 - 0));
            normalize = normalize > 1f ? 1f : normalize < 0.5f ? 0.5f : normalize;
            sound.Pitch(normalize);
            stimulus.Pitch(normalize, StimulusController.Stimulus_Type.STIMULUS);
        }

    }

    public void StopEvent()
    {
        StopAllCoroutines();
    }
}
