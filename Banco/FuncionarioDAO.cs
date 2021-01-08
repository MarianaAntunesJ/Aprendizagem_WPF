using System.Data.SqlClient;

namespace AprendizagemWPF.Banco
{
    public class FuncionarioDAO
    {
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public FuncionarioDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        public bool VerificarLogin(string usuario, string senha)
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = "SELECT * FROM Funcionario WHERE Usuario = @Usuario1 AND Senha = @Senha ";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Usuario1", usuario);
            Cmd.Parameters.AddWithValue("@Senha", senha);

            SqlDataReader rd = Cmd.ExecuteReader();

            while (rd.Read())
            {
                if (rd != null)
                {
                    rd.Close();
                    return true;
                }
            }
            rd.Close();
            return false;
        }
    }
}
