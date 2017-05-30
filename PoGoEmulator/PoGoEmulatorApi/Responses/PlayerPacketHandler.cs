using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using PoGoEmulatorApi.Controllers;
using PoGoEmulatorApi.Database.Tables;
using PoGoEmulatorApi.Responses.Packets;
using POGOProtos.Enums;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

namespace PoGoEmulatorApi.Responses
{
    public static class PlayerPacketHandler
    {
        public static ByteString GetPacket(this BaseRpcController brc, RequestType typ, object msg)
        {
            switch (typ)
            {
                case RequestType.SetFavoritePokemon:
                    SetFavoritePokemonResponse sfp = new SetFavoritePokemonResponse();
                    SetFavoritePokemonMessage m = (SetFavoritePokemonMessage)msg;

                    var usr = brc.Database.Users.SingleOrDefault(p => p.email == brc.UserEmail);
                    var owned = brc.Database.OwnedPokemons.SingleOrDefault(p => p.owner_id == usr.id && m.PokemonId == p.id);
                    if (owned != null)
                    {
                        m.IsFavorite = true;
                        owned.favorite = true;
                        brc.Database.OwnedPokemons.Update(owned);
                        sfp.Result = SetFavoritePokemonResponse.Types.Result.Success;
                    }
                    else
                    {
                        sfp.Result = SetFavoritePokemonResponse.Types.Result.ErrorPokemonNotFound;
                    }
                    return sfp.ToByteString();

                //case RequestType.LevelUpRewards:
                //    LevelUpRewardsResponse lur = new LevelUpRewardsResponse();
                //    return lur.ToByteString();

                //case RequestType.ReleasePokemon:
                //    ReleasePokemonResponse rp = new ReleasePokemonResponse();
                //    return rp.ToByteString();

                //case RequestType.UpgradePokemon:
                //    UpgradePokemonResponse up = new UpgradePokemonResponse();
                //    return up.ToByteString();

                //case RequestType.GetPlayerProfile:
                //    LevelUpRewardsResponse gpp = new LevelUpRewardsResponse();
                //    return gpp.ToByteString();

                //case RequestType.SetAvatar:
                //    LevelUpRewardsResponse sa = new LevelUpRewardsResponse();
                //    return sa.ToByteString();

                case RequestType.GetPlayer:
                    return new GetPlayer().From(brc);

                    //case RequestType.GetInventory:
                    //    LevelUpRewardsResponse gi = new LevelUpRewardsResponse();
                    //    return gi.ToByteString();

                    //case RequestType.GetAssetDigest:
                    //    LevelUpRewardsResponse gad = new LevelUpRewardsResponse();
                    //    return gad.ToByteString();

                    //case RequestType.NicknamePokemon:
                    //    LevelUpRewardsResponse np = new LevelUpRewardsResponse();
                    //    return np.ToByteString();

                    //case RequestType.GetHatchedEggs:
                    //    LevelUpRewardsResponse ghe = new LevelUpRewardsResponse();
                    //    return ghe.ToByteString();

                    //case RequestType.CheckAwardedBadges:
                    //    LevelUpRewardsResponse cab = new LevelUpRewardsResponse();
                    //    return cab.ToByteString();

                    //case RequestType.RecycleInventoryItem:
                    //    LevelUpRewardsResponse rii = new LevelUpRewardsResponse();
                    //    return rii.ToByteString();

                    //case RequestType.ClaimCodename:
                    //    LevelUpRewardsResponse cc = new LevelUpRewardsResponse();
                    //    return cc.ToByteString();
            }
            throw new Exception($"unknown (Player) Returns type: {typ}");
        }
    }
}