using AprendizagemWPF.Banco;
using AprendizagemWPF.ViewModel;
using System.Windows;

namespace AprendizagemWPF.View
{
    public partial class LoginView : Window
    {
        private LoginViewModel _loginViewModel { get; set; } = new LoginViewModel();

        public LoginView()
        {
            InitializeComponent();
        }

        private void BtEntrar_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(TxBUsuario.Text) && !string.IsNullOrEmpty(PBSenha.Password))
            {
                if (_loginViewModel.LoginValido(TxBUsuario.Text, ClienteDAO.GerarHashMd5(PBSenha.Password)))
                {
                    CadastroView cadastroView = new CadastroView();
                    cadastroView.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário e/ou senha inválida. Tente novamente", "Login Inválido");
                }
            }
            else
            {
                MessageBox.Show("Usuário e/ou senha não preenchida. Tente novamente", "Login Inválido");
            }
        }
    }
}
