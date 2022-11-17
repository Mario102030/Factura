using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            //Inicializamos las variables
            ID= cmbProducto.SelectedIndex;
            Nombre= cmbProducto.SelectedItem.ToString();
            Precio = cmbProducto.SelectedIndex;

            //Asignamos valores para que se agreguen a los label.
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
           
            DataGridViewRow Archivo = new DataGridViewRow(); //Usamos los label para agregarlos a la lista.
            Archivo.CreateCells(dgvLista);

            //Creamos celdas para introducir los valores del label
            Archivo.Cells[0].Value = lblID.Text;
            Archivo.Cells[1].Value = lblNombre.Text;
            Archivo.Cells[2].Value = lblPrecio.Text;
            Archivo.Cells[3].Value = txtCantidad.Text;
            try
            {
                Archivo.Cells[4].Value = (float.Parse(lblPrecio.Text) * float.Parse(txtCantidad.Text)).ToString(); //Hacemos el calculo de cantidad por precio.
            }
            catch(System.FormatException)
            {
                Console.WriteLine("A ocurrido un error");
                Console.WriteLine("Reiniciando...");
                InitializeComponent();
            }
            dgvLista.Rows.Add(Archivo); //Agregamos a la tabla.
            lblID.Text = lblNombre.Text = lblPrecio.Text = txtCantidad.Text="";

            obtenerTotal(); //Llamamos al total para que se agregue con el boton de agregar.

        }

        public void obtenerTotal()
        {
            //Creamos un metodo total para que se sume los montos y se vayan acumulando a medida que se agregan mas productos.
            float costo = 0;

            int contador = 0;

            contador = dgvLista.RowCount;

            for(int i = 0 ; i < contador; i++)
            {   
                
                costo += float.Parse(dgvLista.Rows[i].Cells[4].Value.ToString());
            }

            lblTotalPagar.Text = costo.ToString();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult borrar = MessageBox.Show("Desea eliminar este articulo?",
                    "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(borrar == DialogResult.Yes)
                {
                    dgvLista.Rows.Remove(dgvLista.CurrentRow);
                }

            }
            catch
            { 

            }
            obtenerTotal();
        }
       private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {

            try
            {
                lblDevolucion.Text = (float.Parse(txtEfectivo.Text) - float.Parse(lblTotalPagar.Text)).ToString();
            }
            catch { }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {

            clsFactura.CreaTicket Ticket1 = new clsFactura.CreaTicket();

            Ticket1.TextoCentro("Empresa xxxxx "); //imprime una linea de descripcion
            Ticket1.TextoCentro("**********************************");

            Ticket1.TextoIzquierda("Dirc: xxxx");
            Ticket1.TextoIzquierda("Tel:xxxx ");
            Ticket1.TextoIzquierda("Rnc: xxxx");
            Ticket1.TextoIzquierda("");
            Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
            Ticket1.TextoIzquierda("No Fac: 002156");
            Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
            Ticket1.TextoIzquierda("Le Atendio: xxxx");
            Ticket1.TextoIzquierda("");
           clsFactura.CreaTicket.LineasGuion();

            clsFactura.CreaTicket.EncabezadoVenta();
            clsFactura.CreaTicket.LineasGuion();
            foreach (DataGridViewRow r in dgvLista.Rows)
            {
                // PROD                     //PrECIO                                    CANT                         TOTAL
                Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
            }


            clsFactura.CreaTicket.LineasGuion();
            Ticket1.AgregaTotales("Sub-Total", double.Parse("000")); // imprime linea con Subtotal
            Ticket1.AgregaTotales("Menos Descuento", double.Parse("000")); // imprime linea con decuento total
            Ticket1.AgregaTotales("Mas ITBIS", double.Parse("000")); // imprime linea con ITBis total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Total", double.Parse(lblTotalPagar.Text)); // imprime linea con total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(txtEfectivo.Text));
            Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(lblDevolucion.Text));


            // Ticket1.LineasTotales(); // imprime linea 

            Ticket1.TextoIzquierda(" ");
            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoCentro("*     Gracias por preferirnos    *");

            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer";
            Ticket1.ImprimirTiket(impresora);


            MessageBox.Show("Gracias por preferirnos");
            this.Close();
        }

 
    }
}