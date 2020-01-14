using PacjentBaza.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

namespace PacjentBaza.Services
{
    /// <summary>
    /// W PatientService zostały opisane działania bardziej szczegółowo - VirusService działa analogicznie za wyjątkiem tego, że
    /// dotyczy danych o wirusach, a nie o pacjentach
    /// </summary>
    public class VirusService
    {
        private string _connectionString;
        public VirusService()
        {
            this._connectionString = ConfigurationManager.AppSettings["DatabaseKey"];
        }
        public bool Add(string description, int patientId)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO Virus(Description,PatientId) " +
                        "VALUES(@description,@patientId)";

                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@patientId", patientId);

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Edit(string description, int patientId,int id)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "UPDATE Virus set Description = @description, PatientId = @patientId " +
                        "WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@patientId", patientId);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }

        public bool Delete(int id)
        {
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = "DELETE FROM Virus WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();

                    return cmd.ExecuteNonQuery() == 1;
                }
            }
        }
        public List<Virus> Get()
        {
            List<Virus> patients = new List<Virus>();
            using (var con = new SQLiteConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(con))
                {
                    string stm = "SELECT Virus.Id,Description,FirstName || ' ' || LastName as 'Ls', PatientId FROM Virus " +
                        "INNER JOIN Patient ON Virus.PatientId = Patient.Id";
                    cmd.CommandText = stm;
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {

                        while (rdr.Read())
                        {
                            try
                            {
                                patients.Add(new Virus(rdr.GetInt32(0), rdr.GetString(2), rdr.GetString(1), rdr.GetInt32(3)));
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            return patients;
        }
    }
}
