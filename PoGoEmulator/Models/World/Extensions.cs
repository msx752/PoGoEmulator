using System.Collections.Generic;
using POGOProtos.Map;

namespace PoGoEmulator.Models.World
{
    //THIS WILL BE DEPREATE
    public static class Extensions
    {
        public static bool AddNewCell(this World world, MapCell newCell, bool force)
        {
            bool reslt;
            if (force)
                reslt = World.Cells.AddOrUpdate(newCell.S2CellId.ToString(), newCell, (k, v) => newCell) != null;
            else
                reslt = World.Cells.TryAdd(newCell.S2CellId.ToString(), newCell);
            return reslt;
        }

        public static MapCell GetCellByCellId(this World world, string s2CellId)
        {
            MapCell reslt;
            World.Cells.TryGetValue(s2CellId, out reslt);
            return reslt;
        }

        public static int GetCellIndexByCellId(this World world, string s2CellId)
        {
            return 0;
        }

        public static bool CellAlreadyRegistered(this World world, string s2CellId)
        {
            return World.Cells.ContainsKey(s2CellId);
        }

        public static void RefreshSpawns(this World world, string s2CellId = null)
        {
            var s2CellIds = World.Cells.GetEnumerator();
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