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
    public class UserController : ControllerBase
    {
        private readonly Banco _banco;
        public UserController(Banco banco) => _banco = banco;

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {                
                return Ok(_banco.User.ToList());
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
                var userOn = _banco.User.Where(u => u.Id == id);

                if (userOn == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(userOn);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                User userNovo = new User
                {
                    //Id random exclusivamente para teste,(Para Produção Utilizar Guid ou outra Forma de Atribuição)
                    Id = new Random().Next(1, 500000000),
                    Nome = user.Nome,                   
                };

                Conta novaConta = new Conta
                {
                    //Id random exclusivamente para teste,(Para Produção Utilizar Guid ou outra Forma de Atribução)
                    Id = new Random().Next(1, 1000000),
                    Tipo = "TESTE",
                    UserId = userNovo.Id,
                    User = userNovo
                };

                userNovo.Conta = novaConta;
                user.ContaId = novaConta.Id;

                _banco.Conta.Add(novaConta);
                _banco.User.Add(userNovo);                
                _banco.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Object objeto)
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
