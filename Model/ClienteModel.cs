using System;

namespace AprendizagemWPF.Model
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public byte[] Imagem { get; set; } = new byte[1];

        public ClienteModel()
        {
        }

        public ClienteModel(int id, string nome, string cpf, string email, DateTime dataNascimento, bool ativo, string sexo, string estadoCivil, byte[] imagem)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = ativo;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            Imagem = imagem;
        }
    }
}
