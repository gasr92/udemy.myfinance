using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public IHttpContextAccessor _context { get; set; }


        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O saldo é obrigatório")]
        public double Saldo { get; set; }
        public int UsuarioId { get; set; }

        public ContaModel()
        {

        }

        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public List<ContaModel> ListaConta()
        {
            var usuarioId = int.Parse(_context.HttpContext.Session.GetString("IdUsuarioLogado"));

            var sql = $"select id, nome, saldo, usuario_id from conta where usuario_id = {usuarioId}";

            var dal = new DAL();
            var dt = dal.RetDataTable(sql);
            var lst = new List<ContaModel>();

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Nome = dt.Rows[i]["NOME"].ToString();
                item.Saldo = double.Parse(dt.Rows[i]["SALDO"].ToString());
                item.UsuarioId = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lst.Add(item);
            }

            return lst;
        }

        public void Insert()
        {
            var usuarioId = int.Parse(_context.HttpContext.Session.GetString("IdUsuarioLogado"));
            var sql = $"insert into conta (nome, saldo, usuario_id) values ('{Nome}', '{Saldo}', {usuarioId})";
            var dal = new DAL();
            dal.Executar(sql);
        }

        public void Excluir(int id)
        {
            var sql = $"delete from conta where id = {id}";
            var dal = new DAL();
            dal.Executar(sql);
        }
    }
}
