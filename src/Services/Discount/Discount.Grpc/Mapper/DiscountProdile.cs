using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    public class DiscountProdile:Profile
    {
        public DiscountProdile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
