using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
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
            var tokenBalance = balanceList.FirstOrDefault(x => x.TokenAddress == Common.Instance.contractId);

            var intBalance = BigIntegerExtensions.ParseInvariant(tokenBalance.Balance);
            var balance = UnitConversion.Convert.FromWei(intBalance);

            return balance;
        }
    }
}