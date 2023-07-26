using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presupuestos.Models;

namespace Presupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly PresupuestoContext _DBPresupuestoContext;

        public PresupuestoController(PresupuestoContext _dbPresupuestoContex)
        {
            this._DBPresupuestoContext = _dbPresupuestoContex;
        }
        #region Metodos Get
        [HttpGet("GetAllIngresos")]
        public async Task<ActionResult<TbIngreso>> GetAllIngresos()
        {
            var ingresos = await this._DBPresupuestoContext.TbIngresos.ToListAsync();
            return Ok(ingresos);
        }
        
        [HttpGet("GetAllEgresos")]
        public async Task<ActionResult<TbEgreso>>  GetAllEgresos()
        {
            var item_egresos = await this._DBPresupuestoContext.TbEgresos.ToListAsync();
            return Ok(item_egresos);
        }

        [HttpGet("GetByIngresoCode/(id)")]
        public async Task<ActionResult<TbIngreso>> GetByIngresoCode(int id)
        {
            var item_ingreso = await this._DBPresupuestoContext.TbIngresos.FindAsync(id);
            if(item_ingreso == null)
            {
                return NotFound();   
            }
            return Ok(item_ingreso);
        }

        [HttpGet("GetByEgresoCode/(id)")]
        public async Task<ActionResult<TbEgreso>> GetByEgresoCode(int id)
        {
            //var egreso = this._DBPresupuestoContext.TbEgresos.FirstOrDefault(p=>p.Id == code);
            var item_egreso = await this._DBPresupuestoContext.TbEgresos.FindAsync(id);
            if(item_egreso == null)
            {
                return NotFound();   
            }
            return Ok(item_egreso);
        }

        [HttpGet("GetTotalIngresos")]
        public async Task<ActionResult<TbIngreso>> GetTotalIngresos()
        {
            double total_ingreso = (await this._DBPresupuestoContext.TbIngresos.ToListAsync()).Select(x => x.Valor).Sum();
            return Ok(total_ingreso);
        }

        [HttpGet("GetTotalEgresos")]
        public async Task<ActionResult<TbEgreso>> GetTotalEgresos()
        {
            var total_egreso =  (await  this._DBPresupuestoContext.TbEgresos.ToListAsync()).Select(x => x.Valor).Sum();
            return Ok(total_egreso);
        }

        [HttpGet("GetTotaPresupuesto")]
        public async Task<ActionResult<ViewSumaTotal>> GetTotaPresupuesto()
        {
            var total_presupuesto =  await this._DBPresupuestoContext.ViewSumaTotals.FirstOrDefaultAsync();
            return Ok(total_presupuesto);
        }

        #endregion

        #region Metodos Update(Put) 

        [HttpPut("UpDateIngreso/(id)")]
        public async Task<ActionResult<TbIngreso>> UpDateIngreso(int id, TbIngreso tbIngreso)
         {
            if (id != tbIngreso.Id)
            {
            return BadRequest();
            }

            var item_ingreso = await this._DBPresupuestoContext.TbIngresos.FindAsync(id);
            if(item_ingreso == null)
            {
                 return NotFound();   
            }

            item_ingreso.Descripcion = tbIngreso.Descripcion;
            item_ingreso.Valor = tbIngreso.Valor;
             try
             {
                await _DBPresupuestoContext.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException) when (!ingresoExist(id))
             {
                return NotFound();
             }
              return NoContent();

        }
        #endregion
        #region Metodos Borrar(Delete) 

        [HttpDelete("DeleteIngreso/(id)")]
        public async Task<ActionResult<TbIngreso>> DeleteIngreso(int id)
        {
            var item_ingreso = await this._DBPresupuestoContext.TbIngresos.FindAsync(id);
            if(item_ingreso == null)
            {
                return NotFound();   
            }

            this._DBPresupuestoContext.Remove(item_ingreso);
            await _DBPresupuestoContext.SaveChangesAsync();

            return Ok(true);
        }
        #endregion
        #region Private methods
        protected private bool ingresoExist(int id)
        {
            return _DBPresupuestoContext.TbIngresos.Any(e => e.Id == id);
         }

        #endregion
   }
}