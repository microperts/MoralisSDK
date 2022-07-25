using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisSDK
{
    public class Native : MonoBehaviour
    {
        public async Task<NativeBalance> GetBalance()
        {
            // get BSC native balance for a given address
            NativeBalance BSCbalance = await Moralis.Web3Api.Account.GetNativeBalance(Common.Instance.moralisUser.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            return BSCbalance;
        }
    }
}