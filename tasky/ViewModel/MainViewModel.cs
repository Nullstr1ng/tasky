using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using tasky.Model;

namespace tasky.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region vars
        bool _apply_to_task = false;
        #endregion

        #region properties

        private ObservableCollection<Model_TaskDetails> _TasksCollection = new ObservableCollection<Model_TaskDetails>();
        public ObservableCollection<Model_TaskDetails> TasksCollection
        {
            get { return _TasksCollection; }
            set
            {
                if (_TasksCollection != value)
                {
                    _TasksCollection = value;
                    base.RaisePropertyChanged("TasksCollection");
                }
            }
        }

        private ObservableCollection<Model_TaskDetails> _ToDoCollection = new ObservableCollection<Model_TaskDetails>();
        public ObservableCollection<Model_TaskDetails> ToDoCollection
        {
            get { return _ToDoCollection; }
            set
            {
                if (_ToDoCollection != value)
                {
                    _ToDoCollection = value;
                    base.RaisePropertyChanged("ToDoCollection");
                }
            }
        }

        private ObservableCollection<Model_TaskDetails> _InProgress = new ObservableCollection<Model_TaskDetails>();
        public ObservableCollection<Model_TaskDetails> InProgressCollection
        {
            get { return _InProgress; }
            set
            {
                if (_InProgress != value)
                {
                    _InProgress = value;
                    base.RaisePropertyChanged("InProgressCollection");
                }
            }
        }

        private ObservableCollection<Model_TaskDetails> _ReadyCollection = new ObservableCollection<Model_TaskDetails>();
        public ObservableCollection<Model_TaskDetails> ReadyCollection
        {
            get { return _ReadyCollection; }
            set
            {
                if (_ReadyCollection != value)
                {
                    _ReadyCollection = value;
                    base.RaisePropertyChanged("ReadyCollection");
                }
            }
        }

        private ObservableCollection<Model_TaskDetails> _InQAReviewCollection = new ObservableCollection<Model_TaskDetails>();
        public ObservableCollection<Model_TaskDetails> InQAReviewCollection
        {
            get { return _InQAReviewCollection; }
            set
            {
                if (_InQAReviewCollection != value)
                {
                    _InQAReviewCollection = value;
                    base.RaisePropertyChanged("InQAReviewCollection");
                }
            }
        }

        private ObservableCollection<Model_TaskDetails> _TaskDoneCollection = new ObservableCollection<Model_TaskDetails>();
        public ObservableCollection<Model_TaskDetails> TaskDoneCollection
        {
            get { return _TaskDoneCollection; }
            set
            {
                if (_TaskDoneCollection != value)
                {
                    _TaskDoneCollection = value;
                    base.RaisePropertyChanged("TaskDoneCollection");
                }
            }
        }

        private ObservableCollection<string> _ProgressCollection = new ObservableCollection<string>();
        public ObservableCollection<string> ProgressCollection
        {
            get { return _ProgressCollection; }
            set
            {
                if (_ProgressCollection != value)
                {
                    _ProgressCollection = value;
                    base.RaisePropertyChanged("ProgressCollection");
                }
            }
        }

        private int _ProgressIndexSelection = 0;
        public int ProgressIndexSelection
        {
            get { return _ProgressIndexSelection; }
            set
            {
                if (_ProgressIndexSelection != value)
                {
                    _ProgressIndexSelection = value;
                    base.RaisePropertyChanged("ProgressIndexSelection");

                    this._apply_to_task = true;
                }
            }
        }

        private Model_TaskDetails _SelectedTaskDetails = new Model_TaskDetails();
        public Model_TaskDetails SelectedTaskDetails
        {
            get { return _SelectedTaskDetails; }
            set
            {
                if (_SelectedTaskDetails != value)
                {
                    _SelectedTaskDetails = value;
                    base.RaisePropertyChanged("SelectedTaskDetails");
                    //ShowTaskDetails = true;

                    if (value != null)
                    {
                        this.ProgressIndexSelection = (int)((Model_TaskDetails)value).TaskStatus;
                        ShowTaskDetails = true;
                    }
                    else
                    {
                        ShowTaskDetails = false;
                    }
                }
            }
        }

        private bool _ShowTaskDetails = false;
        public bool ShowTaskDetails
        {
            get { return _ShowTaskDetails; }
            set
            {
                if (_ShowTaskDetails != value)
                {
                    _ShowTaskDetails = value;
                    base.RaisePropertyChanged("ShowTaskDetails");
                }
            }
        }
        #endregion

        #region commands
        public ICommand Command_Save { get; internal set; }
        #endregion

        #region ctor
        public MainViewModel()
        {
            DefaultData();

            if (base.IsInDesignMode)
            {
                ShowTaskDetails = true;
                DesignTimeData();
            }
            else
            {
                RuntimeData();
                DesignTimeData();

                this.Command_Save = new RelayCommand<Model_TaskDetails>(Command_Save_Click);
            }

            RefreshCollections();
        }
        #endregion

        #region command methods
        void Command_Save_Click(Model_TaskDetails task)
        {
            if (SelectedTaskDetails != null)
            {
                //if (this._apply_to_task)
                {
                    Enums.Enum_TaskStatus stat = Enums.Enum_TaskStatus.TODO;
                    Enum.TryParse<Enums.Enum_TaskStatus>(this.ProgressIndexSelection.ToString(), out stat);

                    this.SelectedTaskDetails.TaskStatus = stat;

                    RefreshCollections();

                    this.SelectedTaskDetails = null;
                }
            }
        }
        #endregion

        #region methods
        void RefreshCollections()
        {
            var todos = from a in this.TasksCollection
                        where a.TaskStatus == Enums.Enum_TaskStatus.TODO
                        orderby a.Priority descending
                        select a
                        ;
            this.ToDoCollection.Clear();
            foreach(Model_TaskDetails task in todos)
            {
                this.ToDoCollection.Add(task);
            }

            var inprog = from a in this.TasksCollection
                        where a.TaskStatus == Enums.Enum_TaskStatus.IN_PROGRESS
                         orderby a.Priority descending
                         select a;
            this.InProgressCollection.Clear();
            foreach (Model_TaskDetails task in inprog)
            {
                this.InProgressCollection.Add(task);
            }

            var ready = from a in this.TasksCollection
                        where a.TaskStatus == Enums.Enum_TaskStatus.READY
                        orderby a.Priority descending
                        select a;
            this.ReadyCollection.Clear();
            foreach (Model_TaskDetails task in ready)
            {
                this.ReadyCollection.Add(task);
            }

            var inqa = from a in this.TasksCollection
                        where a.TaskStatus == Enums.Enum_TaskStatus.IN_QA_REVIEW
                       orderby a.Priority descending
                       select a;
            this.InQAReviewCollection.Clear();
            foreach (Model_TaskDetails task in inqa)
            {
                this.InQAReviewCollection.Add(task);
            }

            var done = from a in this.TasksCollection
                        where a.TaskStatus == Enums.Enum_TaskStatus.DONE
                       orderby a.Priority descending
                       select a;
            this.TaskDoneCollection.Clear();
            foreach (Model_TaskDetails task in done)
            {
                this.TaskDoneCollection.Add(task);
            }
        }

        void DefaultData()
        {
            ProgressCollection.Add("To Do");
            ProgressCollection.Add("In Progress");
            ProgressCollection.Add("Ready");
            ProgressCollection.Add("In QA Review");
            ProgressCollection.Add("Done");
        }

        void DesignTimeData()
        {
            // todo
            TasksCollection.Add(new Model_TaskDetails()
            {
                GUID = Guid.NewGuid().ToString("X"),
                Title = "WINRET-28",
                ShortDetails = "Customer in queue - Add icons to help reading details",
                Details = @"As in Android and iOS version, we should add icons for each customer in queue like:
* no-phone icon
* booking icon
* booking is arrived icon
* unread messages icon
* callback icon",
                TaskStatus = Enums.Enum_TaskStatus.TODO,
                Collapsed = true, Priority = 1
            });
            TasksCollection.Add(new Model_TaskDetails()
            {
                GUID = Guid.NewGuid().ToString("X"),
                Title = "WINRET-56",
                ShortDetails= "Flush memory cache when logout",
                Details = @"It's actually an Improvement and not a Bug.

Some values inside the mobile app which were not saved on the server side are still set. I guess we have a memory cache in the app.

We should flush it to be sure to get updated values when log in.",
                TaskStatus = Enums.Enum_TaskStatus.TODO,
                Collapsed = false, Priority = 5
            });

            SelectedTaskDetails = TasksCollection[0];
        }

        void RuntimeData()
        {

        }
        #endregion
    }
}