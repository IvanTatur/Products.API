using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.API
{
    [Table("Product", Schema = "Production")]
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public bool? MakeFlag { get; set; }
        public bool? FinishedGoodsFlag { get; set; }
        public string Color { get; set; }
        public short SafetyStockLevel { get; set; } = 5;
        public short ReorderPoint { get; set; } = 1;
        public decimal StandardCost { get; set; } = 1;
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; } = 5;
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public int? ProductSubcategoryId { get; set; } = 1;
        public int? ProductModelId { get; set; } = 1;
        public DateTime SellStartDate { get; set; }
        public DateTime? SellEndDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        //public virtual ProductModel ProductModel { get; set; }
        //public virtual ProductSubcategory ProductSubcategory { get; set; }
        //public virtual UnitMeasure SizeUnitMeasureCodeNavigation { get; set; }
        //public virtual UnitMeasure WeightUnitMeasureCodeNavigation { get; set; }
        //public virtual ICollection<BillOfMaterials> BillOfMaterialsComponent { get; set; }
        //public virtual ICollection<BillOfMaterials> BillOfMaterialsProductAssembly { get; set; }
        //public virtual ICollection<ProductCostHistory> ProductCostHistory { get; set; }
        //public virtual ICollection<ProductInventory> ProductInventory { get; set; }
        //public virtual ICollection<ProductListPriceHistory> ProductListPriceHistory { get; set; }
        //public virtual ICollection<ProductProductPhoto> ProductProductPhoto { get; set; }
        //public virtual ICollection<ProductReview> ProductReview { get; set; }
        //public virtual ICollection<ProductVendor> ProductVendor { get; set; }
        //public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        //public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
        //public virtual ICollection<SpecialOfferProduct> SpecialOfferProduct { get; set; }
        //public virtual ICollection<TransactionHistory> TransactionHistory { get; set; }
        //public virtual ICollection<WorkOrder> WorkOrder { get; set; }
    }
}