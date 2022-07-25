using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisSDK
{
    public class Token : MonoBehaviour
    {
        public async Task<Erc20TokenBalance> GetBalance()
        {
            var user = Common.Instance.moralisUser;

            // get BSC native balance for a given address
            var balanceList = await Moralis.Web3Api.Account.GetTokenBalances(user.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            var balance = balanceList.SingleOrDefault(x => x.TokenAddress == user.ethAddress);
            return balance;
        }
    }
}