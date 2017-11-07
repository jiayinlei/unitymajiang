using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// unity Component Extention
/// </summary>
public static class UnityComponentExtention {
    public static void PlayButtonVoice(this Button go) {
        try {
            LoginAndShare.Controller.BtnVoice();
        } catch (Exception ex) {

        }
    }
    public static void PlayButtonVoice(this Transform go) {
        try {
            LoginAndShare.Controller.BtnVoice();
        } catch (Exception ex) {

        }

    }
}