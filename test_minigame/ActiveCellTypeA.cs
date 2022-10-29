using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace test_minigame
{
    class ActiveCellTypeA : ActiveCell
    {
        public ActiveCellTypeA(Coords coords) : base(coords) {
            this.color = Color.SpringGreen;
        }
    }
}
