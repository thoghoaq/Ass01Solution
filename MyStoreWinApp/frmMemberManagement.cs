using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmMemberManagement()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var mems = memberRepository.GetMembers();
            try
            {
                source = new BindingSource();
                source.DataSource = mems;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;

                if (mems.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
            txtSearch.Text = string.Empty;
            cboFilter.Items.Clear();
            cboFilter.SelectedItem = 0;
            cboFilter.Text = string.Empty;
            LoadFilter();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add car",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string message = "Do you want to delete this member?";
                string title = "Delete a member";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    var member = GetMemberObject();
                    memberRepository.DeleteMember(member.MemberID);
                    LoadMemberList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Search memberName");
            }
        }
        
        private void btnFilter_Click(object sender, EventArgs e)
        {

        }

        private bool btnSortwasClicked = false;
        private void btnSort_Click(object sender, EventArgs e)
        {
            btnSortwasClicked = true;
            LoadMemberList();
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            dgvMemberList.CellDoubleClick += DgvMemberList_CellDoubleClick;
        }

        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private string DoAction()
        {
            string action = "default";
            var searchValue = txtSearch.Text;
            var filterValue = cboFilter.Text;
            if (searchValue != null && !searchValue.Equals(""))
            {
                action = "search";
            }
            if (!filterValue.Equals(""))
            {
                action = "filter";
            }
            if (btnSortwasClicked == true)
            {
                action = "sort";
            }
            return action;
        }

        private void LoadMemberList()
        {
            var mems = memberRepository.GetMembers();
            string searchValue = txtSearch.Text;
            string action = DoAction();
            if (action != "default")
            {
                switch (action)
                {
                    case "search":
                        mems = memberRepository.SearchMember(searchValue);
                        break;
                    case "filter":
                        {
                            mems = memberRepository.GetMemberByFilter(cboFilter.Text);
                        }
                        break;
                    case "sort":
                        {
                            mems = memberRepository.SortDescenByName();
                            btnSortwasClicked = false;
                        }
                        break;
                }
            }
            try
            {
                source = new BindingSource();
                source.DataSource = mems;

                txtMemberID.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberID");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;

                if (mems.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }

        }

        private void ClearText()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }

        private MemberObject GetMemberObject()
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadFilter()
        {
            var list = new List<string>();
            list = memberRepository.GetListCountry();
            for (int i = 0; i < list.Count; i++)
            {
                cboFilter.Items.Add(list[i]);
            }
            var list2 = new List<string>();
            list2 = memberRepository.GetListCity();
            for (int i = 0; i < list2.Count; i++)
            {
                cboFilter.Items.Add(list2[i]);
            }
        }

        private void cboFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadMemberList();
        }
    }
}
