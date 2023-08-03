using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Database.Learner;
using WebApi.Services;
using WebApi.Services.Learner;
using WebApi.Services.Learner.ByLanguage;

namespace WebApi.Controllers.CRUD.Learner;

public abstract class LearnerController<T> : DbModelsController<T, long> where T : ILearnerModel
{
    public LearnerController(ILearnerDbModelService<T> service) : base(service) { }
}
