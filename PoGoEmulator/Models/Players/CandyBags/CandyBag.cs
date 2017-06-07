using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using POGOProtos.Data;
using POGOProtos.Inventory;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.CandyBags
{
    public class CandyBag
    {
        public CandyBag()
        {
        }

        public Candy[] candies { get; set; } = new Candy[GlobalSettings.ServerCfg.MAX_POKEMON_NATIONAL_ID + 1];

        public RepeatedField<InventoryItem> Serialize()
        {
            RepeatedField<InventoryItem> outt = new RepeatedField<InventoryItem>();
            for (int i = 0; i < candies.Length; i++)
            {
                if (candies[i].IsNull() || candies[i].amount < 1) continue;

                InventoryItem v = new InventoryItem();
                v.ModifiedTimestampMs = (long)((double)DateTime.Now.ToUnixTime() - 1e3);
                v.InventoryItemData = new InventoryItemData()
                {
                    Candy = new POGOProtos.Inventory.Candy()
                    {
                        Candy_ = candies[i].amount,
                        FamilyId = (POGOProtos.Enums.PokemonFamilyId)i
                    }
                };
            }
            return outt;
        }
    }

    public class Candy
    {
        public int amount { get; set; }
    }
}