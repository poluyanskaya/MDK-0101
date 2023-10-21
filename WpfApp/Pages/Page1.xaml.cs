using WpfApp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            dtgShip.ItemsSource = DBEntities.GetContext().Shipping.ToList();

            CmbVisit.ItemsSource = DBEntities.GetContext().Shipping
                .Select(x => x.VisitPurpose)
                .Distinct()
                .ToList();

        }

        private void CmbVisit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //фильтр по цели визита
            string visit= (string)CmbVisit.SelectedValue;
            dtgShip.ItemsSource=DBEntities.GetContext().Shipping.Where(x=>x.VisitPurpose==visit).ToList();
        }

        private void BtnResetFiltr_Click(object sender, RoutedEventArgs e)
        {
            //отмена всех фильтров

            dtgShip.ItemsSource = DBEntities.GetContext().Shipping.ToList();

        }

        private void TxtSearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            // поиск по названию порта
            string search = TxtSearchName.Text;
            dtgShip.ItemsSource = DBEntities.GetContext().Shipping.
                Where(x => x.Berth.Contains(search)).ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new Pages.PageAddEdit(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //кнопка изменить
            Classes.ClassFrame.frmObj.Navigate(
                new Pages.PageAddEdit((sender as Button).DataContext as Shipping));
        }

         private void BtnGoListView_Click(object sender, RoutedEventArgs e)
        {
            //переход на страницу с ListView
             Classes.ClassFrame.frmObj.Navigate(new Pages.PageListView());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление нескольких пользователей
            var berthForRemoving = dtgShip.SelectedItems.Cast<Shipping>().ToList();
            if (MessageBox.Show($"Удалить {berthForRemoving.Count()} записей?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)

                try
                {
                    DBEntities.GetContext().Shipping.RemoveRange(berthForRemoving);
                    DBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    dtgShip.ItemsSource = DBEntities.GetContext().Shipping.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

        }



        // private void BtnExcel_Click(object sender, RoutedEventArgs e)
        // {
        //объект Excel
        //    var app = new Excel.Application();

        //создается новая книга 
        //Excel.Workbook wb = app.Workbooks.Add();

        //открыть существующий шаблон
        //    Excel.Workbook wb =
        //    app.Workbooks.Open($"" +
        //   $"{Directory.GetCurrentDirectory()}" +
        //    $"\\Шаблон.xlsx");
        //лист
        //Excel.Worksheet worksheet = 
        //    app.Worksheets.Item[1];
        //    Excel.Worksheet worksheet =
        //        (Excel.Worksheet)wb.Worksheets[1];
        //
        //   int indexRows = 1;
        //ячейка
        //    worksheet.Cells[1][indexRows] = "Номер";
        //    worksheet.Cells[2][indexRows] = "Название книги";
        //    worksheet.Cells[3][indexRows] = "Автор";
        //    worksheet.Cells[4][indexRows] = "Год издания";
        //    worksheet.Cells[5][indexRows] = "Издательство";

        //   var listBooks = DBEntities.
        //        GetContext().Shipping.ToList();

        //     foreach (var book in listBooks)
        //    {
        //        indexRows++;
        //         worksheet.Cells[1][indexRows] = indexRows - 1;
        //         worksheet.Cells[2][indexRows] = book.Name;
        //       worksheet.Cells[3][indexRows] = book.Author.NameAftor;
        //        worksheet.Cells[4][indexRows] = book.Year;
        //         worksheet.Cells[5][indexRows] = book.Publishing.NamePub;
        //     }
        //показать Excel
        //    app.Visible = true;
        //  }

        //  private void BtnEdit_Click(object sender, RoutedEventArgs e)
        // {
        //     Classes.ClassFrame.frmObj.Navigate(
        //         new PageAddEdit((sender as Button).DataContext as Shipping));
        // }
    }
}
