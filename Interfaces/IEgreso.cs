using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presupuestos.Models;

namespace PresupuestosApi.Interfaces
{
    public interface IEgreso
    {
        List<TbEgreso> GetAllEgresos(); //HttpGet
        TbEgreso GetEgresoById(int id);  //HttpGet
        void DeleteEgreso(int id);  //HttpDelete
        void CreateEgreso(TbEgreso oBjEgreso); //HttpPost
        void UpdateEgreso(TbEgreso oBjEgreso); //HttpPut

    }
}