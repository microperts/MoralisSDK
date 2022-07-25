using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuerySection : MonoBehaviour
{
    public Button btnNativeTokenCount;
    public Button btnErc20TokenCount;
    public Button btnNftCount;

    public TMP_InputField ipfContractId;

    public void OnClick_NativeTokenCount()
    {
        Debug.Log("Quering Native Token Count");
    }

    public void OnClick_ERC20TokenCount()
    {
        Debug.Log("Quering ERC20 Token Count");
    }

    public void OnClick_NFTCountByContract()
    {
        Debug.Log("Quering NFT Count By Contract");
    }
}
