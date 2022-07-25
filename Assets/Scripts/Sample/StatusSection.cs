using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusSection : MonoBehaviour
{
    #region Singleton
    private static StatusSection _instance;

    public static StatusSection Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StatusSection>();
            }

            return _instance;
        }
    }
    #endregion

    public TextMeshProUGUI statusText;

    public void SetStatus(string txt)
    {
        statusText.text = txt;
    }
}