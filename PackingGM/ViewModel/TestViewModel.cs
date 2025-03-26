using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace PackingGM.ViewModel
{
    public class TestViewModel
    {
        public TestViewModel()
        {

        }
        AppDb _context = App.GetContext();
        public string Query { get; set; }
        public string Respons { get; set; }
        private RelayCommand _runQueryCommand;
        public RelayCommand RunQueryCommand
        {
            get
            {
                if (_runQueryCommand == null)
                    _runQueryCommand = new RelayCommand(Run);
                return _runQueryCommand;
            }
        }
        private void Run(object obj)
        {
            AddSPUInDb("select nmk_note, nmk_name from nmk where nmk_classif_id=2722 and nmk_classif_type_id=19", 2);
        }
        private void AddTaraInDb(string query, int countColumn)
        {
            string connString = "Server=gt-srv30;Database=gt;User Id=1587;Password=YD30K2D5;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int countRow = 0;
                            while (reader.Read())
                            {
                                Tare spu = new Tare();
                                spu.Note = reader.GetValue(0).ToString();
                                spu.Name = reader.GetValue(1).ToString();
                                //spu.NormalizedText = spu.Note;
                                if (_context.SPUs.Any(s => s.Note == spu.Note))
                                    Debug.Print("Alert!!");
                                else
                                {
                                    //_context.SPUs.Add(spu);
                                    _context.SaveChanges();
                                }
                                countRow++;
                                Debug.Print(countRow.ToString());
                                if (countRow > 5400)
                                    MessageBox.Show("AAAAAAA");
                            }
                            Respons = _context.SPUs.Count().ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Respons = ex.ToString();
                }
            }
        }
        private void AddSPUInDb(string query, int countColumn)
        {
            string connString = "Server=gt-srv30;Database=gt;User Id=1587;Password=YD30K2D5;";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int countRow = 0;
                            while (reader.Read())
                            {
                                SPU spu = new SPU();
                                spu.Note = reader.GetValue(0).ToString();
                                spu.Name = reader.GetValue(1).ToString();
                                spu.NormalizedText = spu.Note;
                                if (_context.SPUs.Any(s => s.Note == spu.Note))
                                    Debug.Print("Alert!!");
                                else
                                {
                                    _context.SPUs.Add(spu);
                                    _context.SaveChanges();
                                }
                                countRow++;
                                Debug.Print(countRow.ToString());
                                if (countRow > 5400)
                                    MessageBox.Show("AAAAAAA");
                            }
                            Respons = _context.SPUs.Count().ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Respons = ex.ToString();
                }
            }
        }

    }
}
