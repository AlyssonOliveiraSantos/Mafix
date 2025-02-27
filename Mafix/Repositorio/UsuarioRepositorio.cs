using Mafix.Data;
using Mafix.DTOs;
using Mafix.Helper;
using Mafix.Models;
using Mafix.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Mafix.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        public readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.setSenhaHash();
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioDTO usuario)
        {

            UsuarioModel usuarioDb = ListarPorId(usuario.Id);

            if (usuarioDb == null) throw new Exception("Houve um erro na atualização do usuario!");

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Email = usuario.Email;
            if(usuario.Senha != null)usuarioDb.Senha = usuario.Senha.GerarHash();
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            _bancoContext.SaveChanges();
            return usuarioDb;
            
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);
            if (usuarioDb == null) throw new Exception("Houve um erro na deleção do usuario!");

            _bancoContext.Usuarios.Remove(usuarioDb);
            _bancoContext.SaveChanges();
            return true;
        }


    }
}
