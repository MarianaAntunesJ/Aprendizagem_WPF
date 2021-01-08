using AprendizagemWPF.Banco;
using AprendizagemWPF.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AprendizagemWPF.ViewModel
{
    public class ClienteViewModel : INotifyPropertyChanged
    {
        private ClienteModel _cliente { get; set; }
        private ClienteDAO _clienteDAO;
        private ObservableCollection<ClienteModel> _clientes { get; set; }

        public ClienteModel Cliente {
            get { return _cliente; }
            set { _cliente = value; OnPropertyChanged("Cliente"); }
        }

        public ObservableCollection<ClienteModel> Clientes
        {
            get { return _clientes; }
            set { _clientes = value; OnPropertyChanged("Clientes"); }
        }

        public ClienteViewModel()
        {
            LimparUsuarioAtualTela();
            _clienteDAO = new ClienteDAO();
            AtualizarLista();
        }

        private bool ValidaCliente()
        {
            if (_cliente.Nome != null && _cliente.CPF != null)
            {
                if (_cliente.Email == null)
                    _cliente.Email = string.Empty;
                if (_cliente.Sexo == null)
                    _cliente.Sexo = string.Empty;
                if (_cliente.EstadoCivil == null)
                    _cliente.EstadoCivil = string.Empty;
                return true;
            }
            return false;
        }

        public bool Salvar()
        {
            bool sucesso;
            if(ValidaCliente())
            { 
                if (_cliente.Id == 0)
                    sucesso = _clienteDAO.Inserir(_cliente);
                else
                    sucesso = _clienteDAO.Atualizar(_cliente);
                if (sucesso)
                {
                    AtualizarLista();
                    LimparUsuarioAtualTela();
                    return true;
                }
            }
            return false;
        }

        private void AtualizarLista()
        {
            Clientes = new ObservableCollection<ClienteModel>(_clienteDAO.Listar());
        }

        public void Consultar(string busca)
        {
            Clientes = new ObservableCollection<ClienteModel>(_clienteDAO.Consultar(busca));
        }

        public void Selecionar(int index)
        {
            Cliente = Clientes[index];
        }

        public void LimparUsuarioAtualTela()
        {
            Cliente = new ClienteModel { DataNascimento = DateTime.Today };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
