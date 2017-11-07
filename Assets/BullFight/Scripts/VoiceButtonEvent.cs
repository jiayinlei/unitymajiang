using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class VoiceButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    string device = "";
    bool isRecording;
    float recordTimer;
    bool isEnd = false;
    private AudioClip audioClip;
    private AudioSource audioSource;
    private GameObject voicePanel;
    private DNMicphone mic;
    Thread thread ;
    //string sceneName = SceneManager.GetActiveScene().name;
    //public Text debug;
    public void OnPointerDown(PointerEventData eventData) {
        Button btn= transform.GetComponent<Button>();
        btn.PlayButtonVoice();
        if (SceneManager.GetActiveScene().name == "GameHall") {
            voicePanel.SetActive(true);
        } else {
            DNUIManager.Instance.PushPanel(UIPanelType.VoicePanel);
        }
        //Thread thread = new Thread(StartRecognize);
        //thread.Start();
        //Debug.Log("1:"+Time.time);
        audioClip = Microphone.Start(null, false, 16, 10000);
        isRecording = true;
        //StartCoroutine(StartRecognize());
        //Debug.Log("1:"+Time.time);
        //isEnd = true;
        //mic.StartRecord();
        //debug.text = audioClip.ToString() + "+" + device;
    }
    IEnumerator StartRecognize() {
        yield return new WaitForSeconds(0);
        audioClip = Microphone.Start(null, false, 16, 10000);
        for (int i = 0; i < Microphone.devices.Length; i++) {
            if (Microphone.IsRecording(Microphone.devices[i])) {
                device = Microphone.devices[i];
                isRecording = true;
                break;
            } else {
                //device = Microphone.devices[i];
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData) {
        //Debug.Log(Time.time);
        if (SceneManager.GetActiveScene().name == "GameHall") {
            voicePanel.SetActive(false);
        } else {
            DNUIManager.Instance.PopPanel();
        }
        //StartCoroutine(ClosePanel());
        //mic.StopRecord();
        if (isRecording) {
            //Debug.Log(Time.time);
            //Debug.Log("a");
            //Stop();
            StartCoroutine(Stop());
            isRecording = false;
            //isEnd = true;
        }
    }
    void ClosePanel() {

    }
    IEnumerator Stop() {
        yield return new WaitForSeconds(0);
        if (recordTimer < 1f) {
            //todo
            Debug.Log("录音时间小于1s");
            Microphone.End(null);
            yield break;
        }
        recordTimer = 0;
        //Debug.Log(Time.time);
        var position = Microphone.GetPosition(null);
        var soundData = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(soundData, 0);
        var newData = new float[position * audioClip.channels];
        for (var i = 0; i < newData.Length; i++) {
            newData[i] = soundData[i];
        }
        //Debug.Log(Time.time);
        audioClip = AudioClip.Create(audioClip.name, audioClip.samples, audioClip.channels, audioClip.frequency, false);
        audioClip.SetData(newData, 0);
        Microphone.End(null);
        string recordedAudioPath;
        var data = WavUtility.FromAudioClip(audioClip, out recordedAudioPath, false);
        //RocordWebSocket.GetInstance().Send(data);
        DNVoiceSocket.GetInstance().Send(data);
        //Debug.Log(Time.time);
    }
    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name == "GameHall") {

            voicePanel = Instantiate(Resources.Load<GameObject>("PanelPrefabs/VoicePanel"));
            voicePanel.transform.SetParent(transform);
            voicePanel.transform.localPosition = new Vector3(0,200,0);
            voicePanel.transform.localScale = Vector3.one;
            voicePanel.SetActive(false);
        }
        //mic = GameObject.Find("GameManager").GetComponent<DNMicphone>();
        //audioSource = GameObject.Find("StaticSource").GetComponent<AudioSource>();
    }
    private void Update() {

        if (isRecording)
            recordTimer += Time.deltaTime;

        if (isRecording && (recordTimer >= 15)) {
            recordTimer = 0;
//isEnd = true;
            isRecording = false;
            //Stop();
            StartCoroutine(Stop());
        }
        //recordTimer += Time.deltaTime;
    }
}
