﻿using System.Collections.ObjectModel;
using System.Windows;

namespace WPF_MVC_UserManagement.Model
{
    public class UserManageTreeModel : NotifierCollection
    {
        public UserManageTreeModel(int depthCount, string primaryKey, string header)
        {
            this.depthCount = depthCount;
            this.primaryKey = primaryKey;
            this.header = header;
            this.isHeader = Visibility.Visible;
            this.isEdit = Visibility.Collapsed;
        }

        public UserManageTreeModel(int depthCount, string primaryKey, string header, Visibility isEdit)
        {
            this.depthCount = depthCount;
            this.primaryKey = primaryKey;
            this.header = header;
            this.isEdit = isEdit;
            this.isEditFocus = true;
            this.isHeader = Visibility.Collapsed;
        }

        private int depthCount;
        public int DepthCount
        {
            get { return this.depthCount; }
            set { this.depthCount = value; NotifyCollection("DepthCount"); }
        }

        private string primaryKey;
        public string PrimaryKey
        {
            get { return this.primaryKey; }
            set { this.primaryKey = value; NotifyCollection("PrimaryKey"); }
        }

        private string header;
        public string Header
        {
            get { return this.header; }
            set { this.header = value; NotifyCollection("Header"); }
        }

        private string inputHeader;
        public string InputHeader
        {
            get { return this.inputHeader; }
            set { this.inputHeader = value; NotifyCollection("InputHeader"); }
        }

        private string parentPrimaryKey;
        public string ParentPrimaryKey
        {
            get { return this.parentPrimaryKey; }
            set { this.parentPrimaryKey = value; NotifyCollection("ParentPrimaryKey"); }
        }

        private Visibility isHeader;
        public Visibility IsHeader
        {
            get { return this.isHeader; }
            set { this.isHeader = value; NotifyCollection("IsHeader"); }
        }

        private Visibility isEdit;
        public Visibility IsEdit
        {
            get { return this.isEdit; }
            set { this.isEdit = value; NotifyCollection("IsEdit"); }
        }

        private bool isEditFocus;
        public bool IsEditFocus
        {
            get { return this.isEditFocus; }
            set { this.isEditFocus = value; NotifyCollection("IsEditFocus"); }
        }

        private ObservableCollection<UserManageTreeModel> childGroupList;
        public ObservableCollection<UserManageTreeModel> ChildGroupList
        {
            get
            {
                if (this.childGroupList == null)
                {
                    this.childGroupList = new ObservableCollection<UserManageTreeModel>();
                }
                return this.childGroupList;
            }
            set { this.childGroupList = value; }
        }
    }
}
