using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakOut.Components
{
    public class PlayerInputComponent
    {
        public bool PressingLeft        { get; set; }
        public bool PressingRight       { get; set; }

        public bool PressingPrimary     { get; set; }
        public bool PressingSecondary   { get; set; }
    }
}
