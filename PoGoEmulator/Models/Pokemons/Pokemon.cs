using System;
using PoGoEmulator.Models.Players;
using PoGoEmulator.Models.Worlds.MapObjects;
using POGOProtos.Data;
using POGOProtos.Enums;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Pokemons
{
    public class Pokemon : MapObject
    {
        public Pokemon()
        {
            _level = 1;
            Random r = new Random();
            cpMultiplier = (float)(r.NextDouble() + 1.0);
        }

        public Pokemon(object obj) : this()
        {
            this.InitializeMapObject(this, obj);

            if (isWild && !isOwned) this.CalcStats(owner);
        }

        public float cpMultiplier { get; set; }
        public int dexNumber { get; set; }
        public int capturedLevel { get; set; }
        public int cp { get; set; }
        public int addCpMultiplier { get; set; }
        public int move1 { get; set; }
        public int move2 { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int stamina { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public int ivAttack { get; set; }
        public int ivDefense { get; set; }
        public int ivStamina { get; set; }
        public int staminaMax { get; set; }
        public int favorite { get; set; }
        public Player owner { get; set; }
        public string nickname { get; set; }
        public string pokeball { get; set; }
        public string spawnPoint { get; set; }
        public bool isWild { get; set; }
        public bool isOwned { get; set; }
        public int _level { get; set; }

        public PokemonData Serialize()
        {
            PokemonData pk = new PokemonData
            {
                Id = GlobalExtensions.GenerateUniqueULongId(),
                PokemonId = (PokemonId)this.dexNumber,
                Cp = this.cp,
                Stamina = this.stamina,
                StaminaMax = this.staminaMax,
                Move1 = (PokemonMove)this.move1,
                Move2 = (PokemonMove)this.move2,
                HeightM = this.height,
                WeightKg = this.weight,
                IndividualAttack = this.ivAttack,
                IndividualDefense = this.ivDefense,
                IndividualStamina = this.ivStamina,
                CpMultiplier = this.cpMultiplier,
                Pokeball = POGOProtos.Inventory.Item.ItemId.ItemPokeBall,
                CapturedCellId = 1337,
                CreationTimeMs = DateTime.Now.ToUnixTime(),
                Favorite = this.favorite,
                Nickname = this.nickname
            };
            return pk;
        }
    }
}