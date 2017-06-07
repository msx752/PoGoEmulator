using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.GameMasters;
using POGOProtos.Enums;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models.Players.CandyBags
{
    public static class CandyBagExtensions
    {
        public static Candy CreateCandy(this CandyBag c, byte dex)
        {
            var id = (int)Extensions.GetPkmnFamily(dex);
            Candy candy = new Candy()
            {
                amount = 0
            };
            c.candies[id] = candy;
            return candy;
        }

        public static Candy GetCandyByDexNumber(this CandyBag c, byte dex)
        {
            var id = (int)Extensions.GetPkmnFamily(dex);
            if (c.candies[id].IsNotNull())
            {
                return c.candies[id];
            }
            else
            {
                return c.CreateCandy(dex);
            }
        }

        public static int GetCandy(this CandyBag c, byte dex)
        {
            var candy = c.GetCandyByDexNumber(dex);
            return candy.amount;
        }

        public static void AddCandy(this CandyBag c, byte dex, int amount)
        {
            var candy = c.GetCandyByDexNumber(dex);
            candy.amount += amount;
        }

        public static void RemoveCandy(this CandyBag c, byte dex, int amount)
        {
            var candy = c.GetCandyByDexNumber(dex);
            candy.amount -= amount;
            if (candy.amount < 0) candy.amount = 0;
        }
    }
}