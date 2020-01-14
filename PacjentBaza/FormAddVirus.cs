using PacjentBaza.Model;
using PacjentBaza.Services;
using System;
using System.Windows.Forms;

namespace PacjentBaza
{
    /// <summary>
    /// Klasa reprezentująca formę do dodawania opisu wirusów jest analogiczna z formą FormAdd - tam znajduje się szerszy opis
    /// </summary>
    public partial class FormAddVirus : Form
    {
        private VirusService _service;
        private PatientService _patientService;
        private bool _isEditMode;
        private int _idEdit;

        public FormAddVirus()
        {
            InitializeComponent();
            this._service = new VirusService();
            this._patientService = new PatientService();
            this.WindowState = FormWindowState.Maximized;
          
        }

        public void SetEditMode()
        {
            this._isEditMode = true;
        }

        public void LoadDataSource()
        {
            this.comboBox1.DataSource = this._patientService.GetPatients();
            this.comboBox1.ValueMember = "Id";
            this.comboBox1.DisplayMember = "FullName";
        }

        public void SetData(Virus virus)
        {
            this._idEdit = virus.Id;
            this.richTextBox1.Text = virus.Description;
            this.comboBox1.SelectedValue = virus.PatientId;
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(this.richTextBox1.Text))
            {
                MessageBox.Show("Wypełnij opis wirusa");
                return;
            }

            if (this.comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Wybierz pacjenta");
                return;
            }

            if (this._isEditMode)
            {
                if (this._service.Edit(this.richTextBox1.Text,Convert.ToInt32(this.comboBox1.SelectedValue), this._idEdit))
                {
                    MessageBox.Show("Dokonano edycji", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (this._service.Add(this.richTextBox1.Text, Convert.ToInt32(this.comboBox1.SelectedValue)))
                {
                    MessageBox.Show("Dodano informacje", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.richTextBox1.Text = String.Empty;
                    return;
                }
            }

            MessageBox.Show("Nie udało się dodać informacji", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
