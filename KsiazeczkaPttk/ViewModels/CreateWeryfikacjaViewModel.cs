namespace KsiazeczkaPttk.API.ViewModels
{
    public class CreateWeryfikacjaViewModel
    {
        public int Wycieczka { get; set; }
        public string Przodownik { get; set; }
        public bool Zaakceptiowana { get; set; }
        public string PowodOdrzucenia { get; set; }
    }
}
