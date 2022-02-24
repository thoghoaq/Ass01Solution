using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string fileName = "appsettings.json";
            string json = File.ReadAllText(fileName);

            var adminAccount = JsonSerializer.Deserialize<MemberObject>(json, null);
            var userAccount = memberRepository.GetMemberByEmail(txtEmail.Text);
            //Got adminAccount email and password
            //Compare to database
            if (adminAccount.Email.Equals(txtEmail.Text) && adminAccount.Password.Equals(txtPassword.Text))
            {
                this.Hide();
                frmMemberManagement frmManagement = new frmMemberManagement();
                frmManagement.Show();

            }
            else if (userAccount != null)
            {
                if (userAccount.Email.Equals(txtEmail.Text) && userAccount.Password.Equals(txtPassword.Text))
                {
                    this.Hide();
                    frmMemberDetails frmMemberDetails = new frmMemberDetails
                    {
                        Text = "Update member",
                        InsertOrUpdate = true,
                        MemberInfo = userAccount,
                        MemberRepository = memberRepository
                    };
                    frmMemberDetails.Show();
                }
            }
            else
            {
                string message = "You don't have admit to access this file!";
                string title = "Login Failed";
                MessageBox.Show(message, title);

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
