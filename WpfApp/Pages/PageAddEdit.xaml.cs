using WpfApp.Classes;
using System;
using System.Collections.Generic;
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
using System.Runtime.Remoting.Contexts;
using System.ComponentModel;

namespace WpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEdit.xaml
    /// </summary>
    public partial class PageAddEdit : Page
    {
        Shipping _currentShipping = new Shipping();
        public PageAddEdit(Shipping shiplocal)
        {
            InitializeComponent();
            CmbBerth.ItemsSource = DBEntities.GetContext().Shipping
                .Select(x => x.Berth)
                .Distinct()
                .ToList();

            CmbShip.ItemsSource = DBEntities.GetContext().Ship.ToList();
            CmbShip.SelectedValuePath = "Shipid";
            CmbShip.DisplayMemberPath = "ShipName";


            if (shiplocal != null)
                _currentShipping = shiplocal;
            //создаем контекст
            DataContext = _currentShipping;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_currentShipping.ShipCode == 0)
                DBEntities.GetContext().Shipping.Add(_currentShipping);
            try
            {
                DBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                ClassFrame.frmObj.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

                Classes.ClassFrame.frmObj.Navigate(new Pages.Page1());
        }
    }
}
