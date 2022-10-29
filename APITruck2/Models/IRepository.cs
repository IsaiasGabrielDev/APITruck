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

    public abstract class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly BaseContext _context;
        public CaminhaoRepository(BaseContext baseContext)
        {
            _context = baseContext;
        }

        public async Task<Caminhao> Atualizar(int id, Caminhao obj)
        {           
            var objdb = await BuscarId(id);

            if(objdb == null)
            {
                throw new Exception($"Id do caminhão não localizado");
            }

            if (obj.AnoModelo > objdb.AnoFabricacao + 1 || obj.AnoModelo < objdb.AnoFabricacao)
            {
                throw new Exception($"Ano do modelo menor que o ano da Fabricação ou maior que um ano mais dela.");
            }

            objdb.AnoFabricacao = obj.AnoFabricacao;
            objdb.AnoModelo = obj.AnoModelo;
            objdb.NomeModelo = obj.NomeModelo;

            await Save();

            return (objdb);
        }

        public async Task<Caminhao> BuscarId(int id)
        {
            var caminhao = await _context.Caminhoes.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
            return caminhao;
        }

        public async Task Deletar(int id)
        {
            var obj = await BuscarId(id); 
            if(obj == null)
            {
                throw new Exception("Caminhao não localizado");
            }
            _context.Caminhoes.Remove(obj);

            await Save();
        }

        public async Task<Caminhao> Inserir(Caminhao obj)
        {
            obj.Id = 0;
            obj.AnoFabricacao = DateTime.Now.Year;

            if (obj.AnoModelo > obj.AnoFabricacao + 1 || obj.AnoModelo < obj.AnoFabricacao)
            {
                throw new Exception($"Ano do modelo menor que o ano atual ou maior que {DateTime.Now.Year + 1}");
            }

            _context.Caminhoes.Add(obj);

            await Save();

            return obj;
        }

        public async Task<List<Caminhao>> Listar()
        {
            var caminhoes = await _context.Caminhoes.AsTracking().ToListAsync();

            return caminhoes;
        }

        private async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel Salvar Bando de dados.");
            }
        }
    }


}
