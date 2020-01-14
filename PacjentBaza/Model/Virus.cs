using System.ComponentModel;

namespace PacjentBaza.Model
{
    /// <summary>
    /// Klasa przechowująca dane o wirusach
    /// </summary>
    public class Virus
    {
        public Virus(int id, string patientFullName, string description,int patientId)
        {
            Id = id;
            PatientFullName = patientFullName;
            Description = description;
            PatientId = patientId;
        }

        public int Id { get; private set; }
        [DisplayName("Pacjent")]
        public string PatientFullName { get; private set; }
        [DisplayName("Opis wirusa")]
        public string Description { get; private set; }
        [Browsable(false)]
        public int PatientId { get;private set; }
        
    }
}
