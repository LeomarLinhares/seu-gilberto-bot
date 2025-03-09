namespace SeuGilbertoBot.DTOs
{
    public class CartolaStatusResponseDTO
    {
        public int RodadaAtual { get; set; }
        public int StatusMercado { get; set; }
        public int EsquemaDefaultId { get; set; }
        public int CartoletaInicial { get; set; }
        public int MaxLigasFree { get; set; }
        public int MaxLigasPro { get; set; }
        public int MaxLigasMatamataFree { get; set; }
        public int MaxCriarLigasMatamataFree { get; set; }
        public int MaxLigasMatamataPro { get; set; }
        public int MaxLigasPatrocinadasFree { get; set; }
        public int MaxLigasPatrocinadasProNum { get; set; }
        public int MaxAtletasFavoritosFree { get; set; }
        public int MaxAtletasFavoritosPro { get; set; }
        public bool GameOver { get; set; }
        public int Temporada { get; set; }
        public bool Reativar { get; set; }
        public bool ExibeSorteioPro { get; set; }
        public Fechamento Fechamento { get; set; }
        public LimitesCompeticao LimitesCompeticao { get; set; }
        public int TimesEscalados { get; set; }
        public bool MercadoPosRodada { get; set; }
        public bool NovoMesRanking { get; set; }
        public bool DegustacaoGatomestre { get; set; }
        public string NomeRodada { get; set; }
        public LimitesCompeticoes LimitesCompeticoes { get; set; }
    }

    public class Fechamento
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public int Hora { get; set; }
        public int Minuto { get; set; }
        public long Timestamp { get; set; }
    }

    public class LimitesCompeticao
    {
        public int TotalConfrontoPro { get; set; }
        public int TotalConfrontoFree { get; set; }
        public int CriacaoConfrontoPro { get; set; }
        public int CriacaoConfrontoFree { get; set; }
    }

    public class LimitesCompeticoes
    {
        public PontosCorridos PontosCorridos { get; set; }
    }

    public class PontosCorridos
    {
        public Free Free { get; set; }
        public Pro Pro { get; set; }
    }

    public class Free
    {
        public int Criacao { get; set; }
        public int Participacao { get; set; }
    }

    public class Pro
    {
        public int Criacao { get; set; }
        public int Participacao { get; set; }
    }
}
