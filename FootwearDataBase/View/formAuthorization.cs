using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FootwearDataBase
{
    public partial class formAuthorization : Form
    {
        public formAuthorization()
        {
            InitializeComponent();
        }

        public List<Entini.Role> Role;
        public List<Entini.User> users;
    

        


        private void Form1_Load(object sender, EventArgs e)
        {
            //List<Entities_Classes_.User> user = Helper.DB.User.ToList(); 1 Способ
            //var user=Helper.DB.User.ToList();

            using (Entini.DataBaseObyv DB = new Entini.DataBaseObyv())
            {
                users = DB.User.ToList();
                Role = DB.Role.ToList();
               
                

                int countUsert=users.Count;
                
                int row = 0;

                // List<String> user = Helper.DB.User.Select(x => x.UserLastName).ToList();
                //dataGridViewUser.DataSource = user;

                dataGridViewUser.Rows.Clear();
                foreach (var item in users)
                {
                    dataGridViewUser.Rows.Add();

                    dataGridViewUser.Rows[row].Cells[0].Value = item.UserID;
                    dataGridViewUser.Rows[row].Cells[1].Value = item.UserName;
                    dataGridViewUser.Rows[row].Cells[2].Value = item.UserLastName;
                    dataGridViewUser.Rows[row].Cells[3].Value = item.UserMiddleName;
                    dataGridViewUser.Rows[row].Cells[4].Value = item.UserLogin;
                    dataGridViewUser.Rows[row].Cells[5].Value = item.UserPassword;
                    dataGridViewUser.Rows[row].Cells[6].Value = item.Role.RoleName;
                    //dataGridViewUser.Rows[row].Cells[6].Value = item.RoleNameType(item.RoleID);
                    row++;
                }


            }

        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
         
            foreach (var item in users) 
            { 
                if(item.UserLogin == textBoxLogin.Text || item.UserPassword == textBoxPassword.Text)
                {
                
                    FootwearDataBase.Classes.UserRoleName.NameUser  = item.UserLastName + " " + item.UserName + " " +item.UserMiddleName;
                    FootwearDataBase.Classes.UserRoleName.RoleUser = item.Role.RoleName;

                    FormProduct formProduct =new FormProduct();
                    this.Hide();
                    formProduct.Show();
                }
            }

       }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEnterGost_Click(object sender, EventArgs e)
        {
            FormProduct formProduct = new FormProduct();
            this.Hide();
            formProduct.Show();
        }
    }
}
