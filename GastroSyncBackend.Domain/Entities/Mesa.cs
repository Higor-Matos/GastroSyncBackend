namespace GastroSyncBackend.Domain.Entities
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public List<Produto> ListaProdutos { get; set; } = new List<Produto>();
    }
}
