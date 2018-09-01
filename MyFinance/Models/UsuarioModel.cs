using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a {0}")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento")]
        public DateTime Data_Nascimento { get; set; }


        public bool ValidarLogin()
        {
            var sql = $"select * from usuario where email = '{Email}' and senha = '{Senha}'";

            var dal = new DAL();
            var dt = dal.RetDataTable(sql);

            if(dt != null && dt.Rows.Count == 1)
            {
                Id = int.Parse(dt.Rows[0]["Id"].ToString());
                Nome = dt.Rows[0]["Nome"].ToString();
                Data_Nascimento = DateTime.Parse(dt.Rows[0]["Data_Nascimento"].ToString());

                return true;
            }

            return false;
        }

        public void RegistrarUsuario()
        {

            string data = Data_Nascimento.ToString("yyyy/MM/dd");
            var sql = $"Insert Into usuario (nome, email, senha, data_nascimento) values ('{Nome}', '{Email}', '{Senha}', '{data}')";
            var obj = new DAL();
            obj.Executar(sql);
        }
    }
}
