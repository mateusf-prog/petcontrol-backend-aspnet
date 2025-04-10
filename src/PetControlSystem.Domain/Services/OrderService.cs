﻿using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository repository,
                    INotificator notificator,
                    IProductRepository productRepository,
                    ICustomerRepository customerRepository) : base(notificator)
        {
            _repository = repository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task Add(Order input)
        {
            if (!ExecuteValidation(new OrderValidation(), input)) return;

            if (await _repository.GetById(input.Id) != null)
            {
                Notify("There is already an order with this ID");
                return;
            }

            if (await _customerRepository.GetById(input.CustomerId) is null)
            {
                Notify($"Customer not found - ID {input.CustomerId}");
                return;
            }

            foreach (var orderProduct in input.OrderProducts)
            {
                var product = await _productRepository.GetById(orderProduct.ProductId);

                if (product is null)
                {
                    Notify($"Product not found - ID {orderProduct.ProductId}");
                    return;
                }

                if (product.Stock < orderProduct.Quantity)
                {
                    Notify($"Insufficient stock of product - {orderProduct.ProductId}");
                    return;
                }

                product.Update(product.Name, product.Price, product.Stock - orderProduct.Quantity, product.Description);
                await _productRepository.Update(product);
                await _repository.Add(input);
            }
        }

        public async Task Update(Guid id, Order input)
        {
            if (!ExecuteValidation(new OrderValidation(), input)) return;

            var result = await _repository.GetById(id);

            if (result is null)
            {
                Notify("Order not found");
                return;
            }

            foreach(var product in input.OrderProducts)
            {
                var productEntity = await _productRepository.GetById(product.ProductId);
                if (productEntity is null)
                {
                    Notify($"Product not found - ID {product.ProductId}");
                    return;
                }
                if (productEntity.Stock < product.Quantity)
                {
                    Notify($"Insufficient stock of product - {product.ProductId}");
                    return;
                }
            }

            result.Update(input.OrderProducts);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            var result = await _repository.GetByIdWithProducts(id);
            if (result is null)
            {
                Notify("Order not found");
                return;
            }

            foreach (var orderProduct in result.OrderProducts)
            {
                var product = await _productRepository.GetById(orderProduct.ProductId);
                product.Update(product.Name, product.Price, product.Stock + orderProduct.Quantity, product.Description);
                await _productRepository.Update(product);
            }

            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
