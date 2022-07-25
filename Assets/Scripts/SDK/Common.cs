using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

public class Common : MonoBehaviour
{
    #region Singleton
    private static Common _instance;

    public static Common Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Common>();
            }

            return _instance;
        }
    }
    #endregion

    public MoralisUser moralisUser;
    public string contractId = "0xcE12E4901F6617C88717D1Fe85F03CeB7f456f5D";

    private async void Start()
    {
        moralisUser = await Moralis.GetUserAsync();
        contractId = contractId.ToLower();
    }
}