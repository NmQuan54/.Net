using QlBanHang.Connections;
using QlBanHang.Helper;
using QlBanHang.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QlBanHang
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ToLogin(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=MSI\MSSQLSERVER02;Initial Catalog=products;Integrated Security=True");
            try
            {
                connection.Open();

                String Username = txtUser.Text;
                String Password = txtPass.Password;

                if (!AlertHelper.IsValidUsername(Username))
                {
                    MessageBox.Show("Username không hợp lệ.");
                    return;
                }

                // Kiểm tra định dạng Password
                if (!AlertHelper.IsValidPassword(Password))
                {
                    MessageBox.Show("Password không hợp lệ.");
                    return;
                }

                string hashedPass = HashPassword(Password);
                    
                    string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", Username);
                        command.Parameters.AddWithValue("@Password", hashedPass);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                MessageBox.Show("Đăng nhập thành công!");

                                // Hiển thị MainWindow
                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();
                                this.Close();
                            }
                            else
                            {   
                                // Sai tên đăng nhập hoặc mật khẩu
                                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private const string Salt = "dotnet";
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string SaltedPass = string.Concat(password, Salt);
                byte[] HashedByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(SaltedPass));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in HashedByte)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }

        }

        private void toRegister(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.ShowDialog();
            this.Close();
        }
    }

}
