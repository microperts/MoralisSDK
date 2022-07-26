using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
using Nethereum.Hex.HexTypes;
using Nethereum.Util;
using UnityEngine;

namespace MoralisSDK
{
    public class Token : MonoBehaviour
    {
        #region Singleton
        private static Token _instance;

        public static Token Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<Token>();
                }

                return _instance;
            }
        }
        #endregion

        public async Task<decimal> GetBalance()
        {
            var user = Common.Instance.moralisUser;

            // get BSC native balance for a given address
            var balanceList = await Moralis.Web3Api.Account.GetTokenBalances(user.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            var tokenBalance = balanceList.FirstOrDefault(x => x.TokenAddress.ToLower() == Common.Instance.contractId.ToLower());

            if (tokenBalance == null)
            {
                Debug.LogError($"Token not found");
                return 0;
            }

            var bigIntBalance = BigIntegerExtensions.ParseInvariant(tokenBalance.Balance);
            var intDecimals = int.Parse(tokenBalance.Decimals);
            var balance = UnitConversion.Convert.FromWei(bigIntBalance,intDecimals);

            return balance;
        }

        public async Task<bool> Send(int transferAmount, string toAddress)
        {
            string fromAddress = Common.Instance.moralisUser.ethAddress;
            string EIPTransferTokenABI = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amount\",\"type\":\"uint256\"}],\"name\":\"transfer\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
            string FunctionName = "transfer";
            var DAIInWei = UnitConversion.Convert.ToWei(transferAmount,18);
            object[] inputParams = { toAddress, DAIInWei.ToString() };
            HexBigInteger value = new HexBigInteger("0x0");
            HexBigInteger gas = new HexBigInteger("0");
            HexBigInteger gasprice = new HexBigInteger("0");

            string txnHash;
            try
            {
                // Execute the transaction.
                txnHash = await Moralis.ExecuteContractFunction(
                    contractAddress: Common.Instance.contractId,
                    abi: EIPTransferTokenABI, 
                    functionName: FunctionName, 
                    args: inputParams,
                    value: value,
                    gas: gas,
                    gasPrice: gasprice);

                Debug.Log($"Transferring {transferAmount} Tokens from {fromAddress} to {toAddress}...  TxnHash: {txnHash}");
            }
            catch (Exception exp)
            {
                Debug.Log($"<color=red>Transfer of {transferAmount} Tokens from {fromAddress} to {toAddress} failed! with error {exp}</color>");
                return false;
            }

            return await Common.Instance.IsTransactionSuccess(txnHash);
        }
    }
}