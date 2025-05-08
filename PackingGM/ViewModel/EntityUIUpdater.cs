using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.ViewModel
{
    public static class EntityUIUpdater
    {
        private static readonly List<RecordGMTest> _instances = new List<RecordGMTest>();

        public static void RegisterInstance(RecordGMTest instance)
        {
            if (!_instances.Contains(instance))
                _instances.Add(instance);
        }

        public static void Unregister(RecordGMTest instance)
        {
            _instances.Remove(instance);
        }

        public static void UpdateUIForAggregateType(int aggregateId)
        {
            foreach (var instance in _instances.Where(i =>
                i.SelectedAggregate?.Id == aggregateId))
            {
                // Только обновляем интерфейс, не меняя данные
                instance.OnPropertyChanged(nameof(instance.SelectedAggregateType));
            }
        }
        public static void UpdateUIForContragent(int orderId)
        {
            foreach(var instance in _instances.Where(i => i.SelectedOrder?.Id == orderId))
                instance.OnPropertyChanged(nameof(instance.SelectedContragent));
        }
    }
}
