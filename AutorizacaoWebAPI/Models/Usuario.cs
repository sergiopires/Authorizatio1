using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutorizacaoWebAPI.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }

        public Usuario Get(string Login, string Senha)
        {
            List<Usuario> usuarios = new List<Usuario>
            {
                new Usuario() {ID = 1, Login = "joao@email.com", Senha="joao", Nome="Joao", Role = "Usuario"},
                new Usuario() {ID = 2, Login = "antonio@email.com", Senha="antonio", Nome="Antônio", Role = "Supervisor"},
                new Usuario() {ID = 3, Login = "maria@email.com", Senha="maria", Nome="Maria", Role = "Admin" }
               };

            return usuarios.Find(x => x.Login == Login && x.Senha == Senha);

        }
    }
}