using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using UnityEngine;

namespace MoralisSDK
{
    public class Native : MonoBehaviour
    {
        #region Singleton
        private static Native _instance;

        public static Native Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Native>();
                }

                return _instance;
            }
        }
        #endregion

        public async Task<decimal> GetBalance()
        {
            // get BSC native balance for a given address
            NativeBalance BSCbalance = await Moralis.Web3Api.Account.GetNativeBalance(Common.Instance.moralisUser.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            var intBalance = BigIntegerExtensions.ParseInvariant(BSCbalance.Balance);
            var balance = UnitConversion.Convert.FromWei(intBalance);
            return balance;
        }

        public async Task<bool> Send(int transferAmount, string toAddress)
        {
            string fromAddress = Common.Instance.moralisUser.ethAddress;

            var HexBigIntegerValue = new HexBigInteger(UnitConversion.Convert.ToWei(transferAmount));
            string txnHash = "";

            try
            {
                // Execute the transaction.
                txnHash = await Moralis.SendTransactionAsync(toAddress, HexBigIntegerValue);
                Debug.Log($"Transferring {transferAmount} ETH from {fromAddress} to {toAddress}...  TxnHash: {txnHash}");
            }
            catch (Exception exp)
            {
                Debug.Log($"<color=red>Transfer of {transferAmount} ETH from {fromAddress} to {toAddress} failed! with error {exp}</color>");
                return false;
            }

            BlockTransaction blockTransaction = null;

            do
            {
                blockTransaction = await Moralis.Web3Api.Native.GetTransaction(txnHash, Moralis.CurrentChain.EnumValue);
                if (blockTransaction == null)
                {
                    Debug.LogError($"Unable to get block transaction, envaluation failed");
                    return false;
                }

                await UniTask.Delay(1000);

            } while (blockTransaction.ReceiptStatus == "0");

            Debug.Log($"<color=green>Transfered {transferAmount} ETH from {fromAddress} to {toAddress}.  TxnHash: {txnHash}</color>");
            return true;
        }
    }
}