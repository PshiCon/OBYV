using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace FootwearDataBase
{
    public partial class formOrder : Form
    {
        Word.Application wordApp;
        Word.Document wordDoc;
        Word.Paragraph wordPar;
        Word.Range wordRange;

        public List<Classes.OrderClass> orderClasses;
        public formOrder()
        {
            InitializeComponent();
            this.orderClasses = FormProduct.order;
        }
        
        string pathExe = Application.StartupPath;

        int uniqueCode;				
        DateTime dateNow;				
        double totalSumma;				
        double totalSale;				
        DateTime dateDelivery;			
        int orderId;
        int nomer;

        private void formOrder_Load(object sender, EventArgs e)
        {
            labelNameUser.Text = Classes.UserRoleName.NameUser;

            comboBoxDelivery.DataSource = Helper.DB.Point.ToList();
            comboBoxDelivery.DisplayMember = "PointAddress";
            comboBoxDelivery.ValueMember = "PointId";
            ShowOrder();
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            FormProduct form = new FormProduct();
            form.Show();
            this.Close();
        }
        public void ShowOrder()
        {
           // dataGridViewOrder=null;
            dataGridViewOrder.RowCount = orderClasses.Count;
            int i = 0;      
            int countProducts = 0;
            totalSumma = 0;					
            totalSale = 0;					
            double cost, sale;
           

            foreach (var item in orderClasses)
            {
                dataGridViewOrder.Rows[i].Cells[0].Value = item.Article;

                Entini.Product product = Helper.DB.Product.Find(item.Article);

                dataGridViewOrder.Rows[i].Cells[1].Value = product.ProductName;
                dataGridViewOrder.Rows[i].Cells[2].Value = item.Count;
                i++;
               
                 cost = Convert.ToDouble(product.ProductCost) * item.Count;
                 sale = Convert.ToDouble(product.ProductDiscount) / 100.00 * cost;
                 totalSumma += cost;
                 totalSale += sale;
                
            }
            textBoxDescOrder.Text = "Позиций в заказе: " + i + Environment.NewLine;
            textBoxDescOrder.Text += "Всего товаров: " + countProducts + Environment.NewLine;
            textBoxDescOrder.Text += "Общая сумма за весь товар: " + totalSumma +
                                                                         Environment.NewLine;
            textBoxDescOrder.Text += "Общая сумма скидки: " + totalSale + Environment.NewLine;
            textBoxDescOrder.Text += "Общая сумма за весь товар со скидкой: " + (totalSumma - totalSale);


        }
        private void dataGridViewOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            string productArticle= dataGridViewOrder.Rows[index].Cells[0].Value.ToString();

            if(e.ColumnIndex == 3)
            {
                orderClasses.RemoveAt(index);
               ShowOrder();
            }
            else
            {
                if(e.ColumnIndex == 1)
                {
                    Entini.Product product = Helper.DB.Product.Find(productArticle);
                    Bitmap bit;
                    string photo = product.ProductPhoto;
                    if (String.IsNullOrEmpty(photo))
                    {
                        bit = Properties.Resources.picture;
                    }
                    else
                    {
                        string s = pathExe + "\\Товар\\" + photo;
                        if (!File.Exists(s))
                        {
                            bit = Properties.Resources.picture;

                        }
                        else
                        {
                            bit=new Bitmap(s);
                        }
                    }
                    pictureBoxProduct.BackgroundImage = bit;


                    string dics = "";
                    dics += "Название: " + product.ProductName + Environment.NewLine;
                    dics += "Описание: " + product.ProductDecription + Environment.NewLine;
                    dics += "Категория: " + product.ProductCategoryId + Environment.NewLine;
                    dics += "Производитель: " + product.Manufacturer.ManufacturerName + Environment.NewLine;
                    dics += "Цена без скидки: " + product.ProductCost.ToString() + Environment.NewLine;
                    dics += "Скидка: " + product.ProductDiscount.ToString() + Environment.NewLine;
                    dics += "Поставщик: " + product.ProductProviderId;
                    textBoxProduct.Text = dics;


                }
            }

        }

        private void dataGridViewOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int res = Convert.ToInt32(dataGridViewOrder.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            orderClasses[e.RowIndex].Count = res;	
            if (res == 0)
            {
                orderClasses.RemoveAt(e.RowIndex);	
            }
            ShowOrder();

        }
        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Не могу освободить объект " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
     

        void DataBaseOrder()
        {
            uniqueCode = nomer;
            DateTime date=DateTime.Now;
            DateTime dateDelivery=DateTime.Now.AddDays(3) ;

            int countInStock;
            foreach(var item in orderClasses)
            {
                Entini.Product product = Helper.DB.Product.Find(item.Article);
                countInStock = product.ProductCount;
                if (countInStock < 3)
                {
                    dateDelivery = DateTime.Now.AddDays(6);
                    break;
                }
            }
           
            int idPickupPoint = Convert.ToInt32(comboBoxDelivery.SelectedIndex)+1;
            Entini.Order order;

            do
            {
                order=Helper.DB.Order.Where(x=>x.UniqueCode== nomer).FirstOrDefault();
            }
            while(order!=null);

            string userFullName = Classes.UserRoleName.NameUser;

            Entini.Order newOrder =new Entini.Order();
            newOrder.OrderId = Helper.DB.Order.ToList().ToList().Last().OrderId +1;
            newOrder.OrderDate = date;
            newOrder.OrderDeliveryDate = dateDelivery;
            newOrder.PointId = idPickupPoint;
            newOrder.UserFullName = userFullName;
            newOrder.UniqueCode = uniqueCode;
            newOrder.StatusId = 2;

            try
            {
                Helper.DB.Order.Add(newOrder);
                Helper.DB.SaveChanges();

                orderId = Helper.DB.Order.ToList().ToList().Last().OrderId;
                foreach(var item in orderClasses)
                {
                    Entini.OrderProduct orderProduct = new Entini.OrderProduct();
                    orderProduct.OrderId = orderId;
                    orderProduct.ProductArticle = item.Article;
                    orderProduct.ProductCount= item.Count;
                    try
                    {
                        Helper.DB.OrderProduct.Add(orderProduct);
                        Helper.DB.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось оформить заказ");
                        this.Close();
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось оформить заказ");
                this.Close();
                return;
            }
           


        }
        private void buttonCreateCoupon_Click_1(object sender, EventArgs e)
        {
            DataBaseOrder();
            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
            }
            catch
            {
                MessageBox.Show("Товарный чек в Word создать не удалось");
                return;
            }

            wordDoc = wordApp.Documents.Add();
            wordDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

            wordDoc.Content.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            Word.Paragraph paragraphPicter = wordDoc.Paragraphs.Add();
            Word.Range rangePicter = paragraphPicter.Range;
            wordDoc.Content.Font.Size = 14;
            Word.InlineShape wordPicter = wordDoc.InlineShapes.AddPicture(pathExe + "\\Товар\\" + "E482R4.jpg", Range: wordApp.Selection.Range);
            wordPicter.Width = 70;
            wordPicter.Height = 70;
            rangePicter.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            rangePicter.InsertParagraphAfter();

            Word.Paragraph paragraphTitle = wordDoc.Paragraphs.Add();
            Word.Range rangeTitle = paragraphTitle.Range;
            rangeTitle.Text = " Номер заказа: №" + nomer;
            rangeTitle.Font.Bold = 1;
            rangeTitle.Font.Size = 20;

            rangeTitle.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            rangeTitle.InsertParagraphAfter();

            Word.Paragraph summa = wordDoc.Paragraphs.Add();
            Word.Range rangesumma = summa.Range;
            rangesumma.Text += "Дата заказа: " + DateTime.Now;
            rangesumma.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            rangesumma.InsertParagraphAfter();


            wordPar = (Word.Paragraph)wordDoc.Paragraphs[1];
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;


            Word.Table wordTable;
            wordTable = wordDoc.Tables.Add(wordRange, dataGridViewOrder.RowCount + 1, 2);
            wordTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;


            Word.Range cellRange;
            cellRange = wordTable.Cell(1, 1).Range;
            cellRange.Text = "Товар";
            cellRange = wordTable.Cell(1, 2).Range;
            cellRange.Text = "Количество";
            int i = 0;
            foreach (var item in orderClasses)
            {                
                Entini.Product product = Helper.DB.Product.Find(item.Article);
                cellRange = wordTable.Cell(i + 2, 1).Range;
                cellRange.Text = product.ProductName;
                
                cellRange = wordTable.Cell(i + 2, 2).Range;
                cellRange.Text = orderClasses[i].Count.ToString();
                
                i++;

            }
           

            Word.Paragraph paragraphtext = wordDoc.Paragraphs.Add();
            Word.Range rangetext = paragraphtext.Range;
            rangetext.Text = "Сумма заказа: " + totalSumma + Environment.NewLine;
            rangetext.Text += "Сумма скидки: " + totalSale + Environment.NewLine;
            rangetext.Text += "Пункт выдачи: " + comboBoxDelivery.Text + Environment.NewLine;
            rangetext.Text += "Дата получение: " + DateTime.Now.AddDays(5); ;
            rangetext.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            rangetext.InsertParagraphAfter();

            ///
            wordDoc.Saved = true;

            string pathDoc = pathExe + "\\Word\\" + nomer;
            wordDoc.SaveAs(pathDoc + ".docx");
            

            MessageBox.Show("Талон оформлен");

            wordDoc.SaveAs(pathDoc + ".pdf", Word.WdExportFormat.wdExportFormatPDF);
            wordDoc.Close(true, null, null);
            wordDoc = null;

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wordApp);
            GC.Collect();
        
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            nomer = ran.Next(100, 999);
            string text = "Номер заказа: " + nomer + Environment.NewLine
                + "Дата создание: " + DateTime.Now + Environment.NewLine
                + "Дата доставки: " + DateTime.Now.AddDays(6);

            MessageBox.Show(text);
            buttonCreateOrder.Enabled = false;
            buttonCreateCoupon.Enabled = true;
        }
    }
}

