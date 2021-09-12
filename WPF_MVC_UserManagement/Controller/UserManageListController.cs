using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WPF_MVC_UserManagement.Model;
using WPF_MVC_UserManagement.View;

namespace WPF_MVC_UserManagement.Controller
{
    public class UserManageListController
    {
        private ObservableCollection<UserManageListModel> userList = new ObservableCollection<UserManageListModel>();
        private UserManageTreeModel treeSelectedItem = null;
        private UserManageListModel editItem = null;

        private bool isTreeInit = false;
        private string imageName, strName = string.Empty;
        private string tableName = "users";

        public delegate void DelegateUserGroupList(ObservableCollection<UserManageListModel> userGroupListData);
        public event DelegateUserGroupList delegateUserGroupList;

        public void CallUserGroupList()
        {
            string selectUserQuery = string.Format("SELECT * FROM {0}", tableName);
            DataSet userDataSet = MainWindow.DBManager.Select(selectUserQuery, tableName);

            foreach (DataRow item in userDataSet.Tables[0].Rows)
            {
                string primaryKey = item["id"].ToString();
                string userName = item["user_name"].ToString();
                string userBirth = item["user_birth"].ToString();
                string userId = item["user_id"].ToString();
                string userPw = item["user_pw"].ToString();
                string userDepartment = item["user_department"].ToString();
                string userEmployeeNum = item["user_employee_num"].ToString();
                string userNumber = item["user_number"].ToString();
                string userGroupName = item["group_name"].ToString();
                string userGroupId = item["group_id"].ToString();
                string userParentGroupId = item["parent_group_idx"].ToString();
                byte[] blob = (item["user_image"].ToString() != string.Empty) ? (byte[])item["user_image"] : null;

                userList.Add(new UserManageListModel(primaryKey, userName, userBirth, userId, userPw, userDepartment, userEmployeeNum, userNumber, userGroupName, userGroupId, userParentGroupId, UserImageShow(blob)));
            }

            this.delegateUserGroupList?.Invoke(userList);
        }

        public void CallSelectedItemChanged(UserManageTreeModel selectedItem)
        {
            treeSelectedItem = selectedItem;

            // userList 초기화
            if (editItem != null && !isTreeInit)
            {
                userList.Remove(editItem);
                editItem = null;
            }
            foreach (UserManageListModel item in userList)
            {
                if (item.IsSelectedVisibility == Visibility.Visible)
                {
                    item.IsSelectedVisibility = Visibility.Collapsed;
                }
            }

            // userList Selected
            switch (selectedItem.DepthCount)
            {
                case 0:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.ParentUserGroupId == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedVisibility = Visibility.Visible;
                        }
                    }
                    break;
                case 1:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.UserGroupId == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedVisibility = Visibility.Visible;
                        }
                    }
                    break;
                case 2:
                    foreach (UserManageListModel item in userList)
                    {
                        if (item.PrimaryKey == selectedItem.PrimaryKey)
                        {
                            item.IsSelectedVisibility = Visibility.Visible;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void CallAddUserClick()
        {
            if (editItem == null)
            {
                editItem = new UserManageListModel();
                userList.Insert(0, editItem);

                UserGroupListInit();
                if (treeSelectedItem != null)
                {
                    switch (treeSelectedItem.DepthCount)
                    {
                        case 0:
                            if (treeSelectedItem.ChildGroupList.Count > 0)
                            {
                                editItem.SelectedComboBoxItem = treeSelectedItem.ChildGroupList[0];
                            }
                            break;
                        case 1:
                            editItem.SelectedComboBoxItem = treeSelectedItem;
                            break;
                        default:
                            break;
                    }
                }
            }

            MainWindow.logListController.CallLogList("유저 생성");
        }

        public void CallEditUserClick(UserManageListModel selectedItem)
        {
            if (editItem == null)
            {
                editItem = selectedItem;

                UserGroupListInit();
                foreach (UserManageTreeModel item in MainWindow.userManageTreeController.parentGroupList)
                {
                    if (editItem.ParentUserGroupId == item.PrimaryKey)
                    {
                        foreach (UserManageTreeModel chileItem in item.ChildGroupList)
                        {
                            if (editItem.UserGroupId == chileItem.PrimaryKey)
                            {
                                editItem.SelectedComboBoxItem = chileItem;
                                break;
                            }
                        }
                    }
                }

                editItem.IsReadOnly = false;
                editItem.TempUserId = editItem.UserId;
                editItem.IsEditModeVisibility = Visibility.Visible;
                editItem.IsUserGroupList = Visibility.Visible;
            }
        }


        public void CallProfileEditClick()
        {
            if(editItem.ProfileImage != null)
            {
                editItem.ProfileImage = null;
            }
            else
            {
                try
                {
                    OpenFileDialog fldlg = new OpenFileDialog();
                    fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                    fldlg.Filter = "Image File (*.png; *.jpg;*.bmp;)|*.png;*.jpg;*.bmp9;";
                    fldlg.ShowDialog();
                    {
                        strName = fldlg.SafeFileName;
                        imageName = fldlg.FileName;

                        if (imageName != string.Empty)
                        {
                            using (FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read))
                            {
                                byte[] imgByteArr = new byte[fs.Length];
                                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                                fs.Close();

                                using (MemoryStream stream = new MemoryStream())
                                {
                                    stream.Write(imgByteArr, 0, imgByteArr.Length);
                                    stream.Position = 0;

                                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

                                    BitmapImage bi = new BitmapImage();
                                    bi.BeginInit();

                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                                        ms.Seek(0, SeekOrigin.Begin);
                                        bi.StreamSource = ms;
                                        bi.EndInit();

                                        editItem.ProfileImage = bi;
                                    }
                                }
                            }
                        }
                    }
                    fldlg = null;
                }

                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                }
            }
        }

        public void CallCancleClick()
        {
            // User Add Cancle
            if (editItem.PrimaryKey == string.Empty)
            {
                userList.Remove(editItem);
            }

            // User Edit Cancle
            else
            {
                UserInfoReset();
                editItem.IsReadOnly = true;
                editItem.IsEditModeVisibility = Visibility.Collapsed;
                editItem.IsUserGroupList = Visibility.Collapsed;
            }

            editItem = null;
        }

        public void CallSaveClick()
        {
            string userSelectQuery = string.Format("SELECT * FROM {0} WHERE user_id = '{1}'", tableName, editItem.UserId);
            if (DuplicateIdCheck(userSelectQuery, tableName) == false) { return; }

            // User Add Save
            if (editItem.PrimaryKey == string.Empty)
            {
                string userInsertQuery = string.Format("INSERT INTO {0}(user_name, user_birth, user_id, user_pw, user_department, user_employee_num, user_number, group_name, group_id, parent_group_idx) VALUES('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}')",
                    tableName, editItem.UserName, editItem.UserBirth, editItem.UserId, editItem.UserPw, editItem.UserDepartment, editItem.UserEmployeeNum, editItem.UserNumber, editItem.SelectedComboBoxItem.Header, editItem.SelectedComboBoxItem.PrimaryKey, editItem.SelectedComboBoxItem.ParentPrimaryKey);

                MainWindow.DBManager.MySqlQueryExecuter(userInsertQuery);
                DataSet parentGroupDataSet = MainWindow.DBManager.Select(userSelectQuery, tableName);
                editItem.PrimaryKey = parentGroupDataSet.Tables[0].Rows[0]["id"].ToString();
                UserTreeAddInit();
            }

            // User Edit Save
            else
            {
                string userUpdateQuery = string.Format("UPDATE {0} SET user_name='{1}', user_birth='{2}', user_id='{3}', user_pw='{4}', user_department='{5}', user_employee_num='{6}', user_number='{7}', group_name='{8}', group_id='{9}', parent_group_idx='{10}' WHERE id = '{11}'",
                    tableName, editItem.UserName, editItem.UserBirth, editItem.UserId, editItem.UserPw, editItem.UserDepartment, editItem.UserEmployeeNum, editItem.UserNumber, editItem.SelectedComboBoxItem.Header, editItem.SelectedComboBoxItem.PrimaryKey, editItem.SelectedComboBoxItem.ParentPrimaryKey, editItem.PrimaryKey);
                MainWindow.DBManager.MySqlQueryExecuter(userUpdateQuery);

                if(editItem.UserGroupId != editItem.SelectedComboBoxItem.PrimaryKey)
                {
                    isTreeInit = true;
                    UserTreeDeleteInit();
                    UserTreeAddInit();
                    editItem.IsSelectedVisibility = Visibility.Collapsed;
                }
            }

            ProfileImageEdit();
            editItem.IsReadOnly = true;
            editItem.IsNormalModeVisibility = Visibility.Visible;
            editItem.IsEditModeVisibility = Visibility.Collapsed;
            editItem.IsUserGroupList = Visibility.Collapsed;
            editItem.UserGroupName = editItem.SelectedComboBoxItem.Header;
            editItem.UserGroupId = editItem.SelectedComboBoxItem.PrimaryKey;
            editItem.ParentUserGroupId = editItem.SelectedComboBoxItem.ParentPrimaryKey;

            isTreeInit = false;
            editItem = null;
        }

        public void CallDeleteUserClick(UserManageListModel selectedItem)
        {
            WarningMessageBoxView messageBox = new WarningMessageBoxView();

            if (messageBox.ShowMessageBox("모든 내용이 영구적으로 삭제됩니다.", 1))
            {
                string userDeleteQuery = string.Format("DELETE FROM {0} WHERE id = '{1}'", tableName, selectedItem.PrimaryKey);
                MainWindow.DBManager.MySqlQueryExecuter(userDeleteQuery);
                userList.Remove(selectedItem);
            }
        }


        private BitmapImage UserImageShow(byte[] blob)
        {
            if (blob == null)
            {
                return null;
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                stream.Write(blob, 0, blob.Length);
                stream.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();

                return bi;
            }
        }

        private void UserGroupListInit()
        {
            foreach (UserManageTreeModel item in MainWindow.userManageTreeController.parentGroupList)
            {
                editItem.UserGroupList.Add(item);

                foreach (UserManageTreeModel childItem in item.ChildGroupList)
                {
                    editItem.UserGroupList.Add(childItem);
                }
            }
        }

        private bool DuplicateIdCheck(string selectQuery, string tableName)
        {
            WarningMessageBoxView messageBox = new WarningMessageBoxView();

            if (editItem.UserName == string.Empty || editItem.UserBirth == string.Empty || editItem.UserId == string.Empty || editItem.UserPw == string.Empty || editItem.UserDepartment == string.Empty
                || editItem.UserEmployeeNum == string.Empty || editItem.UserNumber == string.Empty || editItem.SelectedComboBoxItem == null)
            {
                return messageBox.ShowMessageBox("값을 모두 입력해주세요.", 0);
            }
            else
            {
                DataSet selectDataSet = MainWindow.DBManager.Select(selectQuery, tableName);
                if (selectDataSet.Tables[0].Rows.Count > 0 && (editItem.UserId != editItem.TempUserId))
                {
                    return messageBox.ShowMessageBox("이미 사용중인 아이디입니다.", 0);
                }
            }

            return true;
        }

        private void UserInfoReset()
        {
            string selectUserQuery = string.Format("SELECT * FROM {0} WHERE id = '{1}'", tableName, editItem.PrimaryKey);
            DataSet userDataSet = MainWindow.DBManager.Select(selectUserQuery, tableName);

            editItem.PrimaryKey = userDataSet.Tables[0].Rows[0]["id"].ToString();
            editItem.UserName = userDataSet.Tables[0].Rows[0]["user_name"].ToString();
            editItem.UserBirth = userDataSet.Tables[0].Rows[0]["user_birth"].ToString();
            editItem.UserId = userDataSet.Tables[0].Rows[0]["user_id"].ToString();
            editItem.UserPw = userDataSet.Tables[0].Rows[0]["user_pw"].ToString();
            editItem.UserDepartment = userDataSet.Tables[0].Rows[0]["user_department"].ToString();
            editItem.UserEmployeeNum = userDataSet.Tables[0].Rows[0]["user_employee_num"].ToString();
            editItem.UserNumber = userDataSet.Tables[0].Rows[0]["user_number"].ToString();
            editItem.UserGroupName = userDataSet.Tables[0].Rows[0]["group_name"].ToString();
            editItem.UserGroupId = userDataSet.Tables[0].Rows[0]["group_id"].ToString();
            editItem.ParentUserGroupId = userDataSet.Tables[0].Rows[0]["parent_group_idx"].ToString();
            editItem.ProfileImage = (userDataSet.Tables[0].Rows[0]["user_image"].ToString() != string.Empty) ? UserImageShow((byte[])userDataSet.Tables[0].Rows[0]["user_image"]) : null;
        }

        private void ProfileImageEdit()
        {
            if(imageName != null)
            {
                try
                {
                    using (FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] imgByteArr = new byte[fs.Length];
                        fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                        fs.Close();

                        string userImageInsertQuery = "UPDATE users SET user_image = @user_image WHERE id = " + editItem.PrimaryKey;
                        MainWindow.DBManager.MySqlImageInsertExecuter(userImageInsertQuery, imgByteArr);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                string userImageUpdateQuery = string.Format("UPDATE {0} SET user_image = null WHERE id = {1}", tableName, editItem.PrimaryKey);
                MainWindow.DBManager.MySqlQueryExecuter(userImageUpdateQuery);
            }
        }

        private void UserTreeDeleteInit()
        {
            foreach (UserManageTreeModel parentItem in MainWindow.userManageTreeController.parentGroupList)
            {
                if(editItem.ParentUserGroupId == parentItem.PrimaryKey)
                {
                    foreach (UserManageTreeModel item in parentItem.ChildGroupList)
                    {
                        if(editItem.UserGroupId == item.PrimaryKey)
                        {
                            foreach (UserManageTreeModel chileItem in item.ChildGroupList)
                            {
                                if (editItem.PrimaryKey == chileItem.PrimaryKey)
                                {
                                    item.ChildGroupList.Remove(chileItem);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UserTreeAddInit()
        {
            foreach (UserManageTreeModel item in MainWindow.userManageTreeController.parentGroupList)
            {
                if (editItem.SelectedComboBoxItem.ParentPrimaryKey == item.PrimaryKey)
                {
                    foreach (UserManageTreeModel chileItem in item.ChildGroupList)
                    {
                        if (editItem.SelectedComboBoxItem.PrimaryKey == chileItem.PrimaryKey)
                        {
                            chileItem.ChildGroupList.Add(new UserManageTreeModel(2, editItem.PrimaryKey, editItem.UserName));
                            return;
                        }
                    }
                }
            }
        }
    }
}
