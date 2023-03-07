using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuGM : MonoBehaviour
{
    public GameObject SettingPanel;
    [SerializeField] private Toggle vibrationToggle;
    public static UnityAction<bool> OnVibrationchanged;

    private void Awake()
    {
        

    }

    // Start is called before the first frame update
    void Start()
    {
        int vibrate = PlayerPrefs.GetInt("Vibrate");
        if (vibrate == 1)
        {
            vibrationToggle.isOn = true;

            Handheld.Vibrate();
        }
        else
        {
            vibrationToggle.isOn = false;
        }

    }
    private void onvibchange(bool value)
    {
        Debug.Log("vibration:" + true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingPopUp()
    {
        SettingPanel.SetActive(true);
    }
    public void CLoseSettingPopUp()
    {
        SettingPanel.SetActive(false);
    }

    public void OnvibrationToggleChanged()
    {
        if(PlayerPrefs.HasKey("Vibrate"))
        {
            if(PlayerPrefs.GetInt("Vibrate") == 1)
            {
                PlayerPrefs.SetInt("Vibrate", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Vibrate", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 1);
        }
    }

}
