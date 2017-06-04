using System;
using System.Collections.Generic;
using System.Text;
using Google.Protobuf.Collections;
using PoGoEmulator.Database;
using POGOProtos.Map;

namespace PoGoEmulator.Models
{
    public class FortFuncs
    {
        private PoGoDbContext db { get; }

        public FortFuncs(PoGoDbContext _db)
        {
            db = _db;
        }

        public RepeatedField<MapCell> GetFortsByCells(RepeatedField<ulong> cellIds)
        {
            var cells = new RepeatedField<MapCell>();
            foreach (var cl in cellIds)
            {
                cells.Add(GetFortsByCellId(cl));
            }
            return null;
        }

        public MapCell GetFortsByCellId(ulong cellId)
        {
            MapCell cell = CellAlreadyRegistered(cellId);
            if (cell == null)
            {
                return RegisterCell(cellId);
            }
            else
            {
                LoadForts();
            }
            return null;
        }

        public void LoadForts()
        {
        }

        public MapCell RegisterCell(ulong cellId)
        {
            MapCell c = new MapCell
            {
                S2CellId = cellId
            };
            bool snc = GlobalSettings.MapCells.TryAdd(cellId, c);
            if (snc == false)//other user already added so we can get this
            {
                GlobalSettings.MapCells.TryGetValue(cellId, out c);
            }
            return c;
        }

        public MapCell CellAlreadyRegistered(ulong cellId)
        {
            MapCell cell;
            GlobalSettings.MapCells.TryGetValue(cellId, out cell);
            return cell;
        }
    }
}