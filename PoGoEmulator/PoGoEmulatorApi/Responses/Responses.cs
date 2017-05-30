//using System.Net.Http;
//using Google.Protobuf;
//using Google.Protobuf.Collections;
//using PoGoEmulatorApi.Controllers;
//using POGOProtos.Networking.Envelopes;

//// ReSharper disable once CheckNamespace
//namespace PoGoEmulatorApi
//{
//    public static class Response
//    {
//        public static HttpResponseMessage EnvelopResponse(this BaseRpcController brcontroller, RepeatedField<ByteString> returns = null)
//        {
//            if (returns != null)
//            {
//                brcontroller.Log.Dbg($"ReturnsCount:{returns.Count}");
//                brcontroller.ProtoResponse.Returns.AddRange(returns);
//            }
//            else
//            {
//                brcontroller.Log.Dbg($"ReturnsCount:null");
//            }

// if (brcontroller.ProtoRequest.AuthTicket != null) {
// brcontroller.Log.Dbg($"brcontroller.ProtoRequest.AuthTicket:
// {brcontroller.ProtoRequest.AuthTicket}"); brcontroller.ProtoResponse.AuthTicket = new AuthTicket()
// { }; }

//            brcontroller.Log.Dbg($"brcontroller.ProtoResponse.Unknown6.ResponseType: ADDED 1");
//            brcontroller.ProtoResponse.Unknown6.Add(new Unknown6Response()
//            {
//                ResponseType = 6,
//                Unknown2 = new Unknown6Response.Types.Unknown2()
//                {
//                    Unknown1 = 1
//                }
//            });
//            brcontroller.ProtoResponse.StatusCode = 1;
//            return brcontroller.Rpc();
//        }
//    }
//}