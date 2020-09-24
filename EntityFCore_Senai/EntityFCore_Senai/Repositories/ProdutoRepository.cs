using EntityFCore_Senai.Contexts;
using EntityFCore_Senai.Domains;
using EntityFCore_Senai.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFCore_Senai.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PedidoContext _ctx;

        public ProdutoRepository()
        {
            _ctx = new PedidoContext(); 
        }
        #region Leitura
        /// <summary>
        /// Lista todos os produtos cadastrados
        /// </summary>
        /// <returns>Retorna os produtos cadastrados em lista</returns>
        public List<Produto> Listar()
        {
            try
            {
                return _ctx.Produtos.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Busca um produto pelo seu Id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Lista de produtos</returns>
        public Produto BuscarPorId(Guid id)
        {
            try
            {
                //Uma das formas de buscar o produto pelo id utilizando expressão lambda
                // return _ctx.Produtos.FirstOrDefault(c => c.Id == id);
                
                //Forma "simples" de buscar um produto pelo id
                return _ctx.Produtos.Find(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Busca um produto pelo nome
        /// </summary>
        /// <param name="nome">Nome do produto</param>
        /// <returns>Retorna um produto</returns>
        public List<Produto> BuscarPorNome(string nome)
        {
            try
            {
                //return _ctx.Produtos.Where(c => c.Nome == nome).ToList();

                return _ctx.Produtos.Where(c => c.Nome.Contains(nome)).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Gravação
        /// <summary>
        /// Edita/altera o produto(objeto)
        /// </summary>
        /// <param name="produto"></param>
        public void Editar(Produto produto)
        {
            try
            {
                //Busca o produto pelo seu Id
                Produto produtoTemp = BuscarPorId(produto.Id);

                //Verifica se o produto exite, caso não exista gera uma exception(mensagem tratada)
                if (produtoTemp == null)
                    throw new Exception("Produto não encontrado");

                //Altera o produto no contexto e após alterar salva o que foi feito
                produtoTemp.Nome = produto.Nome;
                produtoTemp.Preco = produto.Preco;
                _ctx.Produtos.Update(produtoTemp);
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo produto(objeto)
        /// </summary>
        /// <param name="produto">Objeto do tipo Produto</param>
        public void Adicionar(Produto produto)
        {
            try
            {
            #region MÉTODO SIMPLES
            //Adiciona objeto do tipo produto ao dbset do contexto
            //Método mais simples
            _ctx.Produtos.Add(produto);

            //Salva as alterações feitas no contexto
            _ctx.SaveChanges();

            //Método mais simples para deletar o "Produto"
            /* _ctx.Produtos.Remove(produto); */

            //Método mais simples de modificar o "Produto"
            /* _ctx.Produtos.Update(produto); */
            #endregion

            }
            catch (Exception ex)
            {
                //Trata a mensagem de erro
                throw new Exception(ex.Message);
            }

            #region OUTRO MÉTODO "SIMPLES"

            //Outro método de adicionar um "Produto"
            /* _ctx.Set<Produto>().Add(produto); */

            //Utilizando o mesmo método para deletar o "Produto"
            /* _ctx.Set<Produto>().Remove(produto); */

            //Utilizando o mesmo método para modificar o "Produto"
            /* _ctx.Set<Produto>().Update(produto); */
            #endregion

            #region MAIS UM MÉTODO DE ADICIONAR/REMOVER/MODIFICAR, PORÉM MAIS COMPLEXO

            //Outro método de adicionar um "Produto"
            /* _ctx.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Added; */

            //Utilizando o mesmo método porém deletando um "Produto"
            /* _ctx.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Deleted; */

            //Utilizando o mesmo método porém modificando um "Produto"
            /* _ctx.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified; */
            #endregion
        }

        /// <summary>
        /// Remove o produto(objeto)
        /// </summary>
        /// <param name="id">Id do produto(objeto)</param>
        public void Remover(Guid id)
        {
            try
            {
                //Buscar pelo Id
                Produto produtoTemp = BuscarPorId(id);

                //Verifica se o produto existe, caso não exista gera uma exception(mensagem tratada)
                if (produtoTemp == null)
                    throw new Exception("Produto não encontrado");

                //Remove o produto no contexto dbSet, e salva no contexto 
                _ctx.Produtos.Remove(produtoTemp);
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
