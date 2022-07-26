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

        public async Task<bool> Send(int tokenId, string toAddress)
        {
            string fromAddress = Common.Instance.moralisUser.ethAddress;
            string EIPTransferNFTABI = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"from\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"tokenId\",\"type\":\"uint256\"}],\"name\":\"transferFrom\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
            string FunctionName = "transferFrom";
            object[] inputParams = { fromAddress, toAddress, tokenId };
            HexBigInteger value = new HexBigInteger("0x0");
            HexBigInteger gas = new HexBigInteger("0");
            HexBigInteger gasprice = new HexBigInteger("0");

            string txnHash;
            try
            {
                // Execute the transaction.
                txnHash = await Moralis.ExecuteContractFunction(contractAddress: Common.Instance.contractId,
                    abi: EIPTransferNFTABI,
                    functionName: FunctionName,
                    args: inputParams,
                    value: value,
                    gas: gas,
                    gasPrice: gasprice);

                Debug.Log($"Transferring {tokenId} NFT ID from {fromAddress} to {toAddress}...  TxnHash: {txnHash}");
            }
            catch (Exception exp)
            {
                Debug.Log($"<color=red>Transfer of {tokenId} NFT ID from {fromAddress} to {toAddress} failed! with error {exp}</color>");
                return false;
            }

            return await Common.Instance.IsTransactionSuccess(txnHash);
        }
    }
}