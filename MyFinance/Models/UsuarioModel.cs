using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
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
    }
}
