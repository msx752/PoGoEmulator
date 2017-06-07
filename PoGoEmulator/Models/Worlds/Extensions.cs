using System;
using System.Collections.Generic;
using System.Reflection;
using POGOProtos.Map;

namespace PoGoEmulator.Models.Worlds
{
    //THIS WILL BE DEPREATE
    public static class Extensions
    {
        public static bool AddNewCell(this Worlds.World world, MapCell newCell, bool force)
        {
            bool reslt;
            if (force)
                reslt = Worlds.World.Cells.AddOrUpdate(newCell.S2CellId.ToString(), newCell, (k, v) => newCell) != null;
            else
                reslt = Worlds.World.Cells.TryAdd(newCell.S2CellId.ToString(), newCell);
            return reslt;
        }

        public static MapCell GetCellByCellId(this Worlds.World world, string s2CellId)
        {
            MapCell reslt;
            Worlds.World.Cells.TryGetValue(s2CellId, out reslt);
            return reslt;
        }

        public static int GetCellIndexByCellId(this Worlds.World world, string s2CellId)
        {
            return 0;
        }

        public static bool CellAlreadyRegistered(this Worlds.World world, string s2CellId)
        {
            return Worlds.World.Cells.ContainsKey(s2CellId);
        }

        public static void RefreshSpawns(this Worlds.World world, string s2CellId = null)
        {
            var s2CellIds = Worlds.World.Cells.GetEnumerator();
            do
            {
                if (s2CellIds.Current.Value.IsNull()) continue;

                if (s2CellId != null)
                {
                    while (s2CellId != s2CellIds.Current.Key)
                    {
                        s2CellIds.MoveNext();
                    }
                }
                KeyValuePair<string, MapCell> currCell = s2CellIds.Current;
                foreach (var fort in currCell.Value.Forts)
                {
                }
            } while (s2CellIds.MoveNext() && s2CellId == null);
        }
    }
}