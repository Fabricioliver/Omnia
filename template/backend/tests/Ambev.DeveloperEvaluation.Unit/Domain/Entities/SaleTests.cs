using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void Aplica10PorCento_Entre4e9Itens()
        {
            var sale = CriarVendaComQuantidade(5, 100);
            sale.ApplyAutomaticDisccount();

            foreach (var item in sale.Items)
            {
                var esperado = item.UnitPrice * item.Quantity * 0.10m;
                Assert.Equal(esperado, item.Discount);
            }
        }

        [Fact]
        public void Aplica20PorCento_Entre10e20Itens()
        {
            var sale = CriarVendaComQuantidade(10, 50);
            sale.ApplyAutomaticDisccount();

            foreach (var item in sale.Items)
            {
                var esperado = item.UnitPrice * item.Quantity * 0.20m;
                Assert.Equal(esperado, item.Discount);
            }
        }

        [Fact]
        public void NaoAplicaDesconto_Ate3Itens()
        {
            var sale = CriarVendaComQuantidade(3, 200);
            sale.ApplyAutomaticDisccount();

            foreach (var item in sale.Items)
            {
                Assert.Equal(0, item.Discount);
            }
        }

        [Fact]
        public void LancaErro_SeMaisDe20Itens()
        {
            var sale = CriarVendaComQuantidade(21, 10);
            Assert.Throws<InvalidOperationException>(() => sale.ApplyAutomaticDisccount());
        }

        [Fact]
        public void CancelaVenda_ComSucesso()
        {
            var sale = CriarVendaComQuantidade(2, 50);
            sale.Cancel();
            Assert.True(sale.IsCancelled);
        }

        [Fact]
        public void LancaErro_AoCancelarJaCancelada()
        {
            var sale = CriarVendaComQuantidade(2, 50);
            sale.Cancel();
            Assert.Throws<InvalidOperationException>(() => sale.Cancel());
        }

        private Sale CriarVendaComQuantidade(int quantidade, decimal preco)
        {
            return new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = "TEST",
                Date = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = quantidade,
                        UnitPrice = preco
                    }
                }
            };
        }
    }
}
