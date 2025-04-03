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
            //Добавлены спу не из д3
            //AddInDb("select nmk_note, nmk_name from nmk where nmk_classif_id=2722 and nmk_classif_type_id=19", "SPU");
            //улучшенный наверное запрос
            //string s = "select n1.nmk_note, n.nmk_note, n.nmk_name from nmk n join specification s on s.nmk_id=n.nmk_id join version v on v.ver_id=s.ver_id join nmk n1 on n1.nmk_id=v.nmk_id where n.nmk_classif_id=2722 and n.nmk_classif_type_id=19 and v.ver_state=1 and n1.nmk_classif_id=22471";
            AddManufactories();

            //string s = "select n1.nmk_note, n1.nmk_name, v.ver_id, v.ver_name, v.ver_state, n.nmk_note, n.nmk_name from nmk n join specification s on s.nmk_id=n.nmk_id join version v on v.ver_id=s.ver_id join nmk n1 on n1.nmk_id=v.nmk_id where n.nmk_classif_id=2722 and n.nmk_classif_type_id=19";
            //AddInDb(s, "", 7);
            
            /*int countRow2 = 0;
            bool AAA = false;
            foreach(SPU spu in _context.SPUs)
            {
                AddInDb($"select v.ver_id, v.ver_name, v.ver_state from version v join nmk n on n.nmk_id=v.nmk_id where n.nmk_note='{spu.Note}' and v.ver_type='S'", "SPUVersion", spu.Id);
                countRow2++;
                Debug.Print(countRow2.ToString());
                if (!AAA && countRow2 > 10000)
                {
                    AAA = true;
                    Debug.Print("AAA");
                }
            }*/
        }
        private void AddInDb(string query, string table, int foreignKey=-1)
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
                            switch (table)
                            {
                                case "SPU":
                                    while (reader.Read())
                                    {
                                        AddSPUInDb(reader);
                                        countRow++;
                                        Debug.Print(countRow.ToString());
                                        if (countRow > 5400)
                                            MessageBox.Show("AAAAAAA");
                                    }
                                    break;
                                case "SPUVersion":
                                    while (reader.Read())
                                    {
                                        AddSPUVersionInDb(reader, foreignKey);
                                        countRow++;
                                        //Debug.Print(countRow.ToString());
                                        if (countRow > 10000)
                                            Debug.Print("AAAAAAA");
                                    }
                                    break;
                                case "Tare":
                                    while (reader.Read())
                                    {
                                        AddTareInDb(reader);
                                        countRow++;
                                        Debug.Print(countRow.ToString());
                                        if (countRow > 10000)
                                            MessageBox.Show("AAAAAAA");
                                    }
                                    break;
                                default:
                                    while(reader.Read())
                                        UnivarsalQuery(reader, foreignKey);
                                    break;
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
        private void UnivarsalQuery(SqlDataReader reader, int countColumn)
        {
            string respons = "";
            D3 d3 = new D3
            {
                Note = reader.GetValue(0).ToString(),
                Name = reader.GetValue(1).ToString(),
                NormalizedText = reader.GetValue(0).ToString()
            };
            if (!_context.D3s.Any(d => d.Note == d3.Note))
            {
                _context.D3s.Add(d3);
                _context.SaveChanges();
            }
            d3 = _context.D3s.First(d => d.Note == d3.Note);
            D3Version d3Version = new D3Version
            {
                D3Id = d3.Id,
                IdTCS = Convert.ToInt32(reader.GetValue(2)),
                Name = reader.GetValue(3).ToString(),
                State = Convert.ToInt16(reader.GetValue(4))
            };
            if(!_context.D3Versions.Any(d => d.IdTCS == d3Version.IdTCS))
            {
                _context.D3Versions.Add(d3Version);
                _context.SaveChanges();
            }
            d3Version = _context.D3Versions.First(d => d.IdTCS == d3Version.IdTCS);
            SPU spu = new SPU
            {
                Note = reader.GetValue(5).ToString(),
                Name = reader.GetValue(6).ToString(),
                NormalizedText = reader.GetValue(5).ToString()
            };
            if(!_context.SPUs.Any(s => s.Note == spu.Note))
            {
                _context.SPUs.Add(spu);
                _context.SaveChanges();
            }
            spu = _context.SPUs.First(s => s.Note == spu.Note);


            for(int i=0; i<countColumn; i++)
            {
                respons += $"{i})"+reader.GetValue(i).ToString()+" ";
            }
            Debug.Print(respons);
        }
        private void AddSPUVersionInDb(SqlDataReader reader, int foreignKey)
        {
            SPUVersion sVersion = new SPUVersion();
            sVersion.IdTCS = Convert.ToInt32(reader.GetValue(0));
            sVersion.Name = reader.GetValue(1).ToString();
            sVersion.State = Convert.ToInt16(reader.GetValue(2));
            sVersion.SPUId = foreignKey;
            if(_context.SPUVersions.Any(s => s.IdTCS == sVersion.IdTCS))
                Debug.Print("Alert!!");
            else
            {
                _context.SPUVersions.Add(sVersion);
                _context.SaveChanges();
            }
        }
        private void AddTareInDb(SqlDataReader reader)
        {
            Tare tare = new Tare();
            tare.Note = reader.GetValue(0).ToString();
            tare.Name = reader.GetValue(1).ToString();
            //spu.NormalizedText = spu.Note;
            if (_context.Tares.Any(s => s.Note == tare.Note && s.Note!=""))
                Debug.Print("Alert!!");
            else
            {
                _context.Tares.Add(tare);
                _context.SaveChanges();
            }
        }
        private void AddSPUInDb(SqlDataReader reader)
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
        }
        private void AddManufactories()
        {
            _context.Manufactories.Add(new Manufactory { Number = "926"});
            _context.Manufactories.Add(new Manufactory { Number = "927"});
            _context.Manufactories.Add(new Manufactory { Number = "928"});
            _context.Manufactories.Add(new Manufactory { Number = "343"});
            _context.Manufactories.Add(new Manufactory { Number = "ОЭД"});
            _context.SaveChanges();
        }

    }
}
