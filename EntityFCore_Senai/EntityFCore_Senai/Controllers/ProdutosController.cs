using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFCore_Senai.Domains;
using EntityFCore_Senai.Interfaces;
using EntityFCore_Senai.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFCore_Senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController()
        {
            _produtoRepository = new ProdutoRepository();
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Lista os produtos no repositório
                var produtos = _produtoRepository.Listar();

                //Verifica se existe produtos, caso não exista retorna
                //NoContent - Sem conteúdo
                if (produtos.Count == 0)
                    return NoContent();

                //Caso exista retorna um Ok junto com os produtos
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                //Caso ocorra algum erro retorna BadRequest e a mensagem de erro tratada
                return BadRequest(ex.Message);
            }
        }
        
        // GET Api/<ProdutoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                //Busca o produto pelo nome no repositório
                Produto produto = _produtoRepository.BuscarPorId(id);

                //Verifica se o produto existe, caso não exista ele retorna NotFound
                if (produto == null)
                    return NotFound();
                //Caso exista o produto retorna um Ok junto com as informações do produto
                return Ok(produto);

            }
            catch (Exception ex)
            {
                //Caso ocorra algum erro retorna um BadRequest com uma mensagem de erro tratada 
                return BadRequest(ex.Message);
            }
        }

        // POST Api/<ProdutosController>/5
        [HttpPost]
        public IActionResult Post(Produto produto)
        {
            try
            {
                //Adiciona um produto 
                _produtoRepository.Adicionar(produto);

                //Retorna um Ok junto com as informações do produto
                return Ok(produto);
            }
            catch (Exception ex)
            {
                //Caso ocorra algum erro retorna um BadRequest com uma mensagem de erro tratada
                return BadRequest(ex.Message);
            }
        }

        // PUT Api/<ProdutosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id,Produto produto)
        {
            try
            {
                var produtoTemp = _produtoRepository.BuscarPorId(id);
                if (produtoTemp == null)
                    return NotFound();
                
                //Edita um produto a partir do Id
                produto.Id = id;
                _produtoRepository.Editar(produto);

                if (produtoTemp == null)
                    return NotFound();

                //Retorna um Ok junto com as informações alteradas do produto
                return Ok(produto);
            }
            catch (Exception ex)
            {
                //Caso ocorra algum erro retorna um BadRequest com uma mensagem de erro tratada
                return BadRequest(ex.Message);
            }
        }

        // DELETE Api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //Remove um produto
                _produtoRepository.Remover(id);

                //Retorna um ok junto com as informações do produto
                return Ok(id);
            }
            catch (Exception ex)
            {
                //Caso ocorra algum erro retorna um BadRequest com uma mensagem de erro tratada
                return BadRequest(ex.Message);
            }
        }

    }
}
