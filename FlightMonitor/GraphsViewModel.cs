using OxyPlot;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace FlightMonitor
{
    class GraphsViewModel : ViewModel
    {
        public GraphsViewModel(IFlightgearMonitorModel model) : base(model) { }
        //Properties
       
        public Boolean VM_FilesInput
        {
            get
            {
                return model.FilesInput;
            }
        }

        public List<string> VM_ColumnNames
        {
            get
            {
                return model.ColumnNames;
            }
        }

        public string VM_Selection
        {
            set
            {
                model.Selection = value;
            }
            get
            {
                return model.Selection;
            }
        }
        public List<DataPoint> VM_SelFeatDataPoints
        {
            get
            {
                return model.SelFeatDataPoints;
            }
        }

        public string VM_CorFeat
        {
            get
            {
                return model.CorFeat;
            }
        }

        public List<DataPoint> VM_CorFeatDataPoints
        {
            get
            {
                return model.CorFeatDataPoints;
            }
        }

        public DataPoint[] VM_LinRegDataPoints
        {
            get
            {
                return model.LinRegDataPoints;
            }
        }
        public List<DataPoint> VM_CombinedDataPoints
        {
            get
            {
                return model.CombinedDataPoints;
            }
        }
        public List<DataPoint> VM_RecentCombinedDataPoints
        {
            get
            {
                return model.RecentCombinedDataPoints;
            }
        }

        public List<DataPoint> VM_DLLDisplay
        {
            get
            {
                return model.DLLDisplay;
            }
        }

        public List<DataPoint> VM_AnomalyPoints
        {
            get
            {
                return model.AnomalyPoints;
            }
        }

        public string VM_DLL
        {
            get
            {
                return model.DLL;
            }
            set
            {
                model.DLL = value;
                NotifyPropertyChanged("DLL");
            }
        }

        public Annotation VM_DLLAnnotation
        {
            get
            {
                return model.DLLAnnotation;
            }
        }
    }
}

