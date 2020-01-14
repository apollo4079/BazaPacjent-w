using System;
using System.Windows.Forms;

namespace PacjentBaza
{
    /// <summary>
    /// Forma - okno logowania
    /// </summary>
    public partial class LogonForm : Form
    {
        /// <summary>
        /// Właściwość eksponowania do stwierdzenia czy user zalogował się prawidłowo
        /// </summary>
        public bool LogonSuccessful { get; set; }
        public LogonForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metoda wywoływana po kliknięciu przycisk anuluj - okno jest zamykane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        /// <summary>
        /// Metoda wywoływana po kliknięciu w przycisk zaloguj jeżeli dane logowania są prawidłowe to zmieniamy właściwość
        /// informującą o zalogowaniu i zamykamy okno w przeciwnym wypadku komunikat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text == "admin" && this.textBox1.Text == "password")
            {
                this.LogonSuccessful = true;
                this.Dispose();

            }
            else
            {
                MessageBox.Show("Błędne dane logowania", "Ups...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
