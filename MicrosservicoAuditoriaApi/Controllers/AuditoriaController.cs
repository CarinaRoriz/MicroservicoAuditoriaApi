using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosservicoAuditoriaApi.Modelos;

namespace MicrosservicoAuditoriaApi.Controllers
{
    [Route("v1/auditoria")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        List<LogAuditoria> listaAuditoria;

        public AuditoriaController()
        {
            //Situação 1: Autorizado
            listaAuditoria = new List<LogAuditoria>
            {
                new LogAuditoria { Id = 1, IdTransacao = 1, IdPedido = 1, LoginUsuario = "carina", Data = new DateTime(2019, 04, 18)  }
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogAuditoria>>> GetLogAuditoria()
        {
            if (listaAuditoria.Count() == 0)
            {
                return NotFound();
            }

            return listaAuditoria;
        }

        [HttpGet("pedido/{id}")]
        public async Task<ActionResult<IEnumerable<LogAuditoria>>> GetLogAuditoria(long id)
        {
            List<LogAuditoria> lista = listaAuditoria.Where(a=>a.IdPedido == id).ToList();

            if (lista.Count() == 0)
            {
                return NotFound();
            }

            return lista;
        }

        [HttpPost]
        public async Task<ActionResult<long>> CadastraLogAuditoria(LogAuditoria logAuditoria)
        {
            try
            {
                LogAuditoria novaAuditoria = new LogAuditoria() { Id = (listaAuditoria.Max(l => l.Id) + 1), IdTransacao = logAuditoria.IdTransacao, IdPedido = logAuditoria.IdPedido, LoginUsuario = logAuditoria.LoginUsuario, Data = logAuditoria.Data };
                listaAuditoria.Add(novaAuditoria);

                return novaAuditoria.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
