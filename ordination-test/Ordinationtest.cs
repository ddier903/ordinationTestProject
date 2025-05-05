namespace ordination_test;

using shared.Model;

[TestClass]
public class OrdinationTest
{
    private Laegemiddel lm;

    [TestInitialize]
    public void Setup()
    {
        lm = new Laegemiddel("Panodil", 1, 1.5, 2, "mg");
    }

    [TestMethod]
    public void TC1_AntalDage_SammeDato_Returns1()
    {
        var ord = new DagligFast(new DateTime(2024, 3, 1), new DateTime(2024, 3, 1), lm, 1, 1, 1, 1);
        Assert.AreEqual(1, ord.antalDage());
    }

    [TestMethod]
    public void TC2_AntalDage_5DagesInterval_Returns5()
    {
        var ord = new DagligFast(new DateTime(2024, 3, 1), new DateTime(2024, 3, 5), lm, 1, 1, 1, 1);
        Assert.AreEqual(5, ord.antalDage());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TC3_AntalDage_SlutFørStart_KasterException()
    {
        var ord = new DagligFast(new DateTime(2024, 3, 5), new DateTime(2024, 3, 1), lm, 1, 1, 1, 1);
        ord.antalDage(); // Forventes at kaste exception
    }

    [TestMethod]
    public void TC4_AntalDage_LangPeriode_Returns61()
    {
        var ord = new DagligFast(new DateTime(2024, 1, 1), new DateTime(2024, 3, 1), lm, 1, 1, 1, 1);
        Assert.AreEqual(61, ord.antalDage());
    }
}