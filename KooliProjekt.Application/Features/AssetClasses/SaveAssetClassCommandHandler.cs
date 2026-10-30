using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.AssetClasses
{
    public class SaveAssetClassCommandHandler
        : IRequestHandler<SaveAssetClassCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveAssetClassCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(
            SaveAssetClassCommand request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            AssetClass item;

            if (request.Id == 0)
            {
                item = new AssetClass();
                _dbContext.AssetClasses.Add(item);
            }
            else
            {
                item = await _dbContext.AssetClasses.FindAsync(
                    new object[] { request.Id },
                    cancellationToken);

                if (item == null)
                {
                    return result.AddPropertyError("Id", "Asset class was not found.");
                }
            }

            item.Name = request.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}