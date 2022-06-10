Binance DEX C# SDK
==================

Source code is under MIT License (MIT)

C# SDK is inspired by Binance DEX Java SDK, (used a lot in previous Binance DEX "competition")

Project uses following NuGet packages:
- NBitcoin
- Google.Protobuf
- RestSharp
- Newtonsoft.Json
- WebsocketRPC.Standalone

SDK supports:
- Creating Wallets
- Creating Signature
- Broadcast to Binance DEX chain
- HTTP API (https://docs.bnbchain.org/docs/beaconchain/develop/api-reference/dex-api/paths)
- WebSockets Streams (https://docs.bnbchain.org/docs/beaconchain/develop/api-reference/dex-api/ws-streams)
- Node RPC (https://docs.bnbchain.org/docs/beaconchain/develop/api-reference/node-rpc)

Wallet
------

Create wallet with private key string value either for TEST_NET or PROD:

``` cs
    Wallet wallet = Wallet.FromPrivateKey(
        "6d3cfc67595db1523915219718a31fa5b467d099a51035d8fb82ea9841496f09",
        BinanceDexEnvironment.TEST_NET);
```

HttpApi
-------

HttpApi objects:
- Times
- Infos
- Validator
- Peer
- Account
- AccountSequence
- Transaction
- Token
- Market
- FeeData
- MarketDepth
- TransactionMetadata
- KLines
- OrderList
- Order
- TickerStatistics
- TradePage
- TxPage

Broadcast objects:
- CancelOrder
- MultiTransfer
- NewOrder
- TokenFreeze
- TokenUnfreeze
- Transfer
- Vote

Example how to transfer founds:

``` cs
    HttpApiClient httpApiClient = new HttpApiClient(BinanceDexEnvironment.TEST_NET);
    Wallet wallet = Wallet.FromPrivateKey(
        "6d3cfc67595db1523915219718a31fa5b467d099a51035d8fb82ea9841496f09",
        BinanceDexEnvironment.TEST_NET);

    Transfer transfer = new Transfer
    {
        Coin = "PND-943",
        FromAddress = wallet.Address,
        ToAddress = "tbnb18086qc9yxtk5ufddple8upf0k3072vvagpm2ml",
        Amount = "0.05"
    };

    TransactionRequest assmebler = new TransactionRequest(wallet, TransactionOption.DefaultInstace);
    string body = assmebler.BuildTransfer(transfer);
    List<TransactionMetadata> result = httpApiClient.Broadcast(body);
```

Web socket streams
------------------

Web socket stream objects:
- OrdersData
- AccountsData
- TransfersData
- TradesData
- MarketDiff
- MarketDepth
- KLine
- Ticker
- AllTickersData
- MiniTicker
- AllMiniTickersData
- Blockheight

Example how to listen web socket stream:

``` cs
    public class Program
    {
        public static void Main(string[] args)
        {
            WebSocketClient client = new WebSocketClient();
            client.Env = BinanceDexEnvironment.TEST_NET;
            client.Topic = ETopic.Blockheight;
            client.StreamData += OnStreamData;
            client.Connect();

            Console.ReadLine();
        }

        private static void OnStreamData(object sender, IStreamData data)
        {
            Blockheight blockheight = (Blockheight)data;
            Console.WriteLine(blockheight.BlockHeight);
        }
    }
```

Node RPC
--------

Node RPC endpoints and response objects:
- AbciInfo: ResponseData 
- ConsensusState: ConsensusRoundStateData
- DumpConsensusState: DumpRoundStateData
- NetInfo: ResultNetInfo
- Genesis: ResultGenesis
- Health: ResultHealth
- NumUnconfirmedTxs: ResultUnconfirmedTxs
- Status: ResultStatus
- AbciQuery: ResultAbciQuery
- Block: ResultBlock
- BlockByHash: ResultBlock
- BlockResults: ResultBlockResults
- Blockchain: ResultBlockchainInfo
- Commit: ResultCommit
- ConsensusParams: ResultConsensusParams
- Tx: ResultTx
- TxSearch: ResultTxSearch
- UnconfirmedTxs: ResultUnconfirmedTxs
- Validators: ResultValidators
- BroadcastTxAsync: ResultBroadcastTx
- BroadcastTxCommit: ResultBroadcastTxCommit
- BroadcastTxSync: ResultBroadcastTx

Example how to communicate with Node RPC:

``` cs
    NodeRpcClient nodeRpcClient = new NodeRpcClient("https://data-seed-pre-0-s1.binance.org");
    ResponseData responseData = nodeRpcClient.AbcInfo();
```
