using WebApi.Models.Database.Learner;

namespace WebApi.Services.Learner;

public interface ILearnerDbModelService<T> : IDbModelService<T, long> where T : ILearnerModel
{
}
