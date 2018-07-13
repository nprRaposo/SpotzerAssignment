using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotzerAssignment.Data;
using SpotzerAssignment.Model;
using SpotzerAssignment.Model.DTO;
using SpotzerAssignment.Model.Exception;
using SpotzerAssignment.Service;
using System.Collections.Generic;

namespace SpotzerAssignment.Test
{
    [TestClass]
    public class OrderServiceTest
    {
        private Mock<IRepository<Order>> _orderRepository;
        private Mock<IRepository<PaidSearchProductLine>> _paidSearchProductRepository;
        private Mock<IRepository<WebSiteProductLine>> _webSiteProductRepository;
        private OrderService _orderService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            this._orderRepository = new Mock<IRepository<Order>>();
            this._paidSearchProductRepository = new Mock<IRepository<PaidSearchProductLine>>();
            this._webSiteProductRepository = new Mock<IRepository<WebSiteProductLine>>();
        }

        [TestMethod]
        public void PartnerA_NotSupport_PaidSearchProduct_Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var orderDTO = new OrderDTO
            {
                Partner = "partnerA",
                LineItems = new List<LineDTO>
                {
                    new LineDTO {AdWordCampaign = new PaidSearchProductLine() }
                }
            };

            Assert.ThrowsException<ProductNotSupportedException>(() => orderService.Save(orderDTO));
        }

        [TestMethod]
        public void PartnerD_NotSupport_WebsiteProduct_Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var orderDTO = new OrderDTO
            {
                Partner = "partnerd",
                LineItems = new List<LineDTO>
                {
                    new LineDTO {WebsiteDetails = new WebSiteProductLine() }
                }
            };

            Assert.ThrowsException<ProductNotSupportedException>(() => orderService.Save(orderDTO));
        }

        [TestMethod]
        public void PartnerB_And_PartnerC_Support_BothProducts_Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var lineItemsBothProducts = new List<LineDTO>
                {
                    new LineDTO {WebsiteDetails = new WebSiteProductLine() },
                    new LineDTO {AdWordCampaign = new PaidSearchProductLine() },
                };

            var orderDTOPartnerC = new OrderDTO
            {
                Partner = "partnerc",
                LineItems = lineItemsBothProducts
            };

            var orderDTOPartnerB = new OrderDTO
            {
                Partner = "partnerb",
                LineItems = lineItemsBothProducts
            };

            orderService.Save(orderDTOPartnerB);
            orderService.Save(orderDTOPartnerC);

            _orderRepository.Verify(mock => mock.Save(It.IsAny<Order>()), Times.Exactly(2));
        }
    }
}
