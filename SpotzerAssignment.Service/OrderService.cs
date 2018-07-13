using SpotzerAssignment.Data;
using SpotzerAssignment.Model;
using SpotzerAssignment.Model.DTO;
using SpotzerAssignment.Model.Exception;
using System;
using System.Collections.Generic;

namespace SpotzerAssignment.Service
{
    public class OrderService : IService<OrderDTO>
    {
        #region OrderService Data
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<PaidSearchProductLine> _paidSearchProductRepository;
        private readonly IRepository<WebSiteProductLine> _webSiteProductRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<PaidSearchProductLine> paidSearchProductRepository, IRepository<WebSiteProductLine> webSiteProductRepository)
        {
            this._orderRepository = orderRepository;
            this._paidSearchProductRepository = paidSearchProductRepository;
            this._webSiteProductRepository = webSiteProductRepository;
        } 
        #endregion

        public int Save(OrderDTO orderDTO)
        {
            var order = this.FromDto(orderDTO);
            var lines = this.GetLinesFromOrder(orderDTO);

            this._orderRepository.Save(order);

            foreach (var productLine in lines)
            {
                productLine.Order = order;

                if (productLine is WebSiteProductLine)
                {
                    this._webSiteProductRepository.Save(productLine as WebSiteProductLine);
                }

                if (productLine is PaidSearchProductLine)
                {
                    this._paidSearchProductRepository.Save(productLine as PaidSearchProductLine);
                }
            }

            return order.Id;
        }

        #region Private Helpers
        private Order FromDto(OrderDTO orderDTO)
        {
            var order = new Order();
            order.Partner = orderDTO.Partner;
            order.OrderId = orderDTO.OrderId;
            order.TypeOfOder = orderDTO.TypeOfOder;
            order.SubmittedBy = orderDTO.SubmittedBy;
            order.CompanyId = orderDTO.CompanyId;
            order.CompanyName = orderDTO.CompanyName;

            if (orderDTO.Partner == PartnerTypes.PartnerA)
            {
                order.ContactFirstName = orderDTO.ContactFirstName;
                order.ContactLastName = orderDTO.ContactLastName;
                order.ContactPhone = orderDTO.ContactPhone;
                order.ContactMobile = orderDTO.ContactMobile;
                order.ContactEmail = orderDTO.ContactEmail;
            }

            if (orderDTO.Partner == PartnerTypes.PartnerC)
            {
                order.ExposureID = orderDTO.ExposureID;
                order.UDAC = orderDTO.UDAC;
                order.RelatedOrder = orderDTO.RelatedOrder;
            }

            return order;
        }

        private IEnumerable<Line> GetLinesFromOrder(OrderDTO orderDTO)
        {
            var lines = new List<Line>();

            foreach (var lineDto in orderDTO.LineItems)
            {
                this.CheckProductWithPartner(orderDTO.Partner, lineDto);
                lines.Add(this.GetLineFromDTO(lineDto));
            }

            return lines;
        }

        private Line GetLineFromDTO(LineDTO lineDto)
        {
            Line line = null;

            if (lineDto.WebsiteDetails != null)
            {
                var websiteProduct = new WebSiteProductLine();
                websiteProduct.TemplateId = lineDto.WebsiteDetails.TemplateId;
                websiteProduct.WebsiteBusinessName = lineDto.WebsiteDetails.WebsiteBusinessName;
                websiteProduct.WebsiteAddressLine1 = lineDto.WebsiteDetails.WebsiteAddressLine1;
                websiteProduct.WebsiteAddressLine2 = lineDto.WebsiteDetails.WebsiteAddressLine2;
                websiteProduct.WebsiteCity = lineDto.WebsiteDetails.WebsiteCity;
                websiteProduct.WebsiteState = lineDto.WebsiteDetails.WebsiteState;
                websiteProduct.WebsitePostCode = lineDto.WebsiteDetails.WebsitePostCode;
                websiteProduct.WebsitePhone = lineDto.WebsiteDetails.WebsitePhone;
                websiteProduct.WebsiteEmail = lineDto.WebsiteDetails.WebsiteEmail;
                websiteProduct.WebsiteMobile = lineDto.WebsiteDetails.WebsiteMobile;
                line = websiteProduct;
            }
            if (lineDto.AdWordCampaign != null)
            {
                var paidSearchProduct = new PaidSearchProductLine();
                paidSearchProduct.CampaignName = lineDto.AdWordCampaign.CampaignName;
                paidSearchProduct.CampaignAddressLine1 = lineDto.AdWordCampaign.CampaignAddressLine1;
                paidSearchProduct.CampaignPostCode = lineDto.AdWordCampaign.CampaignPostCode;
                paidSearchProduct.CampaignRadius = lineDto.AdWordCampaign.CampaignRadius;
                paidSearchProduct.LeadPhoneNumber = lineDto.AdWordCampaign.LeadPhoneNumber;
                paidSearchProduct.UniqueSellingPoint1 = lineDto.AdWordCampaign.UniqueSellingPoint1;
                paidSearchProduct.UniqueSellingPoint2 = lineDto.AdWordCampaign.UniqueSellingPoint2;
                paidSearchProduct.UniqueSellingPoint3 = lineDto.AdWordCampaign.UniqueSellingPoint3;
                paidSearchProduct.Offer = lineDto.AdWordCampaign.Offer;
                paidSearchProduct.DestinationURL = lineDto.AdWordCampaign.DestinationURL;
                line = paidSearchProduct;
            }

            return line;
        }

        private void CheckProductWithPartner(string partner, LineDTO lineDto)
        {
            if (partner.Equals(PartnerTypes.PartnerA, StringComparison.InvariantCultureIgnoreCase)&& lineDto.AdWordCampaign != null)
                throw new ProductNotSupportedException("Partner A not support PaidSearch");

            if (partner.Equals(PartnerTypes.PartnerD, StringComparison.InvariantCultureIgnoreCase) && lineDto.WebsiteDetails != null)
                throw new ProductNotSupportedException("Partner D not support WebSite Product");
        } 
        #endregion
    }
}
