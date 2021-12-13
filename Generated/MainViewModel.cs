﻿using System;
using System.Linq;
using DevExpress.Mvvm.CodeGenerators;

namespace Generated {
    [GenerateViewModel]
    public partial class MainViewModel {
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
        [GenerateCommand]
        void ValidateRow(DevExpress.Mvvm.Xpf.RowValidationArgs args) {
            var item = (EFCoreIssues.Issues.User)args.Item;
            if(args.IsNewItem)
                _Context.Users.Add(item);
            _Context.SaveChanges();
        }
        [GenerateCommand]
        void ValidateRowDeletion(DevExpress.Mvvm.Xpf.ValidateRowDeletionArgs args) {
            var item = (EFCoreIssues.Issues.User)args.Items.Single();
            _Context.Users.Remove(item);
            _Context.SaveChanges();
        }
        [GenerateCommand]
        void DataSourceRefresh(DevExpress.Mvvm.Xpf.DataSourceRefreshArgs args) {
            _ItemsSource = null;
            _Context = null;
            RaisePropertyChanged(new System.ComponentModel.PropertyChangedEventArgs(nameof(ItemsSource)));
        }
    }
}
