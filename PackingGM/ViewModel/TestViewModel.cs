using PackingGM.Data;
using PackingGM.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace PackingGM.ViewModel
{
    public class TestViewModel : BaseViewModel
    {
        public TestViewModel()
        {
            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }
        private readonly BackgroundWorker _worker;
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string connString = "Server=gt-srv30;Database=gt;User Id=1587;Password=YD30K2D5;";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query;
                    int countRow = 0;
                    switch (Query.ToLower())
                    {
                        case "order":
                            query = "select n.norder_name, p.par_value from norders n join part_val1 p on n.norder_id=p.parval_id where p.par_id=1597 and p.par_value<>''";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        AddOrderInDb(reader);
                                        if ((countRow / 10) < (++countRow / 10))
                                            StateApp.Instance.ChangeText(countRow.ToString());
                                    }
                                }
                            }
                            break;
                        case "aggregate":
                            foreach (Order order in _context.Orders)
                            {
                                query = $"select p.par_value from norders n join part_val1 p on n.norder_id=p.parval_id where p.par_id=1597 and (n.norder_name='{order.Number}' or n.norder_name='{order.Number}    ')";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while(reader.Read())
                                        {
                                            AddAggregateInDb(reader, order);
                                            if ((countRow / 10) < (++countRow / 10))
                                                StateApp.Instance.ChangeText(countRow.ToString());
                                        }
                                    }
                                }
                            }
                            break;
                        case "d3":
                            query = $"select nmk_note, nmk_name from nmk where nmk_classif_id=22471 and nmk_classif_type_id=19";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        AddD3InDb(reader);
                                        if ((countRow / 10) < (++countRow / 10))
                                            StateApp.Instance.ChangeText(countRow.ToString());
                                    }
                                }
                            }
                           break;
                        case "d3version"://65 bp 73
                            foreach(D3 d3 in _context.D3s)
                            {
                                query = $"select v.ver_id, v.ver_name, v.ver_state from version v join nmk n on v.nmk_id=n.nmk_id where n.nmk_note='{d3.Note}' and v.ver_state=1 and v.ver_type='S'";
                                //для заполнения все версий Д3(акьтвных в архиве и др.)
                                //query = $"select v.ver_id, v.ver_name, v.ver_state from version v join nmk n on v.nmk_id=n.nmk_id where n.nmk_note='{d3.Note}' and v.ver_type='S'";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            AddD3VersionInDb(reader, d3);
                                            if ((countRow / 10) < (++countRow / 10))
                                                StateApp.Instance.ChangeText(countRow.ToString());
                                        }
                                    }
                                }
                            }
                            break;
                        case "gm":
                            foreach (D3Version d3Version in _context.D3Versions)
                            {
                                query = $"select n.nmk_note, n.nmk_name, sp.specpar_value from nmk n join specification s on s.nmk_id=n.nmk_id join specpar sp on s.spec_id=sp.spec_id and s.ver_id=sp.ver_id where s.ver_id={d3Version.IdTCS} and sp.par_id=1576 and n.nmk_classif_id=2722";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while(reader.Read())
                                        {
                                            AddSPUInDb(reader, d3Version);
                                            if ((countRow / 10) < (++countRow / 10))
                                                StateApp.Instance.ChangeText(countRow.ToString());
                                        }
                                    }
                                }
                            }
                            break;
                        case "spuversion"://пока не заполнено
                            foreach(SPU spu in _context.SPUs)
                            {
                                query = $"select v.ver_id, v.ver_name, v.ver_state from version v join nmk n on v.nmk_id=n.nmk_id where n.nmk_note='{spu.Note}' and v.ver_state=1 and v.ver_type='S'";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            AddSPUVersionInDb(reader, spu);
                                            if ((countRow / 10) < (++countRow / 10))
                                                StateApp.Instance.ChangeText(countRow.ToString());
                                        }
                                    }
                                }
                            }
                            break;
                        case "tare":
                            foreach(SPUVersion spuVersion in _context.SPUVersions)
                            {
                                query = $"select n.nmk_note, n.nmk_name, spec_quantity from nmk n join specification s on s.nmk_id=n.nmk_id where s.ver_id={spuVersion.IdTCS} and (n.nmk_classif_id=23239 or n.nmk_classif_id=23240 or n.nmk_classif_id=22558 or n.nmk_classif_id=5404 or n.nmk_classif_id=23883)";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            AddTareInDb(reader, spuVersion);
                                            if ((countRow / 10) < (++countRow / 10))
                                                StateApp.Instance.ChangeText(countRow.ToString());
                                        }
                                    }
                                }
                            }
                            break;
                        case "drawingname":
                            foreach(D3 d3 in _context.D3s)
                            {
                                query = $"select n2.nmk_note, n2.nmk_name, s.ver_id from nmk n join specification s on s.nmk_id=n.nmk_id join version v on v.ver_id=s.ver_id join nmk n2 on n2.nmk_id=v.nmk_id where n.nmk_note='{d3.Note}' and v.ver_state=1";
                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            AddDrawingNameInDb(reader, d3);
                                            if ((countRow / 10) < (++countRow / 10))
                                                StateApp.Instance.ChangeText(countRow.ToString());
                                        }
                                    }
                                }
                            }
                            break;
                        case "manufactory":
                            AddManufactories();
                            break;
                        default:
                            StateApp.Instance.ChangeText("Не известное имя таблицы");
                            break;
                    }
                    e.Result = true;
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error!=null)
            {
                MessageBox.Show(e.Error.ToString());
                StateApp.Instance.ChangeAll(e.Error.Message, "red");
            }
            else if (e.Result is Exception ex)
            {
                MessageBox.Show(ex.ToString());
                StateApp.Instance.ChangeAll("Ошибка: " + ex.Message, "red");

            }
            else if ((bool)e.Result)
            {
                StateApp.Instance.ChangeAll("Успешно заполнено!", "blue");
            }
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
            StateApp.Instance.ChangeAll("Начато заполнение бд", "blue");
            _worker.RunWorkerAsync();
            //Добавлены спу не из д3
            //AddInDb("select nmk_note, nmk_name from nmk where nmk_classif_id=2722 and nmk_classif_type_id=19", "SPU");
            //улучшенный наверное запрос
            //string s = "select n1.nmk_note, n.nmk_note, n.nmk_name from nmk n join specification s on s.nmk_id=n.nmk_id join version v on v.ver_id=s.ver_id join nmk n1 on n1.nmk_id=v.nmk_id where n.nmk_classif_id=2722 and n.nmk_classif_type_id=19 and v.ver_state=1 and n1.nmk_classif_id=22471";
            //AddManufactories();

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
                                        //AddSPUInDb(reader);
                                        countRow++;
                                        Debug.Print(countRow.ToString());
                                        if (countRow > 5400)
                                            MessageBox.Show("AAAAAAA");
                                    }
                                    break;
                                case "SPUVersion":
                                    while (reader.Read())
                                    {
                                        //AddSPUVersionInDb(reader, foreignKey);
                                        countRow++;
                                        //Debug.Print(countRow.ToString());
                                        if (countRow > 10000)
                                            Debug.Print("AAAAAAA");
                                    }
                                    break;
                                case "Tare":
                                    while (reader.Read())
                                    {
                                        //AddTareInDb(reader);
                                        countRow++;
                                        Debug.Print(countRow.ToString());
                                        if (countRow > 10000)
                                            MessageBox.Show("AAAAAAA");
                                    }
                                    break;
                                case "Order":
                                    while(reader.Read())
                                    {
                                        AddOrderInDb(reader);
                                        if((countRow/10)<(++countRow/10))
                                            StateApp.Instance.ChangeText(countRow.ToString());
                                    }
                                    break;
                                case "Aggregate":
                                    while (reader.Read())
                                    {
                                        //AddAggregateInDb(reader);
                                        if ((countRow / 10) < (++countRow / 10))
                                            StateApp.Instance.ChangeText(countRow.ToString());
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

        private void AddDrawingNameInDb(SqlDataReader reader, D3 d3)
        {
            //if(reader.GetValue(0)==null)
            //{
            //    Debug.Print("Alert!" + d3.Note);
            //    return;
            //}
            //string note = reader.GetValue(0).ToString();
            //DrawingName dn = new DrawingName();
            //if (_context.DrawingNames.Any(d => d.Note == note))
            //    Debug.Print("Alert!" + note);
            //else
            //{
            //    dn.Note = note;
            //    dn.Name = reader.GetValue(1).ToString();
            //    dn.NormalizedText = note;
            //    _context.DrawingNames.Add(dn);
            //    _context.SaveChanges();
            //}
            //dn = _context.DrawingNames.First(d => d.Note == note);
            //int verId = Convert.ToInt32(reader.GetValue(2));
            //DrawingNameVersion dv = new DrawingNameVersion();
            //if (_context.DrawingNameVersions.Any(d => d.IdTCS==verId))
            //    Debug.Print($"Версия Д3 {verId}");
            //else
            //{
            //    dv.IdTCS = verId;
            //    dv.State = 1;
            //    dv.DrawingNameId = dn.Id;
            //    _context.DrawingNameVersions.Add(dv);
            //    _context.SaveChanges();
            //}
            //dv = _context.DrawingNameVersions.First(d => d.IdTCS == verId);
            //if (_context.DrawingNameD3s.Any(d => d.D3Id == d3.Id && d.DrawingNameVersionId == dv.Id))
            //    Debug.Print($"Связь Д3 Чертежное обозначение существует {d3.Note} {dv.IdTCS}");
            //else
            //{
            //    DrawingNameD3 dd = new DrawingNameD3
            //    {
            //        D3Id = d3.Id,
            //        DrawingNameVersionId = dv.Id
            //    };
            //    _context.DrawingNameD3s.Add(dd);
            //    _context.SaveChanges();
            //}
        }

        private void AddTareInDb(SqlDataReader reader, SPUVersion spuVersion)
        {
            string note = reader.GetValue(0).ToString();
            string name = reader.GetValue(1).ToString();
            Tare tare = new Tare();
            if (_context.Tares.Any(t => t.Note == note && t.Name==name))
                Debug.Print($"Alert!! {note} {name}");
            else
            {
                tare.Note = note;
                tare.Name = name;
                tare.NormalizedText = note;
                _context.Tares.Add(tare);
                _context.SaveChanges();
            }
            tare = _context.Tares.First(t => t.Note == note && t.Name == name);
            if (_context.SPUTares.Any(st => st.SPUVersionId == spuVersion.Id && st.TareId == tare.Id))
                Debug.Print($"Alert!! {spuVersion.Name}_{note}");
            else
            {
                SPUTare st = new SPUTare
                {
                    TareId = tare.Id,
                    SPUVersionId = spuVersion.Id,
                    CountNeed = Convert.ToInt32(reader.GetValue(2))
                };
                _context.SPUTares.Add(st);
                _context.SaveChanges();
            }
        }

        
        private void AddSPUInDb(SqlDataReader reader, D3Version d3Version)
        {
            string note = reader.GetValue(0).ToString();
            SPU spu = new SPU();
            if (_context.SPUs.Any(s => s.Note == note))
                Debug.Print("Alert!!" + note);
            else
            {
                spu.Note = note;
                spu.Name = reader.GetValue(1).ToString();
                spu.NormalizedText = note;
                _context.SPUs.Add(spu);
                _context.SaveChanges();
            }
            spu = _context.SPUs.First(s => s.Note == note);
            string numberGM = reader.GetValue(2).ToString();
            if(_context.GMNumbers.Any(g=>g.D3VersionId==d3Version.Id && g.NumberGM==numberGM && g.SPUId==spu.Id))
                Debug.Print($"Alert!! {d3Version.Name}-{note}-{numberGM}");
            else
            {
                GMNumber gm = new GMNumber
                {
                    NumberGM = numberGM,
                    SPUId = spu.Id,
                    D3VersionId = d3Version.Id
                };
                _context.GMNumbers.Add(gm);
                _context.SaveChanges();
            }
        }

        private void AddSPUVersionInDb(SqlDataReader reader, SPU spu)
        {
            int idTCS = Convert.ToInt32(reader.GetValue(0));
            short state = Convert.ToInt16(reader.GetValue(2));
            if (_context.SPUVersions.Any(sv => sv.IdTCS == idTCS))
            {
                _context.SPUVersions.First(sv => sv.IdTCS == idTCS).State = state;
                _context.SaveChanges();
                return;
            }
            SPUVersion spuVersion = new SPUVersion
            {
                IdTCS = idTCS,
                Name = reader.GetValue(1).ToString(),
                State = state,
                SPUId = spu.Id
            };
            _context.SPUVersions.Add(spuVersion);
            _context.SaveChanges();
        }
        private void AddD3InDb(SqlDataReader reader)
        {
            string s = reader.GetValue(0).ToString().Trim(' ');
            if(s.Substring(s.Length - 2) != "Д3")
                return;
            D3 d3 = new D3
            {
                Note = s,
                Name = reader.GetValue(1).ToString().Trim(' '),
                NormalizedText = s,
            };
            if(!_context.D3s.Any(d=>d.Note==d3.Note))
            {
                _context.D3s.Add(d3);
                _context.SaveChanges();
            }
        }

        private void AddD3VersionInDb(SqlDataReader reader, D3 d3)
        {
            int idTCS = Convert.ToInt32(reader.GetValue(0));
            short state = Convert.ToInt16(reader.GetValue(2));
            if (_context.D3Versions.Any(d3v => d3v.IdTCS == idTCS))
            {
                _context.D3Versions.First(d3v => d3v.IdTCS == idTCS).State = state;
                _context.SaveChanges();
                return;
            }
            D3Version d3Version = new D3Version
            {
                IdTCS = idTCS,
                Name = reader.GetValue(1).ToString(),
                State = state,
                D3Id = d3.Id
            };
            _context.D3Versions.Add(d3Version);
            _context.SaveChanges();
        }

        private void AddOrderInDb(SqlDataReader reader)
        {
            Order order = new Order();
            order.Number = reader.GetValue(0).ToString().Trim(' ');
            if (_context.Orders.Any(o => o.Number == order.Number))
                Debug.Print($"Alert!!{order.Number}");
            else
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }

        private void AddAggregateInDb(SqlDataReader reader, Order order)
        {
            try
            {
                string s = reader.GetValue(0).ToString().Trim(' ');
                if (s == null || s == "") return;
                if (s.Contains('.') || s.Contains(',') || s.Contains(':') || s.Contains(' ') || s.Contains('…'))
                {
                    char c = s.First(a=>a=='.' || a==',' || a==' ' || a==':' || a== '…');
                    switch (c)
                    {
                        case '…':
                        case '.':
                        case ':':
                        case ' ':
                            WorkToSubSInAddAggregate(s, c, order);
                            break;
                        case ',':
                            if(s.Contains(", "))
                            {
                                WorkToSubSInAddAggregate(s, c, order);
                            }
                            else
                            {
                                string subS = s.Substring(0, s.IndexOf(c));
                                AddAggregate(subS, order);
                                subS = s.Substring(s.IndexOf(c)+1);
                                AddAggregate(subS, order);
                            }
                            break;
                    }
                }
                else
                {
                    AddAggregate(s, order);
                }
            }
            catch(Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }
        private void AddAggregate(string s, Order order)
        {
            Aggregate aggregate = new Aggregate();
            if (!_context.Aggregates.Any(a => a.Number == s))
            {
                aggregate.Number = s;
                _context.Aggregates.Add(aggregate);
                _context.SaveChanges();
            }
            aggregate = _context.Aggregates.First(a => a.Number == s);
            if (!_context.OrderAggregates.Any(oa => oa.AggregateId == aggregate.Id && oa.OrderId == order.Id))
            {
                OrderAggregate orderAggregate = new OrderAggregate
                {
                    OrderId = order.Id,
                    AggregateId = aggregate.Id
                };
                _context.OrderAggregates.Add(orderAggregate);
                _context.SaveChanges();
            }
        }
        private void WorkToSubSInAddAggregate(string s, char c, Order order)
        {
            string firstSubS = s.Substring(0, s.IndexOf(c));
            int firstSubSInt = Convert.ToInt32(firstSubS.Substring(firstSubS.Length - 3));
            int lastSubSInt = Convert.ToInt32(s.Substring(s.Length - 3));
            firstSubS = firstSubS.Substring(0, firstSubS.Length - 3);
            for (int i = firstSubSInt; i <= lastSubSInt; i++)
            {
                if (i < 10)
                    s = firstSubS + "00" + i.ToString();
                else if(i<100)
                    s = firstSubS + "0" + i.ToString();
                else
                    s = firstSubS + i.ToString();
                AddAggregate(s, order);
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
