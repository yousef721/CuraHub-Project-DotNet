using System;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class CategoriesAndManufactoriesVM
{
    public List<PharmacyCategory> PharmacyCategories { get; set; } = new List<PharmacyCategory>();
    public List<MedicineManufactory> MedicineManufactories { get; set; } = new List<MedicineManufactory>();

    public MedicineVM MedicinesVM { get; set; } = new MedicineVM();
}
