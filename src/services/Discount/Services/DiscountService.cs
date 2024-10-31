using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Discount.GRPC.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services;

class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(
        GetDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c =>
            c.ProductName == request.ProductName
        );

        if (coupon is null)
        {
            coupon = new Coupon
            {
                ProductName = "No Discount",
                Amount = 0,
                Description = "No Discount Coupon",
            };
            logger.LogInformation("Discount is not found. Returning a default coupon.");
        }

        logger.LogInformation(
            "Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}",
            coupon.ProductName,
            coupon.Amount
        );

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = dbContext.Coupons.FirstOrDefault(c => c.Id == request.Id);

        if (coupon is null)
        {
            logger.LogError("Discount is not found. Product ID is {ProductId}", request.Id);
            throw new RpcException(new Status(StatusCode.NotFound, "Discount is not found"));
        }

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully deleted. Product Id is {ProductId}",
            request.Id
        );

        return new DeleteDiscountResponse { Success = true };
    }

    public override async Task<CouponModel> UpdateDiscount(
        UpdateDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            logger.LogError(
                $"Invalid coupon data, ProductName: {request.Coupon.ProductName}, Amount: {request.Coupon.Amount}, Description: {request.Coupon.Description}"
            );
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount successfully updated. ProductName: {ProductName}",
            request.Coupon.ProductName
        );

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(
        CreateDiscountRequest request,
        ServerCallContext context
    )
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            logger.LogError(
                $"Invalid coupon data, ProductName: {request.Coupon.ProductName}, Amount: {request.Coupon.Amount}, Description: {request.Coupon.Description}"
            );
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }

        if (await dbContext.Coupons.AnyAsync(c => c.ProductName == request.Coupon.ProductName))
        {
            logger.LogError(
                "Discount is already exist. ProductName: {ProductName}",
                request.Coupon.ProductName
            );
            throw new RpcException(new Status(StatusCode.AlreadyExists, "Discount already exists"));
        }

        dbContext.Coupons.Add(request.Coupon.Adapt<Coupon>());
        await dbContext.SaveChangesAsync();

        logger.LogInformation(
            "Discount is successfully created. ProductName: {ProductName}",
            request.Coupon.ProductName
        );

        return request.Coupon;
    }
}
