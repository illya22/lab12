List<Products> products = new List<Products>
 {
     new() {Id = Guid.NewGuid().ToString(),Name="Toyo Tires",Price=700,Type="колесо",Brand="Toyo",Count=60,Size="500x500 мм"},
     new() {Id = Guid.NewGuid().ToString(),Name="Renault Traf",Price=20000,Type="двигун",Brand="Opel",Count=10,Size="4000x6000 мм"},
     new() {Id = Guid.NewGuid().ToString(),Name="Fiat Doblo",Price=4000,Type="механічна коробка передач",Brand="Fiat",Count=200,Size="4000x2000 мм"}
 };

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/products", () => products);

app.MapGet("/api/products/{id}", (string id) =>
{
    Products? product = products.FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound(new { message = "Product not found" });
    }
    return Results.Json(product);
});

app.MapDelete("/api/products/{id}", (string id) =>
{
    Products? product = products.FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        return Results.NotFound(new { message = "Product not found" });
    }
    products.Remove(product);
    return Results.Json(product);
});

app.MapPost("/api/products", (Products product) =>
{
    product.Id = Guid.NewGuid().ToString();
    products.Add(product);
    return product;
});

app.MapPut("/api/products", (Products data) =>
{
    var product = products.FirstOrDefault(p => p.Id == data.Id);

    if (product == null)
    {
        return Results.NotFound(new { message = "Product not found" });
    }
    product.Name = data.Name;
    product.Price = data.Price;
    product.Type = data.Type;
    product.Brand = data.Brand;
    product.Count = data.Count;
    product.Size = data.Size;

    return Results.Json(product);
});

app.Run();


public class Products{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public string Type { get; set; } = "";
    public string Brand { get; set; } = "";
    public int Count { get; set; }
    public string Size { get; set; } = "";
}