using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
    }

    public async Task<bool> IsTransactionSuccess(string txnHash)
    {
        BlockTransaction blockTransaction;
        do
        {
            blockTransaction = await Moralis.Web3Api.Native.GetTransaction(txnHash, Moralis.CurrentChain.EnumValue);

            if (blockTransaction == null)
            {
                Debug.Log($"Unable to get block transaction, Waiting for block transaction");
            }

            await UniTask.Delay(1000);

        } while (blockTransaction == null);

        string fromAddress = blockTransaction.FromAddress;
        string toAddress = blockTransaction.ToAddress;

        if (blockTransaction.ReceiptStatus == "0")
        {
            Debug.Log($"<color=red>Transfer Failed from {fromAddress} to {toAddress}.  TxnHash: {txnHash}</color>");
            Debug.Log(blockTransaction.ToString());
            return false;
        }
        else
        {
            Debug.Log($"<color=green>Transfered Success from {fromAddress} to {toAddress}.  TxnHash: {txnHash}</color>");
            Debug.Log(blockTransaction.ToString());
            return true;
        }
    }
}