using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'informacionDataSet.usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.informacionDataSet.usuarios);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            usuariosBindingSource.MoveFirst();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuariosBindingSource.MovePrevious();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            usuariosBindingSource.MoveNext();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            usuariosBindingSource.MoveLast();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (usuariosBindingSource.AllowEdit == true)
            {
                TransactionScope tr = new TransactionScope();
                using (tr)
                {
                    //this.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                    usuariosBindingSource.AllowNew = true;
                    usuariosBindingSource.AddNew();
                    if (informacionDataSet.HasChanges())
                    {
                        Validate();
                        dataGridView1.EndEdit();
                        usuariosBindingSource.EndEdit();
                        DataSet changes = informacionDataSet.GetChanges();
                        usuariosTableAdapter.Update(informacionDataSet.usuarios);
                        informacionDataSet.AcceptChanges();
                    }
                    tr.Complete();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (usuariosBindingSource.AllowRemove == true)
            {
                TransactionScope tr = new TransactionScope();
                using (tr)
                {
                    //this.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                    usuariosBindingSource.RemoveCurrent();
                    usuariosBindingSource.ResetBindings(false);
                    Validate();
                    if (informacionDataSet.HasChanges())
                    {
                        dataGridView1.EndEdit();
                        usuariosBindingSource.EndEdit();
                        DataSet changes = informacionDataSet.GetChanges();
                        usuariosTableAdapter.Update(informacionDataSet.usuarios);
                        informacionDataSet.AcceptChanges();
                    }
                    tr.Complete();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TransactionScope tr = new TransactionScope();
            using (tr)
            {
                if (informacionDataSet.HasChanges())
                {
                    usuariosBindingSource.EndEdit();
                    DataSet changes = informacionDataSet.GetChanges();
                    usuariosTableAdapter.Update(informacionDataSet.usuarios);
                    informacionDataSet.AcceptChanges();
                }
                tr.Complete();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 dlgAñadir = new Form2();
            if (dlgAñadir.ShowDialog(this) != DialogResult.OK) return;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected override void OnBindingContextChanged(EventArgs e)
        {
            foreach (Binding item in DataBindings)
            {
                if (item.PropertyName == "SomeProperty")
                    item.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            }
            base.OnBindingContextChanged(e);

            //usuariosBindingSource.ResetBindings(true);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            button8_Click( sender, e);
        }
    }
}
