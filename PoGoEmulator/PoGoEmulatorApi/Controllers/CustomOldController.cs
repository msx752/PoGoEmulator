using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Database;
using PoGoEmulatorApi.Responses;
using POGOProtos.Networking.Envelopes;

namespace PoGoEmulatorApi.Controllers
{
    [System.Web.Http.RoutePrefix("custom_OLD")]
    public class CustomOldController : BaseRpcController
    {
        public CustomOldController(PoGoDbContext db) : base(db)
        {
            Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            RpcType = Enums.RpcRequestType.Custom;

            UpdatePlayerLocation();
            UpdateCachedUser();
        }

        [System.Web.Http.HttpPost]
        public override HttpResponseMessage Rpc()
        {
            try
            {
                Log.Debug($"HasSignature:{CachedCurrentUser.HasSignature}");
                if (CachedCurrentUser.HasSignature == false)
                {
                    if (ProtoResponse.Unknown6 != null)
                    {
                        //POGOProtos.Networking.Envelopes.Signature
                        //connectedClient.HttpContext.Request.Unknown6.Unknown2.EncryptedSignature
                        var signature = Encryption.Decrypt(
                               ProtoRequest.Unknown6.Unknown2.EncryptedSignature.ToByteArray());
                        var codedStream = new CodedInputStream(signature);
                        var sig = new Signature();
                        sig.MergeFrom(codedStream);
                        if (sig.DeviceInfo != null)
                        {
                            var usrd = CachedCurrentUser;
                            usrd.HasSignature = true;
                            usrd.IsIOS = (sig.DeviceInfo.DeviceBrand == "Apple");
                            bool updtrslt = WebApiApplication.AuthenticatedUsers.TryUpdate(UserEmail, usrd,
                                 CachedCurrentUser);
                            if (!updtrslt)
                            {
                                throw new Exception(" CONCURRENT ACCESS ERROR this shouldn't happen");
                            }
                        }
                    }
                }

                Log.Debug($"HasSignature:{CachedCurrentUser.HasSignature}, Platform:{CachedCurrentUser.Platform}");
                Log.Debug($"ProtoRequest.Requests.Count:{ProtoRequest.Requests.Count}");
                if (ProtoRequest.Requests.Count == 0)
                {
                    if (ProtoRequest.Unknown6 != null && ProtoRequest.Unknown6.RequestType == 6)
                    {
                        Log.Debug($"ProtoRequest.Unknown6.RequestType:{ProtoRequest.Unknown6.RequestType}");
                        this.EnvelopResponse();
                        return base.Rpc();
                    }
                    else
                    {
                        throw new Exception("Invalid Request!.");
                    }
                }
                RepeatedField<ByteString> requests = this.ProcessRequests();
                this.EnvelopResponse(requests);
                return base.Rpc();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }
    }
}