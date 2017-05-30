using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using PoGoEmulatorApi.Controllers;
using PoGoEmulatorApi.Database;
using PoGoEmulatorApi.Database.Tables;
using POGOProtos.Enums;
using POGOProtos.Networking.Responses;

namespace PoGoEmulatorApi.Responses.Packets
{
    public class GetPlayer
    {
        public ByteString From(BaseRpcController brc)
        {
            User usr =
                brc.Database.Users.SingleOrDefault(
                    p => p.email == brc.UserEmail);

            GetPlayerResponse gpr = new GetPlayerResponse();
            gpr.Success = true;
            //update with database
            gpr.PlayerData = new POGOProtos.Data.PlayerData()
            {
                CreationTimestampMs = (long)DateTime.Now.ToUnixTime(new TimeSpan()),
                Username = usr.username,
                Team = (TeamColor)usr.team,
                Avatar = new POGOProtos.Data.Player.PlayerAvatar()
                {
                    Skin = 1,
                    Hair = 1,
                    Shirt = 1,
                    Pants = 1,
                    Eyes = 1,
                    Backpack = 1,
                    Hat = 1,
                    Shoes = 1
                },
                MaxPokemonStorage = 250,
                MaxItemStorage = 350,
                ContactSettings = new POGOProtos.Data.Player.ContactSettings()
                {
                    SendMarketingEmails = usr.send_marketing_emails == 1,
                    SendPushNotifications = usr.send_push_notifications == 1
                },
                RemainingCodenameClaims = 10,
            };
            gpr.PlayerData.TutorialState.AddRange(new List<TutorialState>()
                    {
                        (TutorialState)1,
                        (TutorialState)0,
                        (TutorialState)3,
                        (TutorialState)4,
                        (TutorialState)7
                    });
            return gpr.ToByteString();
        }
    }
}