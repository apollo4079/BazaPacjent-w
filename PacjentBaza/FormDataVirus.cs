using PacjentBaza.Model;
using PacjentBaza.Services;
using System;
using System.Windows.Forms;

namespace PacjentBaza
{
    /// <summary>
    /// Forma do przeglądania danych o wirusach, analogicznie zbudowana jak forma FormData - tam znajdują się opisy metod
    /// </summary>
    public partial class FormDataVirus : Form
    {
        private VirusService _service;

        public FormDataVirus()
        {
            InitializeComponent();
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.RowHeadersVisible = false;
            this._service = new VirusService();
            this.WindowState = FormWindowState.Maximized;
            
        }

        public void LoadDataSource()
        {
            this.dataGridView1.DataSource = this._service.Get(); 
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Wybierz wiersz", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this._service.Delete(((Virus)this.dataGridView1.SelectedRows[0].DataBoundItem).Id);
            this.dataGridView1.DataSource = this._service.Get();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Wybierz wiersz", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FormAddVirus form = new FormAddVirus();
            form.MdiParent = this.MdiParent;
            form.Text = "Edytuj dane";
            form.LoadDataSource();
            form.SetEditMode();
            
            form.SetData(((Virus)this.dataGridView1.SelectedRows[0].DataBoundItem));
            form.Show();
        }
    }
}
