using AprendizagemWPF.ViewModel;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AprendizagemWPF.View
{
    public partial class CadastroView : Window
    {
        private ClienteViewModel _clienteViewModel { get; set; }
        private readonly string _caminhoImagemPadrao = "..\\..\\Imagens\\SuaFotoAqui.jfif";

        public CadastroView()
        {
            InitializeComponent();
            _clienteViewModel = new ClienteViewModel();
            DataContext = _clienteViewModel;
            DefinirImagem(_caminhoImagemPadrao);
        }

        private void DefinirImagem(string caminhoImagem)
        {
            ImgSuaFotoAqui.Source = new ImageSourceConverter().ConvertFromString(caminhoImagem) as ImageSource;
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {

            if (_clienteViewModel.Salvar())
            {
                DefinirImagem(_caminhoImagemPadrao);
                RBCasado.IsChecked = false;
                RBSolteiro.IsChecked = false;
                RBViuvo.IsChecked = false;

                MessageBox.Show("Cliente salvo!", "Salvo");
            }
            else
                MessageBox.Show("Cliente não foi salvo.", "Erro");
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            else if (e.PropertyName == "Imagem")
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void DGClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGClientes.Items.IndexOf(DGClientes.CurrentItem) >= 0)
            {
                _clienteViewModel.Selecionar(DGClientes.Items.IndexOf(DGClientes.CurrentItem));

                if (_clienteViewModel.Cliente.Imagem[0] != 0)
                    ImgSuaFotoAqui.Source = ByteToImage(_clienteViewModel.Cliente.Imagem);
                else
                    DefinirImagem(_caminhoImagemPadrao);

                if (_clienteViewModel.Cliente.Sexo.Equals("Feminino"))
                    CBSexo.SelectedIndex = 0;
                else if (_clienteViewModel.Cliente.Sexo.Equals("Masculino"))
                    CBSexo.SelectedIndex = 1;
                else if (_clienteViewModel.Cliente.Sexo.Equals("Outro"))
                    CBSexo.SelectedIndex = 2;
                if (_clienteViewModel.Cliente.EstadoCivil.Equals("Solteiro"))
                    RBSolteiro.IsChecked = true;
                else if (_clienteViewModel.Cliente.EstadoCivil.Equals("Casado"))
                    RBCasado.IsChecked = true;
                else if (_clienteViewModel.Cliente.EstadoCivil.Equals("Viúvo"))
                    RBViuvo.IsChecked = true;
            }
        }

        private void BtNovo_Click(object sender, RoutedEventArgs e)
        {
            _clienteViewModel.LimparUsuarioAtualTela();
            DefinirImagem(_caminhoImagemPadrao);
        }

        private void BtSair_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void RBEstadoCivil_Checked(object sender, RoutedEventArgs e)
        {
            _clienteViewModel.Cliente.EstadoCivil = (sender as RadioButton).Content.ToString();
        }

        private void ImgSuaFotoAqui_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                FileStream stream = File.OpenRead(dlg.FileName);
                _clienteViewModel.Cliente.Imagem = new byte[stream.Length];
                stream.Read(_clienteViewModel.Cliente.Imagem, 0, _clienteViewModel.Cliente.Imagem.Length);
                stream.Close();
                ImgSuaFotoAqui.Source = ByteToImage(_clienteViewModel.Cliente.Imagem);
            }
        }

        public ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            biImg.BeginInit();
            MemoryStream ms = new MemoryStream(imageData);

            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void TBCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            _clienteViewModel.Consultar(TBCliente.Text);
        }
    }
}