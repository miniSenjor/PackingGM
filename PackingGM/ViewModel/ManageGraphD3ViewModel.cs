using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PackingGM.ViewModel
{
    public class ManageGraphD3ViewModel : BaseViewModel
    {
        public ManageGraphD3ViewModel()
        {
            _context = App.GetContext();
           lGMs = new ObservableCollection<GM>( _context.GMs
                .Include("OrderAggregate.Order.Contragent")
                .Include("OrderAggregate.Aggregate.AggregateType")
                .Include("GMNumber.D3Version.D3")
                .Include("GMNumber.SPU")
                .ToList());
            listGMs = new ObservableCollection<RecordGMTest>(
                lGMs.Select(g => new RecordGMTest(g)));


            //RecordGM._aggregateTypes = new ObservableCollection<AggregateType>(_context.AggregateTypes);
            //RecordGM._orders = new ObservableCollection<Order>(_context.Orders);
            //RecordGM._manufactories = new ObservableCollection<Manufactory>(_context.Manufactories);
            
            //foreach (var gm in gms)
            //{
            //    RecordGM record = new RecordGM();
            //    record.SelectedOrder = null;
            //    record.SelectedOrder = gm.OrderAggregate.Order;
            //    //record.SelectedAggregate = gm.OrderAggregate.Aggregate;
            //    record.SelectedGMNumber = gm.GMNumber;
            //    tempCollection.Add(record);
            //    //record.SelectedAggregateType = gm.OrderAggregate.Aggregate.DrawingName.AggregateType;
            //    //record.SelectedContragent = gm.OrderAggregate.Order.Contragent;

            //    // Принудительное обновление после создания
            //    //OnPropertyChanged(nameof(record.SelectedAggregate));
            //    //OnPropertyChanged(nameof(record.SelectedOrder));

            //}
            //ListGMs = new ObservableCollection<RecordGM>(tempCollection);


            //ListGMs = new ObservableCollection<RecordGM>
            //(_context.GMs
            //    .Select(g => new RecordGM
            //    {
            //        GM = g,
            //        SelectedAggregate = g.OrderAggregate.Aggregate,
            //        SelectedOrder = g.OrderAggregate.Order,
            //        SelectedGMNumber = g.GMNumber,
            //        SelectedAggregateType = g.OrderAggregate.Aggregate.DrawingName.AggregateType,
            //        SelectedContragent = g.OrderAggregate.Order.Contragent
            //    })
            //);
            //for (int i = 0; i < 0; i++)
            //{
            //    ListGMs.Add(new RecordGM { Number = i });
            //}
        }
        public ObservableCollection<GM> lGMs { get; set; }
        public ObservableCollection<RecordGMTest> listGMs { get; set; }
        private List<RecordGMTest> ListAdd { get; set; } = new List<RecordGMTest>();
        private List<RecordGMTest> ListDelete { get; set; } = new List<RecordGMTest>();
        private AppDb _context;
        private RecordGMTest _selectedGM;
        public RecordGMTest SelectedGM
        {
            get => _selectedGM;
            set
            {
                SetField(ref _selectedGM, value, nameof(SelectedGM));
                SelectionChanged();
            }
        }

        private RelayCommand _addGMCommand;
        public virtual RelayCommand AddGMCommand
        {
            get
            {
                if (_addGMCommand == null)
                    _addGMCommand = new RelayCommand(AddGM);
                return _addGMCommand;
            }
        }
        private void AddGM(object obj)
        {
            RecordGMTest recordGMTest = new RecordGMTest();
            listGMs.Add(recordGMTest);
            ListAdd.Add(recordGMTest);
        }

        private RelayCommand _deleteGMCommand;
        public virtual RelayCommand DeleteGMCommand
        {
            get
            {
                if (_deleteGMCommand == null)
                    _deleteGMCommand = new RelayCommand(DeleteGM);
                return _deleteGMCommand;
            }
        }
        private void DeleteGM(object obj)
        {
            try
            {
                if(ListAdd.Contains(SelectedGM))
                {
                    ListAdd.Remove(SelectedGM);
                }
                else
                    ListDelete.Add(SelectedGM);
                listGMs.Remove(SelectedGM);
                EntityUIUpdater.Unregister(SelectedGM);
            }
            catch(Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(Save);
                return _saveCommand;
            }
        }
        private void Save(object obj)
        {
            //из-за тригера не хочет сохранять данные
            try
            {
                foreach (var gm in ListAdd.Where(r=>r.OriginalGM!=null))
                {
                    if (gm != null)
                        _context.GMs.Add(gm.OriginalGM);
                }
                foreach (GM gm in ListDelete.Select(g => g.OriginalGM))
                {
                    if (gm != null)
                        _context.GMs.Remove(gm);
                }
                _context.SaveChanges();
                LoadListGM(null);
                StateApp.Instance.ChangeText("Успешно сохранено");
            }
            catch(Exception ex)
            {
                StateApp.Instance.ChangeAll(ex.ToString(), "red");
            }
        }

        private RelayCommand _loadListGMCommand;
        public RelayCommand LoadListGMCommand
        {
            get
            {
                if (_loadListGMCommand == null)
                    _loadListGMCommand = new RelayCommand(LoadListGM);
                return _loadListGMCommand;
            }
        }
        private void LoadListGM(object obj)
        {
            lGMs = new ObservableCollection<GM>(_context.GMs
                .Include("OrderAggregate.Order.Contragent")
                .Include("OrderAggregate.Aggregate.AggregateType")
                .Include("GMNumber.D3Version.D3")
                .Include("GMNumber.SPU")
                .ToList());
            listGMs = new ObservableCollection<RecordGMTest>(
                lGMs.Select(g => new RecordGMTest(g)));
            ListAdd.Clear();
            ListDelete.Clear();
            OnPropertyChanged(nameof(listGMs));
        }

        private void SelectionChanged()
        {
            if (SelectedGM != null)
            {
                SelectedGM.UpdateSource();
                RecordGMTest g = SelectedGM;
                Debug.Print($"AT={g.SelectedAggregateType?.Name}, A={g.SelectedAggregate?.Number}, O={g.SelectedOrder?.Number}, " +
                    $"Д3={g.SelectedD3?.Note}, ГМ={g.SelectedGMNumber?.NumberGM}, СПУ={g.SelectedSPU?.Note}, C={g.SelectedContragent?.Name}, " +
                    $"Цех={g.SelectedManufactory?.Number}");
            }
        }
    }

    public class RecordGMTest : BaseModel
    {
        public RecordGMTest()
        {
            EntityUIUpdater.RegisterInstance(this);
        }
        ~RecordGMTest()
        {
            EntityUIUpdater.Unregister(this);
        }
        public RecordGMTest(GM gm) : this()
        {
            OriginalGM = gm;
        }
        private static AppDb _context = App.GetContext();
        private GM _originalGM;
        public GM OriginalGM
        {
            get=>_originalGM ?? (_originalGM = new GM());
            set
            {
                SetField(ref _originalGM, value, nameof(OriginalGM));
            }
        }
        private static ObservableCollection<AggregateType> _aggregateTypes;
        public ObservableCollection<AggregateType> AggregateTypes
        {
            get => _aggregateTypes;
            set
            {
                SetField(ref _aggregateTypes, value, nameof(AggregateTypes));
            }
        }
        public AggregateType SelectedAggregateType
        {
            get => SelectedAggregate?.AggregateType;
            set
            {
                if (SelectedAggregate == null)
                {
                    UpdateSource();
                    return;
                }
                if (SelectedAggregateType!=null)
                {
                    OriginalGM.OrderAggregate.Aggregate.AggregateType = value;
                    EntityUIUpdater.UpdateUIForAggregateType(SelectedAggregate.Id);
                }
                else
                    OriginalGM.OrderAggregate.Aggregate.AggregateType = value;
                OnPropertyChanged(nameof(SelectedAggregateType));
            }
        } 

        private static ObservableCollection<Aggregate> _aggregates;
        public ObservableCollection<Aggregate> Aggregates
        {
            get => _aggregates;
            set
            {
                SetField(ref _aggregates, value, nameof(Aggregates));
            }
        }//по логике сначала выбирается заказ, потом агрегат
        public Aggregate SelectedAggregate
        {
            get => OriginalGM?.OrderAggregate?.Aggregate;
            set
            {
                if (SelectedOrder == null)
                    return;
                OriginalGM.OrderAggregate = _context.OrderAggregates.First(oa => oa.OrderId == SelectedOrder.Id && oa.AggregateId == value.Id);
                OnPropertyChanged(nameof(SelectedAggregate));
                OnPropertyChanged(nameof(SelectedAggregateType));
                UpdateAggregateTypes(SelectedAggregate);
                UpdateD3s(SelectedAggregate);
                return;
                if(_context.OrderAggregates.Any(oa=>oa.AggregateId == value.Id))
                    if(_context.OrderAggregates.Any(oa => oa.AggregateId == value.Id && oa.OrderId == SelectedOrder.Id))
                    {
                        OriginalGM.OrderAggregate = _context.OrderAggregates.First(oa => oa.AggregateId == value.Id && oa.OrderId == SelectedOrder.Id);
                        OnPropertyChanged(nameof(SelectedAggregate));
                    }
                    else
                    {

                    }

            }
        }

        private static ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders ?? (_orders = new ObservableCollection<Order>(_context.Orders));
            set
            {
                SetField(ref _orders, value, nameof(Orders));
            }
        }//сначала выбирается заказ потом агрегат
        public Order SelectedOrder
        {
            get => OriginalGM?.OrderAggregate?.Order;
            set
            {
                if (value == null) return;


                OriginalGM.OrderAggregate = _context.OrderAggregates.First(oa => oa.OrderId == value.Id);
                OnPropertyChanged(nameof(SelectedOrder));
                OnPropertyChanged(nameof(SelectedAggregate));
                OnPropertyChanged(nameof(SelectedAggregateType));
                OnPropertyChanged(nameof(SelectedContragent));
                UpdateAggregates(SelectedOrder);
                UpdateContragents(SelectedOrder);
                return;
                if (_context.OrderAggregates.Any(oa => oa.OrderId == value.Id))
                    if (_context.OrderAggregates.Any(oa => oa.OrderId == value.Id && oa.AggregateId == SelectedAggregate.Id))
                    {
                        OriginalGM.OrderAggregate = _context.OrderAggregates.First(oa => oa.OrderId == value.Id && oa.AggregateId == SelectedAggregate.Id);
                        OnPropertyChanged(nameof(SelectedOrder));
                    }
                    else
                    {

                    }
            }
        }

        private static ObservableCollection<D3> _d3s;
        public ObservableCollection<D3> D3s
        {
            get => _d3s;
            set
            {
                SetField(ref _d3s, value, nameof(D3s));
            }
        }
        public D3 SelectedD3
        {
            get => OriginalGM?.GMNumber?.D3Version?.D3;
            set
            {
                if (SelectedAggregate == null)
                    return;
                UpdateGMNumber(value);
                if(SelectedAggregateType!=null && OriginalGM.GMNumber!=null)
                    OriginalGM.GMNumber.D3Version.D3.AggregateType = SelectedAggregateType;
                OnPropertyChanged(nameof(SelectedD3));
                OnPropertyChanged(nameof(SelectedGMNumber));
            }
        }

        public string PR
        {
            get => OriginalGM?.PR;
            set
            {
                OriginalGM.PR = value;
                OnPropertyChanged(nameof(PR));
            }
        }

        private static ObservableCollection<GMNumber> _gmNumbers;
        public ObservableCollection<GMNumber> GMNumbers
        {
            get => _gmNumbers;
            set
            {
                SetField(ref _gmNumbers, value, nameof(GMNumbers));
            }
        }
        public GMNumber SelectedGMNumber
        {
            get => OriginalGM?.GMNumber;
            set
            {
                if (SelectedD3 == null || value == null)
                    return;
                OriginalGM.GMNumber = value;
                OriginalGM.GMNumber.SPU = _context.SPUs.First(s => s.Id == SelectedGMNumber.SPUId);
                OnPropertyChanged(nameof(SelectedGMNumber));
                OnPropertyChanged(nameof(SelectedSPU));
            }
        }

        public SPU SelectedSPU
        {
            get => SelectedGMNumber?.SPU;
        }

        private static ObservableCollection<Manufactory> _manufactories;
        public ObservableCollection<Manufactory> Manufactories
        {
            get => _manufactories ?? (_manufactories = new ObservableCollection<Manufactory>(_context.Manufactories));
            set
            {
                SetField(ref _manufactories, value, nameof(Manufactories));
            }
        }
        public Manufactory SelectedManufactory
        {
            get => OriginalGM?.Manufactory;
            set
            {
                if (SelectedGMNumber == null)
                    return;
                OriginalGM.Manufactory = value;
                OnPropertyChanged(nameof(SelectedManufactory));
            }
        }

        public string PlannedDeadline
        {
            get => OriginalGM.PlannedDeadline.ToString();
        }
        public int PlannedDeadlineWeek
        {
            get => OriginalGM.PlannedDeadline.DayOfYear / 7;
        }

        public int? Waybill
        {
            get => OriginalGM?.Waybill;
            set
            {
                OriginalGM.Waybill = value;
                OnPropertyChanged(nameof(Waybill));
            }
        }
        public string WaybillDate
        {
            get => OriginalGM.WaybillDate.ToString();
        }
        public int FactWeek
        {
            get => OriginalGM.FactWeek;
        }

        public string WhyDelay
        {
            get => OriginalGM.WhyDelay;
            set
            {
                OriginalGM.WhyDelay = value;
            }
        }

        private static ObservableCollection<Contragent> _contragents;
        public ObservableCollection<Contragent> Contragents
        {
            get => _contragents;
            set
            {
                SetField(ref _contragents, value, nameof(Contragents));
            }
        }
        public Contragent SelectedContragent
        {
            get => OriginalGM?.OrderAggregate?.Order?.Contragent;
            set
            {
                if (SelectedOrder == null)
                    return;
                if (SelectedAggregateType != null)
                {
                    OriginalGM.OrderAggregate.Order.Contragent = value;
                    EntityUIUpdater.UpdateUIForContragent(SelectedOrder.Id);
                }
                else
                    OriginalGM.OrderAggregate.Order.Contragent = value;
                OnPropertyChanged(nameof(SelectedContragent));
            }
        }

        public void UpdateSource()
        {
            UpdateAggregates(SelectedOrder);
            UpdateContragents(SelectedOrder);
            UpdateAggregateTypes(SelectedAggregate);
            UpdateD3s(SelectedAggregate);
            UpdateGMNumber(SelectedD3);
        }

        private void UpdateAggregateTypes(Aggregate aggregate)
        {
            if(aggregate == null)
                AggregateTypes = new ObservableCollection<AggregateType>();
            else
                AggregateTypes = new ObservableCollection<AggregateType>(_context.AggregateTypes);
        }

        private void UpdateAggregates(Order order)
        {
            if (order == null)
                Aggregates = null;
            else
                Aggregates = new ObservableCollection<Aggregate>(
                    _context.OrderAggregates
                    .Where(oa => oa.OrderId == order.Id)
                    .Select(oa => oa.Aggregate));
        }

        private void UpdateContragents(Order order)
        {
            if (order == null)
                Contragents = null;
            else
                Contragents = new ObservableCollection<Contragent>(_context.Contragents);
        }

        private void UpdateD3s(Aggregate aggregate)
        {
            //DrawingName dn = _context.DrawingNames.First(d => d.Id == aggregate.DrawingNameId);
            //DrawingNameVersion dnVersion = _context.DrawingNameVersions.First(dnv => dnv.DrawingNameId == dn.Id && dnv.State == 1);
            //D3s = new ObservableCollection<D3>(
            //    _context.DrawingNameD3s
            //    .Where(dd => dd.DrawingNameVersionId == dnVersion.Id)
            //    .Select(dd => dd.D3)
            //    .ToList());
            if (aggregate == null)
                D3s = null;
            else
                D3s = new ObservableCollection<D3>(_context.D3s);
        }

        private void UpdateGMNumber(D3 d3)
        {
            if (d3 == null)
                GMNumbers = null;
            else
                try
                {
                    D3Version d3Version = _context.D3Versions.First(d3v => d3v.D3Id == d3.Id && d3v.State == 1);
                    if(SelectedGMNumber==null || SelectedGMNumber.D3VersionId != d3Version.Id)
                        OriginalGM.GMNumber = _context.GMNumbers.First(gn => gn.D3VersionId == d3Version.Id);
                    GMNumbers = new ObservableCollection<GMNumber>(_context.GMNumbers.Where(gn => gn.D3VersionId == d3Version.Id));
                    StateApp.Instance.ChangeText("Готово");
                    OriginalGM.GMNumber.SPU = _context.SPUs.First(s => s.Id == SelectedGMNumber.SPUId);
                }
                catch
                {
                    StateApp.Instance.ChangeText("У этой ведомости нет активной версии");
                }
                finally
                {
                    OnPropertyChanged(nameof(SelectedGMNumber));
                    OnPropertyChanged(nameof(SelectedSPU));
                }
        }

        
    }

    public class RecordGM : BaseModel
    {
        public RecordGM()
        {
        }
        private AppDb _context = App.GetContext();
        private int _number;
        public int Number
        {
            get => _number;
            set => SetField(ref _number, value, nameof(Number));
        }

        private GM _gm;
        public GM GM
        {
            get => _gm;
            set => SetField(ref _gm, value, nameof(GM));
        }

        internal static ObservableCollection<AggregateType> _aggregateTypes;
        public ObservableCollection<AggregateType> AggregateTypes
        {
            get => _aggregateTypes ?? (_aggregateTypes = new ObservableCollection<AggregateType>(_context.AggregateTypes));
            set => SetField(ref _aggregateTypes, value, nameof(AggregateTypes));
        }

        private AggregateType _selectedAggregateType;
        public AggregateType SelectedAggregateType
        {
            get => _selectedAggregateType;
            set => SetField(ref _selectedAggregateType, value, nameof(SelectedAggregateType));
        }

        internal static ObservableCollection<Aggregate> _aggregates;
        public ObservableCollection<Aggregate> Aggregates
        {
            get => _aggregates ?? (_aggregates = new ObservableCollection<Aggregate>(_context.Aggregates));
            set => SetField(ref _aggregates, value, nameof(Aggregates));
        }

        
        private Aggregate _selectedAggregate;
        public Aggregate SelectedAggregate
        {
            get => _selectedAggregate;
            set
            {
                try
                {
                    SetField(ref _selectedAggregate, value, nameof(SelectedAggregate));
                    if (value == null)
                        return;
                    //DrawingName drawingName = _context.DrawingNames.First(dn => dn.Id == _selectedAggregate.DrawingNameId);
                
                    //DrawingNameVersion drawingNameVersion = _context.DrawingNameVersions.First(v => v.DrawingNameId == drawingName.Id && v.State== 1);
                    //D3s = new ObservableCollection<D3>();
                    //List<DrawingNameD3>  drawingNameD3s = _context.DrawingNameD3s.Where(dd => dd.DrawingNameVersionId == drawingNameVersion.Id).ToList();
                    //foreach (DrawingNameD3 dd in drawingNameD3s)
                    //{
                    //    D3s.Add(_context.D3s.First(d => d.Id == dd.D3Id));
                    //}
                    //OnPropertyChanged(nameof(D3s));
                    //SelectedD3 = D3s.First();
                    SelectedAggregateType = _selectedAggregate.AggregateType;
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }
        }

        internal static ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetField(ref _orders, value, nameof(Orders));
        }
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                SetField(ref _selectedOrder, value, nameof(SelectedOrder));
                //GM.OrderAggregate.Order = value;
                if (value == null)
                    return;
                Aggregates = new ObservableCollection<Aggregate>();
                List<OrderAggregate> orderAggregates = _context.OrderAggregates.Where(oa => oa.OrderId == SelectedOrder.Id).ToList(); 
                foreach (OrderAggregate oa in orderAggregates)
                {
                    Aggregates.Add(_context.Aggregates.First(a => a.Id == oa.AggregateId));
                }
                OnPropertyChanged(nameof(Aggregates));
                OrderAggregate orderAggregate = orderAggregates.First();
                SelectedAggregate = _context.Aggregates.First(a => a.Id == orderAggregate.AggregateId);
                SelectedContragent = _context.Contragents.First(c => c.Id == _selectedOrder.ContragentId);
            }
        }

        internal static ObservableCollection<D3> _d3s;
        public ObservableCollection<D3> D3s
        {
            get => _d3s;
            set => SetField(ref _d3s, value, nameof(D3s));
        }
        private D3 _selectedD3;
        public D3 SelectedD3
        {
            get => _selectedD3;
            set
            {
                try
                {
                    SetField(ref _selectedD3, value, nameof(SelectedD3));
                    if (value == null)
                        return;
                    D3Version d3Version = _context.D3Versions.First(dv => dv.D3Id == _selectedD3.Id && dv.State == 1);
                    GMNumbers = new ObservableCollection<GMNumber>(_context.GMNumbers.Where(gmn => gmn.D3VersionId == d3Version.Id));
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.ToString());
                }
            }
        }

        private static ObservableCollection<GMNumber> _gmNumbers;
        public ObservableCollection<GMNumber> GMNumbers
        {
            get => _gmNumbers;
            set => SetField(ref _gmNumbers, value, nameof(GMNumbers));
        }
        private GMNumber _selectedGMNumber;
        public GMNumber SelectedGMNumber
        {
            get => _selectedGMNumber;
            set
            {
                SetField(ref _selectedGMNumber, value, nameof(SelectedGMNumber));
                if (value == null) return;
                SelectedSPU = _context.SPUs.First(s => s.Id == _selectedGMNumber.SPUId);
            }
        }

        private static ObservableCollection<SPU> _spus;
        public ObservableCollection<SPU> SPUs
        {
            get => _spus;
            set => SetField(ref _spus, value, nameof(SPUs));
        }
        private SPU _selectedSPU;
        public SPU SelectedSPU
        {
            get => _selectedSPU;
            set => SetField(ref _selectedSPU, value, nameof(SelectedSPU));
        }

        internal static ObservableCollection<Manufactory> _manufactories;
        public ObservableCollection<Manufactory> Manufactories
        {
            get => _manufactories;
            set => SetField(ref _manufactories, value, nameof(Manufactories));
        }
        private Manufactory _selectedManufactory;
        public Manufactory SelectedManufactory
        {
            get => _selectedManufactory;
            set => SetField(ref _selectedManufactory, value, nameof(SelectedManufactory));
        }

        private Contragent _selectedContragent;
        public Contragent SelectedContragent
        {
            get => _selectedContragent;
            set => SetField(ref _selectedContragent, value, nameof(SelectedContragent));
        }

        public void UpdateSourse()
        {
            UpdateAggregates();
        }

        private void UpdateAggregates()
        {
            if (SelectedOrder == null)
            {
                Aggregates = new ObservableCollection<Aggregate>(_context.Aggregates);
                return;
            }
            var orderAggregates = _context.OrderAggregates
            .Where(oa => oa.OrderId == SelectedOrder.Id)
            .ToList();

            Aggregates = new ObservableCollection<Aggregate>(
                orderAggregates.Select(oa => _context.Aggregates.First(a => a.Id == oa.AggregateId)));
        }
    }
}
