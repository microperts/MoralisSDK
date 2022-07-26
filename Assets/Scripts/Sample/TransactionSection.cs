using System.Collections;
using System.Collections.Generic;
using MoralisSDK;
using TMPro;
using UnityEngine;

public class TransactionSection : MonoBehaviour
{
    public TMP_InputField nativeAmount;
    public TMP_InputField nativeTarget;

    public TMP_InputField tokenAmount;
    public TMP_InputField tokenTarget;

    public TMP_InputField nftTokenId;
    public TMP_InputField nftTarget;

    public async void OnClick_SendNative()
    {
        if (nativeAmount.text == "") { return; }
        StatusSection.Instance.SetStatus("Working");
        bool result = await Native.Instance.Send(decimal.Parse(nativeAmount.text),nativeTarget.text);
        if (!result)
        {
            StatusSection.Instance.SetStatus("Error");
        }
        else
        {
            StatusSection.Instance.SetStatus("Success");
        }
    }

    public async void OnClick_SendToken()
    {
        if (tokenAmount.text == "") { return; }
        StatusSection.Instance.SetStatus("Working");
        bool result = await Token.Instance.Send(int.Parse(tokenAmount.text),tokenTarget.text);
        if (!result)
        {
            StatusSection.Instance.SetStatus("Error");
        }
        else
        {
            StatusSection.Instance.SetStatus("Success");
        }
    }

    public async void OnClick_SendNFT()
    {
        if (nftTokenId.text == "") { return; }
        if (nftTarget.text == "") { return; }
        StatusSection.Instance.SetStatus("Working");
        bool result = await NFT.Instance.Send(1,nftTarget.text);
        if (!result)
        {
            StatusSection.Instance.SetStatus("Error");
        }
        else
        {
            StatusSection.Instance.SetStatus("Success");
        }
    }
}