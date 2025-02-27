using Mafix.DTOs;
using Mafix.Models;

namespace Mafix.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel ListarPorId(int id);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Adicionar(UsuarioModel usuario);
        UsuarioModel Atualizar(UsuarioDTO usuario);
        bool Apagar(int id);
    }
}
