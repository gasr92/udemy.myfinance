using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class HomeModel
    {
        public string LerNomeUsuario()
        {
            var dal = new DAL();
            var dt = dal.RetDataTable("select * from usuario");

            if(dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Nome"].ToString();
            }

            return "Nenhum registro";
        }
    }
}
