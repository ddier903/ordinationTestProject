namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]
    public void TC1_OpretDagligSkaev()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        Dosis[] doser = new Dosis[]
        {
            new Dosis(DateTime.Now, 2),
            new Dosis(DateTime.Now.AddHours(1), 3)
        };

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void TC2_OpretDagligSkaev_UgyldigStartDato()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(2, service.GetDagligSkæve().Count());

        Dosis[] doser = new Dosis[]
        {
            new Dosis(DateTime.Now, 2),
            new Dosis(DateTime.Now.AddHours(1), 3)
        };

        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
            doser, DateTime.Now.AddDays(10), DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());

    }

    [TestMethod]
    public void TC3_OpretDagligSkaev_IngenPatient()
    {
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(2, service.GetDagligSkæve().Count());

        Dosis[] doser = new Dosis[]
        {
            new Dosis(DateTime.Now, 2),
            new Dosis(DateTime.Now.AddHours(1), 3)
        };

        service.OpretDagligSkaev(0, lm.LaegemiddelId,
            doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void TC4_OpretDagligSkaev_IngenLaegemiddel()
    {
        Patient patient = service.GetPatienter().First();

        Assert.AreEqual(2, service.GetDagligSkæve().Count());

        Dosis[] doser = new Dosis[]
        {
            new Dosis(DateTime.Now, 2),
            new Dosis(DateTime.Now.AddHours(1), 3)
        };

        service.OpretDagligSkaev(patient.PatientId, 0,
            doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void TC5_OpretDagligSkaev_IngenDoser()
    {
        Patient patient = service.GetPatienter().First();
    Laegemiddel lm = service.GetLaegemidler().First();

    Assert.AreEqual(2, service.GetDagligSkæve().Count());

        Dosis[] doser = new Dosis[0];

    service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId,
        doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

[TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.

        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.

        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
}