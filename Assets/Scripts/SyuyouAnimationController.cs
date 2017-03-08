using UnityEngine;
using System.Collections;

public class SyuyouAnimationController : MonoBehaviour {

    public Animator anim;
    public SoundEventController sound;

    private int life = 3;
    public bool Is_Already_Death = false;

	// Use this for initialization
	void Start ()
    {
        Is_Already_Death = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
      //  if (Input.GetKeyUp(KeyCode.Q))
        //    UpdateObsLogic();
	}

    public void UpdateObsLogic()
    {
        Debug.Log(life);

        if (--life < 0)
        {
            anim.SetBool("IS_ADMIT_DAMEGE", true);
            anim.SetBool("IS_ADMIT_DEATH", true);
            Is_Already_Death = true;
        }
        else StartCoroutine("DamegeEvent");
    }

    IEnumerator DamegeEvent()
    {
        anim.SetBool("IS_ADMIT_DAMEGE", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("IS_ADMIT_DAMEGE", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //音
            UpdateObsLogic();
        }
    }
}
