using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PackingGM.ViewModel
{
    public class ManageGraphD3ViewModel : BaseViewModel
    {
        public ManageGraphD3ViewModel()
        {
            _context = App.GetContext();
            RecordGM._aggregateTypes = new ObservableCollection<AggregateType>(_context.AggregateTypes);
            RecordGM._orders = new ObservableCollection<Order>(_context.Orders);
            RecordGM._manufactories = new ObservableCollection<Manufactory>(_context.Manufactories);
            ListGMs = new ObservableCollection<RecordGM>();
            for(int i=0; i<4;i++)
            {
                ListGMs.Add(new RecordGM { Number = i });
            }
        }
        AppDb _context;
        public ObservableCollection<RecordGM> ListGMs { get; set; }
        public RecordGM SelectedGM { get; set; }

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
            ListGMs.Add(new RecordGM());
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
            //SelectedGM.SelectedOrder = RecordGM._orders.First();
            try
            {
                ListGMs.Remove(SelectedGM);
            }
            catch(Exception ex)
            {
                Debug.Print(ex.ToString());
            }
        }

        private RelayCommand _saveCommand;
        public virtual RelayCommand SaveCommand
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
            //DrawingName dn = new DrawingName()
            //{
            //    Note = "010.00.000.000-26",
            //    Name = "Агрегат газоперекачивающий ГПА-Ц-25НК/РМ. С (ПС-90)",
            //    AggregateType = _context.AggregateTypes.First(at => at.Name == "ГПА-25")
            //};
            //_context.DrawingNames.Add(dn);
            //_context.SaveChanges();
            //Aggregate ag = new Aggregate()
            //{
            //    Number = "R5А-068",
            //    DrawingName = dn,
            //};
            //_context.Aggregates.Add(ag);
            //Contragent c = new Contragent()
            //{
            //    Name = "КС Дивенская"
            //};
            //_context.Contragents.Add(c);
            //_context.SaveChanges();
            //Order o = new Order()
            //{
            //    Contragent = c,
            //    Number = 20256882
            //};
            //_context.Orders.Add(o);
            //_context.SaveChanges();
            //OrderAggregate oa = new OrderAggregate()
            //{
            //    Order = o,
            //    Aggregate = ag
            //};
            //_context.OrderAggregates.Add(oa);
            _context.SaveChanges();
        }
    }

    public class RecordGM : BaseModel
    {
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
            get => _aggregates;
            set => SetField(ref _aggregates, value, nameof(Aggregates));
        }

        private void UpdateAggregates()
        {
            if(SelectedOrder == null)
            {
                Aggregates = new ObservableCollection<Aggregate>(_context.Aggregates);
                return;
            }
            var orderAggregates = _context.OrderAggregates
            .Where(oa => oa.OrderId == SelectedOrder.Id)
            .ToList();

            Aggregates = new ObservableCollection<Aggregate>(
                orderAggregates.Select(oa => _context.Aggregates.First(a => a.Id == oa.AggregateId)));

            // Выбираем первый агрегат автоматически
            SelectedAggregate = Aggregates.FirstOrDefault();
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
                    DrawingName drawingName = _context.DrawingNames.First(dn => dn.Id == _selectedAggregate.DrawingNameId);
                
                    DrawingNameVersion drawingNameVersion = _context.DrawingNameVersions.First(v => v.DrawingNameId == drawingName.Id && v.State== 1);
                    D3s = new ObservableCollection<D3>();
                    List<DrawingNameD3>  drawingNameD3s = _context.DrawingNameD3s.Where(dd => dd.DrawingNameVersionId == drawingNameVersion.Id).ToList();
                    foreach (DrawingNameD3 dd in drawingNameD3s)
                    {
                        D3s.Add(_context.D3s.First(d => d.Id == dd.D3Id));
                    }
                    OnPropertyChanged(nameof(D3s));
                    SelectedD3 = D3s.First();
                    SelectedAggregateType = _selectedAggregate.DrawingName.AggregateType;
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
    }
}
