using System.ComponentModel;

namespace PacjentBaza.Model
{
    /// <summary>
    /// Klasa przechowująca dane pacjenta
    /// </summary>
    public class Patient
    {
        public Patient(int id, string firstName, string lastName, string birthDate, string pesel, string address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Pesel = pesel;
            Address = address;
        }

        public int Id { get; private set; }
        [DisplayName("Imię i nazwisko")]
        public string FullName => $"{FirstName} {LastName}";
        [Browsable(false)]
        public string FirstName { get; private set; }
        [Browsable(false)]
        public string LastName { get; private set; }
        [DisplayName("Data urodzenia")]
        public string BirthDate { get; private set; }
        [DisplayName("Pesel")]
        public string Pesel { get; private set; }
        [DisplayName("Adres")]
        public string Address { get; private set; }

      
    }
}
