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

namespace QlBanHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void show_manage_product()
        {
            manage_product.Visibility = Visibility.Visible;
            manage_revenue.Visibility = Visibility.Collapsed;
            manage_bill.Visibility = Visibility.Collapsed;
        }

        private void show_manage_bill()
        {
            manage_bill.Visibility= Visibility.Visible;
            manage_product.Visibility= Visibility.Collapsed;
            manage_revenue.Visibility= Visibility.Collapsed;
        }

        private void show_manage_revenue()
        {
            manage_revenue.Visibility = Visibility.Visible;
            manage_bill.Visibility= Visibility.Collapsed;
            manage_product.Visibility = Visibility.Collapsed;
        }

        private void to_manage_product(object sender, RoutedEventArgs e)
        {
            show_manage_product();
            txtTrangChu.Text = "Sản phẩm";
        }

        private void to_manage_bill(object sender, RoutedEventArgs e)
        {
            show_manage_bill();
            txtTrangChu.Text = "Đơn hàng";
        }

        private void to_manage_revenue(object sender, RoutedEventArgs e)
        {
            show_manage_revenue();
            txtTrangChu.Text= "Doanh thu";
        }

        
    }
}
