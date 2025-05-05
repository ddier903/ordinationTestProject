namespace shared.Model;

public class PN : Ordination
{
    public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();
   

    public PN(DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen)
    {
        this.antalEnheder = antalEnheder;
    }

    public PN() : base(null!, new DateTime(), new DateTime())
    {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen)
    {
        bool isValid = givesDen.dato >= startDen && givesDen.dato <= slutDen;
        if (isValid)
        {
            dates.Add(givesDen);
        }

        return false;
    }

    public override double doegnDosis()
    {

        if (dates.Count < 1)
            return 0;

        var foersteDato = dates.Min(d => d.dato);
        var sidsteDato = dates.Max(d => d.dato);
        var dagInterval = (sidsteDato - foersteDato).Days + 1;

        return dates.Count() * antalEnheder / dagInterval;

    }


    public override double samletDosis()
    {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet()
    {
        return dates.Count();
    }

    public override String getType()
    {
        return "PN";
    }
}
