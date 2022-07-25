using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoralisUnity;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisSDK
{
    public class NFT : MonoBehaviour
    {
        #region Singleton
        private static NFT _instance;

        public static NFT Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<NFT>();
                }

                return _instance;
            }
        }
        #endregion

        public async Task<int> GetUniqueCount()
        {
            NftOwnerCollection polygonNFTs = await Moralis.Web3Api.Account.GetNFTs(Common.Instance.moralisUser.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            return polygonNFTs.Result.Count;
        }
    }
}