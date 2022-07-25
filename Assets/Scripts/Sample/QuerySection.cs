using System.Collections;
using System.Collections.Generic;
using MoralisSDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuerySection : MonoBehaviour
{
    public Button btnNativeTokenCount;
    public Button btnErc20TokenCount;
    public Button btnNftCount;

    public TMP_InputField ipfContractId;

    public async void OnClick_NativeTokenCount()
    {
        Debug.Log("Quering Native Token Count");
        var balance = await Native.Instance.GetBalance();
        StatusSection.Instance.SetStatus(balance.ToString());
    }

    public async void OnClick_ERC20TokenCount()
    {
        Common.Instance.contractId = ipfContractId.text;

        Debug.Log("Quering ERC20 Token Count");
        var balance = await Token.Instance.GetBalance();
        StatusSection.Instance.SetStatus(balance.ToString());
    }

    public async void OnClick_NFTCountByContract()
    {
        Common.Instance.contractId = ipfContractId.text;

        Debug.Log("Quering NFT Count By Contract");
        var balance = await NFT.Instance.GetUniqueCount();
        StatusSection.Instance.SetStatus(balance.ToString());
    }
}
