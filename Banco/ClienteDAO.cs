using AprendizagemWPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AprendizagemWPF.Banco
{
    public class ClienteDAO
    {
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public ClienteDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        public bool Inserir(ClienteModel cliente)
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = @"INSERT INTO Cliente VALUES (@Nome, @CPF, @Email, @DataNascimento, @Ativo, @Sexo, @EstadoCivil, @Imagem)";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
            Cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            Cmd.Parameters.AddWithValue("@Email", cliente.Email);
            Cmd.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(cliente.DataNascimento).ToShortDateString());
            Cmd.Parameters.AddWithValue("@Ativo", true);
            Cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
            Cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
            Cmd.Parameters.AddWithValue("@Imagem", cliente.Imagem);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public List<ClienteModel> Listar()
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = "SELECT * FROM Cliente";

            SqlDataReader rd = Cmd.ExecuteReader();
            List<ClienteModel> clientes = new List<ClienteModel>();

            while (rd.Read())
            {

                ClienteModel cliente = new ClienteModel((int)rd["Id"], (string)rd["Nome"], (string)rd["CPF"], (string)rd["Email"],
                                           (DateTime)rd["DataNascimento"], (bool)rd["Ativo"], (string)rd["Sexo"], (string)rd["EstadoCivil"], (byte[])rd["Imagem"]);
                clientes.Add(cliente);
            }
            rd.Close();
            return clientes;
        }

        public List<ClienteModel> Consultar(string busca)
        {
            busca = $"%{busca}%";
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = "SELECT * FROM Cliente WHERE CPF LIKE @busca OR Nome LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            SqlDataReader rd = Cmd.ExecuteReader();

            List<ClienteModel> clientes = new List<ClienteModel>();
            while (rd.Read())
            {

                ClienteModel cli = new ClienteModel((int)rd["Id"], (string)rd["Nome"], (string)rd["CPF"], (string)rd["Email"],
                                           (DateTime)rd["DataNascimento"], (bool)rd["Ativo"], (string)rd["Sexo"], (string)rd["EstadoCivil"], (byte[])rd["Imagem"]);
                clientes.Add(cli);
            }
            rd.Close();
            return clientes;
        }

        public bool Atualizar(ClienteModel cliente)
        {
            Cmd.Connection = Conexao.RetornarConexao();
            Cmd.CommandText = @"UPDATE Cliente SET Nome = @Nome, CPF = @CPF, Email = @Email, DataNascimento = @DataNascimento, Ativo = @Ativo, Sexo = @Sexo, EstadoCivil = @EstadoCivil, Imagem = @Imagem  WHERE Id = @id";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Id", cliente.Id);
            Cmd.Parameters.AddWithValue("@nome", cliente.Nome);
            Cmd.Parameters.AddWithValue("@CPF", cliente.CPF);
            Cmd.Parameters.AddWithValue("@Email", cliente.Email);
            Cmd.Parameters.AddWithValue("@DataNascimento", Convert.ToDateTime(cliente.DataNascimento).ToShortDateString());
            Cmd.Parameters.AddWithValue("@Ativo", cliente.Ativo);
            Cmd.Parameters.AddWithValue("@Sexo", cliente.Sexo);
            Cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
            Cmd.Parameters.AddWithValue("@Imagem", cliente.Imagem);


            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            return false;
        }

        static public string GerarHashMd5(string entrada)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Converter a String para array de bytes, que é como a biblioteca trabalha.
                byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(entrada));

                // Cria-se um StringBuilder para recompôr a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop para formatar cada byte como uma String em hexadecimal
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
