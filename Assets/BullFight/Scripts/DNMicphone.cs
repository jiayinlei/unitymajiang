﻿using System;
using System.Collections;
using UnityEngine;

public class DNMicphone : MonoBehaviour
{
    public enum AudioRecordResultState
    {
        Success,
        NoMicrophone,
        TooShort
    }

    private static DNMicphone _instance;
    public AudioSource audiorecord;
    private int deviceCount;
    private bool isRecording;

    private AudioClip recordedClip;
    private float recordTimer;
    private string sFrequency = "10000";
    private string sLog = "";

    public static DNMicphone GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        audiorecord = gameObject.GetComponent<AudioSource>();
    }


    private void Start()
    {
        var ms = Microphone.devices;
        deviceCount = ms.Length;
    }

    private void Update()
    {
        if (isRecording) recordTimer += Time.deltaTime;

        if (isRecording && (recordTimer >= 15))
            StopRecord();
    }

    

    public AudioRecordResultState StartRecord()
    {
        audiorecord.Stop();
        audiorecord.loop = false;
        audiorecord.mute = true;

        isRecording = true;
        recordTimer = 0;

        //RecordPageEntry.GetInstance().showRecordPage();
        if (Microphone.devices.Length <= 0)
            return AudioRecordResultState.NoMicrophone;

        recordedClip = Microphone.Start(null, false, 16, 10000);
        while (!(Microphone.GetPosition(null) > 0))
        {
        }

        Debug.Log("start record-------------");
        return AudioRecordResultState.Success;
    }

    /// <summary>
    ///     停止录制
    /// </summary>
    /// <returns>返回音频保存路径</returns>
    public byte[] StopRecord()
    {
        Debug.Log("stop record---------------");
       // RecordPageEntry.GetInstance().DestoryRecordPage();

        //Capture the current clip data
        //isRecording = false;
        isRecording = false;
        audiorecord.mute = false;

        if (recordTimer < 1f)
        {
            //todo
            Debug.Log("录音时间小于1s");
            Microphone.End(null);
            //ControllerBgMusic(1);
            return null;
        }

        var position = Microphone.GetPosition(null);
        var soundData = new float[recordedClip.samples*recordedClip.channels];
        recordedClip.GetData(soundData, 0);

        //Create shortened array for the data that was used for recording
        var newData = new float[position*recordedClip.channels];

        //Copy the used samples to a new array
        for (var i = 0; i < newData.Length; i++) newData[i] = soundData[i];

        recordedClip = AudioClip.Create(recordedClip.name,
            position,
            recordedClip.channels,
            recordedClip.frequency,
            false);

        recordedClip.SetData(newData, 0); //Give it the data from the old clip

        //Replace the old clip
        Microphone.End(null);

        //save to disk
        string recordedAudioPath;
        var data = WavUtility.FromAudioClip(recordedClip, out recordedAudioPath, false);

       // StartCoroutine(sendRecordMsg(data));
       // ControllerBgMusic(1);
        return data;
    }


    //private IEnumerator sendRecordMsg(byte[] data)
    //{
    //    DNVoiceSocket.GetInstance().Send(data);
    //    yield return new WaitForSeconds(0);
    //}

    //获取传入clip数据
    //public byte[] GetData(AudioClip clip)
    //{
    //    var data = new float[clip.samples*clip.channels];
    //    clip.GetData(data, 0);
    //    var bytes = new byte[data.Length*4];
    //    Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);
    //    return bytes;
    //}

    ////将数据设置到clip
    //public void SetData(byte[] bytes)
    //{
    //    var data = new float[bytes.Length/4];
    //    Buffer.BlockCopy(bytes, 0, data, 0, data.Length);

    //    var audioSource = GetComponent<AudioSource>();
    //    audioSource.clip = AudioClip.Create("RecordClip", bytes.Length, 1, 10000, false, false);
    //    audioSource.clip.SetData(data, 0);
    //    audioSource.mute = false;
    //}
}