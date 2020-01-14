using System;
using System.Drawing;
using System.Windows.Forms;

namespace PacjentBaza
{
    /// <summary>
    /// Główna forma aplikacji
    /// </summary>
    public partial class Form1 : Form
    {
        private FormAdd _formAdd;
        private FormData _formData;
        private FormAddVirus _formAddVirus;
        private FormDataVirus _formDataVirus;
        public Form1()
        {
            InitializeComponent();
            // Właściwośc ta sprawia, że jest to kontener MDI oznacza to, że może on otwierać okna jakby "wewnątrz" tej formy.
            this.IsMdiContainer = true;
            
        }

        /// <summary>
        /// Metoda wywoływana po występieniu zdarzenia kliknięcia myszy na przycisku,
        /// reszta metod jest analogiczna - dotyczą każdego przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            /// Ustawiamy tło wszystkich przycisków na biały - systemowy ControlLightLight
            this.toolStripButton1.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton2.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton3.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton4.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton5.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton6.BackColor = SystemColors.ControlLightLight;
            /// Tło tego konkretnego przycisku ustawiamy na systemowy ButtonFace co odpowiada kolorowi szaremu
            ((ToolStripButton)sender).BackColor = SystemColors.ButtonFace;
            // Jeżeli okno nie istnieje to uruchamiamy formę FormAdd - dodawanie pacjenta
            // ustawiamy, żeby MdiParent było okno główne, tak, że nowe okno otworzy się wewnątrz
            if (_formAdd == null)
            {
                _formAdd = new FormAdd();
                _formAdd.MdiParent = this;
                _formAdd.Show();
            }
            // Jeżeli okno było zamknięte to zrobimy to samo utworzymy nowe
            else if (_formAdd.IsDisposed)
            {
                _formAdd = new FormAdd();
                _formAdd.MdiParent = this;
                _formAdd.Show();
            }
            // Zawsze przesuwamy okno najwyżej jako najwyżej widoczne
            _formAdd.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            this.toolStripButton1.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton2.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton3.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton4.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton5.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton6.BackColor = SystemColors.ControlLightLight;
            ((ToolStripButton)sender).BackColor = SystemColors.ButtonFace;
            if (_formData == null)
            {
                _formData = new FormData();
                _formData.MdiParent = this;
                _formData.Show();
            }
            else if (_formData.IsDisposed)
            {
                _formData = new FormData();
                _formData.MdiParent = this;
                _formData.Show();
            }

            _formData.BringToFront();
            _formData.LoadDataSource();
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            this.toolStripButton1.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton2.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton3.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton4.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton5.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton6.BackColor = SystemColors.ControlLightLight;
            ((ToolStripButton)sender).BackColor = SystemColors.ButtonFace;
            if (_formAddVirus == null)
            {
                _formAddVirus = new FormAddVirus();
                _formAddVirus.MdiParent = this;
                _formAddVirus.Show();
            }
            else if (_formAddVirus.IsDisposed)
            {
                _formAddVirus = new FormAddVirus();
                _formAddVirus.MdiParent = this;
                _formAddVirus.Show();
            }

            _formAddVirus.BringToFront();
            _formAddVirus.LoadDataSource();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            this.toolStripButton1.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton2.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton3.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton4.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton5.BackColor = SystemColors.ControlLightLight;
            this.toolStripButton6.BackColor = SystemColors.ControlLightLight;

            ((ToolStripButton)sender).BackColor = SystemColors.ButtonFace;
            if (_formDataVirus == null)
            {
                _formDataVirus = new FormDataVirus();
                _formDataVirus.MdiParent = this;
                _formDataVirus.Show();
            }
            else if (_formDataVirus.IsDisposed)
            {
                _formDataVirus = new FormDataVirus();
                _formDataVirus.MdiParent = this;
                _formDataVirus.Show();
            }

            _formDataVirus.BringToFront();
            _formDataVirus.LoadDataSource();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
