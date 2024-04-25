using Coupons.Application.Data;
using Coupons.Application.Dto;
using Coupons.Application.Interface;
using Coupons.Core.Entities;
using AutoMapper;

namespace Coupons.Application
{
    public class CouponService : ICouponService
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public CouponService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Coupon CreateCoupon(CouponDto couponDto)
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Add(coupon);
            _db.SaveChanges();

            return coupon;
        }

        public Coupon DeleteCoupon(int couponId)
        {
            Coupon coupon = _db.Coupons.First(u => u.CouponId == couponId);
            _db.Coupons.Remove(coupon);
            _db.SaveChanges();

            return coupon;
        }

        public CouponDto GetCouponByCode(string couponCode)
        {
            Coupon coupon = _db.Coupons.First(u => u.CouponCode.ToLower() == couponCode.ToLower());

            return _mapper.Map<CouponDto>(coupon);
        }



        public CouponDto GetCouponById(int couponId)
        {
            Coupon coupon = _db.Coupons.FirstOrDefault(x => x.CouponId == couponId);

            return _mapper.Map<CouponDto>(coupon);
        }

        public IEnumerable<CouponDto> GetCoupons()
        {
            IEnumerable<Coupon> listCoupons = _db.Coupons.ToList();

            return _mapper.Map<IEnumerable<CouponDto>>(listCoupons);
            
        }

        public Coupon UpdateCoupon(CouponDto couponDto)
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _db.Coupons.Update(coupon);
            _db.SaveChanges();

            return coupon;
        }
    }
}
