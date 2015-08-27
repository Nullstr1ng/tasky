using GalaSoft.MvvmLight;

namespace tasky.Model
{
    public class Model_TaskDetails : ViewModelBase
    {
        private string _GUID = string.Empty;
        public string GUID
        {
            get { return _GUID; }
            set
            {
                if (_GUID != value)
                {
                    _GUID = value;
                    base.RaisePropertyChanged("GUID");
                }
            }
        }

        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    base.RaisePropertyChanged("Title");
                }
            }
        }

        private string _ShortDetails = string.Empty;
        public string ShortDetails
        {
            get { return _ShortDetails; }
            set
            {
                if (_ShortDetails != value)
                {
                    _ShortDetails = value;
                    base.RaisePropertyChanged("ShortDetails");
                }
            }
        }

        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set
            {
                if (_Details != value)
                {
                    _Details = value;
                    base.RaisePropertyChanged("Details");
                }
            }
        }

        private Enums.Enum_TaskStatus _TaskStatus = Enums.Enum_TaskStatus.TODO;
        public Enums.Enum_TaskStatus TaskStatus
        {
            get { return _TaskStatus; }
            set
            {
                if (_TaskStatus != value)
                {
                    _TaskStatus = value;
                    base.RaisePropertyChanged("TaskStatus");
                }
            }
        }

        private bool _Collapsed = false;
        public bool Collapsed
        {
            get { return _Collapsed; }
            set
            {
                if (_Collapsed != value)
                {
                    _Collapsed = value;
                    base.RaisePropertyChanged("Collapsed");
                }
            }
        }

        private int _Priority = 0;
        public int Priority
        {
            get { return _Priority; }
            set
            {
                if (_Priority != value)
                {
                    _Priority = value;
                    base.RaisePropertyChanged("Priority");
                }
            }
        }
    }
}
