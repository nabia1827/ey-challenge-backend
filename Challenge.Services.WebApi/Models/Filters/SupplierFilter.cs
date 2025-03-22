namespace Challenge.Services.WebApi.Models.Filters
{
    public class SupplierFilter
    {
        public int Pagina { get; set; } = 1;
        private int CantidadRegistros = 10;
        private readonly int CantidadRegistrosMax = 100;
        public int CantidadRegistrosPorPagina
        {
            get { return CantidadRegistros; }
            set
            {
                CantidadRegistros = (value > CantidadRegistrosMax) ? CantidadRegistrosMax : value;
            }
        }

        public string LegalName { get; set; } = string.Empty;
        public string tradeName { get; set; } = string.Empty;
        public string taxIdentNumber { get; set; } = string.Empty;
        public int countryId { get; set; } = 0;

        public string initDate { get; set; } = string.Empty;
        public string endDate { get; set; } = string.Empty;

        
    }
}
