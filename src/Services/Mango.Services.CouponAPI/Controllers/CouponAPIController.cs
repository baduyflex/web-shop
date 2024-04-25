using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Coupons.Application.Interface;
using Coupons.Core.Entities;
using Coupons.Application.Dto;

namespace Coupons.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private ResponseDto _response;
        private IMapper _mapper;
        private ICouponService _couponService;

        public CouponAPIController(IMapper mapper, ICouponService couponService)
        {
            _response = new ResponseDto();
            _mapper = mapper;
            _couponService = couponService;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<CouponDto> objList = this._couponService.GetCoupons();
                _response.Result = objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;

        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                CouponDto obj = this._couponService.GetCouponById(id);
                _response.Result = obj;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                CouponDto obj = this._couponService.GetCouponByCode(code);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = this._couponService.CreateCoupon(couponDto);

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = this._couponService.CreateCoupon(couponDto);

                _response.Result = coupon;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = this._couponService.DeleteCoupon(id);

                _response.Result = obj;
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.Message = e.Message;
            }

            return _response;
        }
    }
}
