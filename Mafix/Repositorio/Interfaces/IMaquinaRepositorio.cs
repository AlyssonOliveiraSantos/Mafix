﻿using Mafix.Models;

namespace Mafix.Repositorio.Interfaces
{
    public interface IMaquinaRepositorio
    {
        MaquinaModel ListarPorId(int id);
        List<MaquinaModel> BuscarTodas();
        MaquinaModel Adicionar(MaquinaModel maquina);
        MaquinaModel Atualizar(MaquinaModel maquina);
        bool Apagar(int id);
    }
}
