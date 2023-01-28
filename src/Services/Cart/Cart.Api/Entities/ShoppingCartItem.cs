namespace Cart.Api.Entities;

public record ShoppingCartItem(int Quantity, string Color, decimal Price, string ProductId, string ProductName);
