namespace Blockbuster.web.Models
{
    public class IdentificacionVM
    {
        public int IdentificacionId { get; set; }
        public string? Email { get; set; }
        
        //la contra se obtiene como string del form html para convertir en hash
        public string? ContraHash { get; set; }
    }
}
