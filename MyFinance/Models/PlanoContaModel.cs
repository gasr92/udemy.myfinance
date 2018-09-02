using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFinance.Models
{
    public class PlanoContaModel
    {

        public IHttpContextAccessor _context { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        public string Tipo { get; set; }
        public int UsuarioId { get; set; }

        public PlanoContaModel()
        {

        }

        public PlanoContaModel(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public List<PlanoContaModel> ListaPlanoContas()
        {
            var usuarioId = int.Parse(_context.HttpContext.Session.GetString("IdUsuarioLogado"));

            var sql = $"select id, descricao, tipo, usuario_id from plano_contas where usuario_id = {usuarioId}";

            var dal = new DAL();
            var dt = dal.RetDataTable(sql);
            var lst = new List<PlanoContaModel>();

            for(int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new PlanoContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Descricao = dt.Rows[i]["DESCRICAO"].ToString();
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.UsuarioId = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lst.Add(item);
            }

            return lst;
        }

        public PlanoContaModel CarregarRegistro(int id)
        {
            var sql = $"select id, descricao, tipo, usuario_id from plano_contas where id = {id}";

            var dal = new DAL();
            var dt = dal.RetDataTable(sql);

            var item = new PlanoContaModel();
            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Descricao = dt.Rows[0]["DESCRICAO"].ToString();
            item.Tipo = dt.Rows[0]["TIPO"].ToString();
            item.UsuarioId = int.Parse(dt.Rows[0]["USUARIO_ID"].ToString());

            return item;
        }

        public void Salvar()
        {
            var usuarioId = int.Parse(_context.HttpContext.Session.GetString("IdUsuarioLogado"));

            string sql = string.Empty;
            if(Id == 0)
                sql = $"insert into plano_contas (descricao, tipo, usuario_id) values ('{Descricao}', '{Tipo.Substring(0,1)}', {usuarioId})";
            else
                sql = $"update plano_contas set descricao = '{Descricao}', tipo = '{Tipo.Substring(0,1)}' where id = {Id}";


            var dal = new DAL();
            dal.Executar(sql);
        }

        public void Excluir(int id)
        {
            var sql = $"delete from plano_contas where id = {id}";
            var dal = new DAL();
            dal.Executar(sql);
        }


    }
}