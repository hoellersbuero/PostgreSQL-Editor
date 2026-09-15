using System.Collections.Generic;
using System.ComponentModel;

public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _sortDirection = ListSortDirection.Ascending;
    private PropertyDescriptor _sortProperty;

    protected override bool SupportsSortingCore => true;
    protected override bool IsSortedCore => _isSorted;
    protected override PropertyDescriptor SortPropertyCore => _sortProperty;
    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var itemsList = (List<T>)Items;
        var comparer = new PropertyComparer<T>(prop, direction);
        itemsList.Sort(comparer);
        _sortProperty = prop;
        _sortDirection = direction;
        _isSorted = true;
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
    }

    private class PropertyComparer<TItem> : IComparer<TItem>
    {
        private readonly PropertyDescriptor _property;
        private readonly ListSortDirection _direction;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            _property = property;
            _direction = direction;
        }

        public int Compare(TItem x, TItem y)
        {
            var xValue = _property.GetValue(x);
            var yValue = _property.GetValue(y);
            int result = Comparer<object>.Default.Compare(xValue, yValue);
            return _direction == ListSortDirection.Ascending ? result : -result;
        }
    }
}