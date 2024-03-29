﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRoupas.Models;

namespace WebRoupas.Controllers
{
    // Esse método de Route configura a rota da aplicação, ou seja, no postman você vai criar uma requisão do tipo get
    // E o endereço dela vai ficar assim http://seuLocalHost/api/Funcionarios caso seja outro você trocaria o funcionario pelo
    // primeiro nome do controller, ex RoupasController -> http://seuLocalHost/api/Roupas

    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly BancoDeDados _context;

        public FuncionariosController(BancoDeDados context)
        {
            _context = context;
        }

        // GET: api/Funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            // Aqui nesse método, repare que comentei o retorno dele, esse retorno está retornando uma lista de funcionarios cadastrados no banco.
            // Como não cadastrei nenhum substitui por esse retorno abaixo, onde crio uma lista com dois funcionarios e retorno os dois para efeitos de 
            // teste

            var funcionarios = new List<Funcionario>() { new Funcionario() { Id = 1, CPF= "111111111111", Email = "diegol.aquino@gmail.com", Nome = "Diego Aquino", Senha = "123456"},
                    new Funcionario() { Id = 2, CPF= "22222222222222", Email = "gabriel@gmail.com", Nome = "Gabriel", Senha = "777777777" }
            };

            return funcionarios;

            //return await _context.Funcionarios.ToListAsync();
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        // PUT: api/Funcionarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Funcionarios
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionario", new { id = funcionario.Id }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Funcionario>> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return funcionario;
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
