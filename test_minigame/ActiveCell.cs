using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace test_minigame
{
    abstract class ActiveCell
    {
        public ActiveCell(Coords coords) {
            this.coords = coords;
        }

        public Coords coords = new Coords(0,0);
        public Color color = Color.White;
    }

}

