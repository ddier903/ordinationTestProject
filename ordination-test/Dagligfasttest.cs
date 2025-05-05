namespace ordination_test;

using shared.Model;

[TestClass]
public class DagligFastTest
{
    private Laegemiddel lm;
    private DateTime start;
    private DateTime slut;

    [TestInitialize]
    public void Setup()
    {
        lm = new Laegemiddel("Panodil", 1, 1.5, 2, "mg");
        start = new DateTime(2024, 3, 1);
        slut = new DateTime(2024, 3, 5);
    }

    [TestMethod]
    public void TC1_DoegnDosis_AlleDoserErNul_ReturnsZero()
    {
        var ord = new DagligFast(start, slut, lm, 0.0, 0.0, 0.0, 0.0);
        var resultat = ord.doegnDosis();
        Assert.AreEqual(0.0, resultat);
    }

    [TestMethod]
    public void TC2_DoegnDosis_AlleDoserPositive_Returns4()
    {
        var ord = new DagligFast(start, slut, lm, 1.0, 1.0, 1.0, 1.0);
        var resultat = ord.doegnDosis();
        Assert.AreEqual(4.0, resultat);
    }

    [TestMethod]
    public void TC3_DoegnDosis_NogleDoserPositive_Returns3()
    {
        var ord = new DagligFast(start, slut, lm, 2.0, 0.0, 1.0, 0.0);
        var resultat = ord.doegnDosis();
        Assert.AreEqual(3.0, resultat);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TC4_DoegnDosis_NegativDosis_KasterException()
    {
        var ord = new DagligFast(start, slut, lm, -1.0, 1.0, 1.0, 1.0);
        var resultat = ord.doegnDosis(); 
    }
}