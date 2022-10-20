using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GerenteController : ControllerBase
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public GerenteController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost]
        public IActionResult AdicionaGerente([FromBody] CreateGerenteDto createGerenteDto)
        {
            Gerente gerente = _mapper.Map<Gerente>(createGerenteDto);
            _context.Gerentes.Add(gerente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaGerentePorId),
                new { Id = gerente.Id }, gerente);
        }

        [HttpGet]
        public IActionResult RecuperaGerentes()
        {
            List<Gerente> gerentes = _context.Gerentes.ToList();
            if(gerentes == null)
            {
                return NotFound();
            }
            List<ReadGerenteDto> readGerenteDto = _mapper.Map<List<ReadGerenteDto>>(gerentes);
            return Ok(readGerenteDto);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGerentePorId(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente != null)
            {
                ReadGerenteDto readGerenteDto = _mapper.Map<ReadGerenteDto>(gerente);
                return Ok(readGerenteDto);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaGerente(int id)
        {
            Gerente gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
            if (gerente == null)
            {
                return NotFound();
            }
            _context.Remove(gerente);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
