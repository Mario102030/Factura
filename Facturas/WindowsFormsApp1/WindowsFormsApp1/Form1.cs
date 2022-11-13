using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID;
            string Nombre;
            float Precio;

            ID= cmbProducto.SelectedIndex;
            Nombre= cmbProducto.SelectedItem.ToString();
            Precio = cmbProducto.SelectedIndex;

            switch (ID)
            {

                case 0: lblID.Text = "0001"; break;
                case 1: lblID.Text = "0002"; break;
                case 2: lblID.Text = "0003"; break;
                case 3: lblID.Text = "0004"; break;
                default: lblID.Text = "0005"; break;

            }

            switch (Nombre)
            {

                case "Platano": lblNombre.Text = "Platano"; break;
                case "Menta": lblNombre.Text = "Menta"; break;
                case "Arroz": lblNombre.Text = "Arroz"; break;
                case "Jugo": lblNombre.Text = "Jugo"; break;
                default: lblNombre.Text = "Azucar"; break;

            }

            switch(Precio)
            {

                case 0: lblPrecio.Text = "15"; break;
                case 1: lblPrecio.Text = "1"; break;
                case 2: lblPrecio.Text = "150";break;
                case 3: lblPrecio.Text = "2"; break;
                default: lblPrecio.Text = "65"; break;

            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
            DataGridViewRow file = new DataGridViewRow();
            file.CreateCells(dgvLista);

            file.Cells[0].Value = lblID.Text;
            file.Cells[1].Value = lblNombre.Text;
            file.Cells[2].Value = lblPrecio.Text;
            file.Cells[3].Value = txtCantidad.Text;
            file.Cells[4].Value = (float.Parse(lblPrecio.Text) * float.Parse(txtCantidad.Text)).ToString();

            dgvLista.Rows.Add(file);
            lblID.Text = lblPrecio.Text = lblPrecio.Text = txtCantidad.Text="";

        }
    }
}