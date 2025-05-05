namespace ordination_test;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using Data;
using shared.Model;

[TestClass]
public class DagligSkaevTest
{
    private DataService service;

    [TestInitialize]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData(); 
    }

    [TestMethod]
    public void TC1_GyldigOrdination_ReturnsNotNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 3, 5), 1.0) };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void TC2_SlutdatoFoerStartdato_ReturnsNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 3, 7), 1.0) };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 10), new DateTime(2024, 3, 5));
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TC3_PatientFindesIkke_ReturnsNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 3, 10), 1.0) };
        DagligSkæv result = service.OpretDagligSkaev(999, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TC4_LaegemiddelFindesIkke_ReturnsNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 3, 15), 1.0) };
        DagligSkæv result = service.OpretDagligSkaev(1, 999, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TC5_DoserMangler_ReturnsNull()
    {
        var doser = new Dosis[] { };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 10));
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TC6_EnDagsOrdination_ReturnsNotNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 3, 1), 1.0) };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 1));
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void TC7_FlereGyldigeDoser_ReturnsNotNull()
    {
        var doser = new Dosis[]
        {
            new Dosis(new DateTime(2024, 3, 5), 1.0),
            new Dosis(new DateTime(2024, 3, 15), 1.0),
            new Dosis(new DateTime(2024, 3, 25), 1.0)
        };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 30));
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void TC8_DoseUdenforPeriode_ReturnsNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 4, 5), 1.0) };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 31));
        Assert.IsNull(result);
    }

    [TestMethod]
    public void TC9_NegativDosisMaengde_ReturnsNull()
    {
        var doser = new Dosis[] { new Dosis(new DateTime(2024, 3, 20), -5.0) };
        DagligSkæv result = service.OpretDagligSkaev(1, 1, doser, new DateTime(2024, 3, 1), new DateTime(2024, 3, 28));
        Assert.IsNull(result);
    }
}
