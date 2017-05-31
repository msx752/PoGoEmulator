using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoGoEmulator.Database.Tables
{
    [Table("owned_pkmn")]
    public class OwnedPokemon
    {
        public double? additional_cp_multiplier { get; set; }
        public int battles_attacked { get; set; }
        public int battles_defended { get; set; }

        [StringLength(32)]
        public string captured_cell_id { get; set; }

        public int cp { get; set; }
        public double cp_multiplier { get; set; }
        public int? creation_time_ms { get; set; }
        public long? deployed_fort_id { get; set; }
        public int dex_number { get; set; }
        public string egg_incubator_id { get; set; }
        public double? egg_km_walked_start { get; set; }
        public double? egg_km_walked_target { get; set; }
        public bool? favorite { get; set; }
        public int? from_fort { get; set; }
        public double height_m { get; set; }
        public int id { get; set; }
        public int individual_attack { get; set; }
        public int individual_defense { get; set; }
        public int individual_stamina { get; set; }
        public bool is_egg { get; set; }

        [StringLength(32)]
        public String move_1 { get; set; }

        [StringLength(32)]
        public String move_2 { get; set; }

        public string nickname { get; set; }
        public int? num_upgrades { get; set; }
        public int? origin { get; set; }
        public int owner_id { get; set; }

        [StringLength(32)]
        public String pokeball { get; set; }

        public int stamina { get; set; }
        public int stamina_max { get; set; }
        public double weight_kg { get; set; }
    }
}