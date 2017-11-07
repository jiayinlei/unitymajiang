using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAudioManager  {
    private static DynamicAudioManager instance;

    public static DynamicAudioManager Instance {
        get {
            if (instance == null) {
                instance = new DynamicAudioManager();
            }
            return instance;
        }
    }
    public Dictionary<string, AudioClip> audioClip = new Dictionary<string, AudioClip>();
    AudioClip[] audios;
    public void InitAudioSource() {
        audios = Resources.LoadAll<AudioClip>("Music(UnUsed)/UsefulWord");
        foreach (var a in audios) {
            audioClip.Add(a.name,a);
            //Debug.Log(a.name.Length);
        }
        audios = null;
        audios = Resources.LoadAll<AudioClip>("Audio/NiuType");
        foreach (var a in audios) {
            audioClip.Add(a.name, a);
            //Debug.Log(a.name.Length);
        }
        audios = null;
        audios = Resources.LoadAll<AudioClip>("Audio/OptAudio");
        foreach (var a in audios) {
            audioClip.Add(a.name, a);
            //Debug.Log(a.name.Length);
        }
    }
    public AudioClip BaseNameGetAudio(string name) {
        AudioClip audio;
        audioClip.TryGetValue(name,out audio);
        //foreach (var a in audioClip) {
        //    Debug.Log(a);
        //}
        //Debug.Log(name+"+"+audio);
        return audio;
    }
}
