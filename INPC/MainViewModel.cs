using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace INPC {
    public class MainViewModel : INotifyPropertyChanged {
        ICommand<DevExpress.Mvvm.Xpf.ValidateRowDeletionArgs> validateRowDeletionCommand;
        public ICommand<DevExpress.Mvvm.Xpf.ValidateRowDeletionArgs> ValidateRowDeletionCommand {
            get {
                if(validateRowDeletionCommand == null) {
                    validateRowDeletionCommand = new DelegateCommand<DevExpress.Mvvm.Xpf.ValidateRowDeletionArgs>(args => ValidateRowDeletion(args));
                }
                return validateRowDeletionCommand;
            }
        }

        ICommand<DevExpress.Mvvm.Xpf.RowValidationArgs> validateRowCommand;
        public ICommand ValidateRowCommand {
            get {
                if(validateRowCommand == null) {
                    validateRowCommand = new DelegateCommand<DevExpress.Mvvm.Xpf.RowValidationArgs>(args => ValidateRow(args));
                }
                return validateRowCommand;
            }
        }

        ICommand<DevExpress.Mvvm.Xpf.DataSourceRefreshArgs> dataSourceRefreshCommand;
        public ICommand DataSourceRefreshCommand {
            get {
                if(dataSourceRefreshCommand == null) {
                    dataSourceRefreshCommand = new DelegateCommand<DevExpress.Mvvm.Xpf.DataSourceRefreshArgs>(args => DataSourceRefresh(args));
                }
                return dataSourceRefreshCommand;
            }
        }

        EFCoreIssues.Issues.IssuesContext _Context;
        System.Collections.Generic.IList<EFCoreIssues.Issues.User> _ItemsSource;

        public System.Collections.Generic.IList<EFCoreIssues.Issues.User> ItemsSource {
            get {
                if(_ItemsSource == null) {
                    _Context = new EFCoreIssues.Issues.IssuesContext();
                    _ItemsSource = _Context.Users.ToList();
                }
                return _ItemsSource;
            }
        }

        void ValidateRow(DevExpress.Mvvm.Xpf.RowValidationArgs args) {
            var item = (EFCoreIssues.Issues.User)args.Item;
            if(args.IsNewItem)
                _Context.Users.Add(item);
            _Context.SaveChanges();
        }
        void ValidateRowDeletion(DevExpress.Mvvm.Xpf.ValidateRowDeletionArgs args) {
            var item = (EFCoreIssues.Issues.User)args.Items.Single();
            _Context.Users.Remove(item);
            _Context.SaveChanges();
        }
        void DataSourceRefresh(DevExpress.Mvvm.Xpf.DataSourceRefreshArgs args) {
            _ItemsSource = null;
            _Context = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsSource)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
