using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniERP {
    public class Supplier {
        public int Id { get; set; }
        public string Name { get; set; }

        public Supplier(int id, string name) {
            Id = id;
            Name = name;
        }
    }

    public class Product {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public Supplier Supplier { get; set; }

        public Product(int id, string name, int stockQuantity, Supplier supplier) {
            Id = id;
            Name = name;
            StockQuantity = stockQuantity;
            Supplier = supplier;
        }
    }

    public class InventoryService {
        private readonly List<Product> _products;

        public InventoryService(List<Product> products) {
            
        }

        public void AddStock(int productId, int quantity) {
            Product product = _products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            throw new Exception("Product not found.");

            product.StockQuantity += quantity;

            Console.WriteLine(
            $"{quantity} db hozzáadva: {product.Name}");
        }

        public void RemoveStock(int productId, int quantity) {
            Product product = _products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            throw new Exception("Product not found.");

            if (product.StockQuantity < quantity)
            throw new InvalidOperationException(
            "Not enough stock.");

            product.StockQuantity -= quantity;

            Console.WriteLine(
            $"{quantity} db levonva: {product.Name}");
        }

        public List<Product> GetLowStockProducts() {
            return _products
            .Where(p => p.StockQuantity < 10)
            .ToList();
        }

        public Product GetHighestStockProduct() {
            return _products
            .OrderByDescending(p => p.StockQuantity)
            .FirstOrDefault();
        }

        public int GetTotalStock() {
            
        }

        public void PrintInventory() {
        Console.WriteLine("\n=== INVENTORY ===");

            foreach (var product in _products) {
                Console.WriteLine(
                $"{product.Name} | Stock: {product.StockQuantity} | Supplier: {product.Supplier.Name}");
            }
        }

        public void PrintLowStockAlerts() {
            Console.WriteLine("\n=== LOW STOCK ALERTS ===");

            var lowStock = _products
            .Where(p => p.StockQuantity < 5);

            foreach (var product in lowStock) {
                Console.WriteLine(
                $"PRODUCT: {product.Name} | LOW STOCK: {product.StockQuantity} pcs");
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            Supplier logitech = new Supplier(1, "Logitech");
            Supplier dell = new Supplier(2, "Dell");

            List<Product> products = new List<Product> {
                new Product(1, "Mouse", 25, logitech),
                new Product(2, "Keyboard", 8, logitech),
                new Product(3, "Monitor", 15, dell),
                new Product(4, "Laptop", 3, dell)
            };

            InventoryService inventoryService =
            new InventoryService(products);

            inventoryService.PrintInventory();

            Console.WriteLine();

            inventoryService.AddStock(4, 5);

            inventoryService.RemoveStock(2, 4);

            Console.WriteLine();

            inventoryService.PrintInventory();

            Console.WriteLine();

            Console.WriteLine("=== TOTAL STOCK ===");
            Console.WriteLine(
            inventoryService.GetTotalStock());

            Console.WriteLine();

            Console.WriteLine("=== HIGHEST STOCK PRODUCT ===");

            Product highest =
            inventoryService.GetHighestStockProduct();

            Console.WriteLine(
            $"{highest.Name} ({highest.StockQuantity})");

            Console.WriteLine();

            Console.WriteLine("=== LOW STOCK PRODUCTS (<10) ===");

            foreach (var product in inventoryService.GetLowStockProducts()) {
                Console.WriteLine(
                $"{product.Name} ({product.StockQuantity})");
            }

            inventoryService.PrintLowStockAlerts();

            try {
                inventoryService.RemoveStock(4, 100);
            }
            catch (Exception ex) {
                Console.WriteLine();
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Program finished.");
        }
    }
}