using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Controllers;
using POGOProtos.Networking.Envelopes;

// ReSharper disable once CheckNamespace
namespace PoGoEmulatorApi
{
    public static class Response
    {
        public static void EnvelopResponse(this BaseRpcController brcontroller, RepeatedField<ByteString> returns = null)
        {
            if (returns != null)
            {
                brcontroller.ProtoResponse.Returns.AddRange(returns);
            }

            if (brcontroller.ProtoRequest.AuthTicket != null)
                brcontroller.ProtoResponse.AuthTicket = new AuthTicket() { };

            brcontroller.ProtoResponse.Unknown6.Add(new Unknown6Response()
            {
                ResponseType = 6,
                Unknown2 = new Unknown6Response.Types.Unknown2()
                {
                    Unknown1 = 1
                }
            });
            brcontroller.ProtoResponse.StatusCode = 1;
        }
    }
}