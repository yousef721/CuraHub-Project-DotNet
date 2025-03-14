using System;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class MedicinesInOrderVM
{
    public List<MedicineOrder> MedicineOrder { get; set; } = new List<MedicineOrder>();
    public PharmacyOrder PharmacyOrder { get; set; } = new PharmacyOrder();
}
