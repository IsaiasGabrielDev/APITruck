using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITruck.Models;
using Microsoft.EntityFrameworkCore;

namespace APITruck.Models
{
    public interface ICaminhaoRepository
    {
        Task<Caminhao> Inserir(Caminhao obj);
        Task Deletar(int id);
        Task<Caminhao> BuscarId(int id);
        Task<Caminhao> Atualizar(int id, Caminhao obj);
        Task<List<Caminhao>> Listar();

    }
}
