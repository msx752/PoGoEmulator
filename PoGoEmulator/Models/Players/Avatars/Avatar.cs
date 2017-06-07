using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.Avatars
{
    public class Avatar
    {
        private byte _skin1;
        private byte _hair1;
        private byte _shirt1;
        private byte _pants1;
        private byte _hat1;
        private byte _shoes1;
        private byte _gender1;
        private byte _backpack1;
        private byte _eyes1;

        public Avatar()
        {
        }

        public byte _skin
        {
            get { return _skin1; }
            set
            {
                if (Between(value, 0, 3))
                    _skin1 = value;
            }
        }

        public byte _hair
        {
            get { return _hair1; }
            set { if (Between(value, 0, 5)) _hair1 = value; }
        }

        public byte _shirt
        {
            get { return _shirt1; }
            set { if (Between(value, 0, 9)) _shirt1 = value; }
        }

        public byte _pants
        {
            get { return _pants1; }
            set { if (Between(value, 0, 5)) _pants1 = value; }
        }

        public byte _hat
        {
            get { return _hat1; }
            set { if (Between(value, 0, 4)) _hat1 = value; }
        }

        public byte _shoes
        {
            get { return _shoes1; }
            set { if (Between(value, 0, 6)) _shoes1 = value; }
        }

        public byte _eyes
        {
            get { return _eyes1; }
            set { if (Between(value, 0, 4)) _eyes1 = value; }
        }

        public byte _backpack
        {
            get { return _backpack1; }
            set { if (Between(value, 0, 5)) _backpack1 = value; }
        }

        public byte _gender
        {
            get { return _gender1; }
            set { if (Between(value, 0, 1)) _gender1 = value; }
        }

        private bool Between(byte value, byte a, byte b)
        {
            return value >= a && value <= b;
        }
    }
}