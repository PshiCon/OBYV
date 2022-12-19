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
    public partial class formEditingProduct : Form
    {
        public formEditingProduct(string article)
        {
            InitializeComponent();
            productArticle = article;
        }
        public string productArticle;
        public List<string> Categories, Manufactor;
        string photo;
        string fileSource = "";
        string path = Application.StartupPath;
        Entini.Product product;

        private void formEditingProduct_Load(object sender, EventArgs e)
        {
            Manufactor = Helper.DB.Manufacturer.Select(x => x.ManufacturerName).ToList();
            comboBoxManyfactor.DataSource = Manufactor;

            Categories = Helper.DB.Category.Select(x => x.CategoryName).ToList();

            comboBoxCategory.DataSource = Categories;
            if (productArticle != null)
            {
                DataToTextBox();
                textBoxArticle.Enabled = false;
            }
            else
            {
                textBoxArticle.Enabled = true;
                pictureBoxProduct.BackgroundImage = Properties.Resources.picture;
            }


        }



        void DataToTextBox()
        {           
            Entini.Product Product = Helper.DB.Product.Where(x => x.ProductArticle == productArticle).FirstOrDefault();

            textBoxArticle.Text = productArticle;
            textBoxName.Text = Product.ProductName;
            comboBoxManyfactor.SelectedIndex = Product.ProductManufactureId-1;
            comboBoxCategory.SelectedIndex  = Product.ProductCategoryId-1;
            textBoxCost.Text = Product.ProductCost.ToString();
            textBoxDiscount.Text=Product.ProductDiscount.ToString();
            textBoxCount.Text = Product.ProductCount.ToString();
            textBoxDecription.Text =Product.ProductDecription.ToString();

            photo = Product.ProductPhoto;
            if (String.IsNullOrEmpty(photo))
            {
                pictureBoxProduct.BackgroundImage = Properties.Resources.picture;
            }
            else
            {
                string s = path + "\\Товар\\" + photo ;
                Bitmap bitmap=new Bitmap(s);
                pictureBoxProduct.BackgroundImage = bitmap;
               
            }

        }

        private void buttonDeletePhoto_Click(object sender, EventArgs e)
        {
            pictureBoxProduct.BackgroundImage= Properties.Resources.picture;
            photo = null;

        }

        private void buttonAddPhoto_Click(object sender, EventArgs e)
        {
            if(openFileDialogPhoto.ShowDialog() == DialogResult.OK)
            {
                fileSource = openFileDialogPhoto.FileName;
                photo = textBoxArticle.Text + ".jpg";
                Bitmap bitmap = new Bitmap(fileSource);
                pictureBoxProduct.BackgroundImage = bitmap;
            }
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить этот товар","Контроль",
                MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Entini.Product productDelete = Helper.DB.Product.Find(productArticle);
                Helper.DB.Product.Remove(productDelete);
                try
                {
                    Helper.DB.SaveChanges();
                    MessageBox.Show("Товар удален");
                }
                catch
                {
                    MessageBox.Show("Нельзя удалить, так как есть в заказах");
                    return;
                }
            }

            FormProduct form = new FormProduct();
            this.Close();
            form.Show();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
           
            Button button = (Button)sender;
            switch (button.Tag)
            {
                case "addProduct":
                    try
                    {
                        Data();
                        if (photo != null)
                        {
                            string s = path + "\\Товар\\" + productArticle + ".jpg";
                            pictureBoxProduct.BackgroundImage.Save(s);
                        }

                        Helper.DB.Product.Add(product);
                        Helper.DB.SaveChanges();
                        MessageBox.Show("Информация в БД успешно обновлена");
                        FormProduct form = new FormProduct();
                        this.Close();
                        form.Show();

                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при обновление данных в БД");
                        return;
                    }
                    break;

                case "saveProduct":
                    try
                    {
                        Data();
                        if (photo != null)
                        {
                            string s = path + "\\Товар\\" + productArticle + ".jpg";
                            pictureBoxProduct.BackgroundImage.Save(s);
                        }
                        Helper.DB.SaveChanges(); 
                        MessageBox.Show("Информация в БД успешно обновлена");
                        FormProduct form = new FormProduct();
                        this.Close();
                        form.Show();
                                             

                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при обновление данных в БД");
                        return;
                    }
                    break;

            }
 


        }
        void Data()
        {
            string artickle;
            
            artickle = textBoxArticle.Text;
            if (string.IsNullOrEmpty(artickle))
            {
                Entini.Product productFind = Helper.DB.Product.Find(artickle);
                if (productFind != null)
                {
                    MessageBox.Show("Такой артикл уже есть");
                    return;
                }
                product = new Entini.Product();
            }
            else
            {
                product = Helper.DB.Product.Find(artickle);
               
            }

            product.ProductArticle = artickle;
            product.ProductName = textBoxName.Text;
            product.ProductDecription = textBoxDecription.Text;
            product.ProductPhoto = photo;
            product.ProductDiscount = Convert.ToInt32(textBoxDiscount.Text);
            product.ProductCost = Convert.ToDouble(textBoxCost.Text);
            product.ProductManufactureId = comboBoxManyfactor.SelectedIndex + 1;
            product.ProductProviderId = 1;
            product.ProductCategoryId = comboBoxCategory.SelectedIndex + 1;
            product.ProductUnitId = 1;
            product.ProductCount = Convert.ToInt32(textBoxCount.Text);
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            FormProduct form=new FormProduct();
            this.Close();  
            form.Show();
        }

    }
}
