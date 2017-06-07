using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PoGoEmulator.Models.Players.Avatars;
using PoGoEmulator.Models.Players.Bags;
using PoGoEmulator.Models.Worlds.MapObjects;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players
{
    public class Player : MapObject
    {
        public Player()
        {
            avatar = new Avatar();
            bag = new Bag();
        }

        public string _email { get; set; }
        public string username { get; set; }
        public bool email_verified { get; set; }
        public string platform { get; set; }
        public bool isPTCAccount { get; set; }
        public bool isGoogleAccount { get; set; }
        public bool isIOS { get; set; }
        public bool isAndroid { get; set; }
        public bool hasSignature { get; set; }
        public bool authenticated { get; set; }
        public string remoteAddress { get; set; }
        public string currentEncounter { get; set; }
        public Avatar avatar { get; set; }
        public Bag bag { get; set; }

        public Player(object obj) : this()
        {
        }
    }
}