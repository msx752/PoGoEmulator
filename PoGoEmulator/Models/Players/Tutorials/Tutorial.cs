using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POGOProtos.Enums;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.Tutorials
{
    public class Tutorial
    {
        public List<TutorialState> states { get; set; }

        public Tutorial()
        {
            states = new List<TutorialState>();
        }
    }
}