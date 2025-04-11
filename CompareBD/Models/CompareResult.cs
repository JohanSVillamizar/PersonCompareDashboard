namespace PersonCompareDashboard.Models
{
    public class CompareResult
    {
        public int CountSql { get; set; }
        public int CountPostgre { get; set; }
        public List<Persona> MissingInPostgre { get; set; }
        public List<Persona> MissingInSql { get; set; }
        public List<PersonaParalelo> ComparacionesParalelas { get; set; }
    }
}
