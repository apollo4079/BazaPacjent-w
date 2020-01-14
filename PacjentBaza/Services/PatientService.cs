using PacjentBaza.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace PacjentBaza.Services
{
    /// <summary>
    /// Klasa udostępniająca metody do zarządzania pacjentami zawiera metody dodające, modyfikujące, pobierające oraz usuwające pacjentów
    /// </summary>
    public class PatientService
    {
        private string _connectionString;
        /// <summary>
        /// W konstorze pobierane są dane do połączenia z bazy danych dane te pochodzą z pliku app.config - czyli pliku
        /// konfiguracyjnego aplikacji
        /// </summary>
        public PatientService()
        {
            this._connectionString = ConfigurationManager.AppSettings["DatabaseKey"];
        }

        /// <summary>
        /// Metoda dodająca pacjenta do bazy, parametry metody stanowią dane pacjenta
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="pesel"></param>
        /// <param name="birthDate"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool AddPatient(string firstName, string lastName, string pesel, DateTime birthDate, string address)
        {
            ///Tworzenie obiektu połączeniowego do bazy SQLite
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();

                // Tworzenie obiektu komendy - umożliwiającego wykonanie zapytania
                using (var cmd = new SQLiteCommand(con))
                {
                    // Zapytanie insert dodające dane do bazy
                    cmd.CommandText = "INSERT INTO Patient(FirstName, LastName,Pesel,BirthDate,Address) " +
                        "VALUES(@firstName, @lastName,@pesel,@birthDate,@address)";

                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@pesel", pesel);
                    cmd.Parameters.AddWithValue("@birthDate", birthDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Prepare();

                    // jeżeli metoda wykonująca zapytanie zwróci 1 oznacza to, że jeden wiersz uległ modyfikacji - ten dodawany
                    // czyli operacja przebiegła prawidłowo i metoda zwraca true
                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        /// <summary>
        /// Metoda modyfikująca danego pacjenta - działa podobnie jak metoda Add... z tą różicą, że 
        /// zamiast INSERT jest metoda UPDATE a pacjent jest wyszukiwany po id
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="pesel"></param>
        /// <param name="birthDate"></param>
        /// <param name="address"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EditPatient(string firstName, string lastName, string pesel, DateTime birthDate, string address, int id)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "UPDATE Patient set  FirstName = @firstName ," +
                        "LastName = @lastName,Pesel = @pesel,BirthDate = @birthDate,Address = @address " +
                        "WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@pesel", pesel);
                    cmd.Parameters.AddWithValue("@birthDate", birthDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        /// <summary>
        /// Metoda usuwająca pacjenta po określonym id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePatient(int id)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    //Najpierw usuwany jest pacjent
                    cmd.CommandText = "DELETE FROM Patient WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();

                    cmd.ExecuteNonQuery();

                    // Potem wszystkie dane o wirusach, które są powiązane z pacjentem
                    cmd.CommandText = "DELETE FROM Virus WHERE PatientId = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();

                    return cmd.ExecuteNonQuery() == 1;

                }
            }
        }

        // Metoda zwracająca listę pacjentów
        public List<Patient> GetPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(con))
                {
                    string stm = "SELECT Id,FirstName,LastName,BirthDate,Pesel,Address FROM Patient";
                    cmd.CommandText = stm;
                    // Wyniki z bazy danych są przetwarzane za pomocą SQLiteDataRead, pętla przechodzi po każdym wierszu
                    // czyli pacjencie, a Ci są dodawani do listy
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            patients.Add(new Patient(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2),
                                rdr.GetString(3), rdr.GetString(4), rdr.GetString(5)));
                        }
                    }
                }
            }
            return patients;
        }
    }
}
