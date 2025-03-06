namespace Vizvezetek.API.DTOs
{
    public class MunkalapDTO
    {
        public int Id { get; set; }
        public DateTime BeadasDatum { get; set; }
        public DateTime JavitasDatum { get; set; }
        public string Helyszin { get; set; }
        public string Szerelo { get; set; }
        public int Munkaora { get; set; }
        public int Anyagar { get; set; }

        public MunkalapDTO(int id, DateTime beadasDatum, DateTime javitasDatum, string telepules, string utca, string szerelo, int munkaora, int anyagar)
        {
            Id = id;
            BeadasDatum = beadasDatum;
            JavitasDatum = javitasDatum;
            Helyszin = $"{telepules}, {utca}";
            Szerelo = szerelo;
            Munkaora = munkaora;
            Anyagar = anyagar;
        }
    }
}
