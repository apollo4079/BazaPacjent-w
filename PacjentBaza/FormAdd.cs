using PacjentBaza.Model;
using PacjentBaza.Services;
using System;
using System.Windows.Forms;

namespace PacjentBaza
{
    /// <summary>
    /// Forma odpowiadająca za dodawanie i edycję danych pacjenta
    /// </summary>
    public partial class FormAdd : Form
    {
        private PatientService _service;
        private bool _isEditMode;
        private int _idEdit;

        /// <summary>
        /// Konstruktor - tworzymy nowe obiekty klasy serwisu, maksymalizujemy okno
        /// </summary>
        public FormAdd()
        {
            InitializeComponent();
            this._service = new PatientService();
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Metoda wykorzystywana do ustwienia flagi, że forma jest w trybie do edycji
        /// </summary>
        public void SetEditMode()
        {
            this._isEditMode = true;
        }

        /// <summary>
        /// Metoda ustanawiania danych pacjenta to obiektów interfejsu - wykorzystywana jak edytujemy dane pacjenta
        /// </summary>
        /// <param name="patient"></param>
        public void SetData(Patient patient)
        {
            this._idEdit = patient.Id;
            this.textBox2.Text = patient.FirstName;
            this.textBox1.Text = patient.LastName;
            this.textBox3.Text = patient.Pesel;
            if (DateTime.TryParse(patient.BirthDate,out DateTime result))
            {
                this.dateTimePicker1.Value = result;
            }
       
            this.richTextBox1.Text = patient.Address;
        }

        /// <summary>
        /// Metoda wykonywana po kliknięciu w przycisk anuluj - forma jest usuwana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Metoda przycisku myszy Dodaj lub edytuj zależnie od trybu okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            ///Sprawdzenie czy wypełniono pola tekstowe
            if (String.IsNullOrEmpty(this.textBox2.Text.Trim()) ||
                String.IsNullOrEmpty(this.textBox1.Text.Trim()) ||
                String.IsNullOrEmpty(this.textBox3.Text.Trim()))
            {
                MessageBox.Show("Wypełnij pola imię, nazwisko, pesel");
                return;
            }

            ///Sprawdzenie czy pesel składa się z 11 znaków
            if (this.textBox3.Text.Trim().Length != 11)
            {
                MessageBox.Show("Pesel musi mieć 11 znaków");
                return;
            }

            /// Jeżeli okno jest w trybie edytuj to edytuj pacjenta, w przeciwnym wypadku go dodaj
            if (this._isEditMode)
            {
                if (this._service.EditPatient(this.textBox2.Text, this.textBox1.Text, this.textBox3.Text,
                   this.dateTimePicker1.Value, this.richTextBox1.Text,this._idEdit))
                {
                    MessageBox.Show("Dokonano edycji danych pacjenta", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                if (this._service.AddPatient(this.textBox2.Text, this.textBox1.Text, this.textBox3.Text,
                    this.dateTimePicker1.Value, this.richTextBox1.Text))
                {
                    MessageBox.Show("Dodano pacjenta", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.textBox1.Text = String.Empty;
                    this.textBox2.Text = String.Empty;
                    this.textBox3.Text = String.Empty;
                    this.richTextBox1.Text = String.Empty;
                    return;
                }
            }

            MessageBox.Show("Nie udało się dodać pacjenta", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    
    }
}
