using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoGoEmulator.Database.Tables
{
    [Table("users")]
    public class User
    {
        public double altitude { get; set; }

        [StringLength(128)]
        public string avatar { get; set; } = "{}";

        [StringLength(1024)]
        public string candies { get; set; } = "{}";

        [StringLength(32)]
        public string email { get; set; }

        public int exp { get; set; }
        public int id { get; set; }

        [StringLength(255)]
        public string items { get; set; } = "{}";

        public double latitude { get; set; }
        public short level { get; set; }
        public double longitude { get; set; }
        public int pokecoins { get; set; }

        [StringLength(64)]
        public string pokedex { get; set; } = "{}";

        public byte send_marketing_emails { get; set; }
        public byte send_push_notifications { get; set; }
        public int stardust { get; set; }
        public byte team { get; set; }

        [StringLength(64)]
        public string tutorial { get; set; } = "{\"0\":1,\"1\":1,\"3\":1,\"4\":1,\"7\":1}";

        public string username { get; set; }
    }
}