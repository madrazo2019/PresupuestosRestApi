using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presupuestos.Models;

namespace Presupuestos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresupuestoController : ControllerBase
    {
        private readonly PresupuestoContext _dbPresupuestoContext;

        public PresupuestoController(PresupuestoContext DBPresupuestoContex)
        {
            this._dbPresupuestoContext = DBPresupuestoContex;
        }
        #region Metodos Get
        [HttpGet("GetAllIngresos")]
        public async Task<IActionResult> GetAllIngresos()
        {
            var list_ingresos = await this._dbPresupuestoContext.TbIngresos.ToListAsync();
            return Ok(list_ingresos);
        }

        [HttpGet("GetAllEgresos")]
        public async Task<IActionResult> GetAllEgresos()
        {
            var list_egresos = await this._dbPresupuestoContext.TbEgresos.ToListAsync();
            return Ok(list_egresos);
        }

        [HttpGet("GetIngresoById/{id}")]
        public IActionResult GetIngresoById(int id)
        //public async Task<IActionResult> GetIngresoById(int id)
        {
            var item_ingreso = this._dbPresupuestoContext.TbIngresos.FirstOrDefault(p => p.Id == id);

            if (item_ingreso == null)
            {
                return NotFound();
            }
            return Ok(item_ingreso);
        }

        [HttpGet("GetEgresoById/{id}")]
        //public async Task<IActionResult> GetByEgresoIdA(int id)
        public IActionResult GetEgresoById(int id)
        {
            var item_egreso = this._dbPresupuestoContext.TbEgresos.FirstOrDefault(p => p.Id == id);
            //var item_egreso = await this._dbPresupuestoContext.TbEgresos.FindAsync(id);
            if (item_egreso == null)
            {
                return NotFound();
            }
            return Ok(item_egreso);
        }

        [HttpGet("GetTotalIngresos")]
        public async Task<IActionResult> GetTotalIngresos()
        {
            double total_ingreso = (await this._dbPresupuestoContext.TbIngresos.ToListAsync()).Select(x => x.Valor).Sum();
            return Ok(total_ingreso);
        }

        [HttpGet("GetTotalEgresos")]
        public async Task<IActionResult> GetTotalEgresos()
        {
            var total_egreso = (await this._dbPresupuestoContext.TbEgresos.ToListAsync()).Select(x => x.Valor).Sum();
            return Ok(total_egreso);
        }

        [HttpGet("GetTotaPresupuesto")]
        public async Task<IActionResult> GetTotaPresupuesto()
        {
            var total_presupuesto = await this._dbPresupuestoContext.ViewSumaTotals.FirstOrDefaultAsync();
            return Ok(total_presupuesto);
        }

        #endregion

        #region Metodos Update(Put) 

        [HttpPut("UpDateIngreso")]
        public async Task<IActionResult> UpDateIngreso(TbIngreso objIngreso)
        {
            _dbPresupuestoContext.TbIngresos.Update(objIngreso);
            await _dbPresupuestoContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("UpDateEgreso")]
        public async Task<IActionResult> UpDateEgreso(TbEgreso objEgreso)
        {
             _dbPresupuestoContext.TbEgresos.Update(objEgreso);
             await _dbPresupuestoContext.SaveChangesAsync();
           
            return NoContent();
        }
        #endregion
        #region Metodos Create
        [HttpPost("CreateIngreso")]
        public async Task<ActionResult> CreateIngreso(TbIngreso objIngreso)
        {

            _dbPresupuestoContext.TbIngresos.Add(objIngreso);
            await _dbPresupuestoContext.SaveChangesAsync();

            return Created($"/GetIngresoById?id={objIngreso.Id}", objIngreso);
        }
        [HttpPost("CreateEgreso")]
        public async Task<ActionResult> CreateEgreso(TbEgreso objEgreso)
        {

            _dbPresupuestoContext.TbEgresos.Add(objEgreso);
            await _dbPresupuestoContext.SaveChangesAsync();

            return Created($"/GetEgresoById?id={objEgreso.Id}", objEgreso);

        }
        #endregion
        #region Metodos Borrar(Delete) 

        [HttpDelete("DeleteIngreso/{id}")]
       public IActionResult  DeleteIngreso(int id)
        {
            var item_ingreso = this._dbPresupuestoContext.TbIngresos.Find(id);
            if (item_ingreso == null)
            {
                return NotFound();
            }

            this._dbPresupuestoContext.Remove(item_ingreso);
            _dbPresupuestoContext.SaveChanges();

            return Ok(true);
        }

        [HttpDelete("DeleteEgreso/{id}")]
        public IActionResult DeleteEgreso(int id)
        {
            var item_egreso =  this._dbPresupuestoContext.TbEgresos.Find(id);
            if (item_egreso == null)
            {
                return NotFound();
            }

            this._dbPresupuestoContext.Remove(item_egreso);
            _dbPresupuestoContext.SaveChanges();

            return Ok(true);
        }

        #endregion
        #region Private methods
        protected private bool ingresoExist(int id)
        {
            bool bExiste;
            bExiste = _dbPresupuestoContext.TbIngresos.Any(e => e.Id == id);
            return bExiste;
        }
        protected private bool egresoExist(int id)
        {
            bool bExiste;
            bExiste = _dbPresupuestoContext.TbEgresos.Any(e => e.Id == id);

            return bExiste;
        }

        #endregion
    }
}