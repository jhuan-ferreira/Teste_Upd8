using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste_Upd8.Data;
using Teste_Upd8.Models;
using Teste_Upd8.Services;
using Teste_Upd8.Models.ViewModel;

namespace Teste_Upd8.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Upd8Context _context;

        public ClientesController(Upd8Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            AutomacaoEstadosMunicipios automacaoEstadosMunicipios = new AutomacaoEstadosMunicipios();
            List<string> Estados = new List<string>();
            List<string> Cidades = new List<string>();

            Estados = automacaoEstadosMunicipios.BuscarEstados();
            Cidades = automacaoEstadosMunicipios.BuscarMunicipios();

            EstadoMunicipioViewModel estadoMunicipioViewModel = new EstadoMunicipioViewModel { Estados = Estados, Cidades = Cidades};
            
            return View(estadoMunicipioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,DataNascimento,Sexo,Endereco,Estado,Cidade")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            AutomacaoEstadosMunicipios automacaoEstadosMunicipios = new AutomacaoEstadosMunicipios();
            List<string> listEstados = new List<string>();
            List<string> listCidades = new List<string>();

            listEstados = automacaoEstadosMunicipios.BuscarEstados();
            listCidades = automacaoEstadosMunicipios.BuscarMunicipios();

            EstadoMunicipioViewModel estadoMunicipioViewModel = new EstadoMunicipioViewModel {Cliente = cliente, Estados = listEstados, Cidades = listCidades };


            return View(estadoMunicipioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,DataNascimento,Sexo,Endereco,Estado,Cidade")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
