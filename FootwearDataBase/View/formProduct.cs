using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FootwearDataBase
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }
        Bitmap bitmap;
        string fullPhoto;
        public List<string> Categories;
        string sqlProduct=$"SELECT * From Product";
        string sqlCategory;
        string sqlSort;

        int filterDiscont,discontMax,discontMin;

        public List<Entini.Product> Products;
        string path = Application.StartupPath;
        private void FormProduct_Load(object sender, EventArgs e)
        {
            Categories = Helper.DB.Category.Select(x => x.CategoryName).ToList();
            Categories.Insert(0, "Все категории");       
            comboBoxCategory.DataSource = Categories;
            comboBoxCategory.Text = comboBoxCategory.Items[0].ToString();
            comboBoxDiscount.Text = comboBoxDiscount.Items[0].ToString();
            comboBoxSort.Text = comboBoxDiscount.Items[0].ToString();
            labelUser.Text = FootwearDataBase.Classes.UserRoleName.NameUser + "  Роль: " + FootwearDataBase.Classes.UserRoleName.RoleUser;
            DataDisplayProduct();


        }

        
        void DataDisplayProduct()
        {


            dataGridViewProduct.Rows.Clear();
           Products = Helper.DB.Product.SqlQuery(sqlProduct).ToList();
            int row = 0;
            
            foreach (var item in Products)
            {
               
                    dataGridViewProduct.Rows.Add();

                    dataGridViewProduct.Rows[row].Cells[0].Value = item.ProductArticle;
                    string photo = item.ProductPhoto;

                    if (String.IsNullOrEmpty(photo))
                    {
                        bitmap = Properties.Resources.picture;
                    }
                    else
                    {
                        fullPhoto = path + "\\Товар\\" + photo;
                        if (File.Exists(fullPhoto))
                        {
                            bitmap = new Bitmap(fullPhoto);
                        }
                        else
                        {
                            bitmap = Properties.Resources.picture;

                        }
                    }
                    dataGridViewProduct.Rows[row].Cells[1].Value = bitmap;
                    dataGridViewProduct.Rows[row].Cells[2].Value = "Описание товара: " + item.ProductDecription + Environment.NewLine;
                    dataGridViewProduct.Rows[row].Cells[2].Value += "Производитель: " + item.Manufacturer.ManufacturerName + Environment.NewLine;
                    dataGridViewProduct.Rows[row].Cells[2].Value += "Цена: " + item.ProductCost + Environment.NewLine;
                    dataGridViewProduct.Rows[row].Height = 75;
                    if (item.ProductDiscount > 15)
                    {
                        dataGridViewProduct.Rows[row].DefaultCellStyle.BackColor = Color.Chartreuse;
                    }
                    row++;
                
            }
            labelCount.Text = row.ToString();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
           formAuthorization formAuthorization = new formAuthorization();
            formAuthorization.ShowDialog();
        }

        private void textBoxSourt_TextChanged(object sender, EventArgs e)
        {
            string sourt = textBoxSourt.Text.ToString();
       

        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlProduct = $"SELECT * From Product";
            ComboBox combo =sender as ComboBox;
            switch (combo.Tag)
            {
                case "sort": 
                    if(comboBoxSort.SelectedIndex == 0)
                    {
                        sqlSort = " ORDER BY [dbo].[Product].ProductCost DESC";
                    }
                    else
                    {
                        sqlSort = " ORDER BY [dbo].[Product].ProductCost ASC";
                    }
                    break;
                case "discount":
                    filterDiscont = comboBoxDiscount.SelectedIndex;
                    switch (filterDiscont)
                    {
                        case 0: discontMin = 0; discontMax = 100; break;
                        case 1: discontMin = 0; discontMax = 10; break;
                        case 2: discontMin = 11; discontMax = 14; break;
                        case 3: discontMin = 15; discontMax = 100; break;

                    }
                    break;

                case "category":
                    if(comboBoxCategory.SelectedIndex == 0)
                    {
                        sqlCategory = " ";
                    }
                    else
                    {
                        sqlCategory = $" WHERE [dbo].[Product].ProductCategoryId = {comboBoxCategory.SelectedIndex}";
                    }
                    break;
            }
            sqlProduct = sqlProduct + sqlCategory + sqlSort;
            DataDisplayProduct();

        }

      
       
    }
}
