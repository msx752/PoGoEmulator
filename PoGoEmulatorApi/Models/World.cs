using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Protobuf.Collections;
using PoGoEmulatorApi.Controllers;
using POGOProtos.Map;
using POGOProtos.Map.Fort;
using POGOProtos.Networking.Responses;

namespace PoGoEmulatorApi.Models
{
    public static class World
    {
        public static RepeatedField<MapCell> GetMapObjects(RepeatedField<ulong> cellids)
        {
            RepeatedField<MapCell> cells = new RepeatedField<MapCell>();

            foreach (var cellid in cellids)
            {
                MapCell cell = GetCellById(cellid);
                if (cell == null)
                    cell = RegisterCell(cellid);

                cells.Add(cell);
            }
            return cells;
        }

        public static MapCell RegisterCell(ulong cellId)
        {
            var c = new MapCell
            {
                S2CellId = cellId,
                CurrentTimestampMs = (long)DateTime.Now.ToUnixTime()
            };
            c = GlobalSettings.MapCells.AddOrUpdate(cellId, c, (k, v) => c);
            return c;
        }

        public static MapCell GetCellById(ulong cellId)
        {
            MapCell cell;
            GlobalSettings.MapCells.TryGetValue(cellId, out cell);
            return cell;
        }
    }
}