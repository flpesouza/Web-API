using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoApi.Util;
using MySqlX.XDevAPI;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly TodoContext _context;

        Autenticacao AutenticacaoServico;

        public ClientesController(TodoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            AutenticacaoServico = new Autenticacao(httpContextAccessor);
        }


        // GET: api/Clientes/Listagem
        [HttpGet("Listagem")]
        public ActionResult<IEnumerable<ClienteModel>> ListarClientesManual()
        {
            try
            {
                // Código que pode ser utilizado para inserir um cliente no banco de dados
                //using (DAL objDAL = new DAL())
                {
                    /*   string sql = @"
                   INSERT INTO cliente (
                       nome, data_cadastro, cpf_cnpj, data_nascimento, tipo,
                       telefone, email, cep, logradouro, numero, bairro,
                       complemento, cidade, uf
                   )
                   VALUES (
                       'felipe', '2025-03-31', '7070', '2000-05-18', 'f',
                       '31999999', 'felipe@hotmail.com', '30710', 'rua',
                       '1', 'santa maria', '1', 'bh', 'mg'
                   );
               ";

                       objDAL.ExecutarComandoSQL(sql);*/

                    var clientes = new ClienteModel().Listagem();
                    return Ok(clientes);
                }
                //return await _context.TodoItems.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao inserir cliente: {ex.Message}");
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> GetRetornarCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return cliente;
        }

        // POST: api/Clientes/RegistrarCliente
        [HttpPost("RegistrarCliente")]
        public async Task<ActionResult<ClienteModel>> RegistrarCliente(ClienteModel cliente)
        {
            try
            {
                cliente.Data_Cadastro = DateTime.Now.ToString("yyyy-MM-dd");
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                return Ok(cliente);

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao registrar cliente: {ex.Message}");
            }
        }

        // PUT: api/Clientes/atualizar/1
        [HttpPut("Atualizar/{id}")]
        public IActionResult AtualizarCliente(int id, ClienteModel dados)
        {
            try
            {
                dados.Id = id;
                dados.AtualizarCliente(); // chama seu método manual, que usa o DAL

                return Ok(dados);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar cliente: {ex.Message}");
            }
        }




        // DELETE: api/Clientes/5
        [HttpDelete("excluir/{id}")]
        public IActionResult DeletarCliente(int id)
        {
            try
            {
                AutenticacaoServico.Autenticar();
                new ClienteModel().Excluir(id);
                return Ok($"Cliente com ID {id} excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir cliente: {ex.Message}");
            }
        }

    }
}
