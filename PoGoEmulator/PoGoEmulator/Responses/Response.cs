using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;
using PoGoEmulator.Models;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Requests;

namespace PoGoEmulator.Responses
{
    public static class Extensions
    {
        public static void DoAuth(this MyHttpContext context)
        {
            context.Response.StatusCode = 53;
            context.Response.RequestId = context.Request.RequestId;
            context.Response.ApiUrl = "pgorelease.nianticlabs.com/custom";
            context.Response.AuthTicket = new AuthTicket()
            {
                Start = ByteString.Empty,
                ExpireTimestampMs = DateTime.Now.UnixTime(new TimeSpan(0, 30, 0)),
                End = ByteString.Empty
            };
            Global.AuthenticatedUsers.AddOrUpdate(context.UserEmail, true, (k, v) => true);
        }

        public static void ProcessResponse(this MyHttpContext context, Request rq)
        {
            var type = rq.RequestType;
            CodedInputStream codedStream = new CodedInputStream(rq.RequestMessage.ToByteArray());
            var strType = $"POGOProtos.Networking.Requests.Messages.{type}Message";
            var tst = Activator.CreateInstance("POGOProtos.dll", strType);
            MethodInfo methodMergeFrom = tst?.GetType().GetMethods().ToList()
                .FirstOrDefault(p => p.ToString() == "Void MergeFrom(Google.Protobuf.CodedInputStream)");
            methodMergeFrom.Invoke(tst, new object[] { codedStream });

            switch (type)
            {
            }
        }
    }
}