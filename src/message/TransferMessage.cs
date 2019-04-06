﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace binance.dex.sdk.message
{
    //@JsonIgnoreProperties(ignoreUnknown = true)
    /// <summary>
    /// Json property order is alphabetic
    /// </summary>
    public class TransferMessage : ITransactionMessage
    {
        [JsonProperty("inputs", Order = 1)]
        public List<InputOutput> inputs;

        [JsonProperty("outputs", Order = 2)]
        public List<InputOutput> outputs;
    }
}
