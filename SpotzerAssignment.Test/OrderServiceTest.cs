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

        [TestMethod]
        public void Order_With_BothProducts_Calls_To_Both_Repositories_Test()
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

            orderService.Save(orderDTOPartnerC);

            _paidSearchProductRepository.Verify(mock => mock.Save(It.IsAny<PaidSearchProductLine>()), Times.Once);
            _webSiteProductRepository.Verify(mock => mock.Save(It.IsAny<WebSiteProductLine>()), Times.Once);
        }

        [TestMethod]
        public void OrderService_FromDTO_Set_The_SpecialProperties_For_PartnerA_And_Not_PartnerC_Properties_Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var orderDTOPartnerA = this.GetDummyOrder("partnera");
            var order = orderService.FromDto(orderDTOPartnerA);

            Assert.AreEqual("ContactFirstNameDummy", order.ContactFirstName);
            Assert.AreEqual("ContactLastNameDummy", order.ContactLastName);
            Assert.AreEqual("ContactTitleDummy", order.ContactTitle);
            Assert.AreEqual("ContactPhoneDummy", order.ContactPhone);
            Assert.AreEqual("ContactMobileDummy", order.ContactMobile);
            Assert.AreEqual("ContactEmailDummy", order.ContactEmail);
            Assert.IsNull(order.ExposureID);
            Assert.IsNull(order.UDAC);
            Assert.IsNull(order.RelatedOrder);
        }

        [TestMethod]
        public void OrderService_FromDTO_Set_The_SpecialProperties_For_PartnerC_And_Not_PartnerA_Properties_Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var orderDtoPartnerC = this.GetDummyOrder("partnerc");
            var order = orderService.FromDto(orderDtoPartnerC);

            Assert.IsNull(order.ContactFirstName);
            Assert.IsNull(order.ContactLastName);
            Assert.IsNull(order.ContactTitle);
            Assert.IsNull(order.ContactPhone);
            Assert.IsNull(order.ContactMobile);
            Assert.IsNull(order.ContactEmail);
            Assert.AreEqual("ExposureIdDummy", order.ExposureID);
            Assert.AreEqual("UDACDummy", order.UDAC);
            Assert.AreEqual("RelatedOrderDummy", order.RelatedOrder);
        }

        [TestMethod]
        public void OrderService_FromDTO_NotSet_The_SpecialProperties_For_PartnerB__Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var orderDtoPartnerB = this.GetDummyOrder("partnerb");
            var order = orderService.FromDto(orderDtoPartnerB);

            Assert.IsNull(order.ContactFirstName);
            Assert.IsNull(order.ContactLastName);
            Assert.IsNull(order.ContactTitle);
            Assert.IsNull(order.ContactPhone);
            Assert.IsNull(order.ContactMobile);
            Assert.IsNull(order.ContactEmail);
            Assert.IsNull(order.ExposureID);
            Assert.IsNull(order.UDAC);
            Assert.IsNull(order.RelatedOrder);
        }

        [TestMethod]
        public void OrderService_FromDTO_NotSet_The_SpecialProperties_For_PartnerD__Test()
        {
            var orderService = new OrderService(this._orderRepository.Object, this._paidSearchProductRepository.Object, this._webSiteProductRepository.Object);
            var orderDTOPartnerD = this.GetDummyOrder("partnerd");
            var order = orderService.FromDto(orderDTOPartnerD);

            Assert.IsNull(order.ContactFirstName);
            Assert.IsNull(order.ContactLastName);
            Assert.IsNull(order.ContactTitle);
            Assert.IsNull(order.ContactPhone);
            Assert.IsNull(order.ContactMobile);
            Assert.IsNull(order.ContactEmail);
            Assert.IsNull(order.ExposureID);
            Assert.IsNull(order.UDAC);
            Assert.IsNull(order.RelatedOrder);
        }

        #region PrivateHelper
        private OrderDTO GetDummyOrder(string partner)
        {
            return new OrderDTO
            {
                Partner = partner,
                ContactFirstName = "ContactFirstNameDummy",
                ContactLastName = "ContactLastNameDummy",
                ContactTitle = "ContactTitleDummy",
                ContactPhone = "ContactPhoneDummy",
                ContactMobile = "ContactMobileDummy",
                ContactEmail = "ContactEmailDummy",
                ExposureID = "ExposureIdDummy",
                UDAC = "UDACDummy",
                RelatedOrder = "RelatedOrderDummy",
            };
        }
        #endregion
    }
}
