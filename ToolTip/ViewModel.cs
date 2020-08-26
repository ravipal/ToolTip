using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ToolTip
{
    public class ViewModel : ObservableObject, INotifyDataErrorInfo
    {
        private string _selectedMode;
        public ViewModel()
        {
            Modes = new HashSet<string>();
            Modes.Add("Mode1");
            Modes.Add("Mode2");
            Modes.Add("Mode3");
        }

        public HashSet<string> Modes { get; private set; }

        public string SelectedMode
        {
            get
            {
                return _selectedMode;
            }
            set
            {
                SetPropertyValue(ref _selectedMode, value);
                Validate();
            }
        }

        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            // We only support a single validation error right now
            if (string.Equals(propertyName, nameof(SelectedMode), StringComparison.OrdinalIgnoreCase))
            {
                if (SelectedMode.Equals("Mode2"))
                {
                    yield return new ValidationResult(false, new ValidationContent(isWarning: true, message: "The selected Mode is not valid"));
                }
            }
        }

        public bool HasErrors { get; private set; }

        #endregion

        private void Validate()
        {
            HasErrors = SelectedMode.Equals("Mode2");
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(SelectedMode)));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }

    public class ValidationContent
    {
        public ValidationContent(bool isWarning, string message)
        {
            IsWarning = isWarning;
            Message = message;
        }

        public string Message { get; private set; }

        public bool IsWarning { get; private set; }
    }
}
