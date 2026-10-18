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

namespace lab_8_2_registration_form
{
    

    public partial class MainWindow : Window
    {
        string name, secondName;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void txtName_TextChanged(object sender, TextChangedEventArgs e) { name = txtName.Text; }
        private void txtSecondName_TextChanged(object sender, TextChangedEventArgs e){ secondName = txtSecondName.Text; }
        private void txtAge_TextChanged(object sender, TextChangedEventArgs e) {}
        private void txtPasswd_TextChanged(object sender, TextChangedEventArgs e) {}
        private void txtPasswdChek_TextChanged(object sender, TextChangedEventArgs e) {}
        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e) {}
        private void btnClear_Click(object sender, RoutedEventArgs e) { Clear(); }
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;
                MessageBox.Show($"Пользователь {name} {secondName} зарегистрирован." , "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear();

            }
            catch (RegistrationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ex.FieldName.Focus();
            }

        }
        void Clear()
        {
            txtName.Clear();
            txtSecondName.Clear();
            txtAge.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtPasswordChek.Clear();
        }
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите имя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtName.Focus(); // передать фокус на проблемное поле
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                MessageBox.Show("Введите фамилию.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtSecondName.Focus(); // передать фокус на проблемное поле
                return false;
            }
            if (!int.TryParse(txtAge.Text, out int age) || age < 1 || age > 120)
            {
                MessageBox.Show("Возраст: введите число от 1 до 120.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtAge.Focus();
                return false;
            }
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Email должен содержать '@' и '.'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return false;
            }
            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать больше 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }
            if (txtPassword.Text != txtPasswordChek.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPasswordChek.Focus();
                return false;
            }
            return true; // все проверки пройдены
        }
    }
    public class RegistrationException : Exception
    {
        public Control FieldName { get; }
        public RegistrationException(string message, Control field)
        : base(message)
        {
            FieldName = field;
        }
    }
}
