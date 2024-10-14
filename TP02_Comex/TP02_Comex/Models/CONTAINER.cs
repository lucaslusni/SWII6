namespace TP02_Comex.Models
{
    public class CONTAINER
    {
        public int ID_Container { get; set; }
        public string Numero { get; set; }
        public CONTAINER_TYPE CONTAINER_TYPE { get; set; }
        public CONTAINER_SIZE CONTAINER_SIZE { get; set; }
        public Bl Bl { get; set; }

    }
}
