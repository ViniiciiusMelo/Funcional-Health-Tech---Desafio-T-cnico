using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly Banco _banco;
        public ContaController(Banco banco) => _banco = banco;

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var recebe = _banco.Conta;
                return Ok(_banco.Conta.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var accountSelect = _banco.Conta.Where(u => u.Id == id);

                if (accountSelect == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(accountSelect);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Conta conta)
        {
            try
            {
                _banco.Conta.Add(conta);
                _banco.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Sacar/{id}")]
        public IActionResult Sacar(int id, [FromBody] Movimentacoes movimentacao)
        {
            try
            {
                Conta conta = _banco.Conta.Where(x => x.Id == id).FirstOrDefault();

                if (conta != null && movimentacao.valor <= conta.Saldo)
                {
                    conta.Saldo = conta.Saldo - movimentacao.valor;
                    _banco.Conta.Update(conta);
                    _banco.SaveChanges();
                    return Ok(conta.Saldo);
                }
                else
                {
                    throw new Exception("Invalid");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("Depositar/{id}")]
        public IActionResult Depositar(int id, [FromBody] Movimentacoes movimentacao)
        {
            try
            {
                Conta conta = _banco.Conta.Where(x => x.Id == id).FirstOrDefault();

                if (conta != null)
                {
                    conta.Saldo += movimentacao.valor;
                    _banco.Conta.Update(conta);
                    _banco.SaveChanges();
                    return Ok(conta.Saldo);
                }
                else
                {
                    throw new Exception("Invalid");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("QuerySaldo/{id}")]
        public IActionResult QuerySaldo(int id)
        {
            try
            {
                Conta conta = _banco.Conta.Where(x => x.Id == id).FirstOrDefault();

                if (conta != null) return Ok(conta.Saldo);

                throw new Exception("Conta não encontrada");

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
