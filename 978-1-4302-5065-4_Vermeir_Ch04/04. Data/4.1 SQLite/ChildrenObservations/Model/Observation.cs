using System.ComponentModel;
using SQLite;
using System;

namespace ChildrenObservations.Model
{
    [Table("Observation")]
    public class Observation : INotifyPropertyChanged
    {
        private string _childName;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ChildId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        [Ignore]
        public string ChildName
        {
            get { return _childName; }
            set
            {
                _childName = value;

                OnPropertyChanged("ChildName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}