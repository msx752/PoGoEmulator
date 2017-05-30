﻿using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Controllers;
using PoGoEmulatorApi.Database.Tables;
using PoGoEmulatorApi.Responses.Packets;
using POGOProtos.Enums;
using POGOProtos.Inventory;
using POGOProtos.Inventory.Item;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;

namespace PoGoEmulatorApi.Responses
{
    public static class PlayerPacketHandler
    {
        public static ByteString GetPacket(this AuthorizedController brc, RequestType typ, object msg)
        {
            switch (typ)
            {
                case RequestType.SetFavoritePokemon:
                    SetFavoritePokemonResponse sfp = new SetFavoritePokemonResponse();
                    SetFavoritePokemonMessage m = (SetFavoritePokemonMessage)msg;

                    var owned = brc.GetPokemonById((ulong)m.PokemonId);
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

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(SetFavoritePokemonResponse)}");
                    return sfp.ToByteString();

                case RequestType.LevelUpRewards:
                    LevelUpRewardsResponse lur = new LevelUpRewardsResponse();
                    lur.Result = LevelUpRewardsResponse.Types.Result.Success;
                    lur.ItemsAwarded.AddRange(new RepeatedField<ItemAward>()
                    {
                        new ItemAward(){ ItemId=ItemId.ItemPokeBall, ItemCount=2},
                        new ItemAward(){ ItemId=ItemId.ItemTroyDisk, ItemCount=2}
                    });
                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(LevelUpRewardsResponse)}");
                    return lur.ToByteString();

                case RequestType.ReleasePokemon:
                    ReleasePokemonResponse rp = brc.ReleasePokemon((ReleasePokemonMessage)msg);

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(ReleasePokemonResponse)}");
                    return rp.ToByteString();

                case RequestType.UpgradePokemon:
                    UpgradePokemonResponse up = new UpgradePokemonResponse();
                    //UpgradePokemonMessage upm = (UpgradePokemonMessage)msg;
                    //var uptpkmn = brc.GetPokemonById(upm.PokemonId);
                    //if (uptpkmn!=null)
                    //{
                    //}
                    //else
                    //{
                    //    up.Result = UpgradePokemonResponse.Types.Result.ErrorPokemonNotFound;
                    //}

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(UpgradePokemonResponse)}");
                    return up.ToByteString();

                case RequestType.GetPlayerProfile:
                    GetPlayerProfileResponse gpp = new GetPlayerProfileResponse();
                    gpp.Result = GetPlayerProfileResponse.Types.Result.Success;
                    gpp.StartTime = (long)DateTime.Now.ToUnixTime();
                    gpp.Badges.Add(new POGOProtos.Data.PlayerBadge()
                    {
                        BadgeType = BadgeType.BadgeTravelKm,
                        EndValue = 2674,
                        CurrentValue = 1337
                    });
                    return gpp.ToByteString();

                //case RequestType.SetAvatar:
                //    LevelUpRewardsResponse sa = new LevelUpRewardsResponse();
                //    return sa.ToByteString();

                case RequestType.GetPlayer:

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(GetPlayerResponse)}");
                    return new GetPlayer().From(brc);

                case RequestType.GetInventory:
                    RepeatedField<InventoryItem> items = new RepeatedField<InventoryItem>();
                    //ADD ITEMSS
                    GetInventoryResponse gi = new GetInventoryResponse();
                    gi.Success = true;
                    gi.InventoryDelta = new POGOProtos.Inventory.InventoryDelta()
                    {
                        NewTimestampMs = (long)DateTime.Now.ToUnixTime()
                    };
                    gi.InventoryDelta.InventoryItems.AddRange(items);

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(GetInventoryResponse)}");
                    return gi.ToByteString();

                case RequestType.GetAssetDigest:
                    var gad = GlobalSettings.GameAssets[brc.CurrentPlayer.Platform].Value;

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(GetAssetDigestResponse)}");
                    return gad.ToByteString();

                //case RequestType.NicknamePokemon:
                //    LevelUpRewardsResponse np = new LevelUpRewardsResponse();
                //    return np.ToByteString();

                case RequestType.GetHatchedEggs:
                    GetHatchedEggsResponse ghe = new GetHatchedEggsResponse();
                    ghe.Success = true;

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(GetHatchedEggsResponse)}");
                    return ghe.ToByteString();

                case RequestType.CheckAwardedBadges:
                    CheckAwardedBadgesResponse cab = new CheckAwardedBadgesResponse();
                    cab.Success = true;

                    brc.Log.Dbg($"TypeOfResponseMessage: {nameof(CheckAwardedBadgesResponse)}");
                    return cab.ToByteString();

                //case RequestType.RecycleInventoryItem:
                //    LevelUpRewardsResponse rii = new LevelUpRewardsResponse();
                //    return rii.ToByteString();

                //case RequestType.ClaimCodename:
                //    LevelUpRewardsResponse cc = new LevelUpRewardsResponse();
                //    return cc.ToByteString();

                default:
                    throw new Exception($"unknown (Player) Returns type: {typ}");
            }
        }

        public static OwnedPokemon GetPokemonById(this AuthorizedController brc, ulong id)
        {
            var usr = brc.Database.Users.SingleOrDefault(p => p.email == brc.UEmail);
            var owned = brc.Database.OwnedPokemons.SingleOrDefault(p => p.owner_id == usr.id && (int)id == p.id);
            return owned;
        }

        public static ReleasePokemonResponse ReleasePokemon(this AuthorizedController brc, ReleasePokemonMessage msg)
        {
            var owned = brc.GetPokemonById(msg.PokemonId);
            ReleasePokemonResponse rsp = new ReleasePokemonResponse();
            if (owned != null)
            {
                rsp.Result = ReleasePokemonResponse.Types.Result.Success;
                rsp.CandyAwarded = 3;
            }
            else
            {
                rsp.Result = ReleasePokemonResponse.Types.Result.Failed;
            }
            return rsp;
        }
    }
}