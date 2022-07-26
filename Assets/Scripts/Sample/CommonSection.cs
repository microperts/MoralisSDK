using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommonSection : MonoBehaviour
{
    public TMP_InputField contractAddress;

    public void OnClick_Update()
    {
        if (contractAddress.text == "")
        {
            StatusSection.Instance.SetStatus("Provide a valid Contract Address");
            return;
        }

        Common.Instance.contractId = contractAddress.text;
        StatusSection.Instance.SetStatus("Contract Address Updated");
    }
}