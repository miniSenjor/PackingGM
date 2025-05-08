using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace PackingGM.ViewModel
{
    public class ManageGraphViewModel : BaseViewModel
    {
        public ManageGraphViewModel()
        {
            _context = App.GetContext();
            LoadGM();
        }
        private AppDb _context;
        private ObservableCollection<GMTare> lGMs { get; set; }
        public ObservableCollection<Record> ListGMs { get; set; }
        private Record _selectedGMTare;
        public Record SelectedGMTare
        {
            get => _selectedGMTare;
            set
            {
                SetField(ref _selectedGMTare, value, nameof(SelectedGMTare));
                SelectionChanged();
            }
        }
        private void LoadGM()
        {
            lGMs = new ObservableCollection<GMTare>(_context.GMTares
                .Include("GM.OrderAggregate.Order.Contragent")
                .Include("GM.OrderAggregate.Aggregate.AggregateType")
                .Include("GM.GMNumber.SPU")
                .Include("SPUTare.Tare")
                .Include("GM.Manufactory")
                .Where(g => g.GM.GMNumber != null));
            ListGMs = new ObservableCollection<Record>(
                lGMs.Select(g => new Record(g)));
        }
        private void SelectionChanged()
        {
            if (SelectedGMTare != null)
            {
                SelectedGMTare.UpdateSource();
                Record g = SelectedGMTare;
                Debug.Print($"AT={g.SelectedAggregateType?.Name}, A={g.SelectedAggregate?.Number}, O={g.SelectedOrder?.Number}, " +
                    $"ГМ={g.SelectedGMNumber?.NumberGM}, СПУ={g.SelectedSPU?.Note}, C={g.SelectedContragent?.Name}, " +
                    $"Цех={g.SelectedManufactory?.Number}");
            }
        }

        protected override void Back(object obj)
        {
            Navigation.Navigate(PageType.MainView);
            ListGMs = null;
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
            _context.SaveChanges();
        }
    }
    
    public class Record : BaseModel
    {
        public Record()
        {

        }
        public Record(GMTare gm) : this()
        {
            OriginalGM = gm;
        }
        private static AppDb _context = App.GetContext();
        private GMTare _originalGM;
        public GMTare OriginalGM
        {
            get => _originalGM ?? (_originalGM = new GMTare());
            set
            {
                SetField(ref _originalGM, value, nameof(OriginalGM));
            }
        }

        private static ObservableCollection<Manufactory> _manufactories;
        public ObservableCollection<Manufactory> Manufactories
        {
            get => _manufactories;
            set
            {
                _manufactories = value;
                OnPropertyChanged(nameof(Manufactories));
            }
        }
        public Manufactory SelectedManufactory
        {
            get => OriginalGM?.GM?.Manufactory;
            set
            {
                OriginalGM.GM.Manufactory = value;
                OnPropertyChanged(nameof(SelectedManufactory));
            }
        }

        public AggregateType SelectedAggregateType
        {
            get => SelectedAggregate?.AggregateType;
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
            get => OriginalGM?.GM?.OrderAggregate?.Order?.Contragent;
            set
            {

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
        }
        public Aggregate SelectedAggregate
        {
            get => OriginalGM?.GM?.OrderAggregate?.Aggregate;
            set
            {
                //
            }
        }

        private static ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders ?? (_orders = new ObservableCollection<Order>(_context.Orders));
            set => SetField(ref _orders, value, nameof(Orders));
        }
        public Order SelectedOrder
        {
            get => OriginalGM?.GM?.OrderAggregate?.Order;
            set
            {
                OriginalGM.GM.OrderAggregate = _context.OrderAggregates.First(oa => oa.OrderId == value.Id);
                UpdateAggregates(SelectedOrder);
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
            get => OriginalGM?.GM?.GMNumber;
            set
            {
                //
            }
        }

        public SPU SelectedSPU
        {
            get => SelectedGMNumber?.SPU;
        }

        private static ObservableCollection<Tare> _tares;
        public ObservableCollection<Tare> Tares
        {
            get => _tares;
            set => SetField(ref _tares, value, nameof(Tares));
        }
        public Tare SelectedTare
        {
            get => OriginalGM?.SPUTare?.Tare;
            set
            {

            }
        }

        public int? CountNeed
        {
            get => OriginalGM?.SPUTare?.CountNeed;
        }

        public int? CountGet
        {
            get => OriginalGM?.CountGet;
            set
            {
                OriginalGM.CountGet = (int)value;
                OnPropertyChanged(nameof(CountGet));
            }
        }

        public int? Deficit
        {
            get => OriginalGM?.Deficit;
        }

        public string Demind
        {
            get => OriginalGM?.Demind;
            set
            {
                OriginalGM.Demind = value;
                OnPropertyChanged(nameof(Demind));
            }
        }

        public string DemindDate
        {
            get => OriginalGM?.DemindDate.ToString();
        }

        public string PlannedDeadline
        {
            get => OriginalGM?.GM?.PlannedDeadline.ToString();
        }

        public string NecessaryProvisionPeriod
        {
            get => OriginalGM?.GM?.NecessaryProvisionPeriod.ToString();
        }

        public string PromisedProvisionPeriod
        {
            get => OriginalGM?.PromisedProvisionPeriod.ToString();
        }

        public string Comment
        {
            get => OriginalGM?.Comment;
            set
            {
                OriginalGM.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public int? Waybill
        {
            get => OriginalGM?.GM?.Waybill;
            set
            {
                OriginalGM.GM.Waybill = value;
                OnPropertyChanged(nameof(Waybill));
            }
        }

        public string WaybillDate
        {
            get => OriginalGM?.GM?.WaybillDate.ToString();
        }

        public string ServiceNote
        {
            get => OriginalGM?.ServiceNote;
            set
            {
                OriginalGM.ServiceNote = value;
                OnPropertyChanged(nameof(ServiceNote));
            }
        }

        public string NumberGraphPDO
        {
            get;
            set;
        }

        public string ReserveField
        {
            get => OriginalGM?.ReserveField;
            set
            {
                OriginalGM.ReserveField = value;
                OnPropertyChanged(nameof(ReserveField));
            }
        }

        public void UpdateSource()
        {
            UpdateAggregates(SelectedOrder);
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

    }
}
