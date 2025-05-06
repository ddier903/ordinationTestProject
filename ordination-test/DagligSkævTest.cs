namespace ordination_test;

using shared.Model;

[TestClass]
public class DagligSkaevTest
{
    private Laegemiddel lm;
    private DateTime start;
    private DateTime slut;

    [TestInitialize]
    public void Setup()
    {
        lm = new Laegemiddel("Fucidin", 1.0, 1.5, 2.0, "mg");
        start = new DateTime(2024, 3, 1);
        slut = new DateTime(2024, 3, 3); // 3 dage
    }

    [TestMethod]
    public void DoegnDosis_NoDoses_ReturnsZero()
    {
        var ord = new DagligSkæv(start, slut, lm, new Dosis[0]);
        Assert.AreEqual(0.0, ord.doegnDosis());
    }

    [TestMethod]
    public void DoegnDosis_OneDose_ReturnsExactValue()
    {
        var doser = new Dosis[]
        {
            new Dosis(new DateTime(2024, 3, 1, 8, 0, 0), 2.0)
        };
        var ord = new DagligSkæv(start, slut, lm, doser);
        Assert.AreEqual(2.0, ord.doegnDosis());
    }

    [TestMethod]
    public void DoegnDosis_MultipleDoses_ReturnsSum()
    {
        var doser = new Dosis[]
        {
            new Dosis(new DateTime(2024, 3, 1, 8, 0, 0), 1.0),
            new Dosis(new DateTime(2024, 3, 1, 12, 0, 0), 1.5)
        };
        var ord = new DagligSkæv(start, slut, lm, doser);
        Assert.AreEqual(2.5, ord.doegnDosis());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void OpretDosis_NegativAntal_KasterException()
    {
        var ord = new DagligSkæv(start, slut, lm);
        ord.opretDosis(new DateTime(2024, 3, 1, 10, 0, 0), -1.0);
    }

    [TestMethod]
    public void SamletDosis_CorrectOverMultipleDays()
    {
        var doser = new Dosis[]
        {
            new Dosis(new DateTime(2024, 3, 1, 10, 0, 0), 2.0)
        };
        var ord = new DagligSkæv(start, slut, lm, doser); // 3 dage
        Assert.AreEqual(6.0, ord.samletDosis()); // 2 x 3 dage
    }
}