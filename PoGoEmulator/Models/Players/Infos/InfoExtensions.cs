using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using POGOProtos.Inventory;
using POGOProtos.Settings.Master;

namespace PoGoEmulator.Models.Players.Infos
{
    public static class InfoExtensions
    {
        public static int GetCurrentLevel(this Info i)
        {
            var levels = GlobalExtensions.GetLevelSettings().RequiredExperience;
            for (int j = 0; j < levels.Count; j++)
            {
                if (levels[j] << 0 == i.nextLvlExp)
                {
                    return j << 0;
                }
            }
            return 1;
        }

        public static void UpgradeExp(this Info i, int exp)
        {
            if (i.LuckyEggExp != 0 && i.LuckyEggExp > DateTime.Now.Ticks)//wrong calculation correct: ' this.LuckyEggExp > new Date() '
            {
                i._exp *= 2;
            }

            if (!i.MaxLevelReached())
            {
                int currentLevelExp = GlobalExtensions.GetLevelExp((byte)i._level);
                int nextLevelExp = GlobalExtensions.GetLevelExp((byte)(i._level + 1));
                int leftExp = nextLevelExp - i._exp;
                i.levelReward = false;
                if (i._exp + exp >= nextLevelExp)
                {
                    i._level += 1;
                    i.prevLvlExp = nextLevelExp;
                    i.nextLvlExp = GlobalExtensions.GetLevelExp((byte)(i._level + 1));
                    i._exp += leftExp + 1;
                    i.levelReward = true;
                    i.UpgradeExp(exp - leftExp);
                }
                else
                {
                    i._exp += exp;
                }
            }
        }

        public static bool MaxLevelReached(this Info i)
        {
            return i._level + 1 >= GlobalExtensions.GetMaximumLevel();
        }
    }
}