using AprendizagemWPF.Banco;

namespace AprendizagemWPF.ViewModel
{
    public class LoginViewModel
    {
        private FuncionarioDAO _funcionarioDAO { get; set; } = new FuncionarioDAO();

        public bool LoginValido(string usuario, string senha) => _funcionarioDAO.VerificarLogin(usuario, senha);
    }
}
