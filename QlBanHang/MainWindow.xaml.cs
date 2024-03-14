using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace QlBanHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=MSI\\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");



        public MainWindow()
        {
            InitializeComponent();
            loadData();
        }


        // Manage products starts
        private void clearTextBox()
        {
            id.Clear();
            name.Clear();
            quantity.Clear();
            price.Clear();
            type.Clear();
        }

        private void show_manage_product()
        {
            manage_product.Visibility = Visibility.Visible;
            manage_revenue.Visibility = Visibility.Collapsed;
            manage_bill.Visibility = Visibility.Collapsed;
        }

        private void show_manage_bill()
        {
            manage_bill.Visibility = Visibility.Visible;
            manage_product.Visibility = Visibility.Collapsed;
            manage_revenue.Visibility = Visibility.Collapsed;
        }

        private void show_manage_revenue()
        {
            manage_revenue.Visibility = Visibility.Visible;
            manage_bill.Visibility = Visibility.Collapsed;
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
            txtTrangChu.Text = "Doanh thu";
        }


        private void add_product(object sender, RoutedEventArgs e)
        {
            submit_data();
            loadData();
            clearTextBox();
        }
        private void edit_products(object sender, RoutedEventArgs e)
        {
            edit();
            clearTextBox();
        }

        private void edit()
        {
            MessageBoxResult message = MessageBox.Show("Bạn có muốn chỉnh sửa sản phẩm?", "Sửa sản phẩm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(message == MessageBoxResult.Yes)
            {
                if (IsExistName())
                {
                    MessageBox.Show("Ten san pham da ton tai");
                    return;
                }
                else
                {
                    if (DataGrid_products.SelectedItem != null)
                    {
                        Products selected_products = (Products)DataGrid_products.SelectedItem;
                        SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");
                        string query = "UPDATE products SET Name = @name, Quantity = @quantity, Type = @type, Price = @price WHERE ID = @id";
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        decimal priceValue;
                        if (!decimal.TryParse(price.Text, out priceValue))
                        {

                            return;
                        }
                        cmd.Parameters.AddWithValue("@id", selected_products.ID);
                        cmd.Parameters.AddWithValue("@name", name.Text);
                        cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                        cmd.Parameters.AddWithValue("@price", priceValue);
                        cmd.Parameters.AddWithValue("@type", type.Text);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        loadData();

                    }
                }
            }
            else
            {
                return;
                clearTextBox();
            }
            

        }

        private void find_products(object sender, RoutedEventArgs e)
        {
            find();
            search_box.Text = " ";
        }

        private void find()
        {
            if (search_box.Text != null)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");

                connection.Open();
                string query = "SELECT * FROM products WHERE name LIKE @name";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", search_box.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                List<Products> products = new List<Products>();

                while (reader.Read())
                {
                    products.Add(new Products()
                    {
                        ID = reader.GetString(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        Type = reader.GetString(3),
                        Price = reader.GetDecimal(4),
                    });
                }
                DataGrid_products.ItemsSource = products;
                connection.Close();
                if (products == null)
                {
                    MessageBox.Show("San pham khong ton tai");
                    return;
                }
            }


        }

        private void refresh(object sender, EventArgs e)
        {
            loadData();
            clearTextBox();
        }

        private void delete_products(object sender, RoutedEventArgs e)
        {
            delete();
        }

        private void delete()
        {
            MessageBoxResult message = MessageBox.Show("Bạn có muốn xóa sản phẩm?", "Xóa sản phẩm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(message == MessageBoxResult.Yes)
            {
                if (DataGrid_products.SelectedItem != null)
                {
                    SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");
                    connection.Open();
                    Products selected_products = (Products)DataGrid_products.SelectedItem;
                    string query = "DELETE  FROM products WHERE name = @name";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", selected_products.Name);
                    cmd.ExecuteNonQuery();
                    loadData();
                    clearTextBox();
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Vui long chon san pham");
                }
            }
            else
            {
                return;
                clearTextBox();
            }
            

        }

        public void loadData()
        {
            string query = "SELECT * FROM products";
            SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Products> products = new List<Products>();

            while (reader.Read())
            {
                products.Add(new Products()
                {
                    ID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Quantity = reader.GetInt32(2),
                    Type = reader.GetString(3),
                    Price = reader.GetDecimal(4),
                });
            }
            DataGrid_products.ItemsSource = products;
            connection.Close();
        }

        private void submit_data()
        {
            if (IsExistId())
            {
                MessageBox.Show("San pham da co san");
                return;
            }
            else if (IsExistName())
            {
                MessageBox.Show("Ten san pham da ton tai");
                return;
            }
            else
            {
                decimal priceValue;
                if (!decimal.TryParse(price.Text, out priceValue))
                {

                    return;
                }
                SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");

                connection.Open();
                string query1 = "INSERT INTO products VALUES(@id, @name, @quantity, @type, @price) ";

                SqlCommand cmd = new SqlCommand(query1, connection);
                cmd.Parameters.AddWithValue("@id", id.Text);
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                cmd.Parameters.AddWithValue("@price", priceValue);
                cmd.Parameters.AddWithValue("@type", type.Text);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }


        private bool IsExistId()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");
            connection.Open();
            string query = "SELECT COUNT(*) FROM products WHERE id = @id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id.Text);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }

        private bool IsExistName()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");
            connection.Open();
            string query = "SELECT COUNT(*) FROM products WHERE name = @name";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@name", name.Text);
            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid_products.SelectedItem != null)
            {
                Products selected_products = (Products)DataGrid_products.SelectedItem;
                name.Text = selected_products.Name;
                id.Text = selected_products.ID;
                quantity.Text = selected_products.Quantity.ToString();
                type.Text = selected_products.Type;
                price.Text = selected_products.Price.ToString();

            }
        }
        public class Products
        {
            public String ID { get; set; }
            public String Name { get; set; }
            public int Quantity { get; set; }
            public String Type { get; set; }
            public Decimal Price { get; set; }

        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(message == MessageBoxResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
                this.Close();
                connection.Close();
                
            }
            if(message == MessageBoxResult.No)
            {
                return;
            }
        }
    }
    // Manage products end
}
