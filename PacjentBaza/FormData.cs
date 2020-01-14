using PacjentBaza.Model;
using PacjentBaza.Services;
using System;
using System.Windows.Forms;

namespace PacjentBaza
{
    /// <summary>
    /// Forma prezentująca dane o pacjentach
    /// </summary>
    public partial class FormData : Form
    {
        private PatientService _service;

        /// <summary>
        /// W konstruktorze ustawiane są jeszcze opcje dotyczące grida jak wysokośc wiersza, czy nagłowek wiersza ma być pokazywany,
        /// tworzony jest nowy obiekt klasy serwisu i okno jest ustawianie na pełną szereokość kontenera 
        /// </summary>
        public FormData()
        {
            InitializeComponent();
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.RowHeadersVisible = false;
            this._service = new PatientService();
            this.WindowState = FormWindowState.Maximized;
          
        }

        /// <summary>
        /// Metoda wczytująca dane z serwisu do grida
        /// </summary>
        public void LoadDataSource()
        {
            this.dataGridView1.DataSource = this._service.GetPatients();
        }

        /// <summary>
        /// Metoda wywoływana w momencie kiedy chcemy usunąć dane, sprawdzanym jest czy zaznaczono wiersz.
        /// jeżeli nie to komunikat jeżeli tak to usuwamy pacjenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click_1(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Wybierz wiersz", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this._service.DeletePatient(((Patient)this.dataGridView1.SelectedRows[0].DataBoundItem).Id);
            this.dataGridView1.DataSource = this._service.GetPatients();
        }

        /// <summary>
        /// Metoda wywoływana w momencie kiedy chcemy edytować dane, sprawdzanym jest czy zaznaczono wiersz.
        /// jeżeli nie to komunikat jeżeli tak to otwierane jest okno dodania ustawiany tryb do odczytu i przekazywane dane do
        /// okna.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Wybierz wiersz", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FormAdd form = new FormAdd();
            form.MdiParent = this.MdiParent;
            form.Text = "Edytuj dane";
            form.SetEditMode();
            form.SetData(((Patient)this.dataGridView1.SelectedRows[0].DataBoundItem));
            form.Show();
        }
    }
}
