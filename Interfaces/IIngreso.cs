using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presupuestos.Models;

namespace PresupuestosApi.Interfaces
{
    public interface IIngreso
    {
        List<TbIngreso> GetAllIngresos();
        TbIngreso GetIngresoById(int id);
        void DeleteIngreso(int id);  //HttpDelete
        void CreateIngreso(TbEgreso objIngreso); //HttpPost
        void UpdateIngreso(TbEgreso objIngreso); //HttpPut
    }
}