using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.API.Dtos;
using ShoppingApp.API.Models;
using ShoppingApp.API.Repositories;

namespace ShoppingApp.API.Endpoints
{
    public static class ProductApi
    {
        public static void ConfigureProductApi(this RouteGroupBuilder routes)
        {
            routes.MapGet("/products", async (IProductRepository productsRepository) =>
            {
                var products = (await productsRepository.GetAll()).ToList();

                if (products == null || products.Count == 0)
                {
                    return Results.NotFound();
                }

                return Results.Ok(products);
            });

            routes.MapGet("/products/{id}", async (int id, IProductRepository productsRepository) =>
            {
                var product = await productsRepository.GetById(id);

                if (product.Id == 0)
                {
                    return Results.NotFound();
                }
                return Results.Ok(product);
            });

            routes.MapPost("/product", async ([FromBody] ProductDto productDto,
                IProductRepository productsRepository,
                IMapper mapper,
                IValidator<ProductDto> validator
                ) =>
            {
                // validate request dto according the validation rules
                var validationResult = await validator.ValidateAsync(productDto);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var product = mapper.Map<ProductDto, Product>(productDto);

                var newProductId = await productsRepository.Add(product);

                // new product wasn't added and an error has been occured.
                if (newProductId == 0)
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);

                return Results.Ok(newProductId);
            });

            routes.MapPut("/product", async ([FromBody] UpdateProductDto productDto,
                IProductRepository productsRepository,
                IMapper mapper, IValidator<UpdateProductDto> validator) =>
            {
                // validate request dto according the validation rules
                var validationResult = await validator.ValidateAsync(productDto);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var product = mapper.Map<UpdateProductDto, Product>(productDto);

                var isUpdated = await productsRepository.Update(product);

                // Target record wasn't updated due to an error
                if (!isUpdated)
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);

                return Results.Ok(isUpdated);
            });

            routes.MapDelete("/producs/{id}", async (int id, IProductRepository productsRepository) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest("Id can't be less or equals zero.");
                }

                var isDeleted = await productsRepository.Delete(id);

                // target product wasn't deleted due to an error
                if (!isDeleted)
                    return Results.StatusCode(StatusCodes.Status500InternalServerError);

                return Results.Ok(isDeleted);
            });
        }
    }
}
