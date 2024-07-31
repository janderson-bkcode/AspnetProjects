namespace mapaAstral.Models
{
    public class Planeta
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Simbolo { get; set; }
        public string? Elemento { get; set; }
        public int Casa { get; set; }
    }

    public class Elemento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
