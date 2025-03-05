namespace Vizvezetek.API.DTO
{
    public class MunkalapKeresesDTO
    {
        public int HelyszinId { get; set; }
        public int SzereloId { get; set }
        public MunkalapKeresesDTO(int helyszinId, int szereloId)
        {
            HelyszinId = helyszinId;
            SzereloId = szereloId;
        }
    }
}
