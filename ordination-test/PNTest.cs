namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class PNTest
{
    private PN pnOrdination;

    [TestInitialize]
    public void Setup()
    {
        pnOrdination = new PN
        {
            startDen = new DateTime(2024, 3, 1),
            slutDen = new DateTime(2024, 3, 31),
            laegemiddel = new Laegemiddel("Panodil", 1, 1.5, 2, "mg"),
        };

    }

    [TestMethod]
    public void TC1_DatoFoerStartdato_ReturnsFalse()
    {
        var dato = new Dato { dato = new DateTime(2024, 2, 25) };
        Assert.IsFalse(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC2_DatoFoerStartdato_ReturnsFalse()
    {
        var dato = new Dato { dato = new DateTime(2024, 2, 28) };
        Assert.IsFalse(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC3_Startdato_ReturnsTrue()
    {
        var dato = new Dato { dato = new DateTime(2024, 3, 1) };
        Assert.IsTrue(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC4_MidtDato_ReturnsTrue()
    {
        var dato = new Dato { dato = new DateTime(2024, 3, 20) };
        Assert.IsTrue(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC5_SammeDatoToGange_ReturnsTrue()
    {
        var dato = new Dato { dato = new DateTime(2024, 3, 20) };
        Assert.IsTrue(pnOrdination.givDosis(dato));
        Assert.IsTrue(pnOrdination.givDosis(dato)); // Tilladt at få dosis flere gange samme dag
    }

    [TestMethod]
    public void TC6_Slutdato_ReturnsTrue()
    {
        var dato = new Dato { dato = new DateTime(2024, 3, 31) };
        Assert.IsTrue(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC7_EfterSlutdato_ReturnsFalse()
    {
        var dato = new Dato { dato = new DateTime(2024, 4, 1) };
        Assert.IsFalse(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC8_MangeDageEfter_ReturnsFalse()
    {
        var dato = new Dato { dato = new DateTime(2024, 4, 3) };
        Assert.IsFalse(pnOrdination.givDosis(dato));
    }

    [TestMethod]
    public void TC9_NullDato_ReturnsFalse()
    {
        Assert.IsFalse(pnOrdination.givDosis(null));
    }


    //Test af PN doegnDosis()
    
    [TestMethod]
    public void TC1_IngenDoser_ReturnsZero()
    {
        pnOrdination.antalEnheder = 5;
        Assert.AreEqual(0.0, pnOrdination.doegnDosis(), 0.001);
    }

    [TestMethod]
    public void TC2_EnDosis_ReturnsEnDosis()
    {
        pnOrdination.antalEnheder = 5;
        var dato = new Dato { dato = new DateTime(2024, 3, 1) };
        pnOrdination.givDosis(dato);
        Assert.AreEqual(5.0, pnOrdination.doegnDosis(), 0.001);
    }

    [TestMethod]
    public void TC3_ToDoserSammeDag_ReturnsEnDosis()
    {
        pnOrdination.antalEnheder = 6;
        var dato = new Dato { dato = new DateTime(2024, 3, 1) };
        pnOrdination.givDosis(dato);
        pnOrdination.givDosis(dato);
        Assert.AreEqual(12.0, pnOrdination.doegnDosis(), 0.001);
    }

    [TestMethod]
    public void TC4_DosiserFlereDage_ReturnsEnDosis()
    {
        pnOrdination.antalEnheder = 9;
        var dato1 = new Dato { dato = new DateTime(2024, 3, 1) };
        var dato2 = new Dato { dato = new DateTime(2024, 3, 2) };
        var dato3 = new Dato { dato = new DateTime(2024, 3, 3) };
        pnOrdination.givDosis(dato1);
        pnOrdination.givDosis(dato2);
        pnOrdination.givDosis(dato3);
        Assert.AreEqual(9.0, pnOrdination.doegnDosis(), 0.001);
    }

    [TestMethod]
    public void TC5_IngenEnheder_ReturnsZero()
    {
        pnOrdination.antalEnheder = 0;
        var dato1 = new Dato { dato = new DateTime(2024, 3, 1) };
        var dato2 = new Dato { dato = new DateTime(2024, 3, 2) };
        var dato3 = new Dato { dato = new DateTime(2024, 3, 3) };
        pnOrdination.givDosis(dato1);
        pnOrdination.givDosis(dato2);
        pnOrdination.givDosis(dato3);
        Assert.AreEqual(0.0, pnOrdination.doegnDosis(), 0.001);
    }

    [TestMethod]
    public void TC6_TreDoserSammeDag_ReturnerEnDosis()
    {
        pnOrdination.antalEnheder = 6;
        var dato = new Dato { dato = new DateTime(2024, 3, 1) };
        pnOrdination.givDosis(dato);
        pnOrdination.givDosis(dato);
        pnOrdination.givDosis(dato);
        Assert.AreEqual(18.0, pnOrdination.doegnDosis(), 0.001);
    }



}
