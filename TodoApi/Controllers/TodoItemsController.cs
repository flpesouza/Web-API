using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Util;
using System.Data;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        // Construtor da classe que recebe um contexto do banco de dados
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        // Retorna todos os itens da lista de tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            try
            {
                // Código que pode ser utilizado para inserir um cliente no banco de dados
                using (DAL objDAL = new DAL())
                {
                 /*   string sql = @"
                INSERT INTO cliente (
                    nome, data_cadastro, cpf_cnpj, data_nascimento, tipo,
                    telefone, email, cep, logradouro, numero, bairro,
                    complemento, cidade, uf
                )
                VALUES (
                    'felipe', '2025-03-31', '7070', '2000-05-18', 'f',
                    '7070', 'felipe@hotmail.com', '7070', 'rua p',
                    '70', 'sM', '70', 'bh', 'mg'
                );
            ";

                    objDAL.ExecutarComandoSQL(sql);*/
                }

                return await _context.TodoItems.ToListAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao inserir cliente: {ex.Message}");
            }
        }



        // GET: api/TodoItems/5
        // Retorna um item específico pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {

            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                // Retorna erro 404 se o item não for encontrado
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // Atualiza um item existente na lista de tarefas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                // Retorna erro 400 se os IDs não coincidirem
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    // Retorna erro 404 se o item não existir
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Retorna código 204 indicando sucesso sem conteúdo no corpo da resposta
            return NoContent();
        }

        // POST: api/TodoItems
        // Cria um novo item na lista de tarefas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            // Retorna o item criado e o local onde ele pode ser acessado
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPost]
        [Route("registarcliente")]
        public ReturnAllServices RegistrarCliente(ClienteModel dados)
        {
            ReturnAllServices retorno = new ReturnAllServices();
            try
            {
                dados.RegistrarCliente();
                retorno.Result = true;
                retorno .ErrorMensage = string.Empty;
            }
            catch (Exception ex) {
                retorno.Result = false;
                retorno.ErrorMensage = "Erro ao tentar registrar cliente: "+ ex.Message;
            }
            return retorno;
        }

        // DELETE: api/TodoItems/5
        // Remove um item específico da lista de tarefas
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                // Retorna erro 404 se o item não for encontrado
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            // Retorna código 204 para indicar que a exclusão foi bem-sucedida
            return NoContent();
        }

        // Método auxiliar para verificar se um item existe no banco de dados
        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
