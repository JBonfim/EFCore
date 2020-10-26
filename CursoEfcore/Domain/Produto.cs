using CursoEfcore.ValueObjects;

namespace CursoEfcore.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodifoBarras { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public TipoProduto TipoProduto { get; set; }
        public bool Ativo { get; set; }
        

    }
}