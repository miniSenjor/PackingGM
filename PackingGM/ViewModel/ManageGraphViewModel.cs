using PackingGM.Data;
using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace PackingGM.ViewModel
{
    public class ManageGraphViewModel : BaseViewModel
    {
        public ManageGraphViewModel()
        {
            _context = App.GetContext();
            LoadListGM();
        }
        private AppDb _context;
        private ObservableCollection<GMTare> lGMs { get; set; }
        public ObservableCollection<Record> ListGMs { get; set; }
        private List<Record> ListDelete { get; set; } = new List<Record>();
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
            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                entry.State = EntityState.Detached;
            }
            ListGMs.Clear();
            base.Back(obj);
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

                ListDelete.Add(SelectedGMTare);
                ListGMs.Remove(SelectedGMTare);
            }
            catch (Exception ex)
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
            try
            {
                if (!CurrentUser.User.IsAlowedWriting)
                {
                    MessageBox.Show("У вас нет прав сохранять измменения");
                    return;
                }
                foreach (GMTare gm in ListDelete.Select(g => g.OriginalGM))
                {
                    if (gm != null)
                        _context.GMTares.Remove(gm);
                }
                _context.SaveChanges();
                LoadListGM(null);
                StateApp.Instance.ChangeText("Успешно сохранено");
            }
            catch (Exception ex)
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
        private void LoadListGM(object obj = null)
        {
            foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                entry.State = EntityState.Detached;
            }
            lGMs = new ObservableCollection<GMTare>(_context.GMTares
                .Include("GM.OrderAggregate.Order.Contragent")
                .Include("GM.OrderAggregate.Aggregate.AggregateType")
                .Include("GM.GMNumber.SPU")
                .Include("SPUTare.Tare")
                .Include("GM.Manufactory")
                .Where(g => g.GM.GMNumber != null));
            ListGMs = new ObservableCollection<Record>(
                lGMs.Select(g => new Record(g)));
            ListDelete.Clear();
            OnPropertyChanged(nameof(ListGMs));
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
                //
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
                try
                {
                    OriginalGM.CountGet = (int)value;
                }
                catch(ArgumentOutOfRangeException)
                {
                    OriginalGM.CountGet = (int)CountNeed;
                }
                finally
                {
                    OnPropertyChanged(nameof(CountGet));
                    OnPropertyChanged(nameof(Deficit));
                }
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

        public DateTime? DemindDate
        {
            get => OriginalGM?.DemindDate;
            set
            {
                OriginalGM.DemindDate = value;
                OnPropertyChanged(nameof(DemindDate));
            }
        }

        public DateTime? PlannedDeadline
        {
            get => OriginalGM?.GM?.PlannedDeadline;
            set
            {
                OriginalGM.GM.PlannedDeadline = value;
                OnPropertyChanged(nameof(PlannedDeadline));
            }
        }

        public string NecessaryProvisionPeriod
        {
            get => OriginalGM?.GM?.NecessaryProvisionPeriod.ToString();
        }

        public DateTime? PromisedProvisionPeriod
        {
            get => OriginalGM?.PromisedProvisionPeriod;
            set
            {
                OriginalGM.PromisedProvisionPeriod = value;
                OnPropertyChanged(nameof(PromisedProvisionPeriod));
            }
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

        public DateTime? WaybillDate
        {
            get => OriginalGM?.GM?.WaybillDate;
            set
            {
                OriginalGM.GM.WaybillDate = value;
                OnPropertyChanged(nameof(WaybillDate));
            }
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
            UpdateManufactories(SelectedOrder);
            UpdateGMNumbers(SelectedOrder);
            UpdateTares(SelectedGMNumber);
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
        private void UpdateGMNumbers(Order order)
        {
            if (order == null)
                GMNumbers = null;
            else
                GMNumbers = new ObservableCollection<GMNumber>(
                    _context.GMNumbers
                    .Where(gn => gn.SPUId == SelectedSPU.Id));
            Aggregates = new ObservableCollection<Aggregate>(
                _context.OrderAggregates
                .Where(oa => oa.OrderId == order.Id)
                .Select(oa => oa.Aggregate));
        }
        private void UpdateManufactories(Order order)
        {
            if (order == null)
                Manufactories = null;
            else
                Manufactories = new ObservableCollection<Manufactory>(
                _context.Manufactories);
        }
        private void UpdateTares(GMNumber gmNumber)
        {
            if (gmNumber == null)
                Tares = null;
            else
                try
                {
                    SPUVersion spuVersion = _context.SPUVersions.First(sv => sv.SPUId == gmNumber.SPUId);
                    Tares = new ObservableCollection<Tare>(
                        _context.SPUTares
                        .Where(st => st.SPUVersionId == spuVersion.Id)
                        .Select(st => st.Tare));
                }
                catch
                {
                    StateApp.Instance.ChangeText("У этой спецификации нет активной версии");
                }
                finally
                {
                }
        }

    }
}
