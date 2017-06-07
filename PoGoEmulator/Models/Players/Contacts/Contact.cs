using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace PoGoEmulator.Models.Players.Contacts
{
    public class Contact
    {
        public Contact()
        {
        }

        public bool send_marketing_emails { get; set; }
        public bool send_push_notifications { get; set; }
    }
}