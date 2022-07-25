﻿/**
 *           Module: Portfolio.cs
 *  Descriptiontion: Solana Portfolio
 *           Author: Moralis Web3 Technology AB, 559307-5988 - David B. Goodrich
 *  
 *  MIT License
 *  
 *  Copyright (c) 2021 Moralis Web3 Technology AB, 559307-5988
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.
 */
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MoralisUnity.SolanaApi.Models
{
    [DataContract]
    public class Portfolio
    {
        [DataMember(Name = "nativeBalance", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nativeBalance")]
        public NativeBalance NativeBalance { get; set; }

        [DataMember(Name = "nfts", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nfts")]
        public List<SplNft> Nfts { get; set; }

        [DataMember(Name = "tokens", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "tokens")]
        public List<SplTokenBalanace> Tokens { get; set; }
    }
}