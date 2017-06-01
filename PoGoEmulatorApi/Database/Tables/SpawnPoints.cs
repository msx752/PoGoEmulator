using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PoGoEmulatorApi.Database.Tables
{
    [Table("spawn_points")]
    public class SpawnPoint
    {
        public int id { get; set; }

        [StringLength(64)]
        public string cell_id { get; set; }

        public double latitude { get; set; }
        public double longitute { get; set; }

        [StringLength(64)]
        public string encounters { get; set; }

        public int update_interval { get; set; } = 5;
        public int min_spawn_expire { get; set; } = 2;
        public int max_spawn_expire { get; set; } = 15;
    }
}