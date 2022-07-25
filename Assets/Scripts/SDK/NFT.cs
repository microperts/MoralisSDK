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

        public enum NftFilter
        {
            Name,
            Description,
            Attributes,
            Global
        }

        public async Task<int> GetUniqueCount()
        {
            NftOwnerCollection polygonNFTs = await Moralis.Web3Api.Account.GetNFTs(Common.Instance.moralisUser.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            return polygonNFTs.Result.Count;
        }

        public async Task<NftMetadataCollection> GetNFT(string query, NftFilter filter)
        {
            string filterString = "global";

            switch (filter)
            {
                case NftFilter.Name:
                    filterString = "name";
                    break;
                case NftFilter.Description:
                    filterString = "description";
                    break;
                case NftFilter.Attributes:
                    filterString = "attributes";
                    break;
                case NftFilter.Global:
                    filterString = "global";
                    break;
                default:
                    break;
            }

            NftMetadataCollection nfts = await Moralis.Web3Api.Token.SearchNFTs(q: query, Moralis.CurrentChain.EnumValue, filter: filterString);
            return nfts;
        }

        public async Task<NftOwnerCollection> GetAllNFTs()
        {
            NftOwnerCollection nfts = await Moralis.Web3Api.Account.GetNFTs(Common.Instance.moralisUser.ethAddress.ToLower(), Moralis.CurrentChain.EnumValue);
            return nfts;
        }
    }
}