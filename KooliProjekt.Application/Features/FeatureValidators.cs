using FluentValidation;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Assets;
using KooliProjekt.Application.Features.AssetClasses;
using KooliProjekt.Application.Features.MonthlyValues;
using KooliProjekt.Application.Features.Portfolios;
using System.Linq;

namespace KooliProjekt.Application.Features
{
    public class GetAssetValidator : AbstractValidator<GetAssetQuery> { public GetAssetValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found."); }
    public class DeleteAssetValidator : AbstractValidator<DeleteAssetCommand> { public DeleteAssetValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found."); }
    public class ListAssetsValidator : AbstractValidator<ListAssetsQuery> { public ListAssetsValidator() { RuleFor(x => x.Page).GreaterThan(0); RuleFor(x => x.PageSize).InclusiveBetween(1, 100); } }
    public class SaveAssetValidator : AbstractValidator<SaveAssetCommand>
    {
        public SaveAssetValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).Must(id => id == 0 || db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found.");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Ticker).NotEmpty().MaximumLength(20);
            RuleFor(x => x.PortfolioId).GreaterThan(0).Must(id => db.Portfolios.Any(p => p.Id == id)).WithMessage("Portfolio was not found.");
            RuleFor(x => x.AssetClassId).GreaterThan(0).Must(id => db.AssetClasses.Any(c => c.Id == id)).WithMessage("Asset class was not found.");
        }
    }

    public class GetAssetClassValidator : AbstractValidator<GetAssetClassQuery> { public GetAssetClassValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.AssetClasses.Any(a => a.Id == id)).WithMessage("Asset class was not found."); }
    public class DeleteAssetClassValidator : AbstractValidator<DeleteAssetClassCommand> { public DeleteAssetClassValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.AssetClasses.Any(a => a.Id == id)).WithMessage("Asset class was not found."); }
    public class ListAssetClassesValidator : AbstractValidator<ListAssetClassesQuery> { public ListAssetClassesValidator() { RuleFor(x => x.Page).GreaterThan(0); RuleFor(x => x.PageSize).InclusiveBetween(1, 100); } }
    public class SaveAssetClassValidator : AbstractValidator<SaveAssetClassCommand>
    {
        public SaveAssetClassValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).Must(id => id == 0 || db.AssetClasses.Any(a => a.Id == id)).WithMessage("Asset class was not found.");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public class GetPortfolioValidator : AbstractValidator<GetPortfolioQuery> { public GetPortfolioValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.Portfolios.Any(a => a.Id == id)).WithMessage("Portfolio was not found."); }
    public class DeletePortfolioValidator : AbstractValidator<DeletePortfolioCommand> { public DeletePortfolioValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.Portfolios.Any(a => a.Id == id)).WithMessage("Portfolio was not found."); }
    public class ListPortfoliosValidator : AbstractValidator<ListPortfoliosQuery> { public ListPortfoliosValidator() { RuleFor(x => x.Page).GreaterThan(0); RuleFor(x => x.PageSize).InclusiveBetween(1, 100); } }
    public class SavePortfolioValidator : AbstractValidator<SavePortfolioCommand>
    {
        public SavePortfolioValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).Must(id => id == 0 || db.Portfolios.Any(a => a.Id == id)).WithMessage("Portfolio was not found.");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }

    public class GetMonthlyValueValidator : AbstractValidator<GetMonthlyValueQuery> { public GetMonthlyValueValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.MonthlyValues.Any(a => a.Id == id)).WithMessage("Monthly value was not found."); }
    public class DeleteMonthlyValueValidator : AbstractValidator<DeleteMonthlyValueCommand> { public DeleteMonthlyValueValidator(ApplicationDbContext db) => RuleFor(x => x.Id).GreaterThan(0).Must(id => db.MonthlyValues.Any(a => a.Id == id)).WithMessage("Monthly value was not found."); }
    public class ListMonthlyValuesValidator : AbstractValidator<ListMonthlyValuesQuery> { public ListMonthlyValuesValidator() { RuleFor(x => x.Page).GreaterThan(0); RuleFor(x => x.PageSize).InclusiveBetween(1, 100); } }
    public class SaveMonthlyValueValidator : AbstractValidator<SaveMonthlyValueCommand>
    {
        public SaveMonthlyValueValidator(ApplicationDbContext db)
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Id).Must(id => id == 0 || db.MonthlyValues.Any(v => v.Id == id)).WithMessage("Monthly value was not found.");
            RuleFor(x => x.AssetId).GreaterThan(0).Must(id => db.Assets.Any(a => a.Id == id)).WithMessage("Asset was not found.");
            RuleFor(x => x.Year).InclusiveBetween(1900, 9999);
            RuleFor(x => x.Month).InclusiveBetween(1, 12);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Value).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Total).GreaterThanOrEqualTo(0);
        }
    }
}
