using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Setting 
{
    public static UnityAction<bool> OnVibrationchanged;
    private static bool vibrationEnabled;

   

    public static bool VibrationEnabled
    {
        get{
            return vibrationEnabled; 
        }
        set{ 
            vibrationEnabled=value;
            PlayerPrefs.SetInt("VibrationEnabled", (value ? 1 : 0));
        }
    }

    static Setting()
    {
        vibrationEnabled=(PlayerPrefs.GetInt("VibrationEnabled",0)==0 ? false : true);
    }
}
