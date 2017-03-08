using UnityEngine;
using System.Collections;

public class microphone : MonoBehaviour {

    public AudioSource audio;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().clip = Microphone.Start(null, true, 1, 44100);  // マイクからのAudio-InをAudioSourceに流す
        GetComponent<AudioSource>().loop = true;                                      // ループ再生にしておく
      //  GetComponent<AudioSource>().mute = true;                                      // マイクからの入力音なので音を流す必要がない
        while (!(Microphone.GetPosition("") > 0)) { }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        //float vol = GetAveragedVolume();
        //print(vol);

    
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256.0f;
    }
}
