using QlBanHang.Connections;
using QlBanHang.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Data;

namespace QlBanHang.ViewModel
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        public Register()
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

        private void toLogin(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }
        
        private void createAcc(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=QUAN;Initial Catalog=QLBanHang;Integrated Security=True;");
            try
            {
                connection.Open();

                string Username = txtUser.Text;
                string Password = txtPass.Text;
                string Email = txtEmail.Text;
                string RePassword = txtRePass.Text;

                if (!AlertHelper.IsValidUsername(Username))
                {
                    MessageBox.Show("Username không hợp lệ.");
                    return;
                }

                if (!AlertHelper.IsValidPassword(Password))
                {
                    MessageBox.Show("Password không hợp lệ.");
                    return;
                }

                if (!AlertHelper.IsValidEmail(Email))
                {
                    MessageBox.Show("Email không hợp lệ.");
                    return;
                }
                if (!AlertHelper.IsValidRePassword(Password, RePassword))
                {
                    MessageBox.Show("Mật khẩu nhập lại không khớp.");
                    return; 
                }

                   string query = "INSERT INTO Acount (Username, Password, Email) VALUES (@Username, @Password, @Email)";

                   SqlCommand command = new SqlCommand(query, connection);
                    
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Email", Email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Tài khoản đã được tạo thành công!");
                            Login login = new Login();
                            login.Show();   
                        }
                        else
                        {
                            MessageBox.Show("Không thể tạo tài khoản.");
                        }

                    connection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

