using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;

namespace POCO {
    public class MainViewModel {
        public static MainViewModel Create() {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        protected MainViewModel() {
        }

        EFCoreIssues.Issues.IssuesContext _Context;
        System.Collections.Generic.IList<EFCoreIssues.Issues.User> _ItemsSource;

        public virtual System.Collections.Generic.IList<EFCoreIssues.Issues.User> ItemsSource {
            get {
                if(_ItemsSource == null) {
                    _Context = new EFCoreIssues.Issues.IssuesContext();
                    _ItemsSource = _Context.Users.ToList();
                }
                return _ItemsSource;
            }
        }
        public void ValidateRow(DevExpress.Mvvm.Xpf.RowValidationArgs args) {
            var item = (EFCoreIssues.Issues.User)args.Item;
            if(args.IsNewItem)
                _Context.Users.Add(item);
            _Context.SaveChanges();
        }
        public void ValidateRowDeletion(DevExpress.Mvvm.Xpf.ValidateRowDeletionArgs args) {
            var item = (EFCoreIssues.Issues.User)args.Items.Single();
            _Context.Users.Remove(item);
            _Context.SaveChanges();
        }
        public void DataSourceRefresh(DevExpress.Mvvm.Xpf.DataSourceRefreshArgs args) {
            _ItemsSource = null;
            _Context = null;
            this.RaisePropertyChanged(vm => vm.ItemsSource);
        }
    }
}