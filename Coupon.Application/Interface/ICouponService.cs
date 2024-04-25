using Coupons.Application.Dto;
using Coupons.Core.Entities;

namespace Coupons.Application.Interface
{
    public interface ICouponService
    {
        public IEnumerable<CouponDto> GetCoupons();
        public CouponDto GetCouponById(int couponId);
        public CouponDto GetCouponByCode(string couponCode);
        public Coupon CreateCoupon(CouponDto couponDto);
        public Coupon DeleteCoupon(int couponId);
        public Coupon UpdateCoupon(CouponDto couponDto);
    }
}
