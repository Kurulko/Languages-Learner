﻿using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Models.Database.Learner.ByLanguages;
using WebApi.Services.Learner.ByLanguage;
using WebApi.Services.UserServices;

namespace WebApi.Managers.Learner.ByLanguage;

public class WordByLanguageManager : ByLanguageManager<WordByLanguage>, IWordByLanguageService
{
    public WordByLanguageManager(LearnerContext db, IUserService userService)
        : base(db, userService) { }
}
