using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_minigame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 5;
            dataGridView1.Rows[1].Cells[2].Selected = true;
            this.initDisabledCells();

            this.createActiveCell('A', new Coords(0,0));
            this.createActiveCell('A', new Coords(1,4));
            this.createActiveCell('A', new Coords(2,4));
            this.createActiveCell('A', new Coords(3,4));
            this.createActiveCell('A', new Coords(4,0));

            this.createActiveCell('B', new Coords(0, 2));
            this.createActiveCell('B', new Coords(1, 3));
            this.createActiveCell('B', new Coords(3, 1));
            this.createActiveCell('B', new Coords(3, 2));
            this.createActiveCell('B', new Coords(4, 2));

            this.createActiveCell('C', new Coords(0, 4));
            this.createActiveCell('C', new Coords(1, 1));
            this.createActiveCell('C', new Coords(1, 0));
            this.createActiveCell('C', new Coords(3, 0));
            this.createActiveCell('C', new Coords(4, 4));
        }

        Coords coordsSelected = new Coords(0,0);
        Coords coordsMovementPlace = new Coords(0,0);
        Color selectedColor = Color.White;
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (coordsSelected.X == 0 && coordsSelected.Y == 0)
            {
                coordsSelected = new Coords(e.RowIndex, e.ColumnIndex);
                selectedColor = dataGridView1.SelectedCells[0].Style.BackColor;
            }
            else
            {
                coordsMovementPlace = new Coords(e.RowIndex, e.ColumnIndex);
                move(coordsSelected,coordsMovementPlace);
                coordsSelected = new Coords(0,0);
                GameOver();
            }

            
        }

        private List<Coords> disabledCells = new List<Coords>() {
            new Coords(1,0),
            new Coords(3,0),
            new Coords(1,2),
            new Coords(3,2),
            new Coords(1,4),
            new Coords(3,4)
        };

        private void initDisabledCells() {
            foreach (Coords i in this.disabledCells) {
                DataGridViewCell cell = dataGridView1.Rows[i.Y].Cells[i.X];
                cell.Style.BackColor = Color.Gray;
            }
        }

        private void createActiveCell(char type, Coords coords) {
            ActiveCell cell = new ActiveCellTypeA(new Coords(1,3));
            switch (type) {
                case 'A':
                    cell = new ActiveCellTypeA(coords);
                    break;
                case 'B':
                    cell = new ActiveCellTypeB(coords);
                    break;
                case 'C':
                    cell = new ActiveCellTypeC(coords);
                    break;
            }
            if (cell != null)
            {
                dataGridView1.Rows[coords.X].Cells[coords.Y].Style.BackColor = cell.color;
                activeCells.Add(coords, type);
            }

        }

        private List<char> cellType = new List<char>() { 'A', 'B', 'C' };
        private Dictionary<Coords, char> activeCells = new Dictionary<Coords, char>();
        
        private void move(Coords coordsSelected, Coords coordsMovementPlace) {
            if(
                dataGridView1.Rows[coordsMovementPlace.X].Cells[coordsMovementPlace.Y].Style.BackColor == Color.Empty 
                || dataGridView1.Rows[coordsMovementPlace.X].Cells[coordsMovementPlace.Y].Style.BackColor == Color.White
                && !disabledCells.Contains(new Coords(coordsSelected.Y, coordsSelected.X))               
                )
            {
                if(coordsMovementPlace.X == (coordsSelected.X+1) && coordsMovementPlace.Y == (coordsSelected.Y)
                || coordsMovementPlace.X == (coordsSelected.X - 1) && coordsMovementPlace.Y == (coordsSelected.Y)
                || coordsMovementPlace.X == (coordsSelected.X) && coordsMovementPlace.Y == (coordsSelected.Y - 1)
                || coordsMovementPlace.X == (coordsSelected.X) && coordsMovementPlace.Y == (coordsSelected.Y + 1))
                {
                    char type;
                    if(!activeCells.TryGetValue((coordsSelected), out type))
                    {
                        type = 'A';
                    }
                    activeCells.Remove(coordsSelected);
                    dataGridView1.Rows[coordsMovementPlace.X].Cells[coordsMovementPlace.Y].Style.SelectionBackColor = selectedColor;
                    dataGridView1.Rows[coordsMovementPlace.X].Cells[coordsMovementPlace.Y].Style.SelectionForeColor = selectedColor;
                    createActiveCell(type,coordsMovementPlace);
                    dataGridView1.Rows[coordsSelected.X].Cells[coordsSelected.Y].Style.BackColor = Color.White;
                }

            }
        }

        private Dictionary<Coords, char> cellsRigthPosition = new Dictionary<Coords, char>() {
            {new Coords(0,0), 'A'},
            {new Coords(1,0), 'A'},
            {new Coords(2,0), 'A'},
            {new Coords(3,0), 'A'},
            {new Coords(4,0), 'A'},
            {new Coords(0,2), 'B'},
            {new Coords(1,2), 'B'},
            {new Coords(2,2), 'B'},
            {new Coords(3,2), 'B'},
            {new Coords(4,2), 'B'},
            {new Coords(0,4), 'C'},
            {new Coords(1,4), 'C'},
            {new Coords(2,4), 'C'},
            {new Coords(3,4), 'C'},
            {new Coords(4,4), 'C'},
        };

        private void GameOver() {
            DialogResult dialog;
            Boolean isGameOver = true;
            foreach (KeyValuePair<Coords, char> kvp in cellsRigthPosition) {
                if (!activeCells.Contains(kvp))
                    isGameOver = false;
            }
            if (isGameOver)
            {
                dialog = MessageBox.Show("Game over", "Massage", MessageBoxButtons.OK);
                if (dialog == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

    }

    

}
