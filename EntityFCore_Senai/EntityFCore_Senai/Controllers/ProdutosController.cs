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
        public List<Produto> Get()
        {
            return _produtoRepository.Listar();
        }
        
        // GET Api/<ProdutoController>/5
        [HttpGet("{id}")]
        public Produto Get(Guid id)
        {
            return _produtoRepository.BuscarPorId(id);
        }

        // POST Api/<ProdutosController>/5
        [HttpPost]
        public void Post(Produto produto)
        {
            _produtoRepository.Adicionar(produto);
        }

        // PUT Api/<ProdutosController>/5
        [HttpPut("{id}")]
        public void Put(Guid id,Produto produto)
        {
            produto.Id = id;
            _produtoRepository.Editar(produto);
        }

        // DELETE Api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _produtoRepository.Remover(id);
        }

    }
}
