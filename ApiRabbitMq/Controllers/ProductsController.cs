using ApiRabbitMq.Models;
using ApiRabbitMq.RabbitMQ;
using ApiRabbitMq.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiRabbitMq.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService, IRabbitMQProducer rabitMQProducer) : ControllerBase
{
    private readonly IProductService productService = productService;
    private readonly IRabbitMQProducer rabitMQProducer = rabitMQProducer;

    [HttpGet("productlist")]
    public IEnumerable<Product> ProductList()
    {
        var productList = productService.GetProductList();
        return productList;
    }

    [HttpGet("getproductbyid")]
    public Product GetProductById(int Id)
    {
        return productService.GetProductById(Id);
    }

    [HttpPost("addproduct")]
    public Product AddProduct(Product product)
    {
        var productData = productService.AddProduct(product);
        rabitMQProducer.SendProductMessage(productData);
        return productData;
    }

    [HttpPut("updateproduct")]
    public Product UpdateProduct(Product product) 
    {
        return productService.UpdateProduct(product);
    }

    [HttpDelete("deleteproduct")]
    public bool DeleteProduct(int Id) 
    {
        return productService.DeleteProduct(Id);
    }

}


